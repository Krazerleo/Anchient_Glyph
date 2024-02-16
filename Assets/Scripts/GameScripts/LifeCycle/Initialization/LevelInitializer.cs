using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
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

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_componentLocator.IsComponentExist(this))
            {
                Destroy(gameObject);
                return;
            }

            Addressables.InitializeAsync().ToUniTask().GetAwaiter().GetResult();

            _playStateMachine.EnterState<PlayStateInitial, object>(null);
        }
    }
}