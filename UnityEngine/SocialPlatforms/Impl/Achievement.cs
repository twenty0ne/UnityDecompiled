﻿namespace UnityEngine.SocialPlatforms.Impl
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine.SocialPlatforms;

    public class Achievement : IAchievement
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <id>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <percentCompleted>k__BackingField;
        private bool m_Completed;
        private bool m_Hidden;
        private DateTime m_LastReportedDate;

        public Achievement() : this("unknown", 0.0)
        {
        }

        public Achievement(string id, double percent)
        {
            this.id = id;
            this.percentCompleted = percent;
            this.m_Hidden = false;
            this.m_Completed = false;
            this.m_LastReportedDate = DateTime.MinValue;
        }

        public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
        {
            this.id = id;
            this.percentCompleted = percentCompleted;
            this.m_Completed = completed;
            this.m_Hidden = hidden;
            this.m_LastReportedDate = lastReportedDate;
        }

        public void ReportProgress(Action<bool> callback)
        {
            ActivePlatform.Instance.ReportProgress(this.id, this.percentCompleted, callback);
        }

        public void SetCompleted(bool value)
        {
            this.m_Completed = value;
        }

        public void SetHidden(bool value)
        {
            this.m_Hidden = value;
        }

        public void SetLastReportedDate(DateTime date)
        {
            this.m_LastReportedDate = date;
        }

        public override string ToString()
        {
            object[] objArray1 = new object[] { this.id, " - ", this.percentCompleted, " - ", this.completed, " - ", this.hidden, " - ", this.lastReportedDate };
            return string.Concat(objArray1);
        }

        public bool completed
        {
            get
            {
                return this.m_Completed;
            }
        }

        public bool hidden
        {
            get
            {
                return this.m_Hidden;
            }
        }

        public string id { get; set; }

        public DateTime lastReportedDate
        {
            get
            {
                return this.m_LastReportedDate;
            }
        }

        public double percentCompleted { get; set; }
    }
}
