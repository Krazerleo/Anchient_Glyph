using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public sealed class FlyingBehaviour : MoveBehaviour
    {
        public FlyingBehaviour(LevelModel levelModel) : base(levelModel) { }

        protected override int FreeAxis => 3;
    }
}