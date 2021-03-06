﻿namespace UnityEditor.PlaymodeTestsRunner
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.PlaymodeTestsRunner;

    internal class ResultSummarizer
    {
        private TimeSpan m_Duration = TimeSpan.FromSeconds(0.0);
        private int m_ErrorCount = -1;
        private int m_FailureCount;
        private int m_IgnoreCount = -1;
        private int m_InconclusiveCount = -1;
        private int m_NotRunnable = -1;
        private int m_ResultCount;
        private int m_SkipCount;
        private int m_SuccessCount;
        private int m_TestsRun;

        public ResultSummarizer(IEnumerable<TestResult> results)
        {
            foreach (TestResult result in results)
            {
                this.Summarize(result);
            }
        }

        public void Summarize(TestResult result)
        {
            this.m_Duration += TimeSpan.FromSeconds((double) result.duration);
            this.m_ResultCount++;
            if (result.resultType == TestResult.ResultType.NotRun)
            {
                this.m_SkipCount++;
            }
            else
            {
                TestResult.ResultType resultType = result.resultType;
                if (resultType == TestResult.ResultType.Success)
                {
                    this.m_SuccessCount++;
                    this.m_TestsRun++;
                }
                else if (resultType == TestResult.ResultType.Failed)
                {
                    this.m_FailureCount++;
                    this.m_TestsRun++;
                }
                else
                {
                    this.m_SkipCount++;
                }
            }
        }

        public double duration =>
            this.m_Duration.TotalSeconds;

        public int errors =>
            this.m_ErrorCount;

        public int failures =>
            this.m_FailureCount;

        public int ignored =>
            this.m_IgnoreCount;

        public int inconclusive =>
            this.m_InconclusiveCount;

        public int notRunnable =>
            this.m_NotRunnable;

        public int Passed =>
            this.m_SuccessCount;

        public int ResultCount =>
            this.m_ResultCount;

        public int Skipped =>
            this.m_SkipCount;

        public bool success =>
            (this.m_FailureCount == 0);

        public int testsNotRun =>
            ((this.m_SkipCount + this.m_IgnoreCount) + this.m_NotRunnable);

        public int TestsRun =>
            this.m_TestsRun;
    }
}

