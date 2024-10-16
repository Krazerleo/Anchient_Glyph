using UnityEngine;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.CameraInstaller
{
    public class DevCameraInstaller : MonoInstaller<DevCameraInstaller>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _position;

        public override void InstallBindings()
        {
            _camera.transform.position = _position;
            
            Container.Bind<Camera>()
                     .FromComponentInNewPrefab(_camera)
                     .AsSingle();
        }
    }
}