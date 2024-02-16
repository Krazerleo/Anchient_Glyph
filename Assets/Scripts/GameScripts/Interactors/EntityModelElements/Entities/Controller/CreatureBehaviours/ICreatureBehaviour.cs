using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public interface ICreatureBehaviour
    {
        void PlanForTurn(CreatureModel creatureModel,
                         LevelModel levelModel);
    }
}