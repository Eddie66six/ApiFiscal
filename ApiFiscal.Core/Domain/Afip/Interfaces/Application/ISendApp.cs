using ApiFiscal.Core.Application.Afip.Model;

namespace ApiFiscal.Core.Domain.Afip.Interfaces.Application
{
    public interface ISendApp
    {
        dynamic Send(SendModel sendModel);

        dynamic ObterPontoDeVenda(string token, string sign, long cuit, string pathPfx, string password, string expirationTime);
    }
}
