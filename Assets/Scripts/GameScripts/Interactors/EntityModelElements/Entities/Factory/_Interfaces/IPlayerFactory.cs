using AncientGlyph.GameScripts.Interactors.Entities.Controllers;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Interactors.Entities.Factories
{
    public interface IPlayerFactory
    {
        /// <summary>
        /// Instantiate player on scene and
        /// configure its input and persistent values
        /// </summary>
        /// <returns>Player Controller</returns>
        /// <exception cref="ArgumentException"></exception>
        public UniTask<PlayerController> CreatePlayer();
    }
}