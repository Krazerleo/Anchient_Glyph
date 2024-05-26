using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;

using UnityEngine;
using UnityEngine.AddressableAssets;

using Cysharp.Threading.Tasks;
using Zenject;

namespace AncientGlyph.GameScripts.LifeCycle.Initialization
{
    public class LevelInitializer : MonoBehaviour
    {
        private IComponentLocatorService _componentLocator;
        private PlayStateMachine _playStateMachine;

        [Inject]
        public void Construct(PlayStateMachine gameStateMachine,
            IComponentLocatorService componentLocator)
        {
            _componentLocator = componentLocator;
            _playStateMachine = gameStateMachine;
        }

        private async void Awake()
        {
            await Initialize();
        }

        private async UniTask Initialize()
        {
            if (_componentLocator.IsComponentExist(this))
            {
                Destroy(gameObject);
                return;
            }

            await Addressables.InitializeAsync();

            _playStateMachine.EnterState<PlayStateInitial, object>(null);
        }
    }
}