using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class LevelModelInstaller : MonoInstaller<LevelModelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelModel>().FromMethod(GetLevelModel);
        }

        private LevelModel GetLevelModel()
        {
            var levelModelPath = LevelModelPathProvider.GetPathFromRuntime();
            var levelDeserializer = new LevelModelDeserializer(levelModelPath);

            return levelDeserializer.Deserialize();
        }
    }
}