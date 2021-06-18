using System.Configuration;

namespace TasTool.ConfigElements
{
    public class TasGameCollection : ConfigurationElementCollection
    {
        public new TasGameConfigElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (TasGameConfigElement)BaseGet(name);
            }
        }

        public TasGameConfigElement this[int index]
        {
            get { return (TasGameConfigElement)BaseGet(index); }
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
            return new TasGameConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TasGameConfigElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "TasGame"; }
        }
    }
}
