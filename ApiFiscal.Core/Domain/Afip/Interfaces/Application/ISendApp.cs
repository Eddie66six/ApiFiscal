using ApiFiscal.Core.Application.Afip.ModelreceiveParameters;

namespace ApiFiscal.Core.Domain.Afip.Interfaces.Application
{
    public interface ISendApp
    {
        string Send(SendModel sendModel);
    }
}
