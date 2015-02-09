using System.Collections.Generic;
using System.IO;

namespace FirmwarePacking.Repositories
{
    /// <summary>Статус релиза</summary>
    public enum ReleaseStatus
    {
        /// <summary>Статус неизвестен</summary>
        Unknown,

        /// <summary>Стабильная версия</summary>
        Stable,

        /// <summary>Тестовая версия</summary>
        Test,

        /// <summary>Архивная версия</summary>
        Archive
    }

    public interface IPackageProvider
    {
        /// <summary>Открывает поток для чтения пакета прошивки</summary>
        Stream GetPackageStream();
    }

    /// <summary>Элемент репозитория.</summary>
    /// <remarks>
    ///     Ставится в соответствие пакету прошивки в репозитории и позволяет получить информацию о нём, не загружая его
    ///     тело, либо частично загрузить один из компонентов пакета.
    /// </remarks>
    public interface IRepositoryElement : IPackageProvider
    {
        /// <summary>Статус релиза пакета</summary>
        ReleaseStatus Status { get; }

        /// <summary>Информация о пакете</summary>
        PackageInformation Information { get; }

        /// <summary>Список целей, для которых имеются компоненты в данном пакете</summary>
        ICollection<ComponentTarget> Targets { get; }

        /// <summary>Загружает всё тело пакета</summary>
        FirmwarePackage GetPackage();

        /// <summary>Загружает необходимый компонент из тела пакета</summary>
        /// <param name="Target">Цель, компонент для которой требуется</param>
        FirmwareComponent GetComponent(ComponentTarget Target);
    }
}
