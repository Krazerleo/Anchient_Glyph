using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.InputSystem
{
    public class InputConfigValidator
    {
        private static HashSet<string> _stringedKeys = new(Enum.GetNames(typeof(KeyCode)));

        public bool IsSimpleKeyboardKey(string stringKey)
        {
            return _stringedKeys.Contains(stringKey);
        }
    }
}