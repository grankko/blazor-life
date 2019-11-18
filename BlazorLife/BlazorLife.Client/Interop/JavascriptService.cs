using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace BlazorLife.Client.Interop
{
    public class JavascriptService
    {
        private readonly IJSRuntime _jsRuntime;

        public static event EventHandler<CanvasClickedEventArgs> CanvasClicked;

        public JavascriptService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void UpdateElementInnerHtml(string id, string html)
        {
            _jsRuntime.InvokeAsync<bool>("updateElementInnerHtml", id, html);
        }

        public void AddCanvasMouseEvent()
        {
            _jsRuntime.InvokeAsync<bool>("addCanvasMouseEvent");
        }

        public void ClearCanvas()
        {
            _jsRuntime.InvokeAsync<bool>("clearCanvas");
        }        

        public void DrawCellsOnCanvas(IEnumerable<int> xCoordinates, IEnumerable<int> yCoordinates, int cellSize)
        {
            _jsRuntime.InvokeAsync<bool>("drawAllOnCanvas", xCoordinates, yCoordinates, cellSize);
        }

        public void ResizeCanvas()
        {
            _jsRuntime.InvokeAsync<bool>("resizeCanvas");
        }

        [JSInvokable]
        public static void OnCanvasClicked(int x, int y)
        {
            CanvasClicked(null, new CanvasClickedEventArgs(x,y));
        }
    }
}
