﻿namespace UnityEngine.PlaymodeTestsRunner.TestListBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine.PlaymodeTestsRunner;

    internal abstract class TestListElement
    {
        [CompilerGenerated]
        private static Func<TestListElement, IEnumerable<TestListElement>> <>f__am$cache0;
        protected List<TestListElement> children = new List<TestListElement>();
        public string fullName;
        public string id;
        public string name;
        protected TestListElement parent;
        public TestExecutorBase testExecutor;

        protected TestListElement(string id, string name, string fullName)
        {
            this.id = id;
            this.name = name;
            this.fullName = fullName;
        }

        public void AddChildren(List<TestListElement> children)
        {
            foreach (TestListElement element in children)
            {
                element.parent = this;
            }
            this.children.AddRange(children);
        }

        public void AddChildren(TestListElement child)
        {
            List<TestListElement> children = new List<TestListElement> {
                child
            };
            this.AddChildren(children);
        }

        public IEnumerable<TestListElement> GetChildren() => 
            this.children;

        public virtual IEnumerable<TestListElement> GetFlattenedHierarchy()
        {
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = e => e.GetFlattenedHierarchy();
            }
            return Enumerable.SelectMany<TestListElement, TestListElement>(this.children, <>f__am$cache0);
        }

        public virtual TestExecutorBase GetTestExecutor()
        {
            throw new NotImplementedException();
        }
    }
}

