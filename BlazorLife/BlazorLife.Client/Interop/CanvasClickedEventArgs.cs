using System;

namespace BlazorLife.Client.Interop
{
    public class CanvasClickedEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public CanvasClickedEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}