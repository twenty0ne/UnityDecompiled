﻿namespace UnityEngine
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <para>Base class for AnimationClips and BlendTrees.</para>
    /// </summary>
    public class Motion : UnityEngine.Object
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_averageSpeed(out Vector3 value);
        [MethodImpl(MethodImplOptions.InternalCall), Obsolete("ValidateIfRetargetable is not supported anymore. Use isHumanMotion instead.", true)]
        public extern bool ValidateIfRetargetable(bool val);

        public float apparentSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public float averageAngularSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public float averageDuration { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public Vector3 averageSpeed
        {
            get
            {
                Vector3 vector;
                this.INTERNAL_get_averageSpeed(out vector);
                return vector;
            }
        }

        [Obsolete("isAnimatorMotion is not supported anymore. Use !legacy instead.", true)]
        public bool isAnimatorMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public bool isHumanMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public bool isLooping { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public bool legacy { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

