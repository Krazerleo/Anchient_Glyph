using AncientGlyph.GameScripts.Interactors.Interactions;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller
{
    public interface IEntityController : IInteractable
    {
        public bool IsEnabled { get; }
        public IEntityModel EntityModel { get; }
        public UniTask MakeNextTurn();
    }
}