namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class Tributo : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Código de imposto de acordo com o método FEParamGetTiposTributos</param>
        /// <param name="baseImp">Base tributária para a determinação do imposto</param>
        /// <param name="alic">Alíquota</param>
        /// <param name="importe">Tributaçao de importações</param>
        /// <param name="desc">Descrição do tributo.</param>
        private Tributo(short id, double baseImp, double alic, double importe, string desc = null)
        {
            Id = id;
            BaseImp = baseImp;
            Alic = alic;
            Importe = importe;
            Desc = desc;
            ValidateOnCreate();
        }

        protected override void ValidateOnCreate()
        {
            
        }

        public short Id { get; set; }
        public string Desc { get; set; }
        public double BaseImp { get; set; }
        public double Alic { get; set; }
        public double Importe { get; set; }
    }
}