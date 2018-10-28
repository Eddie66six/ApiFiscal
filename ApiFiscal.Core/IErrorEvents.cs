using System.Collections.Generic;

namespace ApiFiscal.Core
{
    public interface IErrorEvents
    {
        void RaiseError(string message, string callerName = "");
        bool IsMessage();
        List<ErrorModel> GetMessages();
    }
}
