using System.Collections.Generic;
using System.Linq;

namespace AncientGlyph.GameScripts.Interactors.Extentions
{
    public static class EntityExtentions
    {
        public static bool IsCellOccupied(this IEnumerable<IEntityModel> entities)
        {
            return entities.Where(ent => ent.IsFullSize).Any();
        }
    }
}