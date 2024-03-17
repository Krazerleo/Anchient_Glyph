using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
{
    public interface IActionCondition
    {
        bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour, LevelModel levelModel);
        
        IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour, LevelModel levelModel);
    }
}