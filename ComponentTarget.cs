using System;
using System.Xml.Linq;

namespace FirmwarePacking
{
    /// <summary>Информация о модуле назначения прошивки</summary>
    public class ComponentTarget : IEquatable<ComponentTarget>
    {
        public ComponentTarget() { }

        public ComponentTarget(int CellId, int CellModification, int Channel, int Module)
            : this()
        {
            this.CellId = CellId;
            this.CellModification = CellModification;
            this.Channel = Channel;
            this.Module = Module;
        }

        public ComponentTarget(XElement XTarget)
            : this()
        {
            CellId = (int)XTarget.Attribute("Cell");
            CellModification = (int)XTarget.Attribute("Modification");
            Channel = (int)XTarget.Attribute("Channel");
            Module = (int)XTarget.Attribute("Module");
        }

        /// <summary>Идентификатор ячейки</summary>
        public int CellId { get; set; }

        /// <summary>Модификация ячейки</summary>
        public int CellModification { get; set; }

        /// <summary>Номер канала (полукомплекта)</summary>
        public int Channel { get; set; }

        /// <summary>Номер модуля</summary>
        public int Module { get; set; }

        public XElement ToXElement() { return ToXElement("TargetModule"); }

        public XElement ToXElement(String ElementName)
        {
            return new XElement(ElementName,
                                new XAttribute("System", 1),
                                new XAttribute("Cell", CellId),
                                new XAttribute("Modification", CellModification),
                                new XAttribute("Channel", Channel),
                                new XAttribute("Module", Module)
                );
        }

        public static explicit operator XElement(ComponentTarget ti) { return ti.ToXElement(); }
        public static explicit operator ComponentTarget(XElement xti) { return new ComponentTarget(xti); }

        public override string ToString() { return string.Format("Cell={0}[{1}]/{2} Module={3}", CellId, CellModification, Channel, Module); }

        #region Equality

        /// <summary>Указывает, равен ли текущий объект другому объекту того же типа.</summary>
        /// <returns>true, если текущий объект равен параметру <paramref name="other" />, в противном случае — false.</returns>
        /// <param name="other">Объект, который требуется сравнить с данным объектом.</param>
        public bool Equals(ComponentTarget other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CellId == other.CellId && CellModification == other.CellModification && Channel == other.Channel && Module == other.Module;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ComponentTarget)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = CellId;
                hashCode = (hashCode * 397) ^ CellModification;
                hashCode = (hashCode * 397) ^ Channel;
                hashCode = (hashCode * 397) ^ Module;
                return hashCode;
            }
        }

        public static bool operator ==(ComponentTarget left, ComponentTarget right) { return Equals(left, right); }
        public static bool operator !=(ComponentTarget left, ComponentTarget right) { return !Equals(left, right); }

        #endregion
    }
}
