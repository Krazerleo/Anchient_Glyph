using System;
using AncientGlyph.GameScripts.EntityModel.Controller;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.EntityModel.Factory
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