using System.Collections.Generic;
using FirmwarePacking.SystemsIndexes.Exceptions;

namespace FirmwarePacking.SystemsIndexes
{
    public class DictionaryCustomPropertiesProvider : ICustomPropertiesProvider
    {
        private readonly IDictionary<string, string> _dictionary;
        public DictionaryCustomPropertiesProvider(IDictionary<string, string> Dictionary) { _dictionary = Dictionary; }

        public string this[string PropertyName]
        {
            get
            {
                if (!_dictionary.TryGetValue(PropertyName, out var res))
                    throw new CustomPropertyIsNotSpecifiedIndexException(PropertyName);
                return res;
            }
        }
    }
}
