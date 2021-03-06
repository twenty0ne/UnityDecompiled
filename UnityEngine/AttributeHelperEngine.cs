﻿namespace UnityEngine
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.Scripting;

    internal class AttributeHelperEngine
    {
        [RequiredByNativeCode]
        private static bool CheckIsEditorScript(System.Type klass)
        {
            while ((klass != null) && (klass != typeof(MonoBehaviour)))
            {
                if (klass.GetCustomAttributes(typeof(ExecuteInEditMode), false).Length != 0)
                {
                    return true;
                }
                klass = klass.BaseType;
            }
            return false;
        }

        private static T GetCustomAttributeOfType<T>(System.Type klass) where T: Attribute
        {
            System.Type attributeType = typeof(T);
            object[] customAttributes = klass.GetCustomAttributes(attributeType, true);
            if ((customAttributes != null) && (customAttributes.Length != 0))
            {
                return (T) customAttributes[0];
            }
            return null;
        }

        [RequiredByNativeCode]
        private static int GetDefaultExecutionOrderFor(System.Type klass)
        {
            DefaultExecutionOrder customAttributeOfType = GetCustomAttributeOfType<DefaultExecutionOrder>(klass);
            return customAttributeOfType?.order;
        }

        [RequiredByNativeCode]
        private static System.Type GetParentTypeDisallowingMultipleInclusion(System.Type type)
        {
            Stack<System.Type> stack = new Stack<System.Type>();
            while ((type != null) && (type != typeof(MonoBehaviour)))
            {
                stack.Push(type);
                type = type.BaseType;
            }
            System.Type type2 = null;
            while (stack.Count > 0)
            {
                type2 = stack.Pop();
                if (type2.GetCustomAttributes(typeof(DisallowMultipleComponent), false).Length != 0)
                {
                    return type2;
                }
            }
            return null;
        }

        [RequiredByNativeCode]
        private static System.Type[] GetRequiredComponents(System.Type klass)
        {
            List<System.Type> list = null;
            while ((klass != null) && (klass != typeof(MonoBehaviour)))
            {
                RequireComponent[] customAttributes = (RequireComponent[]) klass.GetCustomAttributes(typeof(RequireComponent), false);
                System.Type baseType = klass.BaseType;
                foreach (RequireComponent component in customAttributes)
                {
                    if (((list == null) && (customAttributes.Length == 1)) && (baseType == typeof(MonoBehaviour)))
                    {
                        return new System.Type[] { component.m_Type0, component.m_Type1, component.m_Type2 };
                    }
                    if (list == null)
                    {
                        list = new List<System.Type>();
                    }
                    if (component.m_Type0 != null)
                    {
                        list.Add(component.m_Type0);
                    }
                    if (component.m_Type1 != null)
                    {
                        list.Add(component.m_Type1);
                    }
                    if (component.m_Type2 != null)
                    {
                        list.Add(component.m_Type2);
                    }
                }
                klass = baseType;
            }
            return list?.ToArray();
        }
    }
}

