using Microsoft.Extensions.Configuration;
using TeamAdmin.Lib.Util;

namespace TeamAdmin.Lib.Tests
{
    public class Config
    {
        public static void Init()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);

            Settings.Config = builder.Build();
        }
    }
}
