using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FirmwarePacking
{
    /// <summary>
    /// Информация о пакете с ПО
    /// </summary>
    public class PackageInformation
    {
        private string _firmwareVersionLabel;

        /// <summary>Версия прошивки</summary>
        public Version FirmwareVersion { get; set; }

        /// <summary>Метка версии прошивки</summary>
        public String FirmwareVersionLabel
        {
            get { return _firmwareVersionLabel; }
            set
            {
                if (value != null && value.Length > 4) throw new ArgumentException("Длина метки версии не может превышать 4 символов");
                _firmwareVersionLabel = value;
            }
        }

        /// <summary>Дата релиза прошивки</summary>
        public DateTime ReleaseDate { get; set; }

        public PackageInformation() { }
        public PackageInformation(XElement XInformation)
            : this()
        {
            var xVersionInfo = XInformation.Element("Version");
            FirmwareVersion = new Version(
                major: (int)xVersionInfo.Attribute("Major"),
                minor: (int?)xVersionInfo.Attribute("Minor") ?? 0);
            FirmwareVersionLabel = (String)xVersionInfo.Attribute("Label");
            ReleaseDate = (DateTime)xVersionInfo.Attribute("ReleaseDate");
        }

        public XElement ToXElement(String ElementName = "FirmwareInformation")
        {
            return new XElement(ElementName,
                new XElement("Version",
                    new XAttribute("Major", FirmwareVersion.Major),
                    new XAttribute("Minor", FirmwareVersion.Minor),
                    new XAttribute("Label", FirmwareVersionLabel),
                    new XAttribute("ReleaseDate", ReleaseDate.ToString("u"))));
        }

        public static explicit operator XElement(PackageInformation pi) { return pi.ToXElement(); }
        public static explicit operator PackageInformation(XElement xpi) { return new PackageInformation(xpi); }

        public override string ToString()
        {
            return string.Format("Версия {0}{1}", FirmwareVersion,
                                 string.IsNullOrWhiteSpace(FirmwareVersionLabel) ? "" : (" " + FirmwareVersionLabel));
        }
    }
}
