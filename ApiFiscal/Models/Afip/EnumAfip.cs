using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFiscal.Models.Afip
{
    public static class EnumAfipPost
    {
        public static string Moedas => "FEParamGetTiposMonedas";
        public static string Ivas => "FEParamGetTiposIva";
        public static string Cbte => "FEParamGetTiposCbte";
        public static string TiposDoc => "FEParamGetTiposDoc";
        public static string TiposTributos => "FEParamGetTiposTributos";
    }
}
