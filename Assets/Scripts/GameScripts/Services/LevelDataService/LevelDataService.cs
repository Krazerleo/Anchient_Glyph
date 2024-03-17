using System.Collections.Generic;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.LevelDataService
{
    public class LevelDataService : ILevelDataService
    {
        public CellModel At(Vector3Int vec3Int)
        {
            throw new System.NotImplementedException();
        }

        public bool TryMoveEntity(IEntityModel entity, Vector3Int offset)
        {
            throw new System.NotImplementedException();
        }

        public bool TryMoveEntity(IEntityModel entity, int xOffset, int yOffset, int zOffset)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IEntityModel> GetAllCurrentEntities()
        {
            throw new System.NotImplementedException();
        }
    }
}