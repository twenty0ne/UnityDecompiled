﻿namespace UnityEditor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// <para>AssetImportor for importing SpeedTree model assets.</para>
    /// </summary>
    public sealed class SpeedTreeImporter : AssetImporter
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private float <shininess>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private Color <specColor>k__BackingField;
        /// <summary>
        /// <para>Gets an array of name strings for wind quality value.</para>
        /// </summary>
        public static readonly string[] windQualityNames = new string[] { "None", "Fastest", "Fast", "Better", "Best", "Palm" };

        /// <summary>
        /// <para>Generates all necessary materials under materialFolderPath. If Version Control is enabled please first check out the folder.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void GenerateMaterials();
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_hueVariation(out Color value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_mainColor(out Color value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_hueVariation(ref Color value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_mainColor(ref Color value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern void SetMaterialVersionToCurrent();

        /// <summary>
        /// <para>Gets and sets a default alpha test reference values.</para>
        /// </summary>
        public float alphaTestRef { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Indicates if the cross-fade LOD transition, applied to the last mesh LOD and the billboard, should be animated.</para>
        /// </summary>
        public bool animateCrossFading { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Returns the best-possible wind quality on this asset (configured in SpeedTree modeler).</para>
        /// </summary>
        public int bestWindQuality { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Proportion of the last 3D mesh LOD region width which is used for cross-fading to billboard tree.</para>
        /// </summary>
        public float billboardTransitionCrossFadeWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets an array of booleans to enable shadow casting for each LOD.</para>
        /// </summary>
        public bool[] castShadows { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets an array of booleans to enable normal mapping for each LOD.</para>
        /// </summary>
        public bool[] enableBump { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets an array of booleans to enable Hue variation effect for each LOD.</para>
        /// </summary>
        public bool[] enableHue { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Enables smooth LOD transitions.</para>
        /// </summary>
        public bool enableSmoothLODTransition { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Proportion of the billboard LOD region width which is used for fading out the billboard.</para>
        /// </summary>
        public float fadeOutWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Tells if there is a billboard LOD.</para>
        /// </summary>
        public bool hasBillboard { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Tells if the SPM file has been previously imported.</para>
        /// </summary>
        public bool hasImported { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Gets and sets a default Hue variation color and amount (in alpha).</para>
        /// </summary>
        public Color hueVariation
        {
            get
            {
                Color color;
                this.INTERNAL_get_hueVariation(out color);
                return color;
            }
            set
            {
                this.INTERNAL_set_hueVariation(ref value);
            }
        }

        /// <summary>
        /// <para>Gets and sets an array of floats of each LOD's screen height value.</para>
        /// </summary>
        public float[] LODHeights { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets a default main color.</para>
        /// </summary>
        public Color mainColor
        {
            get
            {
                Color color;
                this.INTERNAL_get_mainColor(out color);
                return color;
            }
            set
            {
                this.INTERNAL_set_mainColor(ref value);
            }
        }

        /// <summary>
        /// <para>Returns the folder path where generated materials will be placed in.</para>
        /// </summary>
        public string materialFolderPath { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal bool materialsShouldBeRegenerated { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Gets and sets an array of booleans to enable shadow receiving for each LOD.</para>
        /// </summary>
        public bool[] receiveShadows { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets an array of Reflection Probe usages for each LOD.</para>
        /// </summary>
        public ReflectionProbeUsage[] reflectionProbeUsages { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>How much to scale the tree model compared to what is in the .spm file.</para>
        /// </summary>
        public float scaleFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets a default Shininess value.</para>
        /// </summary>
        [Obsolete("shininess is no longer used and has been deprecated.", true)]
        public float shininess { get; set; }

        /// <summary>
        /// <para>Gets and sets a default specular color.</para>
        /// </summary>
        [Obsolete("specColor is no longer used and has been deprecated.", true)]
        public Color specColor { get; set; }

        /// <summary>
        /// <para>Gets and sets an array of booleans to enable Light Probe lighting for each LOD.</para>
        /// </summary>
        public bool[] useLightProbes { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

        /// <summary>
        /// <para>Gets and sets an array of integers of the wind qualities on each LOD. Values will be clampped by BestWindQuality internally.</para>
        /// </summary>
        public int[] windQualities { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
    }
}

