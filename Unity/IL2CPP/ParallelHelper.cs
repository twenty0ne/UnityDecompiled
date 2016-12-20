﻿namespace Unity.IL2CPP
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using Unity.IL2CPP.Common;

    public static class ParallelHelper
    {
        [CompilerGenerated]
        private static bool <ForEach`1>m__0<TSource>(bool b)
        {
            return (CodeGenOptions.ForceSerial || (b && CodeGenOptions.ForceSerialBetaOnly));
        }

        [CompilerGenerated]
        private static bool <Select`2>m__1<TSource, TResult>(bool b)
        {
            return (CodeGenOptions.ForceSerial || (b && CodeGenOptions.ForceSerialBetaOnly));
        }

        public static void ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> action, [Optional, DefaultParameterValue(true)] bool loadBalance, [Optional, DefaultParameterValue(false)] bool betaTag)
        {
            Unity.IL2CPP.Common.ParallelHelper.ForEach<TSource>(source, action, new Func<bool, bool>(null, (IntPtr) <ForEach`1>m__0<TSource>), loadBalance, betaTag);
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> func, [Optional, DefaultParameterValue(true)] bool loadBalance, [Optional, DefaultParameterValue(false)] bool betaTag)
        {
            return Unity.IL2CPP.Common.ParallelHelper.Select<TSource, TResult>(source, func, new Func<bool, bool>(null, (IntPtr) <Select`2>m__1<TSource, TResult>), loadBalance, betaTag);
        }

        public static bool ForceSerialByDefault
        {
            get
            {
                return (PlatformUtils.RunningWithMono() || true);
            }
        }
    }
}
