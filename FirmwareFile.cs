using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking
{
    public class FirmwareFile
    {
        public String RelativePath { get; set; }
        public Byte[] Content { get; set; }

        public FirmwareFile(String RelativePath, Byte[] Content)
        {
            this.RelativePath = RelativePath;
            this.Content = Content;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", RelativePath, GetLetteredFileSize(Content.Length));
        }


        public static string GetLetteredFileSize(double Size)
        {
            var letter =
                (new String[] { "", "К", "М", "Г", "Т" })
                .Select(l => l + "Б")
                .Select((l, i) => new { m = Math.Pow(1024, i), l = l })
                .First(l => Size < 4000 * l.m);
            return (Math.Round(Size * 10 / letter.m) / 10).ToString() + " " + letter.l;
        }
    }
}
