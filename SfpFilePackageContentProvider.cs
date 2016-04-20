using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FirmwarePacking
{
    internal class SfpFilePackageContentProvider : IPackageContentProvider
    {
        private readonly string _componentDirectoryName;
        private readonly string _fileName;

        public SfpFilePackageContentProvider(string FileName, string ComponentDirectoryName)
        {
            _fileName = FileName;
            _componentDirectoryName = ComponentDirectoryName;
        }

        public IList<FirmwareFile> LoadFirmwareFiles()
        {
            using (ZipStorer zip = ZipStorer.Open(_fileName, FileAccess.Read))
            {
                return zip.ReadCentralDir()
                          .Where(f => GetFirstDirName(f.FilenameInZip) == _componentDirectoryName)
                          .Where(f => !f.FilenameInZip.EndsWith(FirmwarePackage.ZipFileDirectorySeparatorChar + ""))
                          .Select(f => new FirmwareFile(f.FilenameInZip.Substring(_componentDirectoryName.Length + 1),
                                                        GetFile(zip, f)))
                          .ToList();
            }
        }

        private static string GetFirstDirName(string p)
        {
            if (p.Contains(FirmwarePackage.ZipFileDirectorySeparatorChar))
                return p.Substring(0, p.IndexOf(FirmwarePackage.ZipFileDirectorySeparatorChar));
            return p;
        }

        private static byte[] GetFile(ZipStorer zip, ZipStorer.ZipFileEntry f)
        {
            var ms = new MemoryStream((int)f.FileSize);
            zip.ExtractFile(f, ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.GetBuffer();
        }
    }
}
