﻿namespace UnityEngine
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// <para>A helper class that contains static method to inquire status of Unity Cluster.</para>
    /// </summary>
    public sealed class ClusterNetwork
    {
        /// <summary>
        /// <para>Check whether the current instance is disconnected from the cluster network.</para>
        /// </summary>
        public static bool isDisconnected { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Check whether the current instance is a master node in the cluster network.</para>
        /// </summary>
        public static bool isMasterOfCluster { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>To acquire or set the node index of the current machine from the cluster network.</para>
        /// </summary>
        public static int nodeIndex { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
    }
}

