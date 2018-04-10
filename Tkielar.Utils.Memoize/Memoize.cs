using System;
using System.Collections.Concurrent;

namespace Tkielar.Utils.Memoize
{
    public static class Memoize
    {
        public static Func<TArg1, TResult> Create<TArg1, TResult>(Func<TArg1, TResult> func)
        {
            var cache = new ConcurrentDictionary<TArg1, TResult>();
            return arg => cache.GetOrAdd(arg, func);
        }
    }
}
