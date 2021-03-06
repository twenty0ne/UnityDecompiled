﻿namespace UnityEditor.Audio
{
    using System;

    internal class MixerEffectDefinition
    {
        private readonly string m_EffectName;
        private readonly MixerParameterDefinition[] m_Parameters;

        public MixerEffectDefinition(string name, MixerParameterDefinition[] parameters)
        {
            this.m_EffectName = name;
            this.m_Parameters = new MixerParameterDefinition[parameters.Length];
            Array.Copy(parameters, this.m_Parameters, parameters.Length);
        }

        public string name =>
            this.m_EffectName;

        public MixerParameterDefinition[] parameters =>
            this.m_Parameters;
    }
}

