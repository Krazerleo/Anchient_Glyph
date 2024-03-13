using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.LoggingService
{
    [UsedImplicitly]
    public class DebugLoggingService : ILoggingService
    {
        public void LogError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }

        public void LogWarning(string warningMessage)
        {
            Debug.LogWarning(warningMessage);
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}