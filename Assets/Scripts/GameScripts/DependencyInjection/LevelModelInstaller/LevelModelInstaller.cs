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
            var levelModelPath = LevelPathProvider.GetPathFromRuntime();
            var levelDeserializer = new LevelDeserializer(levelModelPath);

            return levelDeserializer.Deserialize();
        }
    }
}