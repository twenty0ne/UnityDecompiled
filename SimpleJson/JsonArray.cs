﻿namespace SimpleJson
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("simple-json", "1.0.0")]
    internal class JsonArray : List<object>
    {
        public JsonArray()
        {
        }

        public JsonArray(int capacity) : base(capacity)
        {
        }

        public override string ToString()
        {
            string str;
            string text1 = SimpleJson.SimpleJson.SerializeObject(this);
            if (text1 != null)
            {
                str = text1;
            }
            else
            {
                str = string.Empty;
            }
            return str;
        }
    }
}
