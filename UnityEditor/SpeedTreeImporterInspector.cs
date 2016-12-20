﻿namespace UnityEditor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEditor.AnimatedValues;
    using UnityEditor.VersionControl;
    using UnityEngine;
    using UnityEngine.Events;

    [CustomEditor(typeof(SpeedTreeImporter)), CanEditMultipleObjects]
    internal class SpeedTreeImporterInspector : AssetImporterInspector
    {
        [CompilerGenerated]
        private static Func<SpeedTreeImporter, bool> <>f__am$cache0;
        [CompilerGenerated]
        private static Func<SpeedTreeImporter, string> <>f__am$cache1;
        [CompilerGenerated]
        private static Func<string, string> <>f__am$cache2;
        [CompilerGenerated]
        private static Func<SpeedTreeImporter, int> <>f__am$cache3;
        [CompilerGenerated]
        private static Func<string, GUIContent> <>f__am$cache4;
        [CompilerGenerated]
        private static Func<float, string> <>f__am$cache5;
        [CompilerGenerated]
        private static Func<LODGroupGUI.LODInfo, bool> <>f__am$cache6;
        [CompilerGenerated]
        private static Func<LODGroupGUI.LODInfo, int> <>f__am$cache7;
        [CompilerGenerated]
        private static Func<LODGroupGUI.LODInfo, bool> <>f__am$cache8;
        [CompilerGenerated]
        private static Func<LODGroupGUI.LODInfo, int> <>f__am$cache9;
        private const float kFeetToMetersRatio = 0.3048f;
        private SerializedProperty m_AlphaTestRef;
        private SerializedProperty m_AnimateCrossFading;
        private SerializedProperty m_BillboardTransitionCrossFadeWidth;
        private SerializedProperty m_EnableSmoothLOD;
        private SerializedProperty m_FadeOutWidth;
        private SerializedProperty m_HueVariation;
        private SerializedProperty m_LODSettings;
        private readonly int m_LODSliderId = "LODSliderIDHash".GetHashCode();
        private SerializedProperty m_MainColor;
        private SerializedProperty m_ScaleFactor;
        private int m_SelectedLODRange = 0;
        private int m_SelectedLODSlider = -1;
        private readonly AnimBool m_ShowCrossFadeWidthOptions = new AnimBool();
        private readonly AnimBool m_ShowSmoothLODOptions = new AnimBool();

        protected override bool ApplyRevertGUIButtons()
        {
            bool flag;
            using (new EditorGUI.DisabledScope(!this.HasModified()))
            {
                base.RevertButton();
                flag = base.ApplyButton("Apply Prefab");
            }
            bool upgradeMaterials = this.upgradeMaterials;
            GUIContent content = (!this.HasModified() && !upgradeMaterials) ? Styles.Regenerate : Styles.ApplyAndGenerate;
            if (!GUILayout.Button(content, new GUILayoutOption[0]))
            {
                return flag;
            }
            bool flag3 = this.HasModified();
            if (flag3)
            {
                this.Apply();
            }
            if (upgradeMaterials)
            {
                foreach (SpeedTreeImporter importer in this.importers)
                {
                    importer.SetMaterialVersionToCurrent();
                }
            }
            this.GenerateMaterials();
            if (!flag3 && !upgradeMaterials)
            {
                return flag;
            }
            base.ApplyAndImport();
            return true;
        }

        public bool CanUnifyLODConfig()
        {
            return (!base.serializedObject.FindProperty("m_HasBillboard").hasMultipleDifferentValues && !this.m_LODSettings.FindPropertyRelative("Array.size").hasMultipleDifferentValues);
        }

        private void DrawLODLevelSlider(Rect sliderPosition, List<LODGroupGUI.LODInfo> lods)
        {
            int controlID = GUIUtility.GetControlID(this.m_LODSliderId, FocusType.Passive);
            Event current = Event.current;
            switch (current.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                {
                    Rect rect = sliderPosition;
                    rect.x -= 5f;
                    rect.width += 10f;
                    if (rect.Contains(current.mousePosition))
                    {
                        current.Use();
                        GUIUtility.hotControl = controlID;
                        if (<>f__am$cache6 == null)
                        {
                            <>f__am$cache6 = new Func<LODGroupGUI.LODInfo, bool>(null, (IntPtr) <DrawLODLevelSlider>m__6);
                        }
                        if (<>f__am$cache7 == null)
                        {
                            <>f__am$cache7 = new Func<LODGroupGUI.LODInfo, int>(null, (IntPtr) <DrawLODLevelSlider>m__7);
                        }
                        IOrderedEnumerable<LODGroupGUI.LODInfo> collection = Enumerable.OrderByDescending<LODGroupGUI.LODInfo, int>(Enumerable.Where<LODGroupGUI.LODInfo>(lods, <>f__am$cache6), <>f__am$cache7);
                        if (<>f__am$cache8 == null)
                        {
                            <>f__am$cache8 = new Func<LODGroupGUI.LODInfo, bool>(null, (IntPtr) <DrawLODLevelSlider>m__8);
                        }
                        if (<>f__am$cache9 == null)
                        {
                            <>f__am$cache9 = new Func<LODGroupGUI.LODInfo, int>(null, (IntPtr) <DrawLODLevelSlider>m__9);
                        }
                        IOrderedEnumerable<LODGroupGUI.LODInfo> enumerable2 = Enumerable.OrderBy<LODGroupGUI.LODInfo, int>(Enumerable.Where<LODGroupGUI.LODInfo>(lods, <>f__am$cache8), <>f__am$cache9);
                        List<LODGroupGUI.LODInfo> list = new List<LODGroupGUI.LODInfo>();
                        list.AddRange(collection);
                        list.AddRange(enumerable2);
                        foreach (LODGroupGUI.LODInfo info in list)
                        {
                            if (info.m_ButtonPosition.Contains(current.mousePosition))
                            {
                                this.m_SelectedLODSlider = info.LODLevel;
                                this.m_SelectedLODRange = info.LODLevel;
                                break;
                            }
                            if (info.m_RangePosition.Contains(current.mousePosition))
                            {
                                this.m_SelectedLODSlider = -1;
                                this.m_SelectedLODRange = info.LODLevel;
                                break;
                            }
                        }
                    }
                    break;
                }
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        current.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (((GUIUtility.hotControl == controlID) && (this.m_SelectedLODSlider >= 0)) && (lods[this.m_SelectedLODSlider] != null))
                    {
                        current.Use();
                        LODGroupGUI.SetSelectedLODLevelPercentage(LODGroupGUI.GetCameraPercent(current.mousePosition, sliderPosition) - 0.001f, this.m_SelectedLODSlider, lods);
                        this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODSlider).FindPropertyRelative("height").floatValue = lods[this.m_SelectedLODSlider].RawScreenPercent;
                    }
                    break;

                case EventType.Repaint:
                    LODGroupGUI.DrawLODSlider(sliderPosition, lods, this.m_SelectedLODRange);
                    break;
            }
        }

        private void GenerateMaterials()
        {
            if (<>f__am$cache1 == null)
            {
                <>f__am$cache1 = new Func<SpeedTreeImporter, string>(null, (IntPtr) <GenerateMaterials>m__1);
            }
            string[] searchInFolders = Enumerable.ToArray<string>(Enumerable.Select<SpeedTreeImporter, string>(this.importers, <>f__am$cache1));
            if (<>f__am$cache2 == null)
            {
                <>f__am$cache2 = new Func<string, string>(null, (IntPtr) <GenerateMaterials>m__2);
            }
            string[] assets = Enumerable.ToArray<string>(Enumerable.Select<string, string>(AssetDatabase.FindAssets("t:Material", searchInFolders), <>f__am$cache2));
            bool flag = true;
            if (assets.Length > 0)
            {
                flag = Provider.PromptAndCheckoutIfNeeded(assets, string.Format("Materials will be checked out in:\n{0}", string.Join("\n", searchInFolders)));
            }
            if (flag)
            {
                foreach (SpeedTreeImporter importer in this.importers)
                {
                    importer.GenerateMaterials();
                }
            }
        }

        internal List<LODGroupGUI.LODInfo> GetLODInfoArray(Rect area)
        {
            <GetLODInfoArray>c__AnonStorey0 storey = new <GetLODInfoArray>c__AnonStorey0 {
                $this = this,
                lodCount = this.m_LODSettings.arraySize
            };
            return LODGroupGUI.CreateLODInfos(storey.lodCount, area, new Func<int, string>(storey, (IntPtr) this.<>m__0), new Func<int, float>(storey, (IntPtr) this.<>m__1));
        }

        public bool HasSameLODConfig()
        {
            if (base.serializedObject.FindProperty("m_HasBillboard").hasMultipleDifferentValues)
            {
                return false;
            }
            if (this.m_LODSettings.FindPropertyRelative("Array.size").hasMultipleDifferentValues)
            {
                return false;
            }
            for (int i = 0; i < this.m_LODSettings.arraySize; i++)
            {
                if (this.m_LODSettings.GetArrayElementAtIndex(i).FindPropertyRelative("height").hasMultipleDifferentValues)
                {
                    return false;
                }
            }
            return true;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            this.m_ShowSmoothLODOptions.valueChanged.RemoveListener(new UnityAction(this.Repaint));
            this.m_ShowCrossFadeWidthOptions.valueChanged.RemoveListener(new UnityAction(this.Repaint));
        }

        private void OnEnable()
        {
            this.m_LODSettings = base.serializedObject.FindProperty("m_LODSettings");
            this.m_EnableSmoothLOD = base.serializedObject.FindProperty("m_EnableSmoothLODTransition");
            this.m_AnimateCrossFading = base.serializedObject.FindProperty("m_AnimateCrossFading");
            this.m_BillboardTransitionCrossFadeWidth = base.serializedObject.FindProperty("m_BillboardTransitionCrossFadeWidth");
            this.m_FadeOutWidth = base.serializedObject.FindProperty("m_FadeOutWidth");
            this.m_MainColor = base.serializedObject.FindProperty("m_MainColor");
            this.m_HueVariation = base.serializedObject.FindProperty("m_HueVariation");
            this.m_AlphaTestRef = base.serializedObject.FindProperty("m_AlphaTestRef");
            this.m_ScaleFactor = base.serializedObject.FindProperty("m_ScaleFactor");
            this.m_ShowSmoothLODOptions.value = this.m_EnableSmoothLOD.hasMultipleDifferentValues || this.m_EnableSmoothLOD.boolValue;
            this.m_ShowSmoothLODOptions.valueChanged.AddListener(new UnityAction(this.Repaint));
            this.m_ShowCrossFadeWidthOptions.value = this.m_AnimateCrossFading.hasMultipleDifferentValues || !this.m_AnimateCrossFading.boolValue;
            this.m_ShowCrossFadeWidthOptions.valueChanged.AddListener(new UnityAction(this.Repaint));
        }

        public override void OnInspectorGUI()
        {
            this.ShowMeshGUI();
            this.ShowMaterialGUI();
            this.ShowLODGUI();
            EditorGUILayout.Space();
            if (this.upgradeMaterials)
            {
                EditorGUILayout.HelpBox(string.Format("SpeedTree materials need to be upgraded. Please back them up (if modified manually) then hit the \"{0}\" button below.", Styles.ApplyAndGenerate.text), MessageType.Warning);
            }
            base.ApplyRevertGUI();
        }

        private void OnResetLODMenuClick(object userData)
        {
            float[] lODHeights = (userData as SpeedTreeImporter).LODHeights;
            for (int i = 0; i < lODHeights.Length; i++)
            {
                this.m_LODSettings.GetArrayElementAtIndex(i).FindPropertyRelative("height").floatValue = lODHeights[i];
            }
        }

        private void ShowLODGUI()
        {
            this.m_ShowSmoothLODOptions.target = this.m_EnableSmoothLOD.hasMultipleDifferentValues || this.m_EnableSmoothLOD.boolValue;
            this.m_ShowCrossFadeWidthOptions.target = this.m_AnimateCrossFading.hasMultipleDifferentValues || !this.m_AnimateCrossFading.boolValue;
            GUILayout.Label(Styles.LODHeader, EditorStyles.boldLabel, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(this.m_EnableSmoothLOD, Styles.SmoothLOD, new GUILayoutOption[0]);
            EditorGUI.indentLevel++;
            if (EditorGUILayout.BeginFadeGroup(this.m_ShowSmoothLODOptions.faded))
            {
                EditorGUILayout.PropertyField(this.m_AnimateCrossFading, Styles.AnimateCrossFading, new GUILayoutOption[0]);
                if (EditorGUILayout.BeginFadeGroup(this.m_ShowCrossFadeWidthOptions.faded))
                {
                    EditorGUILayout.Slider(this.m_BillboardTransitionCrossFadeWidth, 0f, 1f, Styles.CrossFadeWidth, new GUILayoutOption[0]);
                    EditorGUILayout.Slider(this.m_FadeOutWidth, 0f, 1f, Styles.FadeOutWidth, new GUILayoutOption[0]);
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            if (this.HasSameLODConfig())
            {
                EditorGUILayout.Space();
                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.ExpandWidth(true) };
                Rect area = GUILayoutUtility.GetRect((float) 0f, (float) 30f, options);
                List<LODGroupGUI.LODInfo> lODInfoArray = this.GetLODInfoArray(area);
                this.DrawLODLevelSlider(area, lODInfoArray);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                if ((this.m_SelectedLODRange != -1) && (lODInfoArray.Count > 0))
                {
                    EditorGUILayout.LabelField(lODInfoArray[this.m_SelectedLODRange].LODName + " Options", EditorStyles.boldLabel, new GUILayoutOption[0]);
                    bool flag = (this.m_SelectedLODRange == (lODInfoArray.Count - 1)) && this.importers[0].hasBillboard;
                    EditorGUILayout.PropertyField(this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("castShadows"), Styles.CastShadows, new GUILayoutOption[0]);
                    EditorGUILayout.PropertyField(this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("receiveShadows"), Styles.ReceiveShadows, new GUILayoutOption[0]);
                    SerializedProperty property = this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("useLightProbes");
                    EditorGUILayout.PropertyField(property, Styles.UseLightProbes, new GUILayoutOption[0]);
                    if ((!property.hasMultipleDifferentValues && property.boolValue) && flag)
                    {
                        EditorGUILayout.HelpBox("Enabling Light Probe for billboards breaks batched rendering and may cause performance problem.", MessageType.Warning);
                    }
                    EditorGUILayout.PropertyField(this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("enableBump"), Styles.EnableBump, new GUILayoutOption[0]);
                    EditorGUILayout.PropertyField(this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("enableHue"), Styles.EnableHue, new GUILayoutOption[0]);
                    if (<>f__am$cache3 == null)
                    {
                        <>f__am$cache3 = new Func<SpeedTreeImporter, int>(null, (IntPtr) <ShowLODGUI>m__3);
                    }
                    int num = Enumerable.Min<SpeedTreeImporter>(this.importers, <>f__am$cache3);
                    if (num > 0)
                    {
                        if (flag)
                        {
                            num = (num < 1) ? 0 : 1;
                        }
                        if (<>f__am$cache4 == null)
                        {
                            <>f__am$cache4 = new Func<string, GUIContent>(null, (IntPtr) <ShowLODGUI>m__4);
                        }
                        EditorGUILayout.Popup(this.m_LODSettings.GetArrayElementAtIndex(this.m_SelectedLODRange).FindPropertyRelative("windQuality"), Enumerable.ToArray<GUIContent>(Enumerable.Select<string, GUIContent>(Enumerable.Take<string>(SpeedTreeImporter.windQualityNames, num + 1), <>f__am$cache4)), Styles.WindQuality, new GUILayoutOption[0]);
                    }
                }
            }
            else
            {
                if (this.CanUnifyLODConfig())
                {
                    EditorGUILayout.BeginHorizontal(new GUILayoutOption[0]);
                    GUILayout.FlexibleSpace();
                    Rect rect = GUILayoutUtility.GetRect(Styles.ResetLOD, EditorStyles.miniButton);
                    if (GUI.Button(rect, Styles.ResetLOD, EditorStyles.miniButton))
                    {
                        GenericMenu menu = new GenericMenu();
                        foreach (SpeedTreeImporter importer in Enumerable.Cast<SpeedTreeImporter>(base.targets))
                        {
                            if (<>f__am$cache5 == null)
                            {
                                <>f__am$cache5 = new Func<float, string>(null, (IntPtr) <ShowLODGUI>m__5);
                            }
                            string text = string.Format("{0}: {1}", Path.GetFileNameWithoutExtension(importer.assetPath), string.Join(" | ", Enumerable.ToArray<string>(Enumerable.Select<float, string>(importer.LODHeights, <>f__am$cache5))));
                            menu.AddItem(new GUIContent(text), false, new GenericMenu.MenuFunction2(this.OnResetLODMenuClick), importer);
                        }
                        menu.DropDown(rect);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.ExpandWidth(true) };
                Rect rect3 = GUILayoutUtility.GetRect((float) 0f, (float) 30f, optionArray2);
                if (Event.current.type == EventType.Repaint)
                {
                    LODGroupGUI.DrawMixedValueLODSlider(rect3);
                }
            }
            EditorGUILayout.Space();
        }

        private void ShowMaterialGUI()
        {
            GUILayout.Label(Styles.MaterialsHeader, EditorStyles.boldLabel, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(this.m_MainColor, Styles.MainColor, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(this.m_HueVariation, Styles.HueVariation, new GUILayoutOption[0]);
            EditorGUILayout.Slider(this.m_AlphaTestRef, 0f, 1f, Styles.AlphaTestRef, new GUILayoutOption[0]);
        }

        private void ShowMeshGUI()
        {
            GUILayout.Label(Styles.MeshesHeader, EditorStyles.boldLabel, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(this.m_ScaleFactor, Styles.ScaleFactor, new GUILayoutOption[0]);
            if (!this.m_ScaleFactor.hasMultipleDifferentValues && Mathf.Approximately(this.m_ScaleFactor.floatValue, 0.3048f))
            {
                EditorGUILayout.HelpBox(Styles.ScaleFactorHelp.text, MessageType.Info);
            }
        }

        private SpeedTreeImporter[] importers
        {
            get
            {
                return Enumerable.ToArray<SpeedTreeImporter>(Enumerable.Cast<SpeedTreeImporter>(base.targets));
            }
        }

        private bool upgradeMaterials
        {
            get
            {
                if (<>f__am$cache0 == null)
                {
                    <>f__am$cache0 = new Func<SpeedTreeImporter, bool>(null, (IntPtr) <get_upgradeMaterials>m__0);
                }
                return Enumerable.Any<SpeedTreeImporter>(this.importers, <>f__am$cache0);
            }
        }

        [CompilerGenerated]
        private sealed class <GetLODInfoArray>c__AnonStorey0
        {
            internal SpeedTreeImporterInspector $this;
            internal int lodCount;

            internal string <>m__0(int i)
            {
                return (((i != (this.lodCount - 1)) || !(this.$this.target as SpeedTreeImporter).hasBillboard) ? string.Format("LOD {0}", i) : "Billboard");
            }

            internal float <>m__1(int i)
            {
                return this.$this.m_LODSettings.GetArrayElementAtIndex(i).FindPropertyRelative("height").floatValue;
            }
        }

        private class Styles
        {
            public static GUIContent AlphaTestRef = EditorGUIUtility.TextContent("Alpha Cutoff|The alpha-test reference value.");
            public static GUIContent AnimateCrossFading = EditorGUIUtility.TextContent("Animate Cross-fading|Cross-fading is animated instead of being calculated by distance.");
            public static GUIContent ApplyAndGenerate = EditorGUIUtility.TextContent("Apply & Generate Materials|Apply current importer settings and generate materials with new settings.");
            public static GUIContent CastShadows = EditorGUIUtility.TextContent("Cast Shadows|The tree casts shadow.");
            public static GUIContent CrossFadeWidth = EditorGUIUtility.TextContent("Crossfade Width|Proportion of the last 3D mesh LOD region width which is used for cross-fading to billboard tree.");
            public static GUIContent EnableBump = EditorGUIUtility.TextContent("Normal Map|Enable normal mapping (aka Bump mapping).");
            public static GUIContent EnableHue = EditorGUIUtility.TextContent("Enable Hue Variation|Enable Hue variation color (color is adjusted between Main Color and Hue Color).");
            public static GUIContent FadeOutWidth = EditorGUIUtility.TextContent("Fade Out Width|Proportion of the billboard LOD region width which is used for fading out the billboard.");
            public static GUIContent HueVariation = EditorGUIUtility.TextContent("Hue Color|Apply to LODs that have Hue Variation effect enabled.");
            public static GUIContent LODHeader = EditorGUIUtility.TextContent("LODs");
            public static GUIContent MainColor = EditorGUIUtility.TextContent("Main Color|The color modulating the diffuse lighting component.");
            public static GUIContent MaterialsHeader = EditorGUIUtility.TextContent("Materials");
            public static GUIContent MeshesHeader = EditorGUIUtility.TextContent("Meshes");
            public static GUIContent ReceiveShadows = EditorGUIUtility.TextContent("Receive Shadows|The tree receives shadow.");
            public static GUIContent Regenerate = EditorGUIUtility.TextContent("Regenerate Materials|Regenerate materials from the current importer settings.");
            public static GUIContent ResetLOD = EditorGUIUtility.TextContent("Reset LOD to...|Unify the LOD settings for all selected assets.");
            public static GUIContent ScaleFactor = EditorGUIUtility.TextContent("Scale Factor|How much to scale the tree model compared to what is in the .spm file.");
            public static GUIContent ScaleFactorHelp = EditorGUIUtility.TextContent("The default value of Scale Factor is 0.3048, the conversion ratio from feet to meters, as these are the most conventional measurements used in SpeedTree and Unity, respectively.");
            public static GUIContent SmoothLOD = EditorGUIUtility.TextContent("Smooth LOD|Toggles smooth LOD transitions.");
            public static GUIContent UseLightProbes = EditorGUIUtility.TextContent("Use Light Probes|The tree uses light probe for lighting.");
            public static GUIContent UseReflectionProbes = EditorGUIUtility.TextContent("Use Reflection Probes|The tree uses reflection probe for rendering.");
            public static GUIContent WindQuality = EditorGUIUtility.TextContent("Wind Quality|Controls the wind quality.");
        }
    }
}
