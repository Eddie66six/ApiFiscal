﻿using ApiFiscal.Core.Application.Afip.Model;
using ApiFiscal.Core.Domain.Afip.Entity;
using ApiFiscal.Core.Domain.Afip.Entity.Returns;
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
        public dynamic Send(SendModel sendModel)
        {
            if (sendModel == null)
            {
                RaiseError("Dados de login invalidos");
                return null;
            }
            //prepara o obj de login e faz validaçao basica
            var auth = Auth.Create(sendModel.Token, sendModel.Sign, sendModel.Cuit, sendModel.PathPfx, sendModel.Password);
            if (auth == null) return null;

            var afipApi = new AfipService();
            if (!auth.IsLogged())
            {
                var login = Login(auth, afipApi);
                if (login == null) return null;
                auth.UpdateCredencial(login.Credentials.Token, login.Credentials.Sign);
            }

            string strError = null;
            //obtem o ultimo numero enviado
            var xmlUltimoNumero = afipApi.UltimoNumeroAutorizado(auth.GetXmlAuthLastAuthorizedNumber(sendModel.PtoVta, sendModel.CbteTipo), ref strError);
            //verifica erro no requeste do ultimo numero enviado
            if (strError != null)
            {
                RaiseError(strError);
                return null;
            }
            //verifica erro no ultimo numero enviado
            if (xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.Errors != null)
            {
                return new
                {
                    Credencial = new { auth.Token, auth.Sign },
                    Response = (string)null,
                    Error = xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.Errors.Err.Select(p => new ErrorModel(p.Msg, p.Code))
                };
            }
            //obterm o proximo numero
            var proximoNumero = xmlUltimoNumero.Body.FECompUltimoAutorizadoResponse.FECompUltimoAutorizadoResult.CbteNro + 1;

            //prepara objetos para nota
            var feCabReq = FeCabReq.Get(sendModel.CantReg, sendModel.PtoVta, sendModel.CbteTipo);
            var alicIva = AlicIva.Get(sendModel.IdIva, sendModel.Amount, sendModel.Iva);

            var fEcaeDetRequest = FecaeDetRequest.Get(sendModel.Concepto, sendModel.DocTipo, sendModel.DocNro, proximoNumero, proximoNumero, 2.0, 2.0, alicIva.Importe,
                "PES", 1.0, null, null, new System.Collections.Generic.List<AlicIva>() { alicIva }, null);
            //obtem o xml da nota
            var emiti = EmitirNota.Get(auth, feCabReq, fEcaeDetRequest);
            //envia nota
            var xml = afipApi.EmitirNotaAsync(emiti.GetXmlString(),ref strError);
            return new
            {
                Credencial = new { auth.Token, auth.Sign },
                Response = xml?.Body.FECAESolicitarResponse.FECAESolicitarResult.FeDetResp?.FECAEDetResponse.Select(p=> new { p.CAE, p.CAEFchVto, Fecha = xml.Body.FECAESolicitarResponse.FECAESolicitarResult.FeCabResp.FchProceso }),
                Error = xml == null ? new[] { new ErrorModel(strError, "0") } : xml.Body.FECAESolicitarResponse.FECAESolicitarResult.Errors?.Err.Select(p=> new ErrorModel(p.Msg, p.Code) )
            };
        }
    }
}
