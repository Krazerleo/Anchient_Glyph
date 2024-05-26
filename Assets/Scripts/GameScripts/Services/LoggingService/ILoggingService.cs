namespace AncientGlyph.GameScripts.Services.LoggingService
{
    public interface ILoggingService
    {
        void LogFatal(string errorMessage);
        void LogError(string errorMessage);
        void LogWarning(string warningMessage);
        void LogDebug(string debugMessage);
        void LogTrace(string message);
    }
}