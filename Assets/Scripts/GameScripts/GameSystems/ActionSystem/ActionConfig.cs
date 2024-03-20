using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
{
    [CreateAssetMenu(menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Create Action", 
        fileName = "Action", order = 0)]
    public class ActionConfig : SerializedScriptableObject
    {
        [field: OdinSerialize]
        public string Name { get; private set; }
        
        [field: OdinSerialize]
        [field: TextArea(4, 10)]
        public string Description { get; private set; }

        [field: OdinSerialize]
        private IEnumerable<IActionCondition> _applyConditions = new List<IActionCondition>();

        [field: OdinSerialize]
        public IEnumerable<IEffect> TargetEffects { get; } = new List<IEffect>();

        [field: OdinSerialize]
        public IEnumerable<IEffect> SelfEffects { get; }= new List<IEffect>();

        public int CalculatePower()
        {
            return TargetEffects.Sum(effect => effect.GetPower());
        }

        public ICollection<IFeedbackAction> CanBeApplied(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            var feedback = new List<IFeedbackAction>();

            foreach (var condition in _applyConditions)
            {
                if (condition.CanExecute(self, target, moveBehaviour, levelModel) == false)
                {
                    feedback.Add(condition.GetFeedback(self, target, moveBehaviour, levelModel));
                }
            }

            return feedback;
        }
    }
}