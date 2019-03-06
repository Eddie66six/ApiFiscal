using ApiFiscal.Core.Application.Afip.Model;
using ApiFiscal.Core.Domain.Afip.Entity;
using ApiFiscal.Core.Domain.Afip.Entity.Returns;
using ApiFiscal.Core.Domain.Afip.Enum;
using ApiFiscal.Core.Domain.Afip.Interfaces.Application;
using ApiFiscal.Core.Service.Afip;
using System.Linq;

namespace ApiFiscal.Core.Application.Afip
{
    public class SendApp : ErrorEvents, ISendApp
    {
        private LoginAfipReturn Login(Auth auth, AfipService afipService)
        {
            string strError = null;
            var login = afipService.LoginAsync(auth.PathPfx, auth.Password, ref strError);
            if (strError != null)
            {
                RaiseError(strError);
                return null;
            }

            return login;
        }
        //metodo interno separado para poder refazer o login caso o token esteja expirado
        private dynamic InternalSend(SendModel sendModel, ref bool reload)
        {
            if (sendModel == null)
            {
                RaiseError("Dados de login invalidos");
                return null;
            }
            //prepara o obj de login e faz validaçao basica
            var auth = new Auth(sendModel.Token, sendModel.Sign, sendModel.Cuit, "https://w12evostorage.blob.core.windows.net/evo/arquivosLatam/certificado.pfx", sendModel.Password, sendModel.ExpirationTime);
            if (!auth.IsValid) return null;

            var afipApi = new AfipService();
            if (!auth.IsLogged())
            {
                var login = Login(auth, afipApi);
                if (login == null) return null;
                auth.UpdateCredencial(login.Credentials.Token, login.Credentials.Sign, login.Header.ExpirationTime);
                if (!auth.IsValid)
                    return null;
            }

            string strError = null;
            //obtem o ultimo numero enviado
            var xmlUltimoNumero = afipApi.UltimoNumeroAutorizado(auth.GetXmlAuthLastAuthorizedNumber(sendModel.PtoVta, sendModel.CbteTipo), ref strError);

            //verifica erro no ultimo numero enviado
            if (xmlUltimoNumero?.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.Errors != null)
            {
                //login invalido -> retona para tentar relogar
                if (xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.Errors.Err.FirstOrDefault(p => p.Code == "600") != null)
                {
                    reload = true;
                    return null;
                }
                return new
                {
                    Credencial = new { auth.Token, auth.Sign, auth.ExpirationTime },
                    Response = (string)null,
                    Error = xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.Errors.Err.Select(p => new ErrorModel(p.Msg, p.Code))
                };
            }

            //verifica erro no requeste do ultimo numero enviado
            if (strError != null)
            {
                RaiseError(strError);
                return null;
            }

            //obterm o proximo numero
            var proximoNumero = xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.CbteNro + 1;

            //prepara objetos para nota
            var feCabReq = new FeCabReq(sendModel.CantReg, sendModel.PtoVta, sendModel.CbteTipo);
            var alicIva = new AlicIva(sendModel.IdIva, sendModel.Amount, sendModel.Iva);

            var fEcaeDetRequest = new FecaeDetRequest(sendModel.Concepto, (EnumDocTipo)sendModel.DocTipo, sendModel.DocNro, proximoNumero, proximoNumero, 2.0, 2.0, alicIva.Importe,
                "PES", 1.0, null, null, new System.Collections.Generic.List<AlicIva>() { alicIva }, null);
            //obtem o xml da nota
            var emitir = new EmitirNota(auth, feCabReq, fEcaeDetRequest);
            //envia nota caso nao tenha nenhum erro
            var xml = emitir.IsValid ? afipApi.EmitirNotaAsync(emitir.GetXmlString(), ref strError) : null;

            return new
            {
                Credencial = new { auth.Token, auth.Sign, auth.ExpirationTime },
                Response = xml?.Body.FECAESolicitarResponse.FECAESolicitarResult.FeDetResp?.FECAEDetResponse.Select(p => new { p.CAE, p.CAEFchVto, Fecha = xml.Body.FECAESolicitarResponse.FECAESolicitarResult.FeCabResp.FchProceso }),
                Error = xml == null ? new[] { new ErrorModel(strError, "0") } : xml.Body.FECAESolicitarResponse.FECAESolicitarResult.Errors?.Err.Select(p => new ErrorModel(p.Msg, p.Code))
            };
        }

        public dynamic Send(SendModel sendModel)
        {
            var reload = false;
            var result = InternalSend(sendModel, ref reload);
            if (reload)
            {
                sendModel.Token = null;
                sendModel.Sign = null;
                result = InternalSend(sendModel, ref reload);
            }

            return result;
        }
    }
}
