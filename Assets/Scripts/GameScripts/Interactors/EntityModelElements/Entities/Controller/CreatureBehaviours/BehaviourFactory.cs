using System;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public class BehaviourFactory
    {
        public static ICreatureBehaviour CreateCreatureBehaviour(BehaviourType type)
        {
            switch (type)
            {
                case BehaviourType.Grounded:
                    return new GroundedCreatureBehaviour();
                case BehaviourType.Flying:
                    return new FlyingCreatureBehaviour();
                default:
                    throw new ArgumentException("Unexpected type of behaviour");
            }
        }
    }
}