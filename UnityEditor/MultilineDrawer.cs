﻿namespace UnityEditor
{
    using System;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(MultilineAttribute))]
    internal sealed class MultilineDrawer : PropertyDrawer
    {
        private const int kLineHeight = 13;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            (((!EditorGUIUtility.wideMode ? 16f : 0f) + 16f) + ((((MultilineAttribute) base.attribute).lines - 1) * 13));

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                label = EditorGUI.BeginProperty(position, label, property);
                position = EditorGUI.MultiFieldPrefixLabel(position, 0, label, 1);
                EditorGUI.BeginChangeCheck();
                int indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                string str = EditorGUI.TextArea(position, property.stringValue);
                EditorGUI.indentLevel = indentLevel;
                if (EditorGUI.EndChangeCheck())
                {
                    property.stringValue = str;
                }
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Multiline with string.");
            }
        }
    }
}

