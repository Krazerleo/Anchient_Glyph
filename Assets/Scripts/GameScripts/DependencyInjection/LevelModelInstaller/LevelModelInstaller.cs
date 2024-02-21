using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.LevelModelInstaller
{
    public class LevelModelInstaller : MonoInstaller<LevelModelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelModel>()
                .FromMethod(GetLevelModel)
                .AsSingle();
        }

        private LevelModel GetLevelModel()
        {
            var levelModelPath = LevelModelPathProvider.GetPathFromRuntime();
            var levelDeserializer = new LevelModelDeserializer(levelModelPath);

            return levelDeserializer.Deserialize();
        }
    }
}