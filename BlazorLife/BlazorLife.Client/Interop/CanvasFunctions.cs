using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace BlazorLife.Client.Interop
{
    public static class CanvasFunctions
    {
        public static event EventHandler<CanvasClickedEventArgs> CanvasClicked;

        public static void AddCanvasMouseEvent()
        {
            JSRuntime.Current.InvokeAsync<bool>("addCanvasMouseEvent");
        }

        public static void ClearCanvas()
        {
            JSRuntime.Current.InvokeAsync<bool>("clearCanvas");
        }        

        public static void DrawCellsOnCanvas(IEnumerable<int> xCoordinates, IEnumerable<int> yCoordinates, int cellSize)
        {
            JSRuntime.Current.InvokeAsync<bool>("drawAllOnCanvas", xCoordinates, yCoordinates, cellSize);
        }

        public static void ResizeCanvas()
        {
            JSRuntime.Current.InvokeAsync<bool>("resizeCanvas");
        }

        public static void OnCanvasClicked(string x, string y)
        {
            int xValue = int.Parse(x);
            int yValue = int.Parse(y);

            CanvasClicked(null, new CanvasClickedEventArgs(xValue, yValue));
        }
    }
}
