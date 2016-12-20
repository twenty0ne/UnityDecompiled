﻿namespace Unity.IL2CPP.Common
{
    using System;

    public class XboxOneRuntimePlatform : RuntimePlatform
    {
        public override bool ExecutesOnHostMachine
        {
            get
            {
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "XboxOne";
            }
        }
    }
}
