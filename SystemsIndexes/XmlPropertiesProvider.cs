﻿using System;
using System.Xml.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public class XmlPropertiesProvider : ICustomPropertiesProvider
    {
        private readonly XElement _element;
        public XmlPropertiesProvider(XElement Element) { _element = Element; }

        public string this[string PropertyName]
        {
            get { return _element.Attribute(PropertyName).Value; }
        }

        public TValue GetProperty<TValue>(string PropertyName) { return (TValue)Convert.ChangeType(_element.Attribute(PropertyName).Value, typeof (TValue)); }
    }
}
