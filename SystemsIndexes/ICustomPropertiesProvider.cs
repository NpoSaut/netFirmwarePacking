using FirmwarePacking.Annotations;

namespace FirmwarePacking.SystemsIndexes
{
    public interface ICustomPropertiesProvider
    {
        [NotNull]
        string this[string PropertyName] { get; }
    }
}
