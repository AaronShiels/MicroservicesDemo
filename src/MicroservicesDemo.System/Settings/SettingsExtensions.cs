using System.Configuration;
using Autofac;

namespace MicroservicesDemo.System.Settings
{
    public static class SettingsExtensions
    {
        public static void RegisterSettings(this ContainerBuilder builder)
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                builder.RegisterInstance(ConfigurationManager.AppSettings[key])
                       .Keyed<string>(key);
        }
    }
}