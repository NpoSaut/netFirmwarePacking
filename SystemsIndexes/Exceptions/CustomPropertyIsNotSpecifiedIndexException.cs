using System;

namespace FirmwarePacking.SystemsIndexes.Exceptions
{
    /// <Summary>Дополнительное свойство не указано</Summary>
    [Serializable]
    public class CustomPropertyIsNotSpecifiedIndexException : IndexException
    {
        public CustomPropertyIsNotSpecifiedIndexException(string PropertyName) : base($"Дополнительное свойство ({PropertyName}) не указано")
        {
            this.PropertyName = PropertyName;
        }

        public string PropertyName { get; }
    }
}
