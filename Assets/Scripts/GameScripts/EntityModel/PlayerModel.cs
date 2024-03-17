using System.Xml;
using System.Xml.Schema;
using AncientGlyph.GameScripts.Constants;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel
{
    public class PlayerModel : IEntityModel
    {
        public bool IsFullSize => true;
        public Vector3Int Position { get; set; }
        public string Identifier => GameConstants.PlayerId;
        public string Name => GameConstants.PlayerName; 

        public PlayerModel() { }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            // TODO
        }

        public void WriteXml(XmlWriter writer)
        {
            // TODO
        }

        public bool Equals(IEntityModel other)
        {
            return other != null && other.Identifier == Identifier;
        }
    }
}