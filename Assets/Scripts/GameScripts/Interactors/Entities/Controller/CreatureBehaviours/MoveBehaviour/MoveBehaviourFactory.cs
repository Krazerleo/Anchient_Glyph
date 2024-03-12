using System;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour
{
    public static class MoveBehaviourFactory
    {
        public static IMoveBehaviour CreateCreatureBehaviour(MovementType type)
        {
            return type switch
            {
                MovementType.Grounded => new GroundedBehaviour(),
                MovementType.Flying => new FlyingBehaviour(),
                _ => throw new ArgumentException("Unexpected type of behaviour")
            };
        }
    }
}