﻿namespace UnityEditor.Utils
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    internal class Program : IDisposable
    {
        public Process _process;
        private ProcessOutputStreamReader _stderr;
        private Stream _stdin;
        private ProcessOutputStreamReader _stdout;

        protected Program()
        {
            this._process = new Process();
        }

        public Program(ProcessStartInfo si) : this()
        {
            this._process.StartInfo = si;
        }

        public void Dispose()
        {
            this.Kill();
            this._process.Dispose();
        }

        public string GetAllOutput()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("stdout:");
            foreach (string str in this.GetStandardOutput())
            {
                builder.AppendLine(str);
            }
            builder.AppendLine("stderr:");
            foreach (string str2 in this.GetErrorOutput())
            {
                builder.AppendLine(str2);
            }
            return builder.ToString();
        }

        public string[] GetErrorOutput() => 
            this._stderr.GetOutput();

        public string GetErrorOutputAsString() => 
            GetOutputAsString(this.GetErrorOutput());

        private static string GetOutputAsString(string[] output)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in output)
            {
                builder.AppendLine(str);
            }
            return builder.ToString();
        }

        public ProcessStartInfo GetProcessStartInfo() => 
            this._process.StartInfo;

        public Stream GetStandardInput() => 
            this._stdin;

        public string[] GetStandardOutput() => 
            this._stdout.GetOutput();

        public string GetStandardOutputAsString() => 
            GetOutputAsString(this.GetStandardOutput());

        public void Kill()
        {
            if (!this.HasExited)
            {
                this._process.Kill();
                this._process.WaitForExit();
            }
        }

        public void LogProcessStartInfo()
        {
            if (this._process != null)
            {
                LogProcessStartInfo(this._process.StartInfo);
            }
            else
            {
                Console.WriteLine("Failed to retrieve process startInfo");
            }
        }

        private static void LogProcessStartInfo(ProcessStartInfo si)
        {
            Console.WriteLine("Filename: " + si.FileName);
            Console.WriteLine("Arguments: " + si.Arguments);
            IEnumerator enumerator = si.EnvironmentVariables.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                    if (current.Key.ToString().StartsWith("MONO"))
                    {
                        Console.WriteLine("{0}: {1}", current.Key, current.Value);
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            int index = si.Arguments.IndexOf("Temp/UnityTempFile");
            Console.WriteLine("index: " + index);
            if (index > 0)
            {
                string path = si.Arguments.Substring(index);
                Console.WriteLine("Responsefile: " + path + " Contents: ");
                Console.WriteLine(File.ReadAllText(path));
            }
        }

        public void Start()
        {
            this._process.StartInfo.RedirectStandardInput = true;
            this._process.StartInfo.RedirectStandardError = true;
            this._process.StartInfo.RedirectStandardOutput = true;
            this._process.StartInfo.UseShellExecute = false;
            this._process.Start();
            this._stdout = new ProcessOutputStreamReader(this._process, this._process.StandardOutput);
            this._stderr = new ProcessOutputStreamReader(this._process, this._process.StandardError);
            this._stdin = this._process.StandardInput.BaseStream;
        }

        public void WaitForExit()
        {
            this._process.WaitForExit();
        }

        public bool WaitForExit(int milliseconds) => 
            this._process.WaitForExit(milliseconds);

        public int ExitCode =>
            this._process.ExitCode;

        public bool HasExited
        {
            get
            {
                if (this._process == null)
                {
                    throw new InvalidOperationException("You cannot call HasExited before calling Start");
                }
                try
                {
                    return this._process.HasExited;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public int Id =>
            this._process.Id;
    }
}

