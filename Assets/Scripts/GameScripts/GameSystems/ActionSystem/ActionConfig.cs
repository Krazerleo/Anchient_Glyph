using System;
using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
{
    [CreateAssetMenu(menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Create Action", 
        fileName = "Action", order = 0)]
    public class ActionConfig : ScriptableObject
    {
        public string Name { get; private set; }
        
        [field: TextArea(4, 10)]
        public string Description { get; private set; }

        [SerializeReference, SubclassSelector]
        public IActionCondition[] ApplyConditions = Array.Empty<IActionCondition>();

        [SerializeReference, SubclassSelector]
        public IEffect[] TargetEffects = Array.Empty<IEffect>();

        [SerializeReference, SubclassSelector]
        public IEffect[] SelfEffects = Array.Empty<IEffect>();

        public int CalculatePower()
        {
            return TargetEffects.Sum(effect => effect.GetPower());
        }

        public ICollection<IFeedbackAction> CanBeApplied(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            var feedback = new List<IFeedbackAction>();

            foreach (var condition in ApplyConditions)
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