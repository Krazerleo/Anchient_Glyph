using AncientGlyph.GameScripts.Services.SaveDataService;
using UnityEngine;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.SaveDataProviderInstaller
{
    public class DevSaveDataInstaller : MonoInstaller<DevSaveDataInstaller>
    {
        public Vector3Int PlayerPosition;
        public override void InstallBindings()
        {
            Container.Bind<Vector3Int>()
                .FromInstance(PlayerPosition)
                .AsTransient()
                .WhenInjectedInto<DevSaveDataService>();
            
            Container.Bind<ISaveDataService>()
                .To<DevSaveDataService>()
                .AsSingle();
        }
    }
}