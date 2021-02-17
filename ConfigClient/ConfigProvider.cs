using System;

namespace ConfigClient
{
    public class ConfigProvider : IConfigProvider
    {
        public CurrentConfig GetCurrentConfig()
        {
            return new CurrentConfig();
        }

    }
}