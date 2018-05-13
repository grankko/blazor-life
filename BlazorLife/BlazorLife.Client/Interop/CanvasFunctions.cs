using Microsoft.AspNetCore.Blazor.Browser.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorLife.Client.Interop
{
    public static class CanvasFunctions
    {
        public static event EventHandler<CanvasClickedEventArgs> OnCanvasClicked;

        public static void ClearCanvas()
        {
            RegisteredFunction.Invoke<bool>("clearCanvas");
        }        

        public static void DrawCellsOnCanvas(IEnumerable<int> xCoordinates, IEnumerable<int> yCoordinates, int cellSize)
        {
            RegisteredFunction.Invoke<bool>("drawAllOnCanvas", xCoordinates, yCoordinates, cellSize);
        }

        public static void ResizeCanvas()
        {
            RegisteredFunction.Invoke<bool>("resizeCanvas");
        }

        public static void CanvasClicked(string x, string y)
        {
            int xValue = int.Parse(x);
            int yValue = int.Parse(y);

            OnCanvasClicked(null, new CanvasClickedEventArgs(xValue, yValue));
        }
    }
}
