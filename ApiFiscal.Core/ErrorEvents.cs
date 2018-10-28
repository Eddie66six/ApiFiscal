using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ApiFiscal.Core
{
    public class ErrorEvents : IErrorEvents, IDisposable
    {
        private static List<ErrorModel> _handlers;

        public void RaiseError(string message, [CallerMemberName] string callerName = "")
        {
            if (_handlers == null) _handlers = new List<ErrorModel>();
            _handlers.Add(new ErrorModel(message, callerName));
        }

        public bool IsMessage()
        {
            return _handlers?.Count > 0;
        }

        public List<ErrorModel> GetMessages()
        {
            return _handlers;
        }

        public void Dispose()
        {
            _handlers = null;
            GC.SuppressFinalize(this);
        }
    }

    public class ErrorModel
    {
        public ErrorModel(string message, string key)
        {
            Message = message;
            Key = key;
        }
        public string Message { get; set; }
        public string Key { get; set; }
    }
}
