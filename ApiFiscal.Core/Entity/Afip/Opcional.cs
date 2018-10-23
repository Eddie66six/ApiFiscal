namespace ApiFiscal.Core.Entity.Afip
{
    public sealed class Opcional
    {
        public static Opcional Get(string id = null, string valor = null)
        {
            return new Opcional(id, valor);
        }
        private Opcional(string id, string valor)
        {
            Id = id;
            Valor = valor;
        }
        public string Id { get; set; }
        public string Valor { get; set; }
    }
}