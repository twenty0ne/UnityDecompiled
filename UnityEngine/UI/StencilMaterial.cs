﻿namespace UnityEngine.UI
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;

    public static class StencilMaterial
    {
        private static List<MatEntry> m_List = new List<MatEntry>();

        [Obsolete("Use Material.Add instead.", true)]
        public static Material Add(Material baseMat, int stencilID) => 
            null;

        public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask) => 
            Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 0xff, 0xff);

        public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask)
        {
            if (((stencilID <= 0) && (colorWriteMask == ColorWriteMask.All)) || (baseMat == null))
            {
                return baseMat;
            }
            if (!baseMat.HasProperty("_Stencil"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
                return baseMat;
            }
            if (!baseMat.HasProperty("_StencilOp"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
                return baseMat;
            }
            if (!baseMat.HasProperty("_StencilComp"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
                return baseMat;
            }
            if (!baseMat.HasProperty("_StencilReadMask"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
                return baseMat;
            }
            if (!baseMat.HasProperty("_StencilReadMask"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
                return baseMat;
            }
            if (!baseMat.HasProperty("_ColorMask"))
            {
                Debug.LogWarning("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
                return baseMat;
            }
            for (int i = 0; i < m_List.Count; i++)
            {
                MatEntry entry = m_List[i];
                if ((((entry.baseMat == baseMat) && (entry.stencilId == stencilID)) && ((entry.operation == operation) && (entry.compareFunction == compareFunction))) && (((entry.readMask == readMask) && (entry.writeMask == writeMask)) && (entry.colorMask == colorWriteMask)))
                {
                    entry.count++;
                    return entry.customMat;
                }
            }
            MatEntry item = new MatEntry {
                count = 1,
                baseMat = baseMat,
                customMat = new Material(baseMat)
            };
            item.customMat.hideFlags = HideFlags.HideAndDontSave;
            item.stencilId = stencilID;
            item.operation = operation;
            item.compareFunction = compareFunction;
            item.readMask = readMask;
            item.writeMask = writeMask;
            item.colorMask = colorWriteMask;
            item.useAlphaClip = (operation != StencilOp.Keep) && (writeMask > 0);
            item.customMat.name = $"Stencil Id:{stencilID}, Op:{operation}, Comp:{compareFunction}, WriteMask:{writeMask}, ReadMask:{readMask}, ColorMask:{colorWriteMask} AlphaClip:{item.useAlphaClip} ({baseMat.name})";
            item.customMat.SetInt("_Stencil", stencilID);
            item.customMat.SetInt("_StencilOp", (int) operation);
            item.customMat.SetInt("_StencilComp", (int) compareFunction);
            item.customMat.SetInt("_StencilReadMask", readMask);
            item.customMat.SetInt("_StencilWriteMask", writeMask);
            item.customMat.SetInt("_ColorMask", (int) colorWriteMask);
            if (item.customMat.HasProperty("_UseAlphaClip"))
            {
                item.customMat.SetInt("_UseAlphaClip", !item.useAlphaClip ? 0 : 1);
            }
            if (item.useAlphaClip)
            {
                item.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
            }
            else
            {
                item.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
            }
            m_List.Add(item);
            return item.customMat;
        }

        public static void ClearAll()
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                MatEntry entry = m_List[i];
                Misc.DestroyImmediate(entry.customMat);
                entry.baseMat = null;
            }
            m_List.Clear();
        }

        public static void Remove(Material customMat)
        {
            if (customMat != null)
            {
                for (int i = 0; i < m_List.Count; i++)
                {
                    MatEntry entry = m_List[i];
                    if (entry.customMat == customMat)
                    {
                        if (--entry.count == 0)
                        {
                            Misc.DestroyImmediate(entry.customMat);
                            entry.baseMat = null;
                            m_List.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
        }

        private class MatEntry
        {
            public Material baseMat;
            public ColorWriteMask colorMask;
            public CompareFunction compareFunction = CompareFunction.Always;
            public int count;
            public Material customMat;
            public StencilOp operation = StencilOp.Keep;
            public int readMask;
            public int stencilId;
            public bool useAlphaClip;
            public int writeMask;
        }
    }
}

