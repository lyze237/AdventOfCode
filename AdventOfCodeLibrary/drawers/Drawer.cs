using System;

namespace AdventOfCodeLibrary.drawers
{
    public abstract class Drawer
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }

        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }

        public bool Dirty { get; set; } = true;
        public bool Errored { get; set; }

        public bool DoneTick { get; set; }

        private bool done;
        public bool Done
        {
            get => done;
            set
            {
                done = value;
                DoneTick = true;
            }
        } 

        protected Drawer(int x, int y, int width, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black)
        {
            X = x;
            Y = y;
            Width = width;

            Foreground = foreground;
            Background = background;        
        }

        public void Draw()
        {
            Dirty = false;
            DoneTick = false;

            lock (LockConsole.GetLock())
            {
                Console.ForegroundColor = Foreground;
                Console.BackgroundColor = Background;

                Console.SetCursorPosition(X, Y);

                DrawInternal();
            }
        }

        protected abstract void DrawInternal();

        public abstract void Tick();
    }
}