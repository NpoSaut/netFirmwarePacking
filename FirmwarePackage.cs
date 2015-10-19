using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace FirmwarePacking
{
    public class FirmwarePackage
    {
        public const char PathSeparator = '/';

        /// <summary>Текущая версия формата упаковщика</summary>
        public const int Format_ActualVersion = 3;
        /// <summary>Самая старая версия упаковщика, совместимая с текущим форматом</summary>
        public const int Format_CompatibleVersion = 1;
        /// <summary>Текущая версия формата упаковки, совместимая с данной библиотекой</summary>
        public const int Format_LastSupportedVersion = 1;

        public const string FirmwarePackageExtension = "sfp";
        private const char ZipFileDirectorySeparatorChar = '/';

        public PackageInformation Information { get; set; }
        public List<FirmwareComponent> Components { get; set; }

        public static FirmwarePackage Open(String FileName) { return Open(new FileInfo(FileName)); }
        public static FirmwarePackage Open(FileInfo File) { return Open(File.OpenRead()); }
        public static FirmwarePackage Open(Stream PackageStream)
        {
            using (ZipStorer pack = ZipStorer.Open(PackageStream, FileAccess.Read))
            {
                pack.EncodeUTF8 = true;
                var files = pack.ReadCentralDir();
                var index = files.Single(f => f.FilenameInZip == "index.xml");
                var ms = new MemoryStream();
                pack.ExtractFile(index, ms);
                ms.Seek(0, SeekOrigin.Begin);

                XDocument doc = XDocument.Load(ms);

                // Проверка версии файла с прошивкой
                if (IsFormatVersionCompatible((int?)doc.Root.Attribute("FormatVersion") ?? 1, (int?)doc.Root.Attribute("FormatCompatibleVersion") ?? 1) == FormatVersionCompatiblity.NotCompatible)
                    throw new Exceptions.FirmwarePackageUncompatibleFormatException();

                return
                    new FirmwarePackage()
                    {
                        Information = new PackageInformation(doc.Root.Element("FirmwareInformation")),
                        Components = doc.Root.Elements("Component").Select(XComponent =>
                            new FirmwareComponent(
                                XComponent: XComponent,
                                Files: DecodeFilesInZip(pack, (string)XComponent.Attribute("Directory")))).ToList()
                    };
            }
        }

        public enum FormatVersionCompatiblity { Actual, Compatible, NotCompatible }
        public static FormatVersionCompatiblity IsFormatVersionCompatible(int Version, int CompatibleVersion)
        {
            // Если версии форматов полностью совпадают
            if (Version == Format_ActualVersion) return FormatVersionCompatiblity.Actual;
            // Если версия формата файла устарела, но всё ещё является поддерживаемой данной библиотекой
            else if (Version < Format_ActualVersion && Version >= Format_LastSupportedVersion) return FormatVersionCompatiblity.Compatible;
            // Если версия формата новее, чем версия формата данной библиотеки, но она является совместимой с устаревшей версией библиотеки
            else if (Version > Format_ActualVersion && CompatibleVersion <= Format_ActualVersion) return FormatVersionCompatiblity.Compatible;
            // Ну, и во всех остальных случаях считаем версию не совместимой
            else return FormatVersionCompatiblity.NotCompatible;
        }

        private XDocument GetIndex()
        {
            return new XDocument(
                new XElement("FirmwareInfo",
                    new XAttribute("FormatVersion", Format_ActualVersion),
                    new XAttribute("FormatCompatibleVersion", Format_CompatibleVersion),
                    Information.ToXElement(),
                    Components.Select((comp, i) =>
                    new XElement("Component",
                        new XAttribute("Directory", comp.Name),
                        comp.Targets.Select(t => t.ToXElement())
                        ))));
        }

        public void Save(string p)
        {
            using (ZipStorer zip = ZipStorer.Create(p, ""))
            {
                zip.EncodeUTF8 = true;
                ZipFile(zip, GetIndex(), "index.xml");
                foreach (var component in Components)
                {
                    foreach (var file in component.Files)
                    {
                        ZipFile(zip, file.Content, Path.Combine(component.Name, file.RelativePath));
                    }
                }
            }
        }

        private static void ZipFile(ZipStorer zip, XDocument doc, String ZipPath)
        {
            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            zip.AddStream(ZipStorer.Compression.Deflate, ZipPath, ms, DateTime.Now, "");
        }
        private static void ZipFile(ZipStorer zip, Byte[] buff, String ZipPath)
        {
            MemoryStream ms = new MemoryStream(buff);
            zip.AddStream(ZipStorer.Compression.Deflate, ZipPath, ms, DateTime.Now, "");
        }

        public FirmwarePackage()
        {
        }


        private static List<FirmwareFile> DecodeFilesInZip(ZipStorer zip, String componentRoot)
        {
            return zip.ReadCentralDir()
                .Where(f => GetFirstDirName(f.FilenameInZip) == componentRoot)
                .Where(f => !f.FilenameInZip.EndsWith(ZipFileDirectorySeparatorChar + ""))
                .Select(f => new FirmwareFile(f.FilenameInZip.Substring(componentRoot.Length + 1), GetFile(zip, f)))
                .ToList();
        }

        private static byte[] GetFile(ZipStorer zip, ZipStorer.ZipFileEntry f)
        {
            var ms = new MemoryStream((int)f.FileSize);
            zip.ExtractFile(f, ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.GetBuffer();
        }
        private static string GetFirstDirName(string p)
        {
            if (p.Contains(ZipFileDirectorySeparatorChar))
                return p.Substring(0, p.IndexOf(ZipFileDirectorySeparatorChar));
            else return p;
        }

        /// <summary>
        /// Находит компонент, подходящий для указанной цели
        /// </summary>
        /// <param name="Target">Цель для прошивки</param>
        /// <returns>Подходящий компонент</returns>
        public FirmwareComponent GetComponentFor(ComponentTarget Target)
        {
            return Components.First(c => c.Targets.Contains(Target));
        }
    }
}
