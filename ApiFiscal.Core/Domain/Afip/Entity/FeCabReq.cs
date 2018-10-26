namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class FeCabReq
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cantReg">Quantidade de registros dos detalhes do voucher ou lote de vouchers</param>
        /// <param name="ptoVta">Ponto de Venda do voucher que está sendo reportado. Se mais de um recibo for informado, todos devem corresponder ao mesmo ponto de venda. Obs: è oq esta na tabela PONTOS_DE_VENDA</param>
        /// <param name="cbteTipo">Tipo de voucher que está sendo relatado. Se mais de um recibo for informado, todos devem ser do mesmo tipo. OBS: Com Factura A só pode ser usado o documento CUIT(CNPJ), campo RG no EVO. Com factura B somente DNI(CPF)</param>
        public static FeCabReq Get(int cantReg, int ptoVta, int cbteTipo)
        {
            return new FeCabReq(cantReg, ptoVta, cbteTipo);
        }

        private FeCabReq(int cantReg, int ptoVta, int cbteTipo)
        {
            CantReg = cantReg;
            PtoVta = ptoVta;
            CbteTipo = cbteTipo;
        }
        public int CantReg { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
    }
}