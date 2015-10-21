namespace FirmwarePacking.SystemsIndexes
{
    public interface ICustomPropertiesProvider
    {
        string this[string PropertyName] { get; }
        TValue GetProperty<TValue>(string PropertyName);
    }
}
