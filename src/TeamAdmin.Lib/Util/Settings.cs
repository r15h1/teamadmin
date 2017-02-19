using Microsoft.Extensions.Configuration;

namespace TeamAdmin.Lib.Util
{
    public class Settings
    {
        public static IConfigurationRoot Config { get; set; }

        public static string ImageDirectory
        {
            get
            {
                return Config["Images:Directory"];
            }
        }

        public static string ImageUrlRoot
        {
            get
            {
                return Config["Images:UrlRoot"];
            }
        }

        public static string DefaultConnectionString
        {
            get
            {
                return Config["ConnectionStrings:DefaultConnection"];
            }
        }

        public static string SiteUrl
        {
            get
            {
                return Config["Site:Url"];
            }
        }
    }
}
