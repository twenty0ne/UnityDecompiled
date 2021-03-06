﻿namespace UnityEngine
{
    using System;

    public sealed class AndroidJavaException : Exception
    {
        private string mJavaStackTrace;

        internal AndroidJavaException(string message, string javaStackTrace) : base(message)
        {
            this.mJavaStackTrace = javaStackTrace;
        }

        public override string StackTrace =>
            (this.mJavaStackTrace + base.StackTrace);
    }
}

