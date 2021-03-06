﻿namespace SimpleJson
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("simple-json", "1.0.0")]
    internal class JsonObject : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        private readonly Dictionary<string, object> _members;

        public JsonObject()
        {
            this._members = new Dictionary<string, object>();
        }

        public JsonObject(IEqualityComparer<string> comparer)
        {
            this._members = new Dictionary<string, object>(comparer);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this._members.Add(item.Key, item.Value);
        }

        public void Add(string key, object value)
        {
            this._members.Add(key, value);
        }

        public void Clear()
        {
            this._members.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item) => 
            (this._members.ContainsKey(item.Key) && (this._members[item.Key] == item.Value));

        public bool ContainsKey(string key) => 
            this._members.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            int count = this.Count;
            foreach (KeyValuePair<string, object> pair in this)
            {
                array[arrayIndex++] = pair;
                if (--count <= 0)
                {
                    break;
                }
            }
        }

        internal static object GetAtIndex(IDictionary<string, object> obj, int index)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (index >= obj.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            int num = 0;
            foreach (KeyValuePair<string, object> pair in obj)
            {
                if (num++ == index)
                {
                    return pair.Value;
                }
            }
            return null;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => 
            this._members.GetEnumerator();

        public bool Remove(KeyValuePair<string, object> item) => 
            this._members.Remove(item.Key);

        public bool Remove(string key) => 
            this._members.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => 
            this._members.GetEnumerator();

        public override string ToString() => 
            SimpleJson.SimpleJson.SerializeObject(this);

        public bool TryGetValue(string key, out object value) => 
            this._members.TryGetValue(key, out value);

        public int Count =>
            this._members.Count;

        public bool IsReadOnly =>
            false;

        public object this[int index] =>
            GetAtIndex(this._members, index);

        public object this[string key]
        {
            get => 
                this._members[key];
            set
            {
                this._members[key] = value;
            }
        }

        public ICollection<string> Keys =>
            this._members.Keys;

        public ICollection<object> Values =>
            this._members.Values;
    }
}

