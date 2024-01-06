using System.Collections.Generic;
using System.Threading.Tasks;

using AncientGlyph.GameScripts.Interactors.Creatures.Controllers.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.GameLoop
{
    public class GameLoop : MonoBehaviour
    {
        private List<IEntityModelController> _entityControllerList = new();
        private bool _playerMakeTurn = false;

        private async void Update()
        {
            if (_playerMakeTurn == false)
            {
                await Task.Yield();
            }
            else
            {


                foreach (var entity in _entityControllerList)
                {
                    entity.MakeNextTurn();
                }
            }

        }
    }
}