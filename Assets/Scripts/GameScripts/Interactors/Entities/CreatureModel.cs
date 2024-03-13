using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Interactors.Entities.Traits;
using AncientGlyph.GameScripts.Interactors.Interactions;
using AncientGlyph.GameScripts.Serialization.Extensions;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities
{
    [Serializable]
    public class CreatureModel : IEntityModel
    {
        /// <summary>
        /// In deserialization time ScriptableObject 
        /// CreatureTraits is not provided
        /// Configured later in creature factory
        /// Same as CreatureTraits.Name
        /// </summary>
        public string SerializationName;
        
        [field: SerializeField]
        public string Identifier { get; set; }
        
        public CreatureTraits Traits;
        public List<IAction> Actions = new();
        
        [field: SerializeField]
        public Vector3Int Position { get; set; }
        
        public CreatureModel() { }

        public CreatureModel(CreatureTraits traits, string serializationName, Vector3Int position)
        {
            Traits = traits;
            Position = position;
            Identifier = Guid.NewGuid().ToString();
            SerializationName = serializationName;
        }


        public bool IsFullSize => Traits.IsFullSize;

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
            Identifier = xmlReader.GetAttribute(0);

            xmlReader.ReadToDescendant(nameof(SerializationName));
            SerializationName = xmlReader.ReadElementContentAsString();

            xmlReader.ReadToNextSibling(nameof(Position));
            Position = SerializationExtensions
                .ParseVector3Int(xmlReader.ReadElementContentAsString());

            // VOODOO MAGIC
            // WITHOUT IT DESERIALIZING ARRAY
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

        public bool Equals(IEntityModel other)
        {
            return other?.Identifier == Identifier;
        }
    }
}