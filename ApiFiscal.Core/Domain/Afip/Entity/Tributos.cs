namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class Tributo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Código de imposto de acordo com o método FEParamGetTiposTributos</param>
        /// <param name="baseImp">Base tributária para a determinação do imposto</param>
        /// <param name="alic">Alíquota</param>
        /// <param name="importe">Tributaçao de importações</param>
        /// <param name="desc">Descrição do tributo.</param>
        public static Tributo Get(short id, double baseImp, double alic, double importe, string desc = null)
        {
            return new Tributo(id, baseImp, alic, importe, desc);
        }

        private Tributo(short id, double baseImp, double alic, double importe, string desc = null)
        {
            Id = id;
            BaseImp = baseImp;
            Alic = alic;
            Importe = importe;
            Desc = desc;
        }
        public short Id { get; set; }
        public string Desc { get; set; }
        public double BaseImp { get; set; }
        public double Alic { get; set; }
        public double Importe { get; set; }
    }
}