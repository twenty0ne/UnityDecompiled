﻿namespace UnityEngine
{
    using System;
    using UnityEngine.Scripting;

    /// <summary>
    /// <para>All possible particle system vertex shader inputs.</para>
    /// </summary>
    [UsedByNativeCode]
    public enum ParticleSystemVertexStream
    {
        Position,
        Normal,
        Tangent,
        Color,
        UV,
        UV2,
        UV3,
        UV4,
        AnimBlend,
        AnimFrame,
        Center,
        VertexID,
        SizeX,
        SizeXY,
        SizeXYZ,
        Rotation,
        Rotation3D,
        RotationSpeed,
        RotationSpeed3D,
        Velocity,
        Speed,
        AgePercent,
        InvStartLifetime,
        StableRandomX,
        StableRandomXY,
        StableRandomXYZ,
        StableRandomXYZW,
        VaryingRandomX,
        VaryingRandomXY,
        VaryingRandomXYZ,
        VaryingRandomXYZW,
        Custom1X,
        Custom1XY,
        Custom1XYZ,
        Custom1XYZW,
        Custom2X,
        Custom2XY,
        Custom2XYZ,
        Custom2XYZW
    }
}
