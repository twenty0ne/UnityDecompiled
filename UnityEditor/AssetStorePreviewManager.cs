﻿namespace UnityEditor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    internal sealed class AssetStorePreviewManager
    {
        internal int CacheHit = 0;
        private const int kMaxConcurrentDownloads = 15;
        private const int kMaxConvertionsPerTick = 1;
        private const double kQueryDelay = 0.2;
        private int m_Aborted = 0;
        private Dictionary<string, CachedAssetStoreImage> m_CachedAssetStoreImages;
        private int m_CacheRemove = 0;
        private int m_ConvertedThisTick = 0;
        private CachedAssetStoreImage m_DummyItem = new CachedAssetStoreImage();
        private int m_MaxCachedAssetStoreImages = 10;
        private PreviewRenderUtility m_PreviewUtility;
        private int m_Success = 0;
        internal int Requested = 0;
        private static bool s_NeedsRepaint = false;
        private static RenderTexture s_RenderTexture = null;
        private static AssetStorePreviewManager s_SharedAssetStorePreviewManager = null;

        private AssetStorePreviewManager()
        {
        }

        public static void AbortOlderThan(double timestamp)
        {
            foreach (KeyValuePair<string, CachedAssetStoreImage> pair in CachedAssetStoreImages)
            {
                CachedAssetStoreImage image = pair.Value;
                if ((image.lastUsed < timestamp) && (image.client != null))
                {
                    image.requestedWidth = -1;
                    image.client.Abort();
                    image.client = null;
                }
            }
            Instance.m_ConvertedThisTick = 0;
        }

        public static void AbortSize(int size)
        {
            AsyncHTTPClient.AbortByTag("previewSize-" + size.ToString());
            foreach (KeyValuePair<string, CachedAssetStoreImage> pair in CachedAssetStoreImages)
            {
                if (pair.Value.requestedWidth == size)
                {
                    pair.Value.requestedWidth = -1;
                    pair.Value.client = null;
                }
            }
        }

        public static bool CheckRepaint()
        {
            bool flag = s_NeedsRepaint;
            s_NeedsRepaint = false;
            return flag;
        }

        private static void ExpireCacheEntries()
        {
            while (CacheFull)
            {
                string key = null;
                CachedAssetStoreImage image = null;
                foreach (KeyValuePair<string, CachedAssetStoreImage> pair in CachedAssetStoreImages)
                {
                    if ((image == null) || (image.lastUsed > pair.Value.lastUsed))
                    {
                        image = pair.Value;
                        key = pair.Key;
                    }
                }
                CachedAssetStoreImages.Remove(key);
                AssetStorePreviewManager instance = Instance;
                instance.m_CacheRemove++;
                if (image == null)
                {
                    Debug.LogError("Null entry found while removing cache entry");
                    break;
                }
                if (image.client != null)
                {
                    image.client.Abort();
                    image.client = null;
                }
                if (image.image != null)
                {
                    UnityEngine.Object.DestroyImmediate(image.image);
                }
            }
        }

        private static CachedAssetStoreImage RenderEntry(CachedAssetStoreImage cached, GUIStyle labelStyle, GUIStyle iconStyle)
        {
            if ((cached.label != null) && (cached.image != null))
            {
                Texture2D image = cached.image;
                cached.image = new Texture2D(cached.requestedWidth, cached.requestedWidth, TextureFormat.RGB24, false, true);
                ScaleImage(cached.requestedWidth, cached.requestedWidth, image, cached.image, iconStyle);
                UnityEngine.Object.DestroyImmediate(image);
                cached.label = null;
                AssetStorePreviewManager instance = Instance;
                instance.m_ConvertedThisTick++;
            }
            return cached;
        }

        internal static void ScaleImage(int w, int h, Texture2D inimage, Texture2D outimage, GUIStyle bgStyle)
        {
            SavedRenderTargetState state = new SavedRenderTargetState();
            if ((s_RenderTexture != null) && ((s_RenderTexture.width != w) || (s_RenderTexture.height != h)))
            {
                UnityEngine.Object.DestroyImmediate(s_RenderTexture);
                s_RenderTexture = null;
            }
            if (s_RenderTexture == null)
            {
                s_RenderTexture = RenderTexture.GetTemporary(w, h, 0x10, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                s_RenderTexture.hideFlags = HideFlags.HideAndDontSave;
            }
            RenderTexture rt = s_RenderTexture;
            RenderTexture.active = rt;
            Rect screenRect = new Rect(0f, 0f, (float) w, (float) h);
            EditorGUIUtility.SetRenderTextureNoViewport(rt);
            GL.LoadOrtho();
            GL.LoadPixelMatrix(0f, (float) w, (float) h, 0f);
            ShaderUtil.rawViewportRect = new Rect(0f, 0f, (float) w, (float) h);
            ShaderUtil.rawScissorRect = new Rect(0f, 0f, (float) w, (float) h);
            GL.Clear(true, true, new Color(0f, 0f, 0f, 0f));
            Rect rect2 = screenRect;
            if (inimage.width > inimage.height)
            {
                float num = rect2.height * (((float) inimage.height) / ((float) inimage.width));
                rect2.height = (int) num;
                rect2.y += (int) (num * 0.5f);
            }
            else if (inimage.width < inimage.height)
            {
                float num2 = rect2.width * (((float) inimage.width) / ((float) inimage.height));
                rect2.width = (int) num2;
                rect2.x += (int) (num2 * 0.5f);
            }
            if (((bgStyle != null) && (bgStyle.normal != null)) && (bgStyle.normal.background != null))
            {
                Graphics.DrawTexture(screenRect, bgStyle.normal.background);
            }
            Graphics.DrawTexture(rect2, inimage);
            outimage.ReadPixels(screenRect, 0, 0, false);
            outimage.Apply();
            outimage.hideFlags = HideFlags.HideAndDontSave;
            state.Restore();
        }

        private static AsyncHTTPClient SetupTextureDownload(CachedAssetStoreImage cached, string url, string tag)
        {
            <SetupTextureDownload>c__AnonStorey0 storey = new <SetupTextureDownload>c__AnonStorey0 {
                cached = cached,
                url = url
            };
            storey.client = new AsyncHTTPClient(storey.url);
            storey.cached.client = storey.client;
            storey.client.tag = tag;
            storey.client.doneCallback = new AsyncHTTPClient.DoneCallback(storey.<>m__0);
            return storey.client;
        }

        public static string StatsString() => 
            $"Reqs: {Instance.Requested}, Ok: {Instance.m_Success}, Abort: {Instance.m_Aborted}, CacheDel: {Instance.m_CacheRemove}, Cache: {CachedAssetStoreImages.Count}/{Instance.m_MaxCachedAssetStoreImages}, CacheHit: {Instance.CacheHit}";

        public static CachedAssetStoreImage TextureFromUrl(string url, string label, int textureSize, GUIStyle labelStyle, GUIStyle iconStyle, bool onlyCached)
        {
            CachedAssetStoreImage image2;
            if (string.IsNullOrEmpty(url))
            {
                return Instance.m_DummyItem;
            }
            bool flag = true;
            if (CachedAssetStoreImages.TryGetValue(url, out image2))
            {
                image2.lastUsed = EditorApplication.timeSinceStartup;
                bool flag2 = image2.requestedWidth == textureSize;
                bool flag3 = (image2.image != null) && (image2.image.width == textureSize);
                bool flag4 = image2.requestedWidth == -1;
                if (((flag3 || flag2) || onlyCached) && !flag4)
                {
                    AssetStorePreviewManager manager1 = Instance;
                    manager1.CacheHit++;
                    bool flag5 = image2.client != null;
                    bool flag6 = image2.label == null;
                    bool flag7 = flag5 || flag6;
                    bool flag8 = Instance.m_ConvertedThisTick > 1;
                    s_NeedsRepaint = s_NeedsRepaint || flag8;
                    return ((!flag7 && !flag8) ? RenderEntry(image2, labelStyle, iconStyle) : image2);
                }
                flag = false;
                if (Downloading >= 15)
                {
                    return ((image2.image != null) ? image2 : Instance.m_DummyItem);
                }
            }
            else
            {
                if (onlyCached || (Downloading >= 15))
                {
                    return Instance.m_DummyItem;
                }
                image2 = new CachedAssetStoreImage {
                    image = null,
                    lastUsed = EditorApplication.timeSinceStartup
                };
            }
            if (image2.image == null)
            {
                image2.lastFetched = EditorApplication.timeSinceStartup;
            }
            image2.requestedWidth = textureSize;
            image2.label = label;
            AsyncHTTPClient client = null;
            client = SetupTextureDownload(image2, url, "previewSize-" + textureSize);
            ExpireCacheEntries();
            if (flag)
            {
                CachedAssetStoreImages.Add(url, image2);
            }
            client.Begin();
            AssetStorePreviewManager instance = Instance;
            instance.Requested++;
            return image2;
        }

        private static Dictionary<string, CachedAssetStoreImage> CachedAssetStoreImages
        {
            get
            {
                if (Instance.m_CachedAssetStoreImages == null)
                {
                    Instance.m_CachedAssetStoreImages = new Dictionary<string, CachedAssetStoreImage>();
                }
                return Instance.m_CachedAssetStoreImages;
            }
        }

        public static bool CacheFull =>
            (CachedAssetStoreImages.Count >= MaxCachedImages);

        public static int Downloading
        {
            get
            {
                int num = 0;
                foreach (KeyValuePair<string, CachedAssetStoreImage> pair in CachedAssetStoreImages)
                {
                    if (pair.Value.client != null)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        internal static AssetStorePreviewManager Instance
        {
            get
            {
                if (s_SharedAssetStorePreviewManager == null)
                {
                    s_SharedAssetStorePreviewManager = new AssetStorePreviewManager();
                    s_SharedAssetStorePreviewManager.m_DummyItem.lastUsed = -1.0;
                }
                return s_SharedAssetStorePreviewManager;
            }
        }

        public static int MaxCachedImages
        {
            get => 
                Instance.m_MaxCachedAssetStoreImages;
            set
            {
                Instance.m_MaxCachedAssetStoreImages = value;
            }
        }

        [CompilerGenerated]
        private sealed class <SetupTextureDownload>c__AnonStorey0
        {
            internal AssetStorePreviewManager.CachedAssetStoreImage cached;
            internal AsyncHTTPClient client;
            internal string url;

            internal void <>m__0(AsyncHTTPClient c)
            {
                this.cached.client = null;
                if (!this.client.IsSuccess())
                {
                    if (this.client.state != AsyncHTTPClient.State.ABORTED)
                    {
                        string[] textArray1 = new string[] { "error ", this.client.text, " ", this.client.state.ToString(), " '", this.url, "'" };
                        string message = string.Concat(textArray1);
                        if (ObjectListArea.s_Debug)
                        {
                            Debug.LogError(message);
                        }
                        else
                        {
                            Console.Write(message);
                        }
                    }
                    else
                    {
                        AssetStorePreviewManager instance = AssetStorePreviewManager.Instance;
                        instance.m_Aborted++;
                    }
                }
                else
                {
                    if (this.cached.image != null)
                    {
                        UnityEngine.Object.DestroyImmediate(this.cached.image);
                    }
                    this.cached.image = c.texture;
                    AssetStorePreviewManager.s_NeedsRepaint = true;
                    AssetStorePreviewManager manager2 = AssetStorePreviewManager.Instance;
                    manager2.m_Success++;
                }
            }
        }

        public class CachedAssetStoreImage
        {
            internal AsyncHTTPClient client;
            public Texture2D image;
            private const double kFadeTime = 0.5;
            public string label;
            public double lastFetched;
            public double lastUsed;
            public int requestedWidth;

            public Color color =>
                Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), Mathf.Min(1f, (float) ((EditorApplication.timeSinceStartup - this.lastFetched) / 0.5)));
        }
    }
}

