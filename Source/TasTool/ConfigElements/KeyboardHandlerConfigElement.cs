using System;
using System.Configuration;

namespace TasTool.ConfigElements
{
    public class KeyboardHandlerConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        [ConfigurationProperty("enabled", IsRequired = true, IsKey = false)]
        public bool Enabled
        {
            get { return bool.Parse(this["enabled"].ToString()); }
        }
    }
}