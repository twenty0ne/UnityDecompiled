﻿namespace UnityEditor
{
    using System;
    using UnityEngine;

    [Serializable]
    internal class GameViewSize
    {
        private const int kMaxBaseTextLength = 40;
        private const int kMaxResolutionOrAspect = 0x1869f;
        private const int kMinAspect = 0;
        private const int kMinResolution = 10;
        [SerializeField]
        private string m_BaseText;
        [NonSerialized]
        private string m_CachedDisplayText;
        [SerializeField]
        private int m_Height;
        [SerializeField]
        private GameViewSizeType m_SizeType;
        [SerializeField]
        private int m_Width;

        public GameViewSize(GameViewSize other)
        {
            this.Set(other);
        }

        public GameViewSize(GameViewSizeType type, int width, int height, string baseText)
        {
            this.sizeType = type;
            this.width = width;
            this.height = height;
            this.baseText = baseText;
        }

        private void Changed()
        {
            this.m_CachedDisplayText = null;
            ScriptableSingleton<GameViewSizes>.instance.Changed();
        }

        private void Clamp()
        {
            int width = this.m_Width;
            int height = this.m_Height;
            int min = 0;
            GameViewSizeType sizeType = this.sizeType;
            if (sizeType == GameViewSizeType.AspectRatio)
            {
                min = 0;
            }
            else if (sizeType == GameViewSizeType.FixedResolution)
            {
                min = 10;
            }
            else
            {
                Debug.LogError("Unhandled enum!");
            }
            this.m_Width = Mathf.Clamp(this.m_Width, min, 0x1869f);
            this.m_Height = Mathf.Clamp(this.m_Height, min, 0x1869f);
            if ((this.m_Width != width) || (this.m_Height != height))
            {
                this.Changed();
            }
        }

        private string ComposeDisplayString()
        {
            if ((this.width == 0) && (this.height == 0))
            {
                return this.baseText;
            }
            if (string.IsNullOrEmpty(this.baseText))
            {
                return this.sizeText;
            }
            return (this.baseText + " (" + this.sizeText + ")");
        }

        public void Set(GameViewSize other)
        {
            this.sizeType = other.sizeType;
            this.width = other.width;
            this.height = other.height;
            this.baseText = other.baseText;
            this.Changed();
        }

        public float aspectRatio
        {
            get
            {
                if (this.height == 0)
                {
                    return 0f;
                }
                return (((float) this.width) / ((float) this.height));
            }
        }

        public string baseText
        {
            get => 
                this.m_BaseText;
            set
            {
                this.m_BaseText = value;
                if (this.m_BaseText.Length > 40)
                {
                    this.m_BaseText = this.m_BaseText.Substring(0, 40);
                }
                this.Changed();
            }
        }

        public string displayText
        {
            get
            {
                string cachedDisplayText;
                if (this.m_CachedDisplayText != null)
                {
                    cachedDisplayText = this.m_CachedDisplayText;
                }
                else
                {
                    cachedDisplayText = this.m_CachedDisplayText = this.ComposeDisplayString();
                }
                return cachedDisplayText;
            }
        }

        public int height
        {
            get => 
                this.m_Height;
            set
            {
                this.m_Height = value;
                this.Clamp();
                this.Changed();
            }
        }

        public bool isFreeAspectRatio =>
            (this.width == 0);

        private string sizeText
        {
            get
            {
                if (this.sizeType == GameViewSizeType.AspectRatio)
                {
                    return $"{this.width}:{this.height}";
                }
                if (this.sizeType == GameViewSizeType.FixedResolution)
                {
                    return $"{this.width}x{this.height}";
                }
                Debug.LogError("Unhandled game view size type");
                return "";
            }
        }

        public GameViewSizeType sizeType
        {
            get => 
                this.m_SizeType;
            set
            {
                this.m_SizeType = value;
                this.Clamp();
                this.Changed();
            }
        }

        public int width
        {
            get => 
                this.m_Width;
            set
            {
                this.m_Width = value;
                this.Clamp();
                this.Changed();
            }
        }
    }
}

