namespace ApiFiscal.Core.Domain.Afip.Entity
{
    public sealed class Opcional : BaseEntity
    {
        public Opcional(string id, string valor)
        {
            Id = id;
            Valor = valor;
            ValidateOnCreate();
        }
        protected override void ValidateOnCreate()
        {
            
        }

        public string Id { get; set; }
        public string Valor { get; set; }
    }
}