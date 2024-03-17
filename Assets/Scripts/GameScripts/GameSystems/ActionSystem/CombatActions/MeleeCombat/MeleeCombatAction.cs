using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.CombatActions.MeleeCombat
{
    public class MeleeCombatAction : IAction
    {
        private readonly MeleeCombatActionTraits _traits;
        private readonly LevelModel _levelModel;

        public MeleeCombatAction(MeleeCombatActionTraits traits, LevelModel levelModel)
        {
            _traits = traits;
            _levelModel = levelModel;
        }

        public string Id => _traits.ActionName;
        
        public int CalculatePower()
        {
            // TODO : Implement more complex and feature based power calculation.
            return _traits.DamageHitValue;
        }

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour)
        {
            return _levelModel.CheckIsReachable(creatureModel.Position, playerModel.Position);
        }

        public void Execute(CreatureController creatureController, PlayerController playerController)
        {
            creatureController.ExecuteMeleeCombatAction(this, playerController);
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour)
        {
            var offset = moveBehaviour.CalculateNextStep(creatureModel.Position, playerModel.Position);

            if (offset == null)
            {
                return new NullAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}