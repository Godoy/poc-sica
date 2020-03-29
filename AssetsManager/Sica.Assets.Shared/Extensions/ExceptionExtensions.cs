using System;

namespace Sica.Assets.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToStringReccurent(this Exception exception)
        {
            return exception == null ? "empty" : $"Exception: {exception.GetType()}\nMessage: {exception.Message}\nStack Trace: {exception.StackTrace}\nInner {exception.InnerException.ToStringReccurent()}";
        }
    }
}
