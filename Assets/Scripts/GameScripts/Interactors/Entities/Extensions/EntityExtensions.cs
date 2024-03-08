using System.Collections.Generic;
using System.Linq;

namespace AncientGlyph.GameScripts.Interactors.Entities.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsCellOccupied(this IEnumerable<IEntityModel> entities)
        {
            return entities.Any(ent => ent.IsFullSize);
        }
    }
}