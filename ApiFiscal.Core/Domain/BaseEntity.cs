namespace ApiFiscal.Core.Domain
{
    public abstract class BaseEntity : ErrorEvents
    {
        protected bool IsValid { get; set; } = true;
    }
}
