using System.Xml.Linq;
using FirmwarePacking.SystemsIndexes.Exceptions;

namespace FirmwarePacking.SystemsIndexes
{
    public class XmlPropertiesProvider : ICustomPropertiesProvider
    {
        private readonly XElement _element;

        public XmlPropertiesProvider(XElement Element)
        {
            _element = Element;
        }

        public string this[string PropertyName]
        {
            get
            {
                var attribute = _element.Attribute(PropertyName);
                if (attribute == null)
                    throw new CustomPropertyIsNotSpecifiedIndexException(PropertyName);
                return (string)attribute;
            }
        }

        public bool HasProperty(string PropertyName)
        {
            return _element.Attribute(PropertyName) != null;
        }
    }
}
