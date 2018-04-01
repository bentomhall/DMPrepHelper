using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.Core
{
    public class ConfigException : Exception
    {
        public ConfigException(string message, Exception e) : base(message, e) { }

    }
}
