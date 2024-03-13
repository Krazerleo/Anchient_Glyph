using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.PlayStateMachineInstaller
{
    public class PlayStateMachineInstaller :
        MonoInstaller<PlayStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayStateMachine>()
                .FromSubContainerResolve()
                .ByMethod(subcontainer =>
                {
                    subcontainer.Bind<PlayStateMachine>().AsSingle();

                    subcontainer.BindInterfacesAndSelfTo<PlayStateInitial>()
                        .AsSingle();

                    subcontainer.BindInterfacesAndSelfTo<PlayStateRegular>()
                        .AsSingle();

                    subcontainer.BindInterfacesAndSelfTo<PlayStateInFight>()
                        .AsSingle();
                })
                .AsSingle();
        }
    }
}