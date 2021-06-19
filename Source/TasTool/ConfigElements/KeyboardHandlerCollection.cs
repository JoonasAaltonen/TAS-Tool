using System.Configuration;

namespace TasTool.ConfigElements
{
    public class KeyboardHandlerCollection : ConfigurationElementCollection
    {
        public new KeyboardHandlerConfigElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (KeyboardHandlerConfigElement)BaseGet(name);
            }
        }

        public KeyboardHandlerConfigElement this[int index]
        {
            get { return (KeyboardHandlerConfigElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new KeyboardHandlerConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyboardHandlerConfigElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "KeyboardHandlerConfigElement"; }
        }
    }
}