using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.Interactors.Extentions
{
    public static class EntityExtentions
    {
        public static bool IsCellOccupied(this ICollection<IEntityModel> entities)
        {
            return entities.Where(ent => ent.IsFullSize).Any();
        }
    }
}