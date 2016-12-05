using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.ApiViewModels
{
    public class UploadedData
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("errorkeys")]
        public int[] ErrorKeys { get; set; }

        [JsonProperty("initialPreview")]
        public IEnumerable<string> InitialPreview { get; set; }
    }
}
