using UnityEngine;

namespace AncientGlyph.GameScripts.Services.LoggingService
{
    public class DebugLoggingService : ILoggingService
    {
        public void LogError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}