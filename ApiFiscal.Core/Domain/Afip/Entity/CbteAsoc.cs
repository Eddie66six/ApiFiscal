namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class CbteAsoc : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo">Código do tipo de voucher. Verifique o método FEParamGetTiposCbte</param>
        /// <param name="ptoVta">Ponto de venda</param>
        /// <param name="nro">Numero do comprovante</param>
        public CbteAsoc(short tipo, int ptoVta, long nro)
        {
            Tipo = tipo;
            PtoVta = ptoVta;
            Nro = nro;
            ValidateOnCreate();
        }
        protected override void ValidateOnCreate()
        {
            
        }

        public short Tipo { get; set; }
        public int PtoVta { get; set; }
        public long Nro { get; set; }
    }
}