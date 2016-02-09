using System.Collections.Generic;

namespace FirmwarePacking.SystemsIndexes
{
    public class DictionaryCustomPropertiesProvider : ICustomPropertiesProvider
    {
        private readonly IDictionary<string, string> _dictionary;
        public DictionaryCustomPropertiesProvider(IDictionary<string, string> Dictionary) { _dictionary = Dictionary; }

        public string this[string PropertyName]
        {
            get { return _dictionary[PropertyName]; }
        }
    }
}
