using AncientGlyph.GameScripts.InputSystem;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class GameInputInstaller : MonoInstaller<GameInputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInput>()
                     .AsSingle();
        }
    }
}