using System.Xml;
using System.Xml.Schema;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel
{
    public class PlayerModel : IEntityModel
    {
        public bool IsFullSize => true;
        public Vector3Int Position { get; private set; }
        public string Identifier => GameConstants.PlayerId;
        public string Name => GameConstants.PlayerName;
        
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

        public PlayerModel()
        {
        }

        // TODO : Remove after implementing Xml Serialization
        public PlayerModel(Vector3Int startPosition)
        {
            Position = startPosition;
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

        public bool Equals(IEntityModel other)
        {
            return other != null && other.Identifier == Identifier;
        }
    }
}