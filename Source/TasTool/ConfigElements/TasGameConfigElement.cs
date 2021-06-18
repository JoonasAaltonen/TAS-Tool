using System.Configuration;

namespace TasTool.ConfigElements
{
    public class TasGameConfigElement : ConfigurationElement
    {
        private string name;
        private string lpClassName;
        private string windowCaption;

        public TasGameConfigElement()
        {
        }

        public TasGameConfigElement(string name, string lpClassName, string windowCaption)
        {
           
            this.name = name;
            this.lpClassName = lpClassName;
            this.windowCaption = windowCaption;

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