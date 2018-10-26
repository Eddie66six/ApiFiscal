using Newtonsoft.Json;
using System;

namespace ApiFiscal.Core.Domain.Afip.Entity.Returns
{
    public sealed class Header
    {
        [JsonConstructor]
        private Header()
        {

        }
        public string source { get; set; }
        public string destination { get; set; }
        public long uniqueId { get; set; }
        public DateTime generationTime { get; set; }
        public DateTime expirationTime { get; set; }
    }

    public sealed class Credentials
    {
        [JsonConstructor]
        public Credentials()
        {

        }
        public string token { get; set; }
        public string sign { get; set; }
    }

    public sealed class LoginAfipReturn
    {
        [JsonConstructor]
        public LoginAfipReturn()
        {

        }
        public Header header { get; set; }
        public Credentials credentials { get; set; }
        public string Version { get; set; }
    }
}
