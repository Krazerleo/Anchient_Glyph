using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours
{
    public class CreatureBehaviour : ICreatureBehaviour
    {
        private IPriorityQueue<string, ActionConfig> _actionPriority;
        private IPriorityQueue<string, IFeedbackAction> _feedbackPriority;
        private MoveBehaviour.MoveBehaviour _moveBehaviour;
        
        private CreatureBehaviour() { }
        
        public static ICreatureBehaviour CreateFromOptions(MovementType type, LevelModel levelModel)
        {
            var actionComparer = Comparer<ActionConfig>.Create((a, b) => b.CalculatePower().CompareTo(a.CalculatePower()));
            var feedbackComparer =
                Comparer<IFeedbackAction>.Create((a, b) => b.CalculatePriority().CompareTo(a.CalculatePriority()));
            
            var creatureBehaviour = new CreatureBehaviour
            {
                _moveBehaviour = MoveBehaviourFactory.CreateCreatureBehaviour(type, levelModel),
                _actionPriority = new BinaryHeap<string, ActionConfig>(actionComparer, action => action.Name, 0),
                _feedbackPriority =
                    new BinaryHeap<string, IFeedbackAction>(feedbackComparer, action => action.Identifier, 0)
            };
            
            return creatureBehaviour;
        }

        public (ApplyEffectsAction, IFeedbackAction) PlanForTurn(CreatureModel creatureModel, PlayerModel playerModel, LevelModel levelModel)
        {
            foreach (var action in creatureModel.Traits.Actions)
            {
                var feedback = action.CanBeApplied(creatureModel, playerModel, _moveBehaviour, levelModel);
                
                if (feedback.Any() == false)
                {
                    _actionPriority.Enqueue(action);
                }
                else
                {
                    _feedbackPriority.Enqueue(feedback);
                }
            }

            if (_actionPriority.Count != 0)
            {
                var bestAction = _actionPriority.Dequeue();
                _actionPriority.Clear();
                _feedbackPriority.Clear();
                return (new ApplyEffectsAction(bestAction.TargetEffects, bestAction.SelfEffects), null);
            }

            if (_feedbackPriority.Count != 0)
            {
                var bestFeedback = _feedbackPriority.Dequeue();
                _feedbackPriority.Clear();
                return (null, bestFeedback);
            }

            return (null, new DoNothingAction());
        }
    }
}