namespace AncientGlyph.GameScripts.Services.LoggingService
{
    public class TestLoggingService : ILoggingService
    {
        public void LogFatal(string errorMessage) { }

        public void LogError(string errorMessage) { }

        public void LogWarning(string warningMessage) { }

        public void LogDebug(string debugMessage) { }

        public void LogTrace(string message) { }
    }
}