using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.EntityModel.Controller
{
    public interface IEntityController
    {
        public bool IsEnabled { get; }
        public IEntityModel EntityModel { get; }
        public UniTask MakeNextTurn();
    }
}