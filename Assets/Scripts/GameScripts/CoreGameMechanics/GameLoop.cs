using System.Collections.Generic;
using AncientGlyph.GameScripts.EntityModel.Controller;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace AncientGlyph.GameScripts.CoreGameMechanics
{
    /// <summary>
    /// Provides a turn based game
    /// </summary>
    [UsedImplicitly]
    public class GameLoop
    {
        private readonly IList<IEntityController> _entityControllers
            = new List<IEntityController>();

        private bool _isStopped;

        public void InjectEntityController(IEntityController controller)
        {
            _entityControllers.Add(controller);
        }

        public async UniTask StartGameLoop()
        {
            while (true)
            {
                foreach (var controller in _entityControllers)
                {
                    if (_isStopped)
                    {
                        return;
                    }

                    if (controller.IsEnabled == false)
                    {
                        continue;
                    }

                    await controller.MakeNextTurn();
                }
            }
        }

        public void StopGameLoop()
        {
            _isStopped = true;
        }
    }
}