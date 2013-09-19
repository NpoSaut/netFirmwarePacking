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
        public const string FirmwarePackageExtension = "sfp";
        private const char ZipFileDirectorySeparatorChar = '/';

        public PackageInformation Information { get; set; }
        public List<FirmwareComponent> Components { get; set; }

        public static FirmwarePackage Open(String FileName) { return Open(new FileInfo(FileName)); }
        public static FirmwarePackage Open(FileInfo File)
        {
            using (ZipStorer pack = ZipStorer.Open(File.OpenRead(), FileAccess.Read))
            {
                var files = pack.ReadCentralDir();
                var index = files.Single(f => f.FilenameInZip == "index.xml");
                var ms = new MemoryStream();
                pack.ExtractFile(index, ms);
                ms.Seek(0, SeekOrigin.Begin);

                XDocument doc = XDocument.Load(ms);

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

        private XDocument GetIndex()
        {
            return new XDocument(
                new XElement("FirmwareInfo",
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
                .Select(f => new FirmwareFile(GetRelativePath(f.FilenameInZip), GetFile(zip, f)))
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
        private static string GetRelativePath(string p)
        {
            return Path.Combine(p.Split(new char[] { ZipFileDirectorySeparatorChar }).Skip(1).ToArray());
        }

    }
}
