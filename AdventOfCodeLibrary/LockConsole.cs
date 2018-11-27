using System;

namespace AdventOfCodeLibrary
{
    public static class LockConsole
    {
        private static readonly object Lock = new object();

        public static void WriteLine(string msg = "")
        {
            Write(msg + "\n");
        }

        public static void Write(string msg = "")
        {
            lock (Lock)
                Console.Write(msg);
        }

        public static void WriteLineAt(string msg, int x, int y)
        {
            WriteAt(msg + "\n", x, y);
        }

        public static void WriteAt(string msg, int x, int y)
        {
            lock (Lock)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(msg);
            }
        }

        public static void WriteLineIn(string msg, ConsoleColor foreground, ConsoleColor background)
        {
            WriteIn(msg + "\n", foreground, background);
        }

        public static void WriteIn(string msg, ConsoleColor foreground, ConsoleColor background)
        {
            lock (Lock)
            {
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
                Console.Write(msg);
            }
        }

        public static void WriteLineInAt(string msg, int x, int y, ConsoleColor foreground, ConsoleColor background)
        {
            WriteInAt(msg + "\n", x, y, foreground, background);
        }

        public static void WriteInAt(string msg, int x, int y, ConsoleColor foreground, ConsoleColor background)
        {
            lock (Lock)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
                Console.Write(msg);
            }
        }

        public static object GetLock()
        {
            return Lock;
        }

        public static void ResetColor()
        {
            lock (Lock)
            {
                Console.ResetColor();
            }
        }
    }
}
