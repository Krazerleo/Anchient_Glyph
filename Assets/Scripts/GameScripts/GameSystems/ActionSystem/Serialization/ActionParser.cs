using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.Serialization
{
    public class ActionParser : IActionParser
    {
        public IAction GetActionFromDefinition(string actionDefinitionPath)
        {
            using var reader = new StreamReader(actionDefinitionPath);

            var jsonedAction = new JsonTextReader(reader);
            
            return null;
        }
    }
}