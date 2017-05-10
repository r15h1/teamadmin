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
        public IList<string> InitialPreview { get; set; } = new List<string>();

        [JsonProperty("initialPreviewConfig")]
        public IList<InitialPreviewConfig> InitialPreviewConfig { get; set; } = new List<InitialPreviewConfig>();
    }

    public class InitialPreviewConfig
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public dynamic Extra { get; set; }
    }
}
