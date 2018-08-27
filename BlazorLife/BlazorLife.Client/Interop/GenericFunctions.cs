using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorLife.Client.Interop
{
    public static class GenericFunctions
    {
        public static void UpdateElementInnerHtml(string id, string html)
        {
            JSRuntime.Current.InvokeAsync<bool>("updateElementInnerHtml", id, html);
        }
    }
}
