using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZangAPI.Tests
{
    public class JsonRequest
    {
        public string Method { get; set; }

        public string Path { get; set; }

        public List<Parameter> BodyParams { get; set; }

        public List<Parameter> QueryParams { get; set; }

        public string Response { get; set; }

        public JsonRequest()
        {
            this.BodyParams = new List<Parameter>();
            this.QueryParams = new List<Parameter>();
        }
    }
}