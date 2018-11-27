using System;

public abstract class Drawer
{
    public int X { get; set; }
    public int Y { get; set; }

    public ConsoleColor Foreground { get; set; }
    public ConsoleColor Background { get; set; }

    public bool Dirty { get; set; } = true;

    protected Drawer(int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        X = x;
        Y = y;
        Foreground = foreground;
        Background = background;        
    }

    public void Draw()
    {
        Dirty = false;

        Console.ForegroundColor = Foreground;
        Console.BackgroundColor = Background;

        Console.SetCursorPosition(X, Y);

        DrawInternal();
    }

    protected abstract void DrawInternal();

    public abstract void Tick();
}