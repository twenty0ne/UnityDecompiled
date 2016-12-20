﻿namespace Unity.IL2CPP.Building
{
    using System;

    public class EmscriptenJavaScriptArchitecture : Architecture
    {
        public override int Bits
        {
            get
            {
                return 0x20;
            }
        }

        public override string Name
        {
            get
            {
                return "EmscriptenJavaScript";
            }
        }
    }
}
