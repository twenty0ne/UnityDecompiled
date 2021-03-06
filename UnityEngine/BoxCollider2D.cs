﻿namespace UnityEngine
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <para>Collider for 2D physics representing an axis-aligned rectangle.</para>
    /// </summary>
    public sealed class BoxCollider2D : Collider2D
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_size(out Vector2 value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_size(ref Vector2 value);

        /// <summary>
        /// <para>The center point of the collider in local space.</para>
        /// </summary>
        [Obsolete("BoxCollider2D.center has been deprecated. Use BoxCollider2D.offset instead (UnityUpgradable) -> offset", true)]
        public Vector2 center
        {
            get => 
                Vector2.zero;
            set
            {
            }
        }

        /// <summary>
        /// <para>The width and height of the rectangle.</para>
        /// </summary>
        public Vector2 size
        {
            get
            {
                Vector2 vector;
                this.INTERNAL_get_size(out vector);
                return vector;
            }
            set
            {
                this.INTERNAL_set_size(ref value);
            }
        }
    }
}

