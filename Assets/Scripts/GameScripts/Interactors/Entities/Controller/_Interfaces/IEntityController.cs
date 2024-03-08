using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller._Interfaces
{
    public interface IEntityController
    {
        public bool IsEnabled { get; }
        public IEntityModel EntityModel { get; }
        public UniTask MakeNextTurn();
    }
}