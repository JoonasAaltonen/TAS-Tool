using System.Configuration;

namespace TasTool.ConfigElements
{
    public class TasGameConfigElement : ConfigurationElement
    {
        public TasGameConfigElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]       
        public string Name
        {
            get { return (string) this["name"]; }
        }

        [ConfigurationProperty("lpClassName", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string LpClassName
        {
            get { return (string)this["lpClassName"]; }
        }

        [ConfigurationProperty("windowCaption", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string WindowCaption
        {
            get { return (string)this["windowCaption"]; }
        }
    }
}