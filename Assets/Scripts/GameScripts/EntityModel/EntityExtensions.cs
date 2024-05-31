using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel
{
    public static class EntityExtensions
    {
        public static bool IsCellOccupied(this IEnumerable<IEntityModel> entities)
        {
            return entities.Any(ent => ent.IsFullSize);
        }

        public static CellModel GetEntityCell(this IEntityModel entity, LevelModel levelModel)
        {
            return levelModel[entity.Position];
        }
    }
}