using MelonLoader;

namespace DebugMod
{
    public static class CLog
    {
        public static Main ModInstance;

        public static void Info(string msg)
        {
            ModInstance.LoggerInstance.Msg(msg);
        }

        public static void Warning(string msg)
        {
            ModInstance.LoggerInstance.Warning(msg);
        }

        public static void Error(string msg)
        {
            ModInstance.LoggerInstance.Error(msg);
        }
    }
}