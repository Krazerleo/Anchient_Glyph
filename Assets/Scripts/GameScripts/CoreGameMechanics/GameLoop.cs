using System.Collections.Generic;
using AncientGlyph.GameScripts.Interactors.Entities.Controller._Interfaces;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.CoreGameMechanics
{
    /// <summary>
    /// Provides a turn based game
    /// </summary>
    public class GameLoop
    {
        private IList<IEntityController> _entityControllers
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