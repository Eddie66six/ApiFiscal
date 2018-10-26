using ApiFiscal.Core.Application.Afip.ModelreceiveParameters;
using ApiFiscal.Core.Domain.Afip.Entity;
using ApiFiscal.Core.Domain.Afip.Enum;
using ApiFiscal.Core.Domain.Afip.Interfaces.Application;
using ApiFiscal.Core.Service.Afip;

namespace ApiFiscal.Core.Application.Afip
{
    public class SendApp : ISendApp
    {
        public string Send(SendModel sendModel)
        {
            var afipApi = new AfipService();
            Auth auth = null;
            if (sendModel == null || sendModel.Cuit.ToString().Length < 11)
                return null;
            if (string.IsNullOrEmpty(sendModel.Token) || string.IsNullOrEmpty(sendModel.Sign))
            {
                string strError = null;
                var login = afipApi.LoginAsync("..\\teste\\certificado.pfx", "w12", ref strError);
                if(strError != null)
                    return strError;
                auth = new Auth(login?.credentials.token, login?.credentials.sign, auth.Cuit);
                if (!auth.IsValid)
                    return null;
            }

            var feCabReq = FeCabReq.Get(sendModel.CantReg, sendModel.PtoVta, sendModel.CbteTipo);
            var alicIva = AlicIva.Get(sendModel.IdIva, sendModel.Amount, sendModel.Iva);

            var fECAEDetRequest = FecaeDetRequest.Get(sendModel.Concepto, sendModel.DocTipo, sendModel.DocNro, 2, 2, 2.0, 2.0, alicIva.Importe, "PES", 1.0,
                    null, null, new System.Collections.Generic.List<AlicIva>() { alicIva }, null);
            var emiti = EmitirNota.Get(auth, feCabReq, fECAEDetRequest);

            var xml = afipApi.EmitirNotaAsync(emiti.GetXmlString());
            return xml;
        }
    }
}
