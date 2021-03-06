﻿namespace UnityEditorInternal.VersionControl
{
    using System;
    using UnityEditor;
    using UnityEditor.VersionControl;
    using UnityEngine;

    public class ListItem
    {
        private bool accept;
        private string[] actions;
        private bool dummy;
        private bool exclusive;
        private bool expanded;
        private ListItem firstChild;
        private bool hidden;
        private Texture icon;
        private int identifier;
        private int indent;
        private object item;
        private ListItem lastChild;
        private string name;
        private ListItem next;
        private ListItem parent;
        private ListItem prev;

        public ListItem()
        {
            this.Clear();
            this.identifier = -1;
        }

        public void Add(ListItem listItem)
        {
            listItem.parent = this;
            listItem.next = null;
            listItem.prev = this.lastChild;
            listItem.Indent = this.indent + 1;
            if (this.firstChild == null)
            {
                this.firstChild = listItem;
            }
            if (this.lastChild != null)
            {
                this.lastChild.next = listItem;
            }
            this.lastChild = listItem;
        }

        public void Clear()
        {
            this.parent = null;
            this.firstChild = null;
            this.lastChild = null;
            this.prev = null;
            this.next = null;
            this.icon = null;
            this.name = string.Empty;
            this.indent = 0;
            this.expanded = false;
            this.exclusive = false;
            this.dummy = false;
            this.accept = false;
            this.item = null;
        }

        ~ListItem()
        {
            this.Clear();
        }

        public ListItem FindWithIdentifierRecurse(int inIdentifier)
        {
            if (this.Identifier == inIdentifier)
            {
                return this;
            }
            for (ListItem item2 = this.firstChild; item2 != null; item2 = item2.next)
            {
                ListItem item3 = item2.FindWithIdentifierRecurse(inIdentifier);
                if (item3 != null)
                {
                    return item3;
                }
            }
            return null;
        }

        public bool HasPath()
        {
            UnityEditor.VersionControl.Asset item = this.item as UnityEditor.VersionControl.Asset;
            return ((item != null) && (item.path != null));
        }

        public bool IsChildOf(ListItem listItem)
        {
            for (ListItem item = this.Parent; item != null; item = item.Parent)
            {
                if (item == listItem)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(ListItem listItem)
        {
            if (listItem == null)
            {
                return false;
            }
            if (listItem.parent != this)
            {
                return false;
            }
            if (listItem == this.firstChild)
            {
                this.firstChild = listItem.next;
            }
            if (listItem == this.lastChild)
            {
                this.lastChild = listItem.prev;
            }
            if (listItem.prev != null)
            {
                listItem.prev.next = listItem.next;
            }
            if (listItem.next != null)
            {
                listItem.next.prev = listItem.prev;
            }
            listItem.parent = null;
            listItem.prev = null;
            listItem.next = null;
            return true;
        }

        public void RemoveAll()
        {
            for (ListItem item = this.firstChild; item != null; item = item.next)
            {
                item.parent = null;
            }
            this.firstChild = null;
            this.lastChild = null;
        }

        private void SetIntent(ListItem listItem, int indent)
        {
            listItem.indent = indent;
            for (ListItem item = listItem.FirstChild; item != null; item = item.Next)
            {
                this.SetIntent(item, indent + 1);
            }
        }

        public string[] Actions
        {
            get => 
                this.actions;
            set
            {
                this.actions = value;
            }
        }

        public UnityEditor.VersionControl.Asset Asset
        {
            get => 
                (this.item as UnityEditor.VersionControl.Asset);
            set
            {
                this.item = value;
            }
        }

        public bool CanAccept
        {
            get => 
                this.accept;
            set
            {
                this.accept = value;
            }
        }

        public bool CanExpand =>
            ((this.item is ChangeSet) || this.HasChildren);

        public ChangeSet Change
        {
            get => 
                (this.item as ChangeSet);
            set
            {
                this.item = value;
            }
        }

        public int ChildCount
        {
            get
            {
                int num = 0;
                for (ListItem item = this.firstChild; item != null; item = item.next)
                {
                    num++;
                }
                return num;
            }
        }

        public bool Dummy
        {
            get => 
                this.dummy;
            set
            {
                this.dummy = value;
            }
        }

        public bool Exclusive
        {
            get => 
                this.exclusive;
            set
            {
                this.exclusive = value;
            }
        }

        public bool Expanded
        {
            get => 
                this.expanded;
            set
            {
                this.expanded = value;
            }
        }

        public ListItem FirstChild =>
            this.firstChild;

        public bool HasActions =>
            ((this.actions != null) && (this.actions.Length != 0));

        public bool HasChildren =>
            (this.FirstChild != null);

        public bool Hidden
        {
            get => 
                this.hidden;
            set
            {
                this.hidden = value;
            }
        }

        public Texture Icon
        {
            get
            {
                UnityEditor.VersionControl.Asset item = this.item as UnityEditor.VersionControl.Asset;
                if ((this.icon == null) && (item != null))
                {
                    return AssetDatabase.GetCachedIcon(item.path);
                }
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }

        public int Identifier
        {
            get
            {
                if (this.identifier == -1)
                {
                    this.identifier = Provider.GenerateID();
                }
                return this.identifier;
            }
        }

        public int Indent
        {
            get => 
                this.indent;
            set
            {
                this.SetIntent(this, value);
            }
        }

        public object Item
        {
            get => 
                this.item;
            set
            {
                this.item = value;
            }
        }

        public ListItem LastChild =>
            this.lastChild;

        public string Name
        {
            get => 
                this.name;
            set
            {
                this.name = value;
            }
        }

        public ListItem Next =>
            this.next;

        public ListItem NextOpen
        {
            get
            {
                if (this.Expanded && (this.firstChild != null))
                {
                    return this.firstChild;
                }
                if (this.next != null)
                {
                    return this.next;
                }
                for (ListItem item2 = this.parent; item2 != null; item2 = item2.parent)
                {
                    if (item2.Next != null)
                    {
                        return item2.Next;
                    }
                }
                return null;
            }
        }

        public ListItem NextOpenSkip
        {
            get
            {
                ListItem nextOpen = this.NextOpen;
                while ((nextOpen != null) && (nextOpen.Dummy || nextOpen.Hidden))
                {
                    nextOpen = nextOpen.NextOpen;
                }
                return nextOpen;
            }
        }

        public ListItem NextOpenVisible
        {
            get
            {
                ListItem nextOpen = this.NextOpen;
                while ((nextOpen != null) && nextOpen.Hidden)
                {
                    nextOpen = nextOpen.NextOpen;
                }
                return nextOpen;
            }
        }

        public int OpenCount
        {
            get
            {
                if (!this.Expanded)
                {
                    return 0;
                }
                int num2 = 0;
                for (ListItem item = this.firstChild; item != null; item = item.next)
                {
                    if (!item.Hidden)
                    {
                        num2++;
                        num2 += item.OpenCount;
                    }
                }
                return num2;
            }
        }

        public ListItem Parent =>
            this.parent;

        public ListItem Prev =>
            this.prev;

        public ListItem PrevOpen
        {
            get
            {
                for (ListItem item = this.prev; item != null; item = item.lastChild)
                {
                    if ((item.lastChild == null) || !item.Expanded)
                    {
                        return item;
                    }
                }
                if ((this.parent != null) && (this.parent.parent != null))
                {
                    return this.parent;
                }
                return null;
            }
        }

        public ListItem PrevOpenSkip
        {
            get
            {
                ListItem prevOpen = this.PrevOpen;
                while ((prevOpen != null) && (prevOpen.Dummy || prevOpen.Hidden))
                {
                    prevOpen = prevOpen.PrevOpen;
                }
                return prevOpen;
            }
        }

        public ListItem PrevOpenVisible
        {
            get
            {
                ListItem prevOpen = this.PrevOpen;
                while ((prevOpen != null) && prevOpen.Hidden)
                {
                    prevOpen = prevOpen.PrevOpen;
                }
                return prevOpen;
            }
        }
    }
}

