using System.Collections.Generic;
using System.IO;

namespace FirmwarePacking.Repositories
{
    ///// <summary>Элемент репозитория, ссылающийся на поток</summary>
    ///// <remarks>При запросе загружает пакет прошивки из указанного потока</remarks>
    //public class StreamLinkRepositoryElement : LinkRepositoryElement
    //{
    //    private readonly Stream _packageStream;

    //    public StreamLinkRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, Stream PackageStream)
    //        : base(Information, Targets) { _packageStream = PackageStream; }

    //    /// <summary>Производит загрузку пакета прошивки по ссылке</summary>
    //    protected override FirmwarePackage LoadPackage() { return FirmwarePackage.Open(_packageStream); }
    //}
}
