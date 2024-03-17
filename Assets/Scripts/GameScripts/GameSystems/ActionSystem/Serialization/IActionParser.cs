namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.Serialization
{
    public interface IActionParser
    {
        public IAction GetActionFromDefinition(string actionDefinitionPath);
    }
}