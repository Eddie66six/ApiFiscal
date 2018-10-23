namespace ApiFiscal.Core.Entity.Afip
{
    public sealed class CbteAsoc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo">Código do tipo de voucher. Verifique o método FEParamGetTiposCbte</param>
        /// <param name="ptoVta">Ponto de venda</param>
        /// <param name="nro">Numero do comprovante</param>
        public static CbteAsoc Get(short tipo, int ptoVta, long nro)
        {
            return new CbteAsoc(tipo, ptoVta, nro);
        }

        private CbteAsoc(short tipo, int ptoVta, long nro)
        {
            Tipo = tipo;
            PtoVta = ptoVta;
            Nro = nro;
        }
        public short Tipo { get; set; }
        public int PtoVta { get; set; }
        public long Nro { get; set; }
    }
}