namespace Nissensai2022.Console
{
    internal class Logger
    {
        public static void Log(object obj)
        {
            ConsoleController.Instance?.Log(obj);
        }

        public static void Warn(object obj)
        {
            ConsoleController.Instance?.Warn(obj);
        }

        public static void Error(object obj)
        {
            ConsoleController.Instance?.Error(obj);
        }

        public static void Remind(object obj)
        {
            ConsoleController.Instance?.Remind(obj);
        }
    }
}