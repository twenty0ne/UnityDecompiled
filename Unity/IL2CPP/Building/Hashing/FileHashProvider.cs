﻿namespace Unity.IL2CPP.Building.Hashing
{
    using NiceIO;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Unity.IL2CPP.Building;
    using Unity.TinyProfiling;

    internal class FileHashProvider
    {
        private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();
        private readonly string[] _fileExtensions;
        [CompilerGenerated]
        private static Func<string, string> <>f__am$cache0;
        [CompilerGenerated]
        private static Func<NPath, bool> <>f__am$cache1;
        [CompilerGenerated]
        private static Func<string, string, string> <>f__am$cache2;
        [CompilerGenerated]
        private static Func<NPath, bool> <>f__am$cache3;
        [CompilerGenerated]
        private static Func<NPath, string> <>f__am$cache4;
        [CompilerGenerated]
        private static Func<NPath, bool> <>f__am$cache5;
        [CompilerGenerated]
        private static Func<NPath, string> <>f__am$cache6;
        [CompilerGenerated]
        private static Func<NPath, string> <>f__mg$cache0;

        public FileHashProvider(params string[] fileExtensions)
        {
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = new Func<string, string>(null, (IntPtr) <FileHashProvider>m__0);
            }
            this._fileExtensions = fileExtensions.Select<string, string>(<>f__am$cache0).ToArray<string>();
        }

        [CompilerGenerated]
        private static string <FileHashProvider>m__0(string e) => 
            ("*" + e);

        public string HashForAllHeaderFilesReachableByFilesIn(CppCompilationInstruction cppCompilationInstruction)
        {
            if (<>f__am$cache3 == null)
            {
                <>f__am$cache3 = new Func<NPath, bool>(null, (IntPtr) <HashForAllHeaderFilesReachableByFilesIn>m__3);
            }
            if (<>f__am$cache4 == null)
            {
                <>f__am$cache4 = new Func<NPath, string>(null, (IntPtr) <HashForAllHeaderFilesReachableByFilesIn>m__4);
            }
            return string.Concat(cppCompilationInstruction.IncludePaths.Where<NPath>(<>f__am$cache3).OrderBy<NPath, string>(<>f__am$cache4).Select<NPath, string>(new Func<NPath, string>(this, (IntPtr) this.HashOfAllIncludableFilesInDirectory)));
        }

        public string HashForAllIncludableFilesInDirectories(IEnumerable<NPath> directories)
        {
            if (<>f__am$cache5 == null)
            {
                <>f__am$cache5 = new Func<NPath, bool>(null, (IntPtr) <HashForAllIncludableFilesInDirectories>m__5);
            }
            if (<>f__am$cache6 == null)
            {
                <>f__am$cache6 = new Func<NPath, string>(null, (IntPtr) <HashForAllIncludableFilesInDirectories>m__6);
            }
            return string.Concat(directories.Where<NPath>(<>f__am$cache5).OrderBy<NPath, string>(<>f__am$cache6).Select<NPath, string>(new Func<NPath, string>(this, (IntPtr) this.HashOfAllIncludableFilesInDirectory)));
        }

        private string HashOfAllIncludableFilesInDirectory(NPath directory)
        {
            string str;
            <HashOfAllIncludableFilesInDirectory>c__AnonStorey0 storey = new <HashOfAllIncludableFilesInDirectory>c__AnonStorey0 {
                directory = directory
            };
            if (this._cache.TryGetValue(storey.directory.ToString(), ref str))
            {
                return str;
            }
            using (TinyProfiler.Section("HashOfAllIncludableFilesInDirectory", storey.directory.ToString()))
            {
                if (<>f__mg$cache0 == null)
                {
                    <>f__mg$cache0 = new Func<NPath, string>(null, (IntPtr) HashTools.HashOfFile);
                }
                string str3 = string.Concat(this._fileExtensions.SelectMany<string, NPath>(new Func<string, IEnumerable<NPath>>(storey, (IntPtr) this.<>m__0)).Select<NPath, string>(<>f__mg$cache0));
                if (<>f__am$cache2 == null)
                {
                    <>f__am$cache2 = new Func<string, string, string>(null, (IntPtr) <HashOfAllIncludableFilesInDirectory>m__2);
                }
                this._cache.AddOrUpdate(storey.directory.ToString(), str3, <>f__am$cache2);
                return str3;
            }
        }

        public void Initialize(IEnumerable<CppCompilationInstruction> cppSourceCompileInstructions)
        {
            foreach (CppCompilationInstruction instruction in cppSourceCompileInstructions)
            {
                if (<>f__am$cache1 == null)
                {
                    <>f__am$cache1 = new Func<NPath, bool>(null, (IntPtr) <Initialize>m__1);
                }
                foreach (NPath path in instruction.IncludePaths.Where<NPath>(<>f__am$cache1))
                {
                    this.HashOfAllIncludableFilesInDirectory(path);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <HashOfAllIncludableFilesInDirectory>c__AnonStorey0
        {
            private static Func<NPath, string> <>f__am$cache0;
            internal NPath directory;

            internal IEnumerable<NPath> <>m__0(string e)
            {
                if (<>f__am$cache0 == null)
                {
                    <>f__am$cache0 = new Func<NPath, string>(null, (IntPtr) <>m__1);
                }
                return this.directory.Files(e, true).OrderBy<NPath, string>(<>f__am$cache0);
            }

            private static string <>m__1(NPath p) => 
                p.ToString();
        }
    }
}

