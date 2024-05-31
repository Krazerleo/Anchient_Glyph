#nullable enable
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    public interface IItemFactory
    {
        UniTask<GameItemView?> CreateGameItem(ItemSerializationInfo serializationInfo);
    }
}