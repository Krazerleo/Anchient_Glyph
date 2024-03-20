using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using AncientGlyph.GameScripts.EntityModel.Traits;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel
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
        
        public CreatureTraits Traits { get; private set; }
        
        [field: SerializeField]
        public Vector3Int Position { get; private set; }
        
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

        public bool TryMoveToNextCell(Direction moveDirection, LevelModel levelModel)
        {
            var offset = moveDirection.GetNormalizedOffsetFromDirection();

            return TryMoveToNextCell(offset, levelModel);
        }

        public bool TryMoveToNextCell(Vector3Int offset, LevelModel levelModel)
        {
            if (levelModel.TryMoveEntity(this, offset))
            {
                Position += offset;
                return true;
            }

            return false;
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

        public void PostInitialize(CreatureTraitsSource traitsSource)
        {
            Traits = traitsSource.CreatureTraits;
        }
    }
}