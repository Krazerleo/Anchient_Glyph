using System.Collections.Generic;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors;

using UnityEngine;

namespace AncientGlyph.GameScripts.Services.LevelDataProviderService
{
    /// <summary>
    /// LevelModel wrapper considering diff
    /// from save data
    /// </summary>
    public interface ILevelDataProviderService
    {
        public CellModel At(Vector3Int vec3Int);

        public bool TryMoveEntity(IEntityModel entity, Vector3Int offset);

        public bool TryMoveEntity(IEntityModel entity, int xOffset, int yOffset, int zOffset);

        public IEnumerable<IEntityModel> GetAllCurrentEntities();
    }
}