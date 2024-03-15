using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions
{
    public class GoToAction : IAction
    {
        public readonly Vector3Int Offset;
        
        public GoToAction(Vector3Int offset)
        {
            Offset = offset;
        }
        
        public string Id => nameof(GoToAction);
        
        public int CalculatePower() => 1;

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour) => true;

        public void Execute(CreatureController creatureController, PlayerController playerController)
        {
            creatureController.ExecuteMove(this);
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour)
        {
            Debug.LogError("Trying to get feedback action from feedback action <<GoToAction>>");
            return new NullAction();
        }
    }
}