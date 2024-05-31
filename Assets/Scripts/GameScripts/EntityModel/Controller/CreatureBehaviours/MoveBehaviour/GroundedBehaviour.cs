using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public sealed class GroundedBehaviour : MoveBehaviour
    {
        public GroundedBehaviour(LevelModel levelModel) : base(levelModel) { }

        protected override int FreeAxis => 2;
    }
}