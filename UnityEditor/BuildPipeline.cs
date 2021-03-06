﻿namespace UnityEditor
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEditor.BuildReporting;
    using UnityEngine;
    using UnityEngine.Internal;

    /// <summary>
    /// <para>Lets you programmatically build players or AssetBundles which can be loaded from the web.</para>
    /// </summary>
    public sealed class BuildPipeline
    {
        /// <summary>
        /// <para>Builds an asset bundle.</para>
        /// </summary>
        /// <param name="mainAsset">Lets you specify a specific object that can be conveniently retrieved using AssetBundle.mainAsset.</param>
        /// <param name="assets">An array of assets to write into the bundle.</param>
        /// <param name="pathName">The filename where to write the compressed asset bundle.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform to build the bundle for.</param>
        /// <param name="crc">The optional crc output parameter can be used to get a CRC checksum for the generated AssetBundle, which can be used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.</param>
        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName) => 
            WebPlayerAssetBundlesAreNoLongerSupported();

        /// <summary>
        /// <para>Builds an asset bundle.</para>
        /// </summary>
        /// <param name="mainAsset">Lets you specify a specific object that can be conveniently retrieved using AssetBundle.mainAsset.</param>
        /// <param name="assets">An array of assets to write into the bundle.</param>
        /// <param name="pathName">The filename where to write the compressed asset bundle.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform to build the bundle for.</param>
        /// <param name="crc">The optional crc output parameter can be used to get a CRC checksum for the generated AssetBundle, which can be used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.</param>
        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, BuildAssetBundleOptions assetBundleOptions) => 
            WebPlayerAssetBundlesAreNoLongerSupported();

        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, out uint crc)
        {
            crc = 0;
            return WebPlayerAssetBundlesAreNoLongerSupported();
        }

        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions)
        {
            crc = 0;
            return WebPlayerAssetBundlesAreNoLongerSupported();
        }

        /// <summary>
        /// <para>Builds an asset bundle.</para>
        /// </summary>
        /// <param name="mainAsset">Lets you specify a specific object that can be conveniently retrieved using AssetBundle.mainAsset.</param>
        /// <param name="assets">An array of assets to write into the bundle.</param>
        /// <param name="pathName">The filename where to write the compressed asset bundle.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform to build the bundle for.</param>
        /// <param name="crc">The optional crc output parameter can be used to get a CRC checksum for the generated AssetBundle, which can be used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.</param>
        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            uint num;
            return BuildAssetBundle(mainAsset, assets, pathName, out num, assetBundleOptions, targetPlatform);
        }

        [Obsolete("BuildAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static bool BuildAssetBundle(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            crc = 0;
            try
            {
                return BuildAssetBundleInternal(mainAsset, assets, null, pathName, assetBundleOptions, targetPlatform, out crc);
            }
            catch (Exception exception)
            {
                LogBuildExceptionAndExit("BuildPipeline.BuildAssetBundle", exception);
                return false;
            }
        }

        /// <summary>
        /// <para>Builds an asset bundle, with custom names for the assets.</para>
        /// </summary>
        /// <param name="assets">A collection of assets to be built into the asset bundle. Asset bundles can contain any asset found in the project folder.</param>
        /// <param name="assetNames">An array of strings of the same size as the number of assets.
        /// These will be used as asset names, which you can then pass to AssetBundle.Load to load a specific asset. Use BuildAssetBundle to just use the asset's path names instead.</param>
        /// <param name="pathName">The location where the compressed asset bundle will be written to.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform where the asset bundle will be used.</param>
        /// <param name="crc">An optional output parameter used to get a CRC checksum for the generated AssetBundle. (Used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.)</param>
        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName) => 
            WebPlayerAssetBundlesAreNoLongerSupported();

        /// <summary>
        /// <para>Builds an asset bundle, with custom names for the assets.</para>
        /// </summary>
        /// <param name="assets">A collection of assets to be built into the asset bundle. Asset bundles can contain any asset found in the project folder.</param>
        /// <param name="assetNames">An array of strings of the same size as the number of assets.
        /// These will be used as asset names, which you can then pass to AssetBundle.Load to load a specific asset. Use BuildAssetBundle to just use the asset's path names instead.</param>
        /// <param name="pathName">The location where the compressed asset bundle will be written to.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform where the asset bundle will be used.</param>
        /// <param name="crc">An optional output parameter used to get a CRC checksum for the generated AssetBundle. (Used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.)</param>
        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName, BuildAssetBundleOptions assetBundleOptions) => 
            WebPlayerAssetBundlesAreNoLongerSupported();

        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName, out uint crc)
        {
            crc = 0;
            return WebPlayerAssetBundlesAreNoLongerSupported();
        }

        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.", true)]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions)
        {
            crc = 0;
            return WebPlayerAssetBundlesAreNoLongerSupported();
        }

        /// <summary>
        /// <para>Builds an asset bundle, with custom names for the assets.</para>
        /// </summary>
        /// <param name="assets">A collection of assets to be built into the asset bundle. Asset bundles can contain any asset found in the project folder.</param>
        /// <param name="assetNames">An array of strings of the same size as the number of assets.
        /// These will be used as asset names, which you can then pass to AssetBundle.Load to load a specific asset. Use BuildAssetBundle to just use the asset's path names instead.</param>
        /// <param name="pathName">The location where the compressed asset bundle will be written to.</param>
        /// <param name="assetBundleOptions">Automatically include dependencies or always include complete assets instead of just the exact referenced objects.</param>
        /// <param name="targetPlatform">The platform where the asset bundle will be used.</param>
        /// <param name="crc">An optional output parameter used to get a CRC checksum for the generated AssetBundle. (Used to verify content when downloading AssetBundles using WWW.LoadFromCacheOrDownload.)</param>
        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            uint num;
            return BuildAssetBundleExplicitAssetNames(assets, assetNames, pathName, out num, assetBundleOptions, targetPlatform);
        }

        [Obsolete("BuildAssetBundleExplicitAssetNames has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static bool BuildAssetBundleExplicitAssetNames(UnityEngine.Object[] assets, string[] assetNames, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            crc = 0;
            try
            {
                return BuildAssetBundleInternal(null, assets, assetNames, pathName, assetBundleOptions, targetPlatform, out crc);
            }
            catch (Exception exception)
            {
                LogBuildExceptionAndExit("BuildPipeline.BuildAssetBundleExplicitAssetNames", exception);
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern bool BuildAssetBundleInternal(UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string[] assetNames, string pathName, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform, out uint crc);
        [Obsolete("BuildAssetBundles signature has changed. Please specify the targetPlatform parameter", true), ExcludeFromDocs]
        public static AssetBundleManifest BuildAssetBundles(string outputPath)
        {
            BuildAssetBundleOptions none = BuildAssetBundleOptions.None;
            return BuildAssetBundles(outputPath, none);
        }

        [Obsolete("BuildAssetBundles signature has changed. Please specify the targetPlatform parameter", true)]
        public static AssetBundleManifest BuildAssetBundles(string outputPath, [DefaultValue("BuildAssetBundleOptions.None")] BuildAssetBundleOptions assetBundleOptions)
        {
            WebPlayerAssetBundlesAreNoLongerSupported();
            return null;
        }

        [Obsolete("BuildAssetBundles signature has changed. Please specify the targetPlatform parameter", true), ExcludeFromDocs]
        public static AssetBundleManifest BuildAssetBundles(string outputPath, AssetBundleBuild[] builds)
        {
            BuildAssetBundleOptions none = BuildAssetBundleOptions.None;
            return BuildAssetBundles(outputPath, builds, none);
        }

        [Obsolete("BuildAssetBundles signature has changed. Please specify the targetPlatform parameter", true)]
        public static AssetBundleManifest BuildAssetBundles(string outputPath, AssetBundleBuild[] builds, [DefaultValue("BuildAssetBundleOptions.None")] BuildAssetBundleOptions assetBundleOptions)
        {
            WebPlayerAssetBundlesAreNoLongerSupported();
            return null;
        }

        /// <summary>
        /// <para>Build all AssetBundles specified in the editor.</para>
        /// </summary>
        /// <param name="outputPath">Output path for the AssetBundles.</param>
        /// <param name="assetBundleOptions">AssetBundle building options.</param>
        /// <param name="targetPlatform">Chosen target build platform.</param>
        /// <returns>
        /// <para>The manifest listing all AssetBundles included in this build.</para>
        /// </returns>
        public static AssetBundleManifest BuildAssetBundles(string outputPath, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            if (!Directory.Exists(outputPath))
            {
                throw new ArgumentException("The output path \"" + outputPath + "\" doesn't exist");
            }
            return BuildAssetBundlesInternal(outputPath, assetBundleOptions, targetPlatform);
        }

        /// <summary>
        /// <para>Build AssetBundles from a building map.</para>
        /// </summary>
        /// <param name="outputPath">Output path for the AssetBundles.</param>
        /// <param name="builds">AssetBundle building map.</param>
        /// <param name="assetBundleOptions">AssetBundle building options.</param>
        /// <param name="targetPlatform">Target build platform.</param>
        /// <returns>
        /// <para>The manifest listing all AssetBundles included in this build.</para>
        /// </returns>
        public static AssetBundleManifest BuildAssetBundles(string outputPath, AssetBundleBuild[] builds, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)
        {
            if (!Directory.Exists(outputPath))
            {
                throw new ArgumentException("The output path \"" + outputPath + "\" doesn't exist");
            }
            if (builds == null)
            {
                throw new ArgumentException("AssetBundleBuild cannot be null.");
            }
            return BuildAssetBundlesWithInfoInternal(outputPath, builds, assetBundleOptions, targetPlatform);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern AssetBundleManifest BuildAssetBundlesInternal(string outputPath, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern AssetBundleManifest BuildAssetBundlesWithInfoInternal(string outputPath, AssetBundleBuild[] builds, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform);
        /// <summary>
        /// <para>Builds a player.</para>
        /// </summary>
        /// <param name="buildPlayerOptions">Provide various options to control the behavior of BuildPipeline.BuildPlayer.</param>
        /// <returns>
        /// <para>An error message if an error occurred.</para>
        /// </returns>
        public static string BuildPlayer(BuildPlayerOptions buildPlayerOptions) => 
            BuildPlayer(buildPlayerOptions.scenes, buildPlayerOptions.locationPathName, buildPlayerOptions.assetBundleManifestPath, buildPlayerOptions.target, buildPlayerOptions.options);

        /// <summary>
        /// <para>Builds a player. These overloads are still supported, but will be replaces by BuildPlayer (BuildPlayerOptions). Please use it instead.</para>
        /// </summary>
        /// <param name="levels">The scenes to be included in the build. If empty, the currently open scene will be built. Paths are relative to the project folder (AssetsMyLevelsMyScene.unity).</param>
        /// <param name="locationPathName">The path where the application will be built.</param>
        /// <param name="target">The BuildTarget to build.</param>
        /// <param name="options">Additional BuildOptions, like whether to run the built player.</param>
        /// <returns>
        /// <para>An error message if an error occurred.</para>
        /// </returns>
        public static string BuildPlayer(string[] levels, string locationPathName, BuildTarget target, BuildOptions options)
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions {
                scenes = levels,
                locationPathName = locationPathName,
                target = target,
                options = options
            };
            return BuildPlayer(buildPlayerOptions);
        }

        /// <summary>
        /// <para>Builds a player. These overloads are still supported, but will be replaces by BuildPlayer (BuildPlayerOptions). Please use it instead.</para>
        /// </summary>
        /// <param name="levels">The scenes to be included in the build. If empty, the currently open scene will be built. Paths are relative to the project folder (AssetsMyLevelsMyScene.unity).</param>
        /// <param name="locationPathName">The path where the application will be built.</param>
        /// <param name="target">The BuildTarget to build.</param>
        /// <param name="options">Additional BuildOptions, like whether to run the built player.</param>
        /// <returns>
        /// <para>An error message if an error occurred.</para>
        /// </returns>
        public static string BuildPlayer(EditorBuildSettingsScene[] levels, string locationPathName, BuildTarget target, BuildOptions options)
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(levels),
                locationPathName = locationPathName,
                target = target,
                options = options
            };
            return BuildPlayer(buildPlayerOptions);
        }

        private static string BuildPlayer(string[] scenes, string locationPathName, string assetBundleManifestPath, BuildTarget target, BuildOptions options)
        {
            if (string.IsNullOrEmpty(locationPathName))
            {
                if (!(((options & BuildOptions.InstallInBuildFolder) != BuildOptions.CompressTextures) && PostprocessBuildPlayer.SupportsInstallInBuildFolder(target)))
                {
                    return "The 'locationPathName' parameter for BuildPipeline.BuildPlayer should not be null or empty.";
                }
            }
            else if (string.IsNullOrEmpty(Path.GetFileName(locationPathName)))
            {
                string extensionForBuildTarget = PostprocessBuildPlayer.GetExtensionForBuildTarget(target, options);
                if (!string.IsNullOrEmpty(extensionForBuildTarget))
                {
                    return $"For the '{target}' target the 'locationPathName' parameter for BuildPipeline.BuildPlayer should not end with a directory separator.
Provided path: '{locationPathName}', expected a path with the extension '.{extensionForBuildTarget}'.";
                }
            }
            try
            {
                return BuildPlayerInternal(scenes, locationPathName, assetBundleManifestPath, target, options).SummarizeErrors();
            }
            catch (Exception exception)
            {
                LogBuildExceptionAndExit("BuildPipeline.BuildPlayer", exception);
                return "";
            }
        }

        private static BuildReport BuildPlayerInternal(string[] levels, string locationPathName, string assetBundleManifestPath, BuildTarget target, BuildOptions options)
        {
            if (((BuildOptions.EnableHeadlessMode & options) != BuildOptions.CompressTextures) && ((BuildOptions.Development & options) != BuildOptions.CompressTextures))
            {
                throw new Exception("Unsupported build setting: cannot build headless development player");
            }
            return BuildPlayerInternalNoCheck(levels, locationPathName, assetBundleManifestPath, target, options, false);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern BuildReport BuildPlayerInternalNoCheck(string[] levels, string locationPathName, string assetBundleManifestPath, BuildTarget target, BuildOptions options, bool delayToAfterScriptReload);
        /// <summary>
        /// <para>Builds one or more scenes and all their dependencies into a compressed asset bundle.</para>
        /// </summary>
        /// <param name="levels">Pathnames of levels to include in the asset bundle.</param>
        /// <param name="locationPath">Pathname for the output asset bundle.</param>
        /// <param name="target">Runtime platform on which the asset bundle will be used.</param>
        /// <param name="crc">Output parameter to receive CRC checksum of generated assetbundle.</param>
        /// <param name="options">Build options. See BuildOptions for possible values.</param>
        /// <returns>
        /// <para>String with an error message, empty on success.</para>
        /// </returns>
        [Obsolete("BuildStreamedSceneAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static string BuildStreamedSceneAssetBundle(string[] levels, string locationPath, BuildTarget target) => 
            BuildPlayer(levels, locationPath, target, BuildOptions.BuildAdditionalStreamedScenes);

        /// <summary>
        /// <para>Builds one or more scenes and all their dependencies into a compressed asset bundle.</para>
        /// </summary>
        /// <param name="levels">Pathnames of levels to include in the asset bundle.</param>
        /// <param name="locationPath">Pathname for the output asset bundle.</param>
        /// <param name="target">Runtime platform on which the asset bundle will be used.</param>
        /// <param name="crc">Output parameter to receive CRC checksum of generated assetbundle.</param>
        /// <param name="options">Build options. See BuildOptions for possible values.</param>
        /// <returns>
        /// <para>String with an error message, empty on success.</para>
        /// </returns>
        [Obsolete("BuildStreamedSceneAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static string BuildStreamedSceneAssetBundle(string[] levels, string locationPath, BuildTarget target, BuildOptions options) => 
            BuildPlayer(levels, locationPath, target, options | BuildOptions.BuildAdditionalStreamedScenes);

        [Obsolete("BuildStreamedSceneAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static string BuildStreamedSceneAssetBundle(string[] levels, string locationPath, BuildTarget target, out uint crc) => 
            BuildStreamedSceneAssetBundle(levels, locationPath, target, out crc, BuildOptions.CompressTextures);

        [Obsolete("BuildStreamedSceneAssetBundle has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static string BuildStreamedSceneAssetBundle(string[] levels, string locationPath, BuildTarget target, out uint crc, BuildOptions options)
        {
            crc = 0;
            try
            {
                BuildReport report = BuildPlayerInternal(levels, locationPath, null, target, (options | BuildOptions.BuildAdditionalStreamedScenes) | BuildOptions.ComputeCRC);
                crc = report.crc;
                string str = report.SummarizeErrors();
                UnityEngine.Object.DestroyImmediate(report, true);
                return str;
            }
            catch (Exception exception)
            {
                LogBuildExceptionAndExit("BuildPipeline.BuildStreamedSceneAssetBundle", exception);
                return "";
            }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetBuildTargetAdvancedLicenseName(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern BuildTarget GetBuildTargetByName(string platform);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern BuildTargetGroup GetBuildTargetGroup(BuildTarget platform);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern BuildTargetGroup GetBuildTargetGroupByName(string platform);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetBuildTargetGroupDisplayName(BuildTargetGroup targetPlatformGroup);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetBuildTargetGroupName(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        internal static extern string GetBuildTargetName(BuildTarget targetPlatform);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetBuildToolsDirectory(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetCRCForAssetBundle(string targetPath, out uint crc);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        internal static extern string GetEditorTargetName();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetHashForAssetBundle(string targetPath, out Hash128 hash);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetMonoBinDirectory(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetMonoLibDirectory(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetMonoProfileLibDirectory(BuildTarget target, string profile);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        internal static extern string GetPlaybackEngineDirectory(BuildTarget target, BuildOptions options);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool IsBuildTargetCompatibleWithOS(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool IsBuildTargetSupported(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool IsUnityScriptEvalSupported(BuildTarget target);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        internal static extern bool LicenseCheck(BuildTarget target);
        private static void LogBuildExceptionAndExit(string buildFunctionName, Exception exception)
        {
            object[] args = new object[] { buildFunctionName };
            Debug.LogErrorFormat("Internal Error in {0}:", args);
            Debug.LogException(exception);
            EditorApplication.Exit(1);
        }

        /// <summary>
        /// <para>Lets you manage cross-references and dependencies between different asset bundles and player builds.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.InternalCall), Obsolete("PopAssetDependencies has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static extern void PopAssetDependencies();
        /// <summary>
        /// <para>Lets you manage cross-references and dependencies between different asset bundles and player builds.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.InternalCall), Obsolete("PushAssetDependencies has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
        public static extern void PushAssetDependencies();
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void SetPlaybackEngineDirectory(BuildTarget target, BuildOptions options, string playbackEngineDirectory);
        [Obsolete("WebPlayer has been removed in 5.4", true)]
        private static bool WebPlayerAssetBundlesAreNoLongerSupported()
        {
            throw new InvalidOperationException("WebPlayer asset bundles can no longer be built in 5.4+");
        }

        /// <summary>
        /// <para>Is a player currently being built?</para>
        /// </summary>
        public static bool isBuildingPlayer { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

