using System;
using System.Xml.Linq;

namespace FirmwarePacking
{
    public class ComponentCustomProperty
    {
        public ComponentCustomProperty(int Index, int Value)
        {
            if (Index < 107 || Index > 127)
                throw new ArgumentOutOfRangeException(nameof(Index));

            this.Index = Index;
            this.Value = Value;
        }

        internal ComponentCustomProperty(XElement XProperty)
            : this((int)XProperty.Attribute("Index"),
                   (int)XProperty.Attribute("Value"))
        {}

        public int Index { get; }
        public int Value { get; }

        public XElement ToXElement() => ToXElement("Property");

        public XElement ToXElement(string ElementName)
        {
            return new XElement(ElementName,
                new XAttribute("Index", Index),
                new XAttribute("Value", Value));
        }
        
        public static explicit operator XElement(ComponentCustomProperty ti)  { return ti.ToXElement(); }
        public static explicit operator ComponentCustomProperty(XElement xti) { return new ComponentCustomProperty(xti); }

        public override string ToString()
        {
            return $"[{Index}] = {Value}";
        }
    }
}