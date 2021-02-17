using System;
using System.Collections.Generic;

namespace ConfigClient
{
    public class CurrentConfig
    {
        public IDictionary<string, ConfigKey> Values { get; set; }
        public IDictionary<string, string> FeatureFlags { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Source { get; set; }
    }
}
