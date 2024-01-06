using System.Threading.Tasks;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Creatures.Controllers.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.Interactors.Creatures.Controllers
{
    public class PlayerController : IEntityModelController
    {
        public PlayerController(LevelModel levelModel, IEntityModel entityModel)
        {
            _levelModel = levelModel;
            _playerEntity = entityModel;
        }

        private LevelModel _levelModel;
        private IEntityModel _playerEntity;

        public int TurnsCount => 1;

        public IEntityModel CreatureEntity => throw new System.NotImplementedException();

        public IInteraction PlayerAction;
        public IEntityModel InteractedEntity;
        public Direction? WalkDirection;

        public bool PlayerMakeTurn()
        {
            return true;
        }

        public async void MakeNextTurn()
        {
            if (PlayerAction == null && WalkDirection == null)
            {
                await Task.Yield();
            }
            else
            {
                if (PlayerAction != null)
                {
                    PlayerAction.InteractTo(InteractedEntity);
                }
                else
                {
                    var xOffset = WalkDirection == Direction.North ? 1 : WalkDirection == Direction.South ? -1 : 0;
                    var zOffset = WalkDirection == Direction.East ? 1 : WalkDirection == Direction.West ? -1 : 0;
                    var yOffset = WalkDirection == Direction.Up ? 1 : WalkDirection == Direction.Down ? -1 : 0;

                    if (_levelModel.MoveEntity(_playerEntity, xOffset, zOffset, yOffset))
                    {
                        _playerEntity.Position.Set(_playerEntity.Position.x + xOffset, _playerEntity.Position.z + zOffset, _playerEntity.Position.y + yOffset);
                    }

                }
            }
        }
    }
}