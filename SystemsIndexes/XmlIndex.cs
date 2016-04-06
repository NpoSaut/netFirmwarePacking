using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public class XmlIndex : Index
    {
        private ReadOnlyCollection<BlockKind> _Blocks;
        public override ReadOnlyCollection<BlockKind> Blocks { get { return _Blocks; } }

        public XmlIndex(String Filename)
            : this(XDocument.Load(Filename).Root)
        { }
        public XmlIndex(XElement XRoot)
        {
            _Blocks =
                new ReadOnlyCollection<BlockKind>(
                XRoot.Elements("block").Select(XBlock =>
                            new BlockKind(
                                (int)XBlock.Attribute("id"),
                                (String)XBlock.Attribute("name"),
                                (int)XBlock.Attribute("channels"),
                                XBlock.Elements("module").Select(XModule =>
                                    new ModuleKind(
                                        (int)XModule.Attribute("id"),
                                        (String)XModule.Attribute("name"),
                                        (bool?)XModule.Attribute("obsolete") ?? false,
                                        new XmlPropertiesProvider(XModule))).ToList(),
                                XBlock.Elements("modification").Select(XModification =>
                                    new ModificationKind(
                                        (int)XModification.Attribute("id"),
                                        (String)XModification.Attribute("name"),
                                        (String)XModification.Attribute("device"),
                                        (bool?)XModification.Attribute("obsolete") ?? false,
                                        new XmlPropertiesProvider(XModification)))
                                    .ToList()))
                            .ToList());
        }
    }

    public class ResourceXmlIndex : XmlIndex
    {
        public ResourceXmlIndex() : base(GetInjectedIndex()) { }

        private static XElement GetInjectedIndex()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = assembly.GetManifestResourceStream("FirmwarePacking.BlockKinds.xml");
            var document = XDocument.Load(manifestResourceStream);
            return document.Root;
        }
    }
}
