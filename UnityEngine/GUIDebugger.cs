﻿namespace UnityEngine
{
    using System;
    using System.Runtime.CompilerServices;

    internal class GUIDebugger
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_LogLayoutEntry(ref Rect rect, RectOffset margins, GUIStyle style);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_LogLayoutGroupEntry(ref Rect rect, RectOffset margins, GUIStyle style, bool isVertical);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void LogLayoutEndGroup();
        public static void LogLayoutEntry(Rect rect, RectOffset margins, GUIStyle style)
        {
            INTERNAL_CALL_LogLayoutEntry(ref rect, margins, style);
        }

        public static void LogLayoutGroupEntry(Rect rect, RectOffset margins, GUIStyle style, bool isVertical)
        {
            INTERNAL_CALL_LogLayoutGroupEntry(ref rect, margins, style, isVertical);
        }
    }
}

