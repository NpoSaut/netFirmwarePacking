using FirmwarePacking.Annotations;

namespace FirmwarePacking.SystemsIndexes
{
    public interface ICustomPropertiesProvider
    {
        [CanBeNull]
        string this[string PropertyName] { get; }
    }
}
