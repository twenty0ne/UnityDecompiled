﻿namespace UnityEngine
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine.Scripting;

    /// <summary>
    /// <para>Information about a generated line of text.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential), UsedByNativeCode]
    public struct UILineInfo
    {
        /// <summary>
        /// <para>Index of the first character in the line.</para>
        /// </summary>
        public int startCharIdx;
        /// <summary>
        /// <para>Height of the line.</para>
        /// </summary>
        public int height;
        /// <summary>
        /// <para>The upper Y position of the line in pixels. This is used for text annotation such as the caret and selection box in the InputField.</para>
        /// </summary>
        public float topY;
    }
}

