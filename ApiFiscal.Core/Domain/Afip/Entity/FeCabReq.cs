namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class FeCabReq : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cantReg">Quantidade de registros dos detalhes do voucher ou lote de vouchers</param>
        /// <param name="ptoVta">Ponto de Venda do voucher que está sendo reportado. Se mais de um recibo for informado, todos devem corresponder ao mesmo ponto de venda. Obs: è oq esta na tabela PONTOS_DE_VENDA</param>
        /// <param name="cbteTipo">Tipo de voucher que está sendo relatado. Se mais de um recibo for informado, todos devem ser do mesmo tipo. OBS: Com Factura A só pode ser usado o documento CUIT(CNPJ), campo RG no EVO. Com factura B somente DNI(CPF)</param>
        public FeCabReq(int cantReg, int ptoVta, int cbteTipo)
        {
            CantReg = cantReg;
            PtoVta = ptoVta;
            CbteTipo = cbteTipo;
            ValidateOnCreate();
        }

        protected override void ValidateOnCreate()
        {
            if (CantReg <= 0)
            {
                RaiseError("CantReg deve ser maior que 0");
                IsValid = false;
            }
        }
        public int CantReg { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
    }
}