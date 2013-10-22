using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FirmwarePacking
{
    /// <summary>
    /// Информация о модуле назначения прошивки
    /// </summary>
    public class ComponentTarget
    {
        /// <summary>Идентификатор ячейки</summary>
        public int CellId { get; set; }
        /// <summary>Модификация ячейки</summary>
        public int CellModification { get; set; }
        /// <summary>Номер канала (полукомплекта)</summary>
        public int Channel { get; set; }
        /// <summary>Номер модуля</summary>
        public int Module { get; set; }

        public ComponentTarget() { }
        public ComponentTarget(int SystemId, int CellId, int CellModification, int Channel, int Module)
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

        public XElement ToXElement() { return ToXElement("TargetModule"); }
        public XElement ToXElement(String ElementName)
        {
            return new XElement(ElementName,
#warning Атрибут с типом системы добавлен только для совместимости с предидущими версиями
                new XAttribute("System", 1),
                new XAttribute("Cell", CellId),
                new XAttribute("Modification", CellModification),
                new XAttribute("Channel", Channel),
                new XAttribute("Module", Module)
                );
        }

        public static explicit operator XElement(ComponentTarget ti) { return ti.ToXElement(); }
        public static explicit operator ComponentTarget(XElement xti) { return new ComponentTarget(xti); }

        public override string ToString()
        {
            return string.Format("Cell={0}[{1}]/{2} Module={3}", CellId, CellModification, Channel, Module);
        }

        public static bool operator ==(ComponentTarget a, ComponentTarget b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            else if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)) return false;
            else return a.Equals(b);
        }
        public static bool operator !=(ComponentTarget a, ComponentTarget b) { return !(a == b); }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            else return Equals(obj as ComponentTarget);
        }
        public bool Equals(ComponentTarget t)
        {
            if (t == null) return false;
            else return
                t.CellId == this.CellId &&
                t.CellModification == this.CellModification &&
                t.Module == this.Module &&
                t.Channel == this.Channel;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
