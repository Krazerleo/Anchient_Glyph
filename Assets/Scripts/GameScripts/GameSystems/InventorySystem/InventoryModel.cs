using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class InventoryModel : IXmlSerializable
    {
        public Storage MainPlayerBackpack { get; private set; }

        public Storage LeftPlayerPocket { get; private set; }
        public bool IsLeftPlayerPocketEnabled { get; private set; }

        public Storage RightPlayerPocket { get; private set; }
        public bool IsRightPlayerPocketEnabled { get; private set; }

        /// <summary>
        /// Only for serialization
        /// </summary>
        public InventoryModel() {}
        
        /// <summary>
        /// If left or right Storage passed as null values then
        /// considered these player storages is disabled.
        /// Main storage can never be null.
        /// </summary>
        public InventoryModel(Storage main, Storage left, Storage right)
        {
            MainPlayerBackpack = main;
            LeftPlayerPocket = left;
            RightPlayerPocket = right;
            
            if (left != null)
            {
                IsLeftPlayerPocketEnabled = true;
            }

            if (right != null)
            {
                IsRightPlayerPocketEnabled = true;
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer deserializer = new(typeof(Storage));
            reader.ReadToFollowing("PlayerInventory");
            
            reader.ReadToDescendant(nameof(MainPlayerBackpack));
            MainPlayerBackpack = deserializer.Deserialize(reader) as Storage;

            reader.ReadToNextSibling(nameof(LeftPlayerPocket));
            IsLeftPlayerPocketEnabled = bool.Parse(reader.GetAttribute("IsEnabled")!);

            if (IsRightPlayerPocketEnabled)
            {
                LeftPlayerPocket = deserializer.Deserialize(reader) as Storage;
            }
            
            reader.ReadToNextSibling(nameof(RightPlayerPocket));
            IsRightPlayerPocketEnabled = bool.Parse(reader.GetAttribute("IsEnabled")!);

            if (IsRightPlayerPocketEnabled)
            {
                LeftPlayerPocket = deserializer.Deserialize(reader) as Storage;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new(typeof(Storage));
            writer.WriteStartElement("PlayerInventory");
            
            writer.WriteStartElement(nameof(MainPlayerBackpack));
            writer.WriteAttributeString("IsEnabled", true.ToString());
            serializer.Serialize(writer, MainPlayerBackpack);
            writer.WriteEndElement();
            
            writer.WriteStartElement(nameof(LeftPlayerPocket));
            writer.WriteAttributeString("IsEnabled", IsLeftPlayerPocketEnabled.ToString());
            serializer.Serialize(writer, LeftPlayerPocket);
            writer.WriteEndElement();
            
            writer.WriteStartElement(nameof(RightPlayerPocket));
            writer.WriteAttributeString("IsEnabled", IsRightPlayerPocketEnabled.ToString());
            serializer.Serialize(writer, RightPlayerPocket);
            writer.WriteEndElement();
            
            writer.WriteEndElement();
        }
    }
}