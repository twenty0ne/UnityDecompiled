﻿namespace UnityEditor
{
    using System;
    using UnityEngine;

    [Serializable]
    internal class ZoomableArea
    {
        private int horizontalScrollbarID;
        private const float kMaxScale = 100000f;
        private const float kMinHeight = 0.1f;
        private const float kMinScale = 1E-05f;
        private const float kMinWidth = 0.1f;
        [SerializeField]
        private Rect m_DrawArea;
        [SerializeField]
        private bool m_EnableMouseInput;
        [SerializeField]
        private bool m_EnableSliderZoom;
        [SerializeField]
        private bool m_HAllowExceedBaseRangeMax;
        [SerializeField]
        private bool m_HAllowExceedBaseRangeMin;
        [SerializeField]
        private float m_HBaseRangeMax;
        [SerializeField]
        private float m_HBaseRangeMin;
        [SerializeField]
        private bool m_HRangeLocked;
        private float m_HScaleMax;
        private float m_HScaleMin;
        [SerializeField]
        private bool m_HSlider;
        [SerializeField]
        private bool m_IgnoreScrollWheelUntilClicked;
        [SerializeField]
        private Rect m_LastShownAreaInsideMargins;
        [SerializeField]
        private float m_MarginBottom;
        [SerializeField]
        private float m_MarginLeft;
        [SerializeField]
        private float m_MarginRight;
        [SerializeField]
        private float m_MarginTop;
        [SerializeField]
        private bool m_MinimalGUI;
        private static Vector2 m_MouseDownPosition = new Vector2(-1000000f, -1000000f);
        [SerializeField]
        internal Vector2 m_Scale;
        [SerializeField]
        private bool m_ScaleWithWindow;
        private Styles m_Styles;
        [SerializeField]
        internal Vector2 m_Translation;
        public bool m_UniformScale;
        [SerializeField]
        private YDirection m_UpDirection;
        [SerializeField]
        private bool m_VAllowExceedBaseRangeMax;
        [SerializeField]
        private bool m_VAllowExceedBaseRangeMin;
        [SerializeField]
        private float m_VBaseRangeMax;
        [SerializeField]
        private float m_VBaseRangeMin;
        [SerializeField]
        private bool m_VRangeLocked;
        private float m_VScaleMax;
        private float m_VScaleMin;
        [SerializeField]
        private bool m_VSlider;
        private int verticalScrollbarID;
        private static int zoomableAreaHash = "ZoomableArea".GetHashCode();

        public ZoomableArea()
        {
            this.m_HBaseRangeMin = 0f;
            this.m_HBaseRangeMax = 1f;
            this.m_VBaseRangeMin = 0f;
            this.m_VBaseRangeMax = 1f;
            this.m_HAllowExceedBaseRangeMin = true;
            this.m_HAllowExceedBaseRangeMax = true;
            this.m_VAllowExceedBaseRangeMin = true;
            this.m_VAllowExceedBaseRangeMax = true;
            this.m_HScaleMin = 1E-05f;
            this.m_HScaleMax = 100000f;
            this.m_VScaleMin = 1E-05f;
            this.m_VScaleMax = 100000f;
            this.m_ScaleWithWindow = false;
            this.m_HSlider = true;
            this.m_VSlider = true;
            this.m_IgnoreScrollWheelUntilClicked = false;
            this.m_EnableMouseInput = true;
            this.m_EnableSliderZoom = true;
            this.m_UpDirection = YDirection.Positive;
            this.m_DrawArea = new Rect(0f, 0f, 100f, 100f);
            this.m_Scale = new Vector2(1f, -1f);
            this.m_Translation = new Vector2(0f, 0f);
            this.m_LastShownAreaInsideMargins = new Rect(0f, 0f, 100f, 100f);
            this.m_MinimalGUI = false;
        }

        public ZoomableArea(bool minimalGUI)
        {
            this.m_HBaseRangeMin = 0f;
            this.m_HBaseRangeMax = 1f;
            this.m_VBaseRangeMin = 0f;
            this.m_VBaseRangeMax = 1f;
            this.m_HAllowExceedBaseRangeMin = true;
            this.m_HAllowExceedBaseRangeMax = true;
            this.m_VAllowExceedBaseRangeMin = true;
            this.m_VAllowExceedBaseRangeMax = true;
            this.m_HScaleMin = 1E-05f;
            this.m_HScaleMax = 100000f;
            this.m_VScaleMin = 1E-05f;
            this.m_VScaleMax = 100000f;
            this.m_ScaleWithWindow = false;
            this.m_HSlider = true;
            this.m_VSlider = true;
            this.m_IgnoreScrollWheelUntilClicked = false;
            this.m_EnableMouseInput = true;
            this.m_EnableSliderZoom = true;
            this.m_UpDirection = YDirection.Positive;
            this.m_DrawArea = new Rect(0f, 0f, 100f, 100f);
            this.m_Scale = new Vector2(1f, -1f);
            this.m_Translation = new Vector2(0f, 0f);
            this.m_LastShownAreaInsideMargins = new Rect(0f, 0f, 100f, 100f);
            this.m_MinimalGUI = minimalGUI;
        }

        public ZoomableArea(bool minimalGUI, bool enableSliderZoom)
        {
            this.m_HBaseRangeMin = 0f;
            this.m_HBaseRangeMax = 1f;
            this.m_VBaseRangeMin = 0f;
            this.m_VBaseRangeMax = 1f;
            this.m_HAllowExceedBaseRangeMin = true;
            this.m_HAllowExceedBaseRangeMax = true;
            this.m_VAllowExceedBaseRangeMin = true;
            this.m_VAllowExceedBaseRangeMax = true;
            this.m_HScaleMin = 1E-05f;
            this.m_HScaleMax = 100000f;
            this.m_VScaleMin = 1E-05f;
            this.m_VScaleMax = 100000f;
            this.m_ScaleWithWindow = false;
            this.m_HSlider = true;
            this.m_VSlider = true;
            this.m_IgnoreScrollWheelUntilClicked = false;
            this.m_EnableMouseInput = true;
            this.m_EnableSliderZoom = true;
            this.m_UpDirection = YDirection.Positive;
            this.m_DrawArea = new Rect(0f, 0f, 100f, 100f);
            this.m_Scale = new Vector2(1f, -1f);
            this.m_Translation = new Vector2(0f, 0f);
            this.m_LastShownAreaInsideMargins = new Rect(0f, 0f, 100f, 100f);
            this.m_MinimalGUI = minimalGUI;
            this.m_EnableSliderZoom = enableSliderZoom;
        }

        public void BeginViewGUI()
        {
            if (this.styles.horizontalScrollbar == null)
            {
                this.styles.InitGUIStyles(this.m_MinimalGUI, this.m_EnableSliderZoom);
            }
            if (this.enableMouseInput)
            {
                this.HandleZoomAndPanEvents(this.m_DrawArea);
            }
            this.horizontalScrollbarID = GUIUtility.GetControlID(EditorGUIExt.s_MinMaxSliderHash, FocusType.Passive);
            this.verticalScrollbarID = GUIUtility.GetControlID(EditorGUIExt.s_MinMaxSliderHash, FocusType.Passive);
            if (!this.m_MinimalGUI || (Event.current.type != EventType.Repaint))
            {
                this.SliderGUI();
            }
        }

        public Vector2 DrawingToViewTransformPoint(Vector2 lhs) => 
            new Vector2((lhs.x * this.m_Scale.x) + this.m_Translation.x, (lhs.y * this.m_Scale.y) + this.m_Translation.y);

        public Vector3 DrawingToViewTransformPoint(Vector3 lhs) => 
            new Vector3((lhs.x * this.m_Scale.x) + this.m_Translation.x, (lhs.y * this.m_Scale.y) + this.m_Translation.y, 0f);

        public Vector2 DrawingToViewTransformVector(Vector2 lhs) => 
            new Vector2(lhs.x * this.m_Scale.x, lhs.y * this.m_Scale.y);

        public Vector3 DrawingToViewTransformVector(Vector3 lhs) => 
            new Vector3(lhs.x * this.m_Scale.x, lhs.y * this.m_Scale.y, 0f);

        public void EndViewGUI()
        {
            if (this.m_MinimalGUI && (Event.current.type == EventType.Repaint))
            {
                this.SliderGUI();
            }
        }

        public void EnforceScaleAndRange()
        {
            float a = this.rect.width / this.m_HScaleMin;
            float num2 = this.rect.height / this.m_VScaleMin;
            if ((this.hRangeMax != float.PositiveInfinity) && (this.hRangeMin != float.NegativeInfinity))
            {
                a = Mathf.Min(a, this.hRangeMax - this.hRangeMin);
            }
            if ((this.vRangeMax != float.PositiveInfinity) && (this.vRangeMin != float.NegativeInfinity))
            {
                num2 = Mathf.Min(num2, this.vRangeMax - this.vRangeMin);
            }
            Rect lastShownAreaInsideMargins = this.m_LastShownAreaInsideMargins;
            Rect shownAreaInsideMargins = this.shownAreaInsideMargins;
            if (shownAreaInsideMargins != lastShownAreaInsideMargins)
            {
                float num3 = 1E-05f;
                if (shownAreaInsideMargins.width < (lastShownAreaInsideMargins.width - num3))
                {
                    float t = Mathf.InverseLerp(lastShownAreaInsideMargins.width, shownAreaInsideMargins.width, this.rect.width / this.m_HScaleMax);
                    float x = Mathf.Lerp(lastShownAreaInsideMargins.x, shownAreaInsideMargins.x, t);
                    float width = Mathf.Lerp(lastShownAreaInsideMargins.width, shownAreaInsideMargins.width, t);
                    shownAreaInsideMargins = new Rect(x, shownAreaInsideMargins.y, width, shownAreaInsideMargins.height);
                }
                if (shownAreaInsideMargins.height < (lastShownAreaInsideMargins.height - num3))
                {
                    float num5 = Mathf.InverseLerp(lastShownAreaInsideMargins.height, shownAreaInsideMargins.height, this.rect.height / this.m_VScaleMax);
                    float y = Mathf.Lerp(lastShownAreaInsideMargins.y, shownAreaInsideMargins.y, num5);
                    shownAreaInsideMargins = new Rect(shownAreaInsideMargins.x, y, shownAreaInsideMargins.width, Mathf.Lerp(lastShownAreaInsideMargins.height, shownAreaInsideMargins.height, num5));
                }
                if (shownAreaInsideMargins.width > (lastShownAreaInsideMargins.width + num3))
                {
                    float num6 = Mathf.InverseLerp(lastShownAreaInsideMargins.width, shownAreaInsideMargins.width, a);
                    float introduced16 = Mathf.Lerp(lastShownAreaInsideMargins.x, shownAreaInsideMargins.x, num6);
                    float introduced17 = Mathf.Lerp(lastShownAreaInsideMargins.width, shownAreaInsideMargins.width, num6);
                    shownAreaInsideMargins = new Rect(introduced16, shownAreaInsideMargins.y, introduced17, shownAreaInsideMargins.height);
                }
                if (shownAreaInsideMargins.height > (lastShownAreaInsideMargins.height + num3))
                {
                    float num7 = Mathf.InverseLerp(lastShownAreaInsideMargins.height, shownAreaInsideMargins.height, num2);
                    float introduced18 = Mathf.Lerp(lastShownAreaInsideMargins.y, shownAreaInsideMargins.y, num7);
                    shownAreaInsideMargins = new Rect(shownAreaInsideMargins.x, introduced18, shownAreaInsideMargins.width, Mathf.Lerp(lastShownAreaInsideMargins.height, shownAreaInsideMargins.height, num7));
                }
                if (shownAreaInsideMargins.xMin < this.hRangeMin)
                {
                    shownAreaInsideMargins.x = this.hRangeMin;
                }
                if (shownAreaInsideMargins.xMax > this.hRangeMax)
                {
                    shownAreaInsideMargins.x = this.hRangeMax - shownAreaInsideMargins.width;
                }
                if (shownAreaInsideMargins.yMin < this.vRangeMin)
                {
                    shownAreaInsideMargins.y = this.vRangeMin;
                }
                if (shownAreaInsideMargins.yMax > this.vRangeMax)
                {
                    shownAreaInsideMargins.y = this.vRangeMax - shownAreaInsideMargins.height;
                }
                this.shownAreaInsideMarginsInternal = shownAreaInsideMargins;
                this.m_LastShownAreaInsideMargins = shownAreaInsideMargins;
            }
        }

        public void HandleZoomAndPanEvents(Rect area)
        {
            GUILayout.BeginArea(area);
            area.x = 0f;
            area.y = 0f;
            int controlID = GUIUtility.GetControlID(zoomableAreaHash, FocusType.Passive, area);
            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    if (area.Contains(Event.current.mousePosition))
                    {
                        GUIUtility.keyboardControl = controlID;
                        if (this.IsZoomEvent() || this.IsPanEvent())
                        {
                            GUIUtility.hotControl = controlID;
                            m_MouseDownPosition = this.mousePositionInDrawing;
                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        m_MouseDownPosition = new Vector2(-1000000f, -1000000f);
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlID)
                    {
                        if (this.IsZoomEvent())
                        {
                            this.HandleZoomEvent(m_MouseDownPosition, false);
                            Event.current.Use();
                        }
                        else if (this.IsPanEvent())
                        {
                            this.Pan();
                            Event.current.Use();
                        }
                        break;
                    }
                    break;

                case EventType.ScrollWheel:
                    if (area.Contains(Event.current.mousePosition))
                    {
                        if (!this.m_IgnoreScrollWheelUntilClicked || (GUIUtility.keyboardControl == controlID))
                        {
                            this.HandleZoomEvent(this.mousePositionInDrawing, true);
                            Event.current.Use();
                        }
                        break;
                    }
                    break;
            }
            GUILayout.EndArea();
        }

        private void HandleZoomEvent(Vector2 zoomAround, bool scrollwhell)
        {
            float num = Event.current.delta.x + Event.current.delta.y;
            if (scrollwhell)
            {
                num = -num;
            }
            float num2 = Mathf.Max((float) 0.01f, (float) (1f + (num * 0.01f)));
            this.SetScaleFocused(zoomAround, (Vector2) (num2 * this.m_Scale), Event.current.shift, EditorGUI.actionKey);
        }

        private bool IsPanEvent() => 
            (((Event.current.button == 0) && Event.current.alt) || ((Event.current.button == 2) && !Event.current.command));

        private bool IsZoomEvent() => 
            ((Event.current.button == 1) && Event.current.alt);

        public Vector2 NormalizeInViewSpace(Vector2 vec)
        {
            vec = Vector2.Scale(vec, this.m_Scale);
            vec = (Vector2) (vec / vec.magnitude);
            return Vector2.Scale(vec, new Vector2(1f / this.m_Scale.x, 1f / this.m_Scale.y));
        }

        private void Pan()
        {
            if (!this.m_HRangeLocked)
            {
                this.m_Translation.x += Event.current.delta.x;
            }
            if (!this.m_VRangeLocked)
            {
                this.m_Translation.y += Event.current.delta.y;
            }
            this.EnforceScaleAndRange();
        }

        public float PixelDeltaToTime(Rect rect) => 
            (this.shownArea.width / rect.width);

        public float PixelToTime(float pixelX, Rect rect) => 
            ((((pixelX - rect.x) * this.shownArea.width) / rect.width) + this.shownArea.x);

        private void SetAllowExceed(ref float rangeEnd, ref bool allowExceed, float value)
        {
            if ((value == float.NegativeInfinity) || (value == float.PositiveInfinity))
            {
                rangeEnd = (value != float.NegativeInfinity) ? ((float) 1) : ((float) 0);
                allowExceed = true;
            }
            else
            {
                rangeEnd = value;
                allowExceed = false;
            }
        }

        internal void SetDrawRectHack(Rect r)
        {
            this.m_DrawArea = r;
        }

        public void SetScaleFocused(Vector2 focalPoint, Vector2 newScale)
        {
            this.SetScaleFocused(focalPoint, newScale, false, false);
        }

        public void SetScaleFocused(Vector2 focalPoint, Vector2 newScale, bool lockHorizontal, bool lockVertical)
        {
            if (!this.m_HRangeLocked && !lockHorizontal)
            {
                this.m_Translation.x -= focalPoint.x * (newScale.x - this.m_Scale.x);
                this.m_Scale.x = newScale.x;
            }
            if (!this.m_VRangeLocked && !lockVertical)
            {
                this.m_Translation.y -= focalPoint.y * (newScale.y - this.m_Scale.y);
                this.m_Scale.y = newScale.y;
            }
            this.EnforceScaleAndRange();
        }

        public void SetShownHRange(float min, float max)
        {
            float num = max - min;
            if (num < 0.1f)
            {
                num = 0.1f;
            }
            this.m_Scale.x = this.drawRect.width / num;
            this.m_Translation.x = -min * this.m_Scale.x;
            this.EnforceScaleAndRange();
        }

        public void SetShownHRangeInsideMargins(float min, float max)
        {
            float num = (this.drawRect.width - this.leftmargin) - this.rightmargin;
            if (num < 0.1f)
            {
                num = 0.1f;
            }
            float num2 = max - min;
            if (num2 < 0.1f)
            {
                num2 = 0.1f;
            }
            this.m_Scale.x = num / num2;
            this.m_Translation.x = (-min * this.m_Scale.x) + this.leftmargin;
            this.EnforceScaleAndRange();
        }

        public void SetShownVRange(float min, float max)
        {
            if (this.m_UpDirection == YDirection.Positive)
            {
                this.m_Scale.y = -this.drawRect.height / (max - min);
                this.m_Translation.y = this.drawRect.height - (min * this.m_Scale.y);
            }
            else
            {
                this.m_Scale.y = this.drawRect.height / (max - min);
                this.m_Translation.y = -min * this.m_Scale.y;
            }
            this.EnforceScaleAndRange();
        }

        public void SetShownVRangeInsideMargins(float min, float max)
        {
            if (this.m_UpDirection == YDirection.Positive)
            {
                this.m_Scale.y = -((this.drawRect.height - this.topmargin) - this.bottommargin) / (max - min);
                this.m_Translation.y = (this.drawRect.height - (min * this.m_Scale.y)) - this.topmargin;
            }
            else
            {
                this.m_Scale.y = ((this.drawRect.height - this.topmargin) - this.bottommargin) / (max - min);
                this.m_Translation.y = (-min * this.m_Scale.y) - this.bottommargin;
            }
            this.EnforceScaleAndRange();
        }

        public void SetTransform(Vector2 newTranslation, Vector2 newScale)
        {
            this.m_Scale = newScale;
            this.m_Translation = newTranslation;
            this.EnforceScaleAndRange();
        }

        private void SliderGUI()
        {
            if (this.m_HSlider || this.m_VSlider)
            {
                using (new EditorGUI.DisabledScope(!this.enableMouseInput))
                {
                    float num;
                    float num2;
                    Bounds drawingBounds = this.drawingBounds;
                    Rect shownAreaInsideMargins = this.shownAreaInsideMargins;
                    float num3 = this.styles.sliderWidth - this.styles.visualSliderWidth;
                    float num4 = (!this.vSlider || !this.hSlider) ? 0f : num3;
                    Vector2 scale = this.m_Scale;
                    if (this.m_HSlider)
                    {
                        Rect position = new Rect(this.drawRect.x + 1f, this.drawRect.yMax - num3, this.drawRect.width - num4, this.styles.sliderWidth);
                        float width = shownAreaInsideMargins.width;
                        float xMin = shownAreaInsideMargins.xMin;
                        if (this.m_EnableSliderZoom)
                        {
                            EditorGUIExt.MinMaxScroller(position, this.horizontalScrollbarID, ref xMin, ref width, drawingBounds.min.x, drawingBounds.max.x, float.NegativeInfinity, float.PositiveInfinity, this.styles.horizontalScrollbar, this.styles.horizontalMinMaxScrollbarThumb, this.styles.horizontalScrollbarLeftButton, this.styles.horizontalScrollbarRightButton, true);
                        }
                        else
                        {
                            xMin = GUI.Scroller(position, xMin, width, drawingBounds.min.x, drawingBounds.max.x, this.styles.horizontalScrollbar, this.styles.horizontalMinMaxScrollbarThumb, this.styles.horizontalScrollbarLeftButton, this.styles.horizontalScrollbarRightButton, true);
                        }
                        num = xMin;
                        num2 = xMin + width;
                        if (num > shownAreaInsideMargins.xMin)
                        {
                            num = Mathf.Min(num, num2 - (this.rect.width / this.m_HScaleMax));
                        }
                        if (num2 < shownAreaInsideMargins.xMax)
                        {
                            num2 = Mathf.Max(num2, num + (this.rect.width / this.m_HScaleMax));
                        }
                        this.SetShownHRangeInsideMargins(num, num2);
                    }
                    if (this.m_VSlider)
                    {
                        if (this.m_UpDirection == YDirection.Positive)
                        {
                            Rect rect8 = new Rect(this.drawRect.xMax - num3, this.drawRect.y, this.styles.sliderWidth, this.drawRect.height - num4);
                            float height = shownAreaInsideMargins.height;
                            float num8 = -shownAreaInsideMargins.yMax;
                            if (this.m_EnableSliderZoom)
                            {
                                EditorGUIExt.MinMaxScroller(rect8, this.verticalScrollbarID, ref num8, ref height, -drawingBounds.max.y, -drawingBounds.min.y, float.NegativeInfinity, float.PositiveInfinity, this.styles.verticalScrollbar, this.styles.verticalMinMaxScrollbarThumb, this.styles.verticalScrollbarUpButton, this.styles.verticalScrollbarDownButton, false);
                            }
                            else
                            {
                                num8 = GUI.Scroller(rect8, num8, height, -drawingBounds.max.y, -drawingBounds.min.y, this.styles.verticalScrollbar, this.styles.verticalMinMaxScrollbarThumb, this.styles.verticalScrollbarUpButton, this.styles.verticalScrollbarDownButton, false);
                            }
                            num = -(num8 + height);
                            num2 = -num8;
                            if (num > shownAreaInsideMargins.yMin)
                            {
                                num = Mathf.Min(num, num2 - (this.rect.height / this.m_VScaleMax));
                            }
                            if (num2 < shownAreaInsideMargins.yMax)
                            {
                                num2 = Mathf.Max(num2, num + (this.rect.height / this.m_VScaleMax));
                            }
                            this.SetShownVRangeInsideMargins(num, num2);
                        }
                        else
                        {
                            Rect rect14 = new Rect(this.drawRect.xMax - num3, this.drawRect.y, this.styles.sliderWidth, this.drawRect.height - num4);
                            float size = shownAreaInsideMargins.height;
                            float yMin = shownAreaInsideMargins.yMin;
                            if (this.m_EnableSliderZoom)
                            {
                                EditorGUIExt.MinMaxScroller(rect14, this.verticalScrollbarID, ref yMin, ref size, drawingBounds.min.y, drawingBounds.max.y, float.NegativeInfinity, float.PositiveInfinity, this.styles.verticalScrollbar, this.styles.verticalMinMaxScrollbarThumb, this.styles.verticalScrollbarUpButton, this.styles.verticalScrollbarDownButton, false);
                            }
                            else
                            {
                                yMin = GUI.Scroller(rect14, yMin, size, drawingBounds.min.y, drawingBounds.max.y, this.styles.verticalScrollbar, this.styles.verticalMinMaxScrollbarThumb, this.styles.verticalScrollbarUpButton, this.styles.verticalScrollbarDownButton, false);
                            }
                            num = yMin;
                            num2 = yMin + size;
                            if (num > shownAreaInsideMargins.yMin)
                            {
                                num = Mathf.Min(num, num2 - (this.rect.height / this.m_VScaleMax));
                            }
                            if (num2 < shownAreaInsideMargins.yMax)
                            {
                                num2 = Mathf.Max(num2, num + (this.rect.height / this.m_VScaleMax));
                            }
                            this.SetShownVRangeInsideMargins(num, num2);
                        }
                    }
                    if (this.uniformScale)
                    {
                        float num11 = this.drawRect.width / this.drawRect.height;
                        scale -= this.m_Scale;
                        Vector2 vector14 = new Vector2(-scale.y * num11, -scale.x / num11);
                        this.m_Scale -= vector14;
                        this.m_Translation.x -= scale.y / 2f;
                        this.m_Translation.y -= scale.x / 2f;
                        this.EnforceScaleAndRange();
                    }
                }
            }
        }

        public float TimeToPixel(float time, Rect rect) => 
            ((((time - this.shownArea.x) / this.shownArea.width) * rect.width) + rect.x);

        public Vector2 ViewToDrawingTransformPoint(Vector2 lhs) => 
            new Vector2((lhs.x - this.m_Translation.x) / this.m_Scale.x, (lhs.y - this.m_Translation.y) / this.m_Scale.y);

        public Vector3 ViewToDrawingTransformPoint(Vector3 lhs) => 
            new Vector3((lhs.x - this.m_Translation.x) / this.m_Scale.x, (lhs.y - this.m_Translation.y) / this.m_Scale.y, 0f);

        public Vector2 ViewToDrawingTransformVector(Vector2 lhs) => 
            new Vector2(lhs.x / this.m_Scale.x, lhs.y / this.m_Scale.y);

        public Vector3 ViewToDrawingTransformVector(Vector3 lhs) => 
            new Vector3(lhs.x / this.m_Scale.x, lhs.y / this.m_Scale.y, 0f);

        public float bottommargin
        {
            get => 
                this.m_MarginBottom;
            set
            {
                this.m_MarginBottom = value;
            }
        }

        public virtual Bounds drawingBounds =>
            new Bounds(new Vector3((this.hBaseRangeMin + this.hBaseRangeMax) * 0.5f, (this.vBaseRangeMin + this.vBaseRangeMax) * 0.5f, 0f), new Vector3(this.hBaseRangeMax - this.hBaseRangeMin, this.vBaseRangeMax - this.vBaseRangeMin, 1f));

        public Matrix4x4 drawingToViewMatrix =>
            Matrix4x4.TRS((Vector3) this.m_Translation, Quaternion.identity, new Vector3(this.m_Scale.x, this.m_Scale.y, 1f));

        public Rect drawRect =>
            this.m_DrawArea;

        public bool enableMouseInput
        {
            get => 
                this.m_EnableMouseInput;
            set
            {
                this.m_EnableMouseInput = value;
            }
        }

        public bool hAllowExceedBaseRangeMax
        {
            get => 
                this.m_HAllowExceedBaseRangeMax;
            set
            {
                this.m_HAllowExceedBaseRangeMax = value;
            }
        }

        public bool hAllowExceedBaseRangeMin
        {
            get => 
                this.m_HAllowExceedBaseRangeMin;
            set
            {
                this.m_HAllowExceedBaseRangeMin = value;
            }
        }

        public float hBaseRangeMax
        {
            get => 
                this.m_HBaseRangeMax;
            set
            {
                this.m_HBaseRangeMax = value;
            }
        }

        public float hBaseRangeMin
        {
            get => 
                this.m_HBaseRangeMin;
            set
            {
                this.m_HBaseRangeMin = value;
            }
        }

        public bool hRangeLocked
        {
            get => 
                this.m_HRangeLocked;
            set
            {
                this.m_HRangeLocked = value;
            }
        }

        public float hRangeMax
        {
            get => 
                (!this.hAllowExceedBaseRangeMax ? this.hBaseRangeMax : float.PositiveInfinity);
            set
            {
                this.SetAllowExceed(ref this.m_HBaseRangeMax, ref this.m_HAllowExceedBaseRangeMax, value);
            }
        }

        public float hRangeMin
        {
            get => 
                (!this.hAllowExceedBaseRangeMin ? this.hBaseRangeMin : float.NegativeInfinity);
            set
            {
                this.SetAllowExceed(ref this.m_HBaseRangeMin, ref this.m_HAllowExceedBaseRangeMin, value);
            }
        }

        public float hScaleMax
        {
            get => 
                this.m_HScaleMax;
            set
            {
                this.m_HScaleMax = Mathf.Clamp(value, 1E-05f, 100000f);
            }
        }

        public float hScaleMin
        {
            get => 
                this.m_HScaleMin;
            set
            {
                this.m_HScaleMin = Mathf.Clamp(value, 1E-05f, 100000f);
            }
        }

        public bool hSlider
        {
            get => 
                this.m_HSlider;
            set
            {
                Rect rect = this.rect;
                this.m_HSlider = value;
                this.rect = rect;
            }
        }

        public bool ignoreScrollWheelUntilClicked
        {
            get => 
                this.m_IgnoreScrollWheelUntilClicked;
            set
            {
                this.m_IgnoreScrollWheelUntilClicked = value;
            }
        }

        public float leftmargin
        {
            get => 
                this.m_MarginLeft;
            set
            {
                this.m_MarginLeft = value;
            }
        }

        public float margin
        {
            set
            {
                this.m_MarginLeft = this.m_MarginRight = this.m_MarginTop = this.m_MarginBottom = value;
            }
        }

        public Vector2 mousePositionInDrawing =>
            this.ViewToDrawingTransformPoint(Event.current.mousePosition);

        public Rect rect
        {
            get => 
                new Rect(this.drawRect.x, this.drawRect.y, this.drawRect.width + (!this.m_VSlider ? 0f : this.styles.visualSliderWidth), this.drawRect.height + (!this.m_HSlider ? 0f : this.styles.visualSliderWidth));
            set
            {
                Rect rect = new Rect(value.x, value.y, value.width - (!this.m_VSlider ? 0f : this.styles.visualSliderWidth), value.height - (!this.m_HSlider ? 0f : this.styles.visualSliderWidth));
                if (rect != this.m_DrawArea)
                {
                    if (this.m_ScaleWithWindow)
                    {
                        this.m_DrawArea = rect;
                        this.shownAreaInsideMargins = this.m_LastShownAreaInsideMargins;
                    }
                    else
                    {
                        this.m_Translation += new Vector2((rect.width - this.m_DrawArea.width) / 2f, (rect.height - this.m_DrawArea.height) / 2f);
                        this.m_DrawArea = rect;
                    }
                }
                this.EnforceScaleAndRange();
            }
        }

        public float rightmargin
        {
            get => 
                this.m_MarginRight;
            set
            {
                this.m_MarginRight = value;
            }
        }

        public Vector2 scale =>
            this.m_Scale;

        public bool scaleWithWindow
        {
            get => 
                this.m_ScaleWithWindow;
            set
            {
                this.m_ScaleWithWindow = value;
            }
        }

        public Rect shownArea
        {
            get
            {
                if (this.m_UpDirection == YDirection.Positive)
                {
                    return new Rect(-this.m_Translation.x / this.m_Scale.x, -(this.m_Translation.y - this.drawRect.height) / this.m_Scale.y, this.drawRect.width / this.m_Scale.x, this.drawRect.height / -this.m_Scale.y);
                }
                return new Rect(-this.m_Translation.x / this.m_Scale.x, -this.m_Translation.y / this.m_Scale.y, this.drawRect.width / this.m_Scale.x, this.drawRect.height / this.m_Scale.y);
            }
            set
            {
                float num = (value.width >= 0.1f) ? value.width : 0.1f;
                float num2 = (value.height >= 0.1f) ? value.height : 0.1f;
                if (this.m_UpDirection == YDirection.Positive)
                {
                    this.m_Scale.x = this.drawRect.width / num;
                    this.m_Scale.y = -this.drawRect.height / num2;
                    this.m_Translation.x = -value.x * this.m_Scale.x;
                    this.m_Translation.y = this.drawRect.height - (value.y * this.m_Scale.y);
                }
                else
                {
                    this.m_Scale.x = this.drawRect.width / num;
                    this.m_Scale.y = this.drawRect.height / num2;
                    this.m_Translation.x = -value.x * this.m_Scale.x;
                    this.m_Translation.y = -value.y * this.m_Scale.y;
                }
                this.EnforceScaleAndRange();
            }
        }

        public Rect shownAreaInsideMargins
        {
            get => 
                this.shownAreaInsideMarginsInternal;
            set
            {
                this.shownAreaInsideMarginsInternal = value;
                this.EnforceScaleAndRange();
            }
        }

        private Rect shownAreaInsideMarginsInternal
        {
            get
            {
                float num = this.leftmargin / this.m_Scale.x;
                float num2 = this.rightmargin / this.m_Scale.x;
                float num3 = this.topmargin / this.m_Scale.y;
                float num4 = this.bottommargin / this.m_Scale.y;
                Rect shownArea = this.shownArea;
                shownArea.x += num;
                shownArea.y -= num3;
                shownArea.width -= num + num2;
                shownArea.height += num3 + num4;
                return shownArea;
            }
            set
            {
                float num = (value.width >= 0.1f) ? value.width : 0.1f;
                float num2 = (value.height >= 0.1f) ? value.height : 0.1f;
                float num3 = (this.drawRect.width - this.leftmargin) - this.rightmargin;
                if (num3 < 0.1f)
                {
                    num3 = 0.1f;
                }
                float num4 = (this.drawRect.height - this.topmargin) - this.bottommargin;
                if (num4 < 0.1f)
                {
                    num4 = 0.1f;
                }
                if (this.m_UpDirection == YDirection.Positive)
                {
                    this.m_Scale.x = num3 / num;
                    this.m_Scale.y = -num4 / num2;
                    this.m_Translation.x = (-value.x * this.m_Scale.x) + this.leftmargin;
                    this.m_Translation.y = (this.drawRect.height - (value.y * this.m_Scale.y)) - this.topmargin;
                }
                else
                {
                    this.m_Scale.x = num3 / num;
                    this.m_Scale.y = num4 / num2;
                    this.m_Translation.x = (-value.x * this.m_Scale.x) + this.leftmargin;
                    this.m_Translation.y = (-value.y * this.m_Scale.y) + this.topmargin;
                }
            }
        }

        private Styles styles
        {
            get
            {
                if (this.m_Styles == null)
                {
                    this.m_Styles = new Styles(this.m_MinimalGUI);
                }
                return this.m_Styles;
            }
        }

        public float topmargin
        {
            get => 
                this.m_MarginTop;
            set
            {
                this.m_MarginTop = value;
            }
        }

        public Vector2 translation =>
            this.m_Translation;

        public bool uniformScale
        {
            get => 
                this.m_UniformScale;
            set
            {
                this.m_UniformScale = value;
            }
        }

        public YDirection upDirection
        {
            get => 
                this.m_UpDirection;
            set
            {
                if (this.m_UpDirection != value)
                {
                    this.m_UpDirection = value;
                    this.m_Scale.y = -this.m_Scale.y;
                }
            }
        }

        public bool vAllowExceedBaseRangeMax
        {
            get => 
                this.m_VAllowExceedBaseRangeMax;
            set
            {
                this.m_VAllowExceedBaseRangeMax = value;
            }
        }

        public bool vAllowExceedBaseRangeMin
        {
            get => 
                this.m_VAllowExceedBaseRangeMin;
            set
            {
                this.m_VAllowExceedBaseRangeMin = value;
            }
        }

        public float vBaseRangeMax
        {
            get => 
                this.m_VBaseRangeMax;
            set
            {
                this.m_VBaseRangeMax = value;
            }
        }

        public float vBaseRangeMin
        {
            get => 
                this.m_VBaseRangeMin;
            set
            {
                this.m_VBaseRangeMin = value;
            }
        }

        public bool vRangeLocked
        {
            get => 
                this.m_VRangeLocked;
            set
            {
                this.m_VRangeLocked = value;
            }
        }

        public float vRangeMax
        {
            get => 
                (!this.vAllowExceedBaseRangeMax ? this.vBaseRangeMax : float.PositiveInfinity);
            set
            {
                this.SetAllowExceed(ref this.m_VBaseRangeMax, ref this.m_VAllowExceedBaseRangeMax, value);
            }
        }

        public float vRangeMin
        {
            get => 
                (!this.vAllowExceedBaseRangeMin ? this.vBaseRangeMin : float.NegativeInfinity);
            set
            {
                this.SetAllowExceed(ref this.m_VBaseRangeMin, ref this.m_VAllowExceedBaseRangeMin, value);
            }
        }

        public float vScaleMax
        {
            get => 
                this.m_VScaleMax;
            set
            {
                this.m_VScaleMax = Mathf.Clamp(value, 1E-05f, 100000f);
            }
        }

        public float vScaleMin
        {
            get => 
                this.m_VScaleMin;
            set
            {
                this.m_VScaleMin = Mathf.Clamp(value, 1E-05f, 100000f);
            }
        }

        public bool vSlider
        {
            get => 
                this.m_VSlider;
            set
            {
                Rect rect = this.rect;
                this.m_VSlider = value;
                this.rect = rect;
            }
        }

        public class Styles
        {
            public GUIStyle horizontalMinMaxScrollbarThumb;
            public GUIStyle horizontalScrollbar;
            public GUIStyle horizontalScrollbarLeftButton;
            public GUIStyle horizontalScrollbarRightButton;
            public float sliderWidth;
            public GUIStyle verticalMinMaxScrollbarThumb;
            public GUIStyle verticalScrollbar;
            public GUIStyle verticalScrollbarDownButton;
            public GUIStyle verticalScrollbarUpButton;
            public float visualSliderWidth;

            public Styles(bool minimalGUI)
            {
                if (minimalGUI)
                {
                    this.visualSliderWidth = 0f;
                    this.sliderWidth = 15f;
                }
                else
                {
                    this.visualSliderWidth = 15f;
                    this.sliderWidth = 15f;
                }
            }

            public void InitGUIStyles(bool minimalGUI, bool enableSliderZoom)
            {
                if (minimalGUI)
                {
                    this.horizontalMinMaxScrollbarThumb = !enableSliderZoom ? "MiniSliderhorizontal" : "MiniMinMaxSliderHorizontal";
                    this.horizontalScrollbarLeftButton = GUIStyle.none;
                    this.horizontalScrollbarRightButton = GUIStyle.none;
                    this.horizontalScrollbar = GUIStyle.none;
                    this.verticalMinMaxScrollbarThumb = !enableSliderZoom ? "MiniSliderVertical" : "MiniMinMaxSlidervertical";
                    this.verticalScrollbarUpButton = GUIStyle.none;
                    this.verticalScrollbarDownButton = GUIStyle.none;
                    this.verticalScrollbar = GUIStyle.none;
                }
                else
                {
                    this.horizontalMinMaxScrollbarThumb = !enableSliderZoom ? "horizontalscrollbarthumb" : "horizontalMinMaxScrollbarThumb";
                    this.horizontalScrollbarLeftButton = "horizontalScrollbarLeftbutton";
                    this.horizontalScrollbarRightButton = "horizontalScrollbarRightbutton";
                    this.horizontalScrollbar = GUI.skin.horizontalScrollbar;
                    this.verticalMinMaxScrollbarThumb = !enableSliderZoom ? "verticalscrollbarthumb" : "verticalMinMaxScrollbarThumb";
                    this.verticalScrollbarUpButton = "verticalScrollbarUpbutton";
                    this.verticalScrollbarDownButton = "verticalScrollbarDownbutton";
                    this.verticalScrollbar = GUI.skin.verticalScrollbar;
                }
            }
        }

        public enum YDirection
        {
            Positive,
            Negative
        }
    }
}

