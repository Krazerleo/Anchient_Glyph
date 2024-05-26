using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace AncientGlyph.GameScripts.LifeCycle.Initialization
{
    public class GameInitializer : MonoBehaviour
    {
        private IComponentLocatorService _componentLocator;
        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            Initialize();
        }

        [Inject]
        public void Construct(IComponentLocatorService componentLocator,
            GameStateMachine gameStateMachine)
        {
            _componentLocator = componentLocator;
            _gameStateMachine = gameStateMachine;
        }

        private void Initialize()
        {
            if (_componentLocator.IsComponentExist(this))
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Addressables.InitializeAsync().ToUniTask().GetAwaiter().GetResult();

            _gameStateMachine.EnterState<BootstrapState, object>(null);
        }
    }
}