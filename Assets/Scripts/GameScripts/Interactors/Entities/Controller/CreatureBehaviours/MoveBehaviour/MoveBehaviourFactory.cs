using System;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour
{
    public static class MoveBehaviourFactory
    {
        public static IMoveBehaviour CreateCreatureBehaviour(MovementType type, LevelModel levelModel)
        {
            return type switch
            {
                MovementType.Grounded => new GroundedBehaviour(levelModel),
                MovementType.Flying => new FlyingBehaviour(levelModel),
                _ => throw new ArgumentException("Unexpected type of behaviour")
            };
        }
    }
}