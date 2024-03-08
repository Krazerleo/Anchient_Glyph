using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public interface ICreatureBehaviour
    {
        void PlanForTurn(CreatureModel creatureModel,
                         LevelModel levelModel);
    }
}