using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Interactors.Interaction;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities
{
    public class PlayerModel : IEntityModel
    {
        public bool IsFullSize => true;
        public Vector3Int Position { get; }
        public string Identifier { get; }
        public string Name { get; }

        public PlayerModel() { }

        public void AcceptInteraction(HitInteraction hit)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptInteraction(FunctionalInteraction func)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptInteraction(ICollection<object> listItems)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptInteraction(ICollection<GameItem> listItems)
        {
            throw new System.NotImplementedException();
        }

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
    }
}