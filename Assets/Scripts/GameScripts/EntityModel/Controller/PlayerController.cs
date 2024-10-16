using System;
using AncientGlyph.GameScripts.EntityModel.Controller.PlayerControllerComponents;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.EntityModel.Controller
{
    public class PlayerController : IEntityController, IEffectAcceptor, IDisposable
    {
        public PlayerModel EntityModel { get; }
        private readonly MoveComponent _moveComponent;
        private readonly ActionComponent _actionComponent;
        private readonly InputRegistry _inputRegistry;
        
        public PlayerController(PlayerModel playerModel, MoveComponent moveComponent, ActionComponent actionComponent)
        {
            EntityModel = playerModel;
            _inputRegistry = new InputRegistry();
            
            _moveComponent = moveComponent;
            _moveComponent.RegisterMoveEvents(_inputRegistry);

            _actionComponent = actionComponent;
            _actionComponent.RegisterActionEvents(_inputRegistry);
        }

        IEntityModel IEntityController.EntityModel => EntityModel;
        public bool IsEnabled => true;

        public UniTask MakeNextTurn()
        {
            return _inputRegistry.ListenInput();
        }

        public void AcceptDamageEffect(DamageEffect damageEffect)
        {
            throw new NotImplementedException();
        }

        public void AcceptGoToEffect(GoToEffect goToEffect)
        {
            throw new NotImplementedException();
        }

        public void AcceptHealEffect(HealEffect healEffect)
        {
            throw new NotImplementedException();
        }
        
        public void Dispose()
        {
            _moveComponent.DeregisterMoveEvents();
            _actionComponent.DeregisterActionEvents();
        }
    }
}