﻿using System;
using System.Xml.Linq;

namespace FirmwarePacking
{
    /// <summary>Информация о пакете с ПО</summary>
    public class PackageInformation
    {
        private string _firmwareVersionLabel;

        public PackageInformation() { }

        /// <summary>Создаёт описание прошивки</summary>
        /// <param name="FirmwareVersion">Версия прошивки</param>
        /// <param name="FirmwareVersionLabel">Метка версии прошивки</param>
        /// <param name="ReleaseDate">Дата выпуска прошивки</param>
        public PackageInformation(Version FirmwareVersion, string FirmwareVersionLabel, DateTime ReleaseDate)
            : this()
        {
            this.FirmwareVersion = FirmwareVersion;
            _firmwareVersionLabel = FirmwareVersionLabel;
            this.ReleaseDate = ReleaseDate;
            Guid = Guid.NewGuid();
        }

        public PackageInformation(XElement XInformation)
            : this()
        {
            XAttribute guidAttribute = XInformation.Attribute("Guid");
            if (guidAttribute != null)
                Guid = (Guid)guidAttribute;

            XElement xVersionInfo = XInformation.Element("Version");
            FirmwareVersion = new Version((int)xVersionInfo.Attribute("Major"), (int?)xVersionInfo.Attribute("Minor") ?? 0);
            FirmwareVersionLabel = (String)xVersionInfo.Attribute("Label");
            ReleaseDate = (DateTime)xVersionInfo.Attribute("ReleaseDate");
        }

        /// <summary>GUID пакета</summary>
        public Guid Guid { get; set; }

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

        public XElement ToXElement(String ElementName = "FirmwareInformation")
        {
            return new XElement(ElementName,
                                new XAttribute("Guid", Guid),
                                new XElement("Version",
                                             new XAttribute("Major", FirmwareVersion.Major),
                                             new XAttribute("Minor", FirmwareVersion.Minor),
                                             FirmwareVersionLabel != null ? new XAttribute("Label", FirmwareVersionLabel) : null,
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
