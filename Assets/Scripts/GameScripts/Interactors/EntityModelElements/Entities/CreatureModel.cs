using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Interactors.Entities.Traits;
using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.Serialization.Extensions;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities
{
    public class CreatureModel : IEntityModel
    {
        /// <summary>
        /// In deserialization time ScriptableObject 
        /// CreatureTraits is not provided
        /// Configured later in creature factory
        /// Same as CreatureTraits.Name
        /// </summary>
        public string SerializationName;
        public CreatureTraits Traits;

        private Vector3Int _position;
        private Guid _identifier;

        public CreatureModel() { }

        public CreatureModel(CreatureTraits traits, Vector3Int position, string entityId, string serializationName)
        {
            Traits = traits;
            _position = position;
            _identifier = new Guid(entityId);
            SerializationName = serializationName;
        }

        public Vector3Int Position => _position;

        public bool IsFullSize => Traits.IsFullSize;

        public string Identifier => _identifier.ToString();

        public string Name => Traits.Name;

        public void InteractWith(IEntityModel targetEntity)
        {
            //TODO
        }

        public void AcceptInteraction(HitInteraction hit)
        {
            //TODO
        }

        public void AcceptInteraction(FunctionalInteraction func)
        {
            //TODO
        }

        public void AcceptInteraction(ICollection<GameItem> listItems)
        {
            //TODO
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader xmlReader)
        {
            _identifier = new Guid(xmlReader.GetAttribute(0));

            xmlReader.ReadToDescendant(nameof(SerializationName));
            SerializationName = xmlReader.ReadElementContentAsString();

            xmlReader.ReadToNextSibling(nameof(Position));
            _position = SerializationExtensions
                .ParseVector3Int(xmlReader.ReadElementContentAsString());

            // VOODOO MAGIC
            // WITHOUT IT DESERIALIZNG ARRAY
            // STOPS ON FIRST ELEMENT ???
            xmlReader.Read();
            xmlReader.Read();
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteAttributeString(nameof(Identifier), Identifier);
            xmlWriter.WriteElementString(nameof(SerializationName),
                                         SerializationName);
            xmlWriter.WriteElementString(nameof(Position), Position.ToString());
        }
    }
}