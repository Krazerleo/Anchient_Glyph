using UnityEngine;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.CameraInstaller
{
    public class CameraInstaller : MonoInstaller<CameraInstaller>
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromComponentInNewPrefab(_camera)
                .AsSingle();
        }
    }
}