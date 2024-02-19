using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public interface ICreatureBehaviour
    {
        void PlanForTurn(CreatureModel creatureModel,
                         LevelModel levelModel);
    }
}