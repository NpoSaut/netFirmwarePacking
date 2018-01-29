using FirmwarePacking.Annotations;
using FirmwarePacking.SystemsIndexes.Exceptions;

namespace FirmwarePacking.SystemsIndexes
{
    public interface ICustomPropertiesProvider
    {
        /// <summary>Возвращает значение пользовательского свойства или выбрасывает исключение, если свойство не найдено</summary>
        /// <param name="PropertyName">Имя свойства</param>
        /// <exception cref="CustomPropertyIsNotSpecifiedIndexException">Свойство с таким именем не указано для данной ячейки</exception>
        [NotNull]
        string this[string PropertyName] { get; }

        bool HasProperty(string PropertyName);
    }
}
