using ApiFiscal.Core.Application.Afip.Model;

namespace ApiFiscal.Core.Domain.Afip.Interfaces.Application
{
    public interface ISendApp
    {
        dynamic Send(SendModel sendModel);
    }
}
