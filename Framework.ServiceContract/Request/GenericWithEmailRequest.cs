using System.Collections.Generic;

namespace Framework.ServiceContract.Request
{
    public class GenericWithEmailRequest<T> : GenericRequest<T>
    {
        public string EmailTemplateFilePath { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
