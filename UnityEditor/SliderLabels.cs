﻿namespace UnityEditor
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    internal struct SliderLabels
    {
        public GUIContent leftLabel;
        public GUIContent rightLabel;
        public void SetLabels(GUIContent leftLabel, GUIContent rightLabel)
        {
            if (Event.current.type == EventType.Repaint)
            {
                this.leftLabel = leftLabel;
                this.rightLabel = rightLabel;
            }
        }

        public bool HasLabels() => 
            ((Event.current.type == EventType.Repaint) && ((this.leftLabel != null) && (this.rightLabel != null)));
    }
}

