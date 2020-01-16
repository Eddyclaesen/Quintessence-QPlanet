using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Services;

namespace Quintessence.QPlanet.Webshell.Infrastructure.Controllers
{
    public static class Extensions
    {
        public static TResult InvokeService<TService, TResult>(this ControllerBase controllerContext, Func<TService, TResult> action)
        {
            var invoker = new ServiceInvoker<TService>();
            return invoker.Execute(controllerContext.ControllerContext.HttpContext.ApplicationInstance.Context, action);
        }

        public static void InvokeService<TService>(this ControllerBase controllerContext, Action<TService> action)
        {
            var invoker = new ServiceInvoker<TService>();
            invoker.Execute(controllerContext.ControllerContext.HttpContext.ApplicationInstance.Context, action);
        }

        public static string StripHtml(this string text)
        {
            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(text, pattern, string.Empty);
        }
    }
}