﻿namespace UnityEditor.Macros
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityScript.Scripting;

    public static class MacroEvaluator
    {
        [CompilerGenerated]
        private static Func<Type, bool> <>f__am$cache0;
        private static readonly EvaluationContext EditorEvaluationContext = new EvaluationContext(new EditorEvaluationDomainProvider());

        public static string Eval(string macro)
        {
            if (macro.StartsWith("ExecuteMethod: "))
            {
                return ExecuteMethodThroughReflection(macro);
            }
            object obj2 = Evaluator.Eval(EditorEvaluationContext, macro);
            return ((obj2 != null) ? obj2.ToString() : "Null");
        }

        private static string ExecuteMethodThroughReflection(string macro)
        {
            <ExecuteMethodThroughReflection>c__AnonStorey0 storey = new <ExecuteMethodThroughReflection>c__AnonStorey0();
            Match match = new Regex(@"ExecuteMethod: (?<type>.*)\.(?<method>.*)").Match(macro);
            storey.typename = match.Groups["type"].ToString();
            string name = match.Groups["method"].ToString();
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = new Func<Type, bool>(null, (IntPtr) <ExecuteMethodThroughReflection>m__0);
            }
            MethodInfo method = Enumerable.First<Type>(Enumerable.Where<Type>(Enumerable.Select<Assembly, Type>(EditorAssemblies.loadedAssemblies, new Func<Assembly, Type>(storey, (IntPtr) this.<>m__0)), <>f__am$cache0)).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            if (method == null)
            {
                throw new ArgumentException(string.Format("cannot find method {0} in type {1}", name, storey.typename));
            }
            if (method.GetParameters().Length > 0)
            {
                throw new ArgumentException("You can only invoke static methods with no arguments");
            }
            object obj2 = method.Invoke(null, new object[0]);
            return ((obj2 != null) ? obj2.ToString() : "Null");
        }

        [CompilerGenerated]
        private sealed class <ExecuteMethodThroughReflection>c__AnonStorey0
        {
            internal string typename;

            internal Type <>m__0(Assembly a)
            {
                return a.GetType(this.typename, false);
            }
        }

        private class EditorEvaluationDomainProvider : SimpleEvaluationDomainProvider
        {
            [CompilerGenerated]
            private static Func<Assembly, IEnumerable<AssemblyName>> <>f__am$cache0;
            [CompilerGenerated]
            private static Func<AssemblyName, Assembly> <>f__am$cache1;
            [CompilerGenerated]
            private static Func<Assembly, bool> <>f__am$cache2;
            private static readonly string[] DefaultImports = new string[] { "UnityEditor", "UnityEngine" };

            public EditorEvaluationDomainProvider() : base(DefaultImports)
            {
            }

            public override Assembly[] GetAssemblyReferences()
            {
                Assembly[] loadedAssemblies = EditorAssemblies.loadedAssemblies;
                if (<>f__am$cache0 == null)
                {
                    <>f__am$cache0 = new Func<Assembly, IEnumerable<AssemblyName>>(null, (IntPtr) <GetAssemblyReferences>m__0);
                }
                if (<>f__am$cache1 == null)
                {
                    <>f__am$cache1 = new Func<AssemblyName, Assembly>(null, (IntPtr) <GetAssemblyReferences>m__1);
                }
                if (<>f__am$cache2 == null)
                {
                    <>f__am$cache2 = new Func<Assembly, bool>(null, (IntPtr) <GetAssemblyReferences>m__2);
                }
                IEnumerable<Assembly> second = Enumerable.Where<Assembly>(Enumerable.Select<AssemblyName, Assembly>(Enumerable.SelectMany<Assembly, AssemblyName>(loadedAssemblies, <>f__am$cache0), <>f__am$cache1), <>f__am$cache2);
                return Enumerable.ToArray<Assembly>(Enumerable.Concat<Assembly>(loadedAssemblies, second));
            }

            private static Assembly TryToLoad(AssemblyName a)
            {
                try
                {
                    return Assembly.Load(a);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
