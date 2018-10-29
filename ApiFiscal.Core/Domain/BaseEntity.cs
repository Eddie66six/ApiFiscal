namespace ApiFiscal.Core.Domain
{
    public abstract class BaseEntity : ErrorEvents
    {
        public bool IsValid { get; protected set; } = true;

        protected abstract void ValidateOnCreate();
    }
}