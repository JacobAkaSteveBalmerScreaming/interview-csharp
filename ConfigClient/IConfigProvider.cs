using System;
using System.Collections.Generic;

namespace ConfigClient
{
    public interface IConfigProvider
    {
        CurrentConfig GetCurrentConfig();
    }
}