using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public interface IEntityController
    {
        public bool IsEnabled { get; }
        public IEntityModel EntityModel { get; }
        public UniTask MakeNextTurn();
    }
}