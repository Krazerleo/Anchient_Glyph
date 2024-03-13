using AncientGlyph.GameScripts.CoreGameMechanics;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.GameLoopInstaller
{
    public class GameLoopInstaller : MonoInstaller<GameLoopInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameLoop>().AsSingle();
        }
    }
}