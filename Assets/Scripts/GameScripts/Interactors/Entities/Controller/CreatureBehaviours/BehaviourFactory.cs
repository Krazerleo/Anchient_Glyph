using System;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public static class BehaviourFactory
    {
        public static ICreatureBehaviour CreateCreatureBehaviour(BehaviourType type)
        {
            return type switch
            {
                BehaviourType.Grounded => new GroundedCreatureBehaviour(),
                BehaviourType.Flying => new FlyingCreatureBehaviour(),
                _ => throw new ArgumentException("Unexpected type of behaviour")
            };
        }
    }
}