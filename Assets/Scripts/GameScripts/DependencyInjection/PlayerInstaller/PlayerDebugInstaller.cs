using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controllers;
using AncientGlyph.GameScripts.PlayerInput;

using UnityEngine;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class PlayerDebugInstaller : MonoInstaller<PlayerDebugInstaller>
    {
        public Vector3Int SpawnPoint;

        public GameObject PlayerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerController>()
                .FromSubContainerResolve()
                .ByMethod(BindPlayerController)
                .AsSingle();
        }

        private void BindPlayerController(DiContainer subContainer)
        {
            subContainer.Bind<PlayerController>().AsSingle();

            subContainer.Bind<PlayerModel>().AsSingle();

            subContainer.Bind(typeof(PlayerAnimator), typeof(PlayerMoveInput))
                .FromComponentInNewPrefab(PlayerPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}