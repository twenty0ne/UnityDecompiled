﻿namespace UnityEditor
{
    using System;
    using UnityEngine;

    internal class InitialModuleUI : ModuleUI
    {
        public SerializedProperty m_AutoRandomSeed;
        public SerializedMinMaxGradient m_Color;
        public SerializedProperty m_CustomSimulationSpace;
        public SerializedMinMaxCurve m_GravityModifier;
        public SerializedProperty m_LengthInSec;
        public SerializedMinMaxCurve m_LifeTime;
        public SerializedProperty m_Looping;
        public SerializedProperty m_MaxNumParticles;
        public SerializedProperty m_PlayOnAwake;
        public SerializedProperty m_Prewarm;
        public SerializedProperty m_RandomizeRotationDirection;
        public SerializedProperty m_RandomSeed;
        public SerializedProperty m_Rotation3D;
        public SerializedMinMaxCurve m_RotationX;
        public SerializedMinMaxCurve m_RotationY;
        public SerializedMinMaxCurve m_RotationZ;
        public SerializedProperty m_ScalingMode;
        public SerializedProperty m_SimulationSpace;
        public SerializedProperty m_SimulationSpeed;
        public SerializedProperty m_Size3D;
        public SerializedMinMaxCurve m_SizeX;
        public SerializedMinMaxCurve m_SizeY;
        public SerializedMinMaxCurve m_SizeZ;
        public SerializedMinMaxCurve m_Speed;
        public SerializedMinMaxCurve m_StartDelay;
        private static Texts s_Texts;

        public InitialModuleUI(ParticleSystemUI owner, SerializedObject o, string displayName) : base(owner, o, "InitialModule", displayName, ModuleUI.VisibilityState.VisibleAndFoldedOut)
        {
            this.Init();
        }

        public override float GetXAxisScalar() => 
            base.m_ParticleSystemUI.GetEmitterDuration();

        protected override void Init()
        {
            if (s_Texts == null)
            {
                s_Texts = new Texts();
            }
            if (this.m_LengthInSec == null)
            {
                this.m_LengthInSec = base.GetProperty0("lengthInSec");
                this.m_Looping = base.GetProperty0("looping");
                this.m_Prewarm = base.GetProperty0("prewarm");
                this.m_StartDelay = new SerializedMinMaxCurve(this, s_Texts.startDelay, "startDelay", false, true);
                this.m_StartDelay.m_AllowCurves = false;
                this.m_PlayOnAwake = base.GetProperty0("playOnAwake");
                this.m_SimulationSpace = base.GetProperty0("moveWithTransform");
                this.m_CustomSimulationSpace = base.GetProperty0("moveWithCustomTransform");
                this.m_SimulationSpeed = base.GetProperty0("simulationSpeed");
                this.m_ScalingMode = base.GetProperty0("scalingMode");
                this.m_AutoRandomSeed = base.GetProperty0("autoRandomSeed");
                this.m_RandomSeed = base.GetProperty0("randomSeed");
                this.m_LifeTime = new SerializedMinMaxCurve(this, s_Texts.lifetime, "startLifetime");
                this.m_Speed = new SerializedMinMaxCurve(this, s_Texts.speed, "startSpeed", ModuleUI.kUseSignedRange);
                this.m_Color = new SerializedMinMaxGradient(this, "startColor");
                this.m_Color.m_AllowRandomColor = true;
                this.m_Size3D = base.GetProperty("size3D");
                this.m_SizeX = new SerializedMinMaxCurve(this, s_Texts.x, "startSize");
                this.m_SizeY = new SerializedMinMaxCurve(this, s_Texts.y, "startSizeY");
                this.m_SizeZ = new SerializedMinMaxCurve(this, s_Texts.z, "startSizeZ");
                this.m_Rotation3D = base.GetProperty("rotation3D");
                this.m_RotationX = new SerializedMinMaxCurve(this, s_Texts.x, "startRotationX", ModuleUI.kUseSignedRange);
                this.m_RotationY = new SerializedMinMaxCurve(this, s_Texts.y, "startRotationY", ModuleUI.kUseSignedRange);
                this.m_RotationZ = new SerializedMinMaxCurve(this, s_Texts.z, "startRotation", ModuleUI.kUseSignedRange);
                this.m_RotationX.m_RemapValue = 57.29578f;
                this.m_RotationY.m_RemapValue = 57.29578f;
                this.m_RotationZ.m_RemapValue = 57.29578f;
                this.m_RotationX.m_DefaultCurveScalar = 3.141593f;
                this.m_RotationY.m_DefaultCurveScalar = 3.141593f;
                this.m_RotationZ.m_DefaultCurveScalar = 3.141593f;
                this.m_RandomizeRotationDirection = base.GetProperty("randomizeRotationDirection");
                this.m_GravityModifier = new SerializedMinMaxCurve(this, s_Texts.gravity, "gravityModifier", ModuleUI.kUseSignedRange);
                this.m_MaxNumParticles = base.GetProperty("maxNumParticles");
            }
        }

        public override void OnInspectorGUI(ParticleSystem s)
        {
            if (s_Texts == null)
            {
                s_Texts = new Texts();
            }
            ModuleUI.GUIFloat(s_Texts.duration, this.m_LengthInSec, "f2", new GUILayoutOption[0]);
            this.m_LengthInSec.floatValue = Mathf.Min(100000f, Mathf.Max(0f, this.m_LengthInSec.floatValue));
            bool boolValue = this.m_Looping.boolValue;
            ModuleUI.GUIToggle(s_Texts.looping, this.m_Looping, new GUILayoutOption[0]);
            if ((this.m_Looping.boolValue && !boolValue) && (s.time >= this.m_LengthInSec.floatValue))
            {
                s.time = 0f;
            }
            using (new EditorGUI.DisabledScope(!this.m_Looping.boolValue))
            {
                ModuleUI.GUIToggle(s_Texts.prewarm, this.m_Prewarm, new GUILayoutOption[0]);
            }
            using (new EditorGUI.DisabledScope(this.m_Prewarm.boolValue && this.m_Looping.boolValue))
            {
                ModuleUI.GUIMinMaxCurve(s_Texts.startDelay, this.m_StartDelay, new GUILayoutOption[0]);
            }
            ModuleUI.GUIMinMaxCurve(s_Texts.lifetime, this.m_LifeTime, new GUILayoutOption[0]);
            ModuleUI.GUIMinMaxCurve(s_Texts.speed, this.m_Speed, new GUILayoutOption[0]);
            EditorGUI.BeginChangeCheck();
            bool flag2 = ModuleUI.GUIToggle(s_Texts.size3D, this.m_Size3D, new GUILayoutOption[0]);
            if (EditorGUI.EndChangeCheck())
            {
                if (flag2)
                {
                    this.m_SizeX.RemoveCurveFromEditor();
                }
                else
                {
                    this.m_SizeX.RemoveCurveFromEditor();
                    this.m_SizeY.RemoveCurveFromEditor();
                    this.m_SizeZ.RemoveCurveFromEditor();
                }
            }
            MinMaxCurveState state = this.m_SizeX.state;
            this.m_SizeY.state = state;
            this.m_SizeZ.state = state;
            if (flag2)
            {
                this.m_SizeX.m_DisplayName = s_Texts.x;
                base.GUITripleMinMaxCurve(GUIContent.none, s_Texts.x, this.m_SizeX, s_Texts.y, this.m_SizeY, s_Texts.z, this.m_SizeZ, null, new GUILayoutOption[0]);
            }
            else
            {
                this.m_SizeX.m_DisplayName = s_Texts.size;
                ModuleUI.GUIMinMaxCurve(s_Texts.size, this.m_SizeX, new GUILayoutOption[0]);
            }
            EditorGUI.BeginChangeCheck();
            bool flag3 = ModuleUI.GUIToggle(s_Texts.rotation3D, this.m_Rotation3D, new GUILayoutOption[0]);
            if (EditorGUI.EndChangeCheck())
            {
                if (flag3)
                {
                    this.m_RotationZ.RemoveCurveFromEditor();
                }
                else
                {
                    this.m_RotationX.RemoveCurveFromEditor();
                    this.m_RotationY.RemoveCurveFromEditor();
                    this.m_RotationZ.RemoveCurveFromEditor();
                }
            }
            state = this.m_RotationZ.state;
            this.m_RotationY.state = state;
            this.m_RotationX.state = state;
            if (flag3)
            {
                this.m_RotationZ.m_DisplayName = s_Texts.z;
                base.GUITripleMinMaxCurve(GUIContent.none, s_Texts.x, this.m_RotationX, s_Texts.y, this.m_RotationY, s_Texts.z, this.m_RotationZ, null, new GUILayoutOption[0]);
            }
            else
            {
                this.m_RotationZ.m_DisplayName = s_Texts.rotation;
                ModuleUI.GUIMinMaxCurve(s_Texts.rotation, this.m_RotationZ, new GUILayoutOption[0]);
            }
            ModuleUI.GUIFloat(s_Texts.randomizeRotationDirection, this.m_RandomizeRotationDirection, new GUILayoutOption[0]);
            base.GUIMinMaxGradient(s_Texts.color, this.m_Color, new GUILayoutOption[0]);
            ModuleUI.GUIMinMaxCurve(s_Texts.gravity, this.m_GravityModifier, new GUILayoutOption[0]);
            string[] options = new string[] { "Local", "World", "Custom" };
            if ((ModuleUI.GUIPopup(s_Texts.simulationSpace, this.m_SimulationSpace, options, new GUILayoutOption[0]) == 2) && (this.m_CustomSimulationSpace != null))
            {
                ModuleUI.GUIObject(s_Texts.customSimulationSpace, this.m_CustomSimulationSpace, new GUILayoutOption[0]);
            }
            ModuleUI.GUIFloat(s_Texts.simulationSpeed, this.m_SimulationSpeed, new GUILayoutOption[0]);
            if ((base.m_ParticleSystemUI.m_ParticleSystem.shape.shapeType != ParticleSystemShapeType.SkinnedMeshRenderer) && (base.m_ParticleSystemUI.m_ParticleSystem.shape.shapeType != ParticleSystemShapeType.MeshRenderer))
            {
                string[] textArray2 = new string[] { "Hierarchy", "Local", "Shape" };
                ModuleUI.GUIPopup(s_Texts.scalingMode, this.m_ScalingMode, textArray2, new GUILayoutOption[0]);
            }
            bool flag4 = this.m_PlayOnAwake.boolValue;
            bool newPlayOnAwake = ModuleUI.GUIToggle(s_Texts.autoplay, this.m_PlayOnAwake, new GUILayoutOption[0]);
            if (flag4 != newPlayOnAwake)
            {
                base.m_ParticleSystemUI.m_ParticleEffectUI.PlayOnAwakeChanged(newPlayOnAwake);
            }
            ModuleUI.GUIInt(s_Texts.maxParticles, this.m_MaxNumParticles, new GUILayoutOption[0]);
            if (!ModuleUI.GUIToggle(s_Texts.autoRandomSeed, this.m_AutoRandomSeed, new GUILayoutOption[0]))
            {
                if (base.m_ParticleSystemUI.m_ParticleEffectUI.m_Owner is ParticleSystemInspector)
                {
                    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                    ModuleUI.GUIInt(s_Texts.randomSeed, this.m_RandomSeed, new GUILayoutOption[0]);
                    GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.Width(60f) };
                    if (GUILayout.Button("Reseed", EditorStyles.miniButton, optionArray1))
                    {
                        this.m_RandomSeed.intValue = base.m_ParticleSystemUI.m_ParticleSystem.GenerateRandomSeed();
                    }
                    GUILayout.EndHorizontal();
                }
                else
                {
                    ModuleUI.GUIInt(s_Texts.randomSeed, this.m_RandomSeed, new GUILayoutOption[0]);
                    if (GUILayout.Button("Reseed", EditorStyles.miniButton, new GUILayoutOption[0]))
                    {
                        this.m_RandomSeed.intValue = base.m_ParticleSystemUI.m_ParticleSystem.GenerateRandomSeed();
                    }
                }
            }
        }

        public override void UpdateCullingSupportedString(ref string text)
        {
            if (this.m_SimulationSpace.intValue != 0)
            {
                text = text + "\n\tLocal space simulation is not being used.";
            }
        }

        private class Texts
        {
            public GUIContent autoplay = EditorGUIUtility.TextContent("Play On Awake*|If enabled, the system will start playing automatically. Note that this setting is shared between all Particle Systems in the current particle effect.");
            public GUIContent autoRandomSeed = EditorGUIUtility.TextContent("Auto Random Seed|Simulate differently each time the effect is played.");
            public GUIContent color = EditorGUIUtility.TextContent("Start Color|The start color of particles.");
            public GUIContent customSimulationSpace = EditorGUIUtility.TextContent("Custom Simulation Space|Makes particle positions simulate relative to a custom Transform component.");
            public GUIContent duration = EditorGUIUtility.TextContent("Duration|The length of time the Particle System is emitting particles. If the system is looping, this indicates the length of one cycle.");
            public GUIContent gravity = EditorGUIUtility.TextContent("Gravity Modifier|Scales the gravity defined in Physics Manager");
            public GUIContent lifetime = EditorGUIUtility.TextContent("Start Lifetime|Start lifetime in seconds, particle will die when its lifetime reaches 0.");
            public GUIContent looping = EditorGUIUtility.TextContent("Looping|If true, the emission cycle will repeat after the duration.");
            public GUIContent maxParticles = EditorGUIUtility.TextContent("Max Particles|The number of particles in the system will be limited by this number. Emission will be temporarily halted if this is reached.");
            public GUIContent prewarm = EditorGUIUtility.TextContent("Prewarm|When played a prewarmed system will be in a state as if it had emitted one loop cycle. Can only be used if the system is looping.");
            public GUIContent randomizeRotationDirection = EditorGUIUtility.TextContent("Randomize Rotation|Cause some particles to spin in the opposite direction. (Set between 0 and 1, where a higher value causes more to flip)");
            public GUIContent randomSeed = EditorGUIUtility.TextContent("Random Seed|Randomize the look of the Particle System. Using the same seed will make the Particle System play identically each time.");
            public GUIContent rotation = EditorGUIUtility.TextContent("Start Rotation|The start rotation of particles in degrees.");
            public GUIContent rotation3D = EditorGUIUtility.TextContent("3D Start Rotation|If enabled, you can control the rotation separately for each axis.");
            public GUIContent scalingMode = EditorGUIUtility.TextContent("Scaling Mode|Should we use the combined scale from our entire hierarchy, just this particle node, or just apply scale to the shape module?");
            public GUIContent simulationSpace = EditorGUIUtility.TextContent("Simulation Space|Makes particle positions simulate in world, local or custom space. In local space they stay relative to their own Transform, and in custom space they are relative to the custom Transform.");
            public GUIContent simulationSpeed = EditorGUIUtility.TextContent("Simulation Speed|Scale the playback speed of the Particle System.");
            public GUIContent size = EditorGUIUtility.TextContent("Start Size|The start size of particles.");
            public GUIContent size3D = EditorGUIUtility.TextContent("3D Start Size|If enabled, you can control the size separately for each axis.");
            public GUIContent speed = EditorGUIUtility.TextContent("Start Speed|The start speed of particles, applied in the starting direction.");
            public GUIContent startDelay = EditorGUIUtility.TextContent("Start Delay|Delay in seconds that this Particle System will wait before emitting particles. Cannot be used together with a prewarmed looping system.");
            public GUIContent x = EditorGUIUtility.TextContent("X");
            public GUIContent y = EditorGUIUtility.TextContent("Y");
            public GUIContent z = EditorGUIUtility.TextContent("Z");
        }
    }
}

