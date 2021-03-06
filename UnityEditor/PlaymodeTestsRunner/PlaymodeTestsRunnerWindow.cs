﻿namespace UnityEditor.PlaymodeTestsRunner
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEditor.PlaymodeTestsRunner.GUI;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.SceneManagement;

    [Serializable]
    internal class PlaymodeTestsRunnerWindow : EditorWindow, IHasCustomMenu
    {
        [CompilerGenerated]
        private static EditorApplication.CallbackFunction <>f__mg$cache0;
        [CompilerGenerated]
        private static EditorApplication.CallbackFunction <>f__mg$cache1;
        [CompilerGenerated]
        private static UnityAction<Scene, NewSceneMode> <>f__mg$cache2;
        [CompilerGenerated]
        private static UnityAction<Scene, OpenSceneMode> <>f__mg$cache3;
        [CompilerGenerated]
        private static EditorApplication.CallbackFunction <>f__mg$cache4;
        [CompilerGenerated]
        private static GenericMenu.MenuFunction <>f__mg$cache5;
        [CompilerGenerated]
        private static GenericMenu.MenuFunction <>f__mg$cache6;
        public TestFilterSettings filterSettings;
        [SerializeField]
        private EditModeTestListGUI m_EditModeTestListGUI;
        private readonly GUIContent m_GUIBlockUI = new GUIContent("Block UI when running", "Block UI when running tests");
        private readonly GUIContent m_GUICodebasedTests = new GUIContent("Code-based tests");
        private readonly GUIContent m_GUIHorizontalSplit = new GUIContent("Horizontal layout");
        private readonly GUIContent m_GUIPauseOnFailure = new GUIContent("Pause on test failure");
        private readonly GUIContent m_GUIScenebasedTests = new GUIContent("Scene-based tests");
        private readonly GUIContent m_GUIVerticalSplit = new GUIContent("Vertical layout");
        private bool m_IsBuilding;
        [SerializeField]
        internal bool m_IsSceneBased;
        [SerializeField]
        private PlayModeTestListGUI m_PlayModeTestListGUI;
        internal TestListGUI m_SelectedTestTypes;
        private PlaymodeTestsRunnerWindowSettings m_Settings;
        private SplitterState m_Spl;
        [SerializeField]
        private int m_TestTypeToolbarIndex;
        internal static PlaymodeTestsRunnerWindow s_Instance;

        static PlaymodeTestsRunnerWindow()
        {
            InitBackgroundRunners();
        }

        public PlaymodeTestsRunnerWindow()
        {
            float[] relativeSizes = new float[] { 75f, 25f };
            int[] minSizes = new int[] { 0x20, 0x20 };
            this.m_Spl = new SplitterState(relativeSizes, minSizes, null);
            this.m_TestTypeToolbarIndex = 0;
        }

        public void AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(this.m_GUIBlockUI, this.m_Settings.blockUIWhenRunning, new GenericMenu.MenuFunction(this.m_Settings.ToggleBlockUIWhenRunning));
            menu.AddItem(this.m_GUIPauseOnFailure, this.m_Settings.pauseOnTestFailure, new GenericMenu.MenuFunction(this.m_Settings.TogglePauseOnTestFailure));
            menu.AddSeparator(null);
            menu.AddItem(this.m_GUIVerticalSplit, this.m_Settings.verticalSplit, new GenericMenu.MenuFunction(this.m_Settings.ToggleVerticalSplit));
            menu.AddItem(this.m_GUIHorizontalSplit, !this.m_Settings.verticalSplit, new GenericMenu.MenuFunction(this.m_Settings.ToggleVerticalSplit));
            menu.AddSeparator(null);
            if (<>f__mg$cache5 == null)
            {
                <>f__mg$cache5 = new GenericMenu.MenuFunction(PlaymodeTestsRunnerWindow.ShowPlaymodeTestsRunnerWindowCodeBased);
            }
            menu.AddItem(this.m_GUICodebasedTests, !this.m_IsSceneBased, <>f__mg$cache5);
            if (<>f__mg$cache6 == null)
            {
                <>f__mg$cache6 = new GenericMenu.MenuFunction(PlaymodeTestsRunnerWindow.ShowPlaymodeTestsRunnerWindowSceneBased);
            }
            menu.AddItem(this.m_GUIScenebasedTests, this.m_IsSceneBased, <>f__mg$cache6);
        }

        [DidReloadScripts]
        private static void CompilationCallback()
        {
            if ((s_Instance != null) && (s_Instance.m_SelectedTestTypes != null))
            {
                s_Instance.m_SelectedTestTypes.Repaint();
            }
        }

        private static void InitBackgroundRunners()
        {
            if (<>f__mg$cache0 == null)
            {
                <>f__mg$cache0 = new EditorApplication.CallbackFunction(PlaymodeTestsRunnerWindow.OnPlaymodeStateChanged);
            }
            EditorApplication.playmodeStateChanged = (EditorApplication.CallbackFunction) Delegate.Remove(EditorApplication.playmodeStateChanged, <>f__mg$cache0);
            if (<>f__mg$cache1 == null)
            {
                <>f__mg$cache1 = new EditorApplication.CallbackFunction(PlaymodeTestsRunnerWindow.OnPlaymodeStateChanged);
            }
            EditorApplication.playmodeStateChanged = (EditorApplication.CallbackFunction) Delegate.Combine(EditorApplication.playmodeStateChanged, <>f__mg$cache1);
            if (<>f__mg$cache2 == null)
            {
                <>f__mg$cache2 = new UnityAction<Scene, NewSceneMode>(PlaymodeTestsRunnerWindow.SceneWasCreated);
            }
            EditorSceneManager.sceneWasCreated = (UnityAction<Scene, NewSceneMode>) Delegate.Combine(EditorSceneManager.sceneWasCreated, <>f__mg$cache2);
            if (<>f__mg$cache3 == null)
            {
                <>f__mg$cache3 = new UnityAction<Scene, OpenSceneMode>(PlaymodeTestsRunnerWindow.SceneWasOpened);
            }
            EditorSceneManager.sceneWasOpened = (UnityAction<Scene, OpenSceneMode>) Delegate.Combine(EditorSceneManager.sceneWasOpened, <>f__mg$cache3);
        }

        public void OnDestroy()
        {
            if (<>f__mg$cache4 == null)
            {
                <>f__mg$cache4 = new EditorApplication.CallbackFunction(PlaymodeTestsRunnerWindow.OnPlaymodeStateChanged);
            }
            EditorApplication.playmodeStateChanged = (EditorApplication.CallbackFunction) Delegate.Remove(EditorApplication.playmodeStateChanged, <>f__mg$cache4);
        }

        public void OnEnable()
        {
            s_Instance = this;
            this.m_Settings = new PlaymodeTestsRunnerWindowSettings("UnityEdior.PlaymodeTestsRunnerWindow");
            this.filterSettings = new TestFilterSettings("UnityTest.IntegrationTestsRunnerWindow");
            if (this.m_SelectedTestTypes == null)
            {
                this.SelectTestListGUI(this.m_TestTypeToolbarIndex);
            }
            this.m_SelectedTestTypes.Init(this);
            this.m_SelectedTestTypes.Reload();
        }

        public void OnGUI()
        {
            if (BuildPipeline.isBuildingPlayer)
            {
                this.m_IsBuilding = true;
            }
            else if (this.m_IsBuilding)
            {
                this.m_IsBuilding = false;
                base.Repaint();
            }
            int testTypeToolbarIndex = this.m_TestTypeToolbarIndex;
            this.m_TestTypeToolbarIndex = GUILayout.Toolbar(this.m_TestTypeToolbarIndex, Enum.GetNames(typeof(TestRunnerMenuLabels)), new GUILayoutOption[0]);
            if (testTypeToolbarIndex != this.m_TestTypeToolbarIndex)
            {
                this.SelectTestListGUI(this.m_TestTypeToolbarIndex);
            }
            EditorGUILayout.BeginVertical(new GUILayoutOption[0]);
            using (new EditorGUI.DisabledScope(EditorApplication.isPlayingOrWillChangePlaymode))
            {
                this.m_SelectedTestTypes.PrintHeadPanel();
            }
            EditorGUILayout.EndVertical();
            if (this.m_Settings.verticalSplit)
            {
                SplitterGUILayout.BeginVerticalSplit(this.m_Spl, new GUILayoutOption[0]);
            }
            else
            {
                SplitterGUILayout.BeginHorizontalSplit(this.m_Spl, new GUILayoutOption[0]);
            }
            EditorGUILayout.BeginVertical(new GUILayoutOption[0]);
            EditorGUILayout.BeginVertical(Styles.testList, new GUILayoutOption[0]);
            this.m_SelectedTestTypes.RenderTestList();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            this.m_SelectedTestTypes.RenderDetails();
            if (this.m_Settings.verticalSplit)
            {
                SplitterGUILayout.EndVerticalSplit();
            }
            else
            {
                SplitterGUILayout.EndHorizontalSplit();
            }
        }

        private static void OnPlaymodeStateChanged()
        {
            if (((s_Instance != null) && !EditorApplication.isPlaying) && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                s_Instance.m_SelectedTestTypes.TestSelectionCallback(s_Instance.m_SelectedTestTypes.m_TestListState.selectedIDs.ToArray());
                s_Instance.Repaint();
            }
        }

        private static void SceneWasCreated(Scene scene, NewSceneMode mode)
        {
        }

        private static void SceneWasOpened(Scene scene, OpenSceneMode mode)
        {
            if ((s_Instance != null) && (s_Instance.m_SelectedTestTypes != null))
            {
                s_Instance.m_SelectedTestTypes.Reload();
            }
        }

        private void SelectTestListGUI(int testTypeToolbarIndex)
        {
            if (testTypeToolbarIndex == 0)
            {
                if (this.m_PlayModeTestListGUI == null)
                {
                    this.m_PlayModeTestListGUI = new PlayModeTestListGUI();
                }
                this.m_SelectedTestTypes = this.m_PlayModeTestListGUI;
            }
            else if (testTypeToolbarIndex == 1)
            {
                if (this.m_EditModeTestListGUI == null)
                {
                    this.m_EditModeTestListGUI = new EditModeTestListGUI();
                }
                this.m_SelectedTestTypes = this.m_EditModeTestListGUI;
            }
            this.m_SelectedTestTypes.Init(this);
            this.m_SelectedTestTypes.Reload();
            this.m_SelectedTestTypes.Repaint();
        }

        private void SetSceneBaseMode(bool b)
        {
            this.m_IsSceneBased = b;
            this.m_SelectedTestTypes.Reload();
            this.m_SelectedTestTypes.Repaint();
        }

        [UnityEditor.MenuItem("Window/Playmode Tests Runner (Code-based)", false, 0x7e0, true)]
        public static void ShowPlaymodeTestsRunnerWindowCodeBased()
        {
            if (s_Instance != null)
            {
                s_Instance.Close();
            }
            PlaymodeTestsRunnerWindow window = EditorWindow.GetWindow<PlaymodeTestsRunnerWindow>("Playmode Tests");
            window.SetSceneBaseMode(false);
            window.Show();
        }

        public static void ShowPlaymodeTestsRunnerWindowSceneBased()
        {
            if (s_Instance != null)
            {
                s_Instance.Close();
            }
            PlaymodeTestsRunnerWindow window = EditorWindow.GetWindow<PlaymodeTestsRunnerWindow>("Playmode Tests (Scene-based)");
            window.SetSceneBaseMode(true);
            window.Show();
        }

        internal static class Styles
        {
            public static GUIStyle info = new GUIStyle(EditorStyles.wordWrappedLabel);
            public static GUIStyle testList;

            static Styles()
            {
                info.wordWrap = false;
                info.stretchHeight = true;
                info.margin.right = 15;
                testList = new GUIStyle("CN Box");
                testList.margin.top = 0;
                testList.padding.left = 3;
            }
        }

        private enum TestRunnerMenuLabels
        {
            PlayMode,
            EditMode
        }
    }
}

