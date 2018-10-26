namespace ApiFiscal.Core.Domain
{
    public abstract class BaseEntity
    {
        public bool IsValid { get; set; } = true;
        protected virtual void Validate() { }
    }
}
