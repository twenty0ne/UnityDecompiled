﻿namespace Unity.IL2CPP
{
    using System;
    using System.Linq;
    using Unity.IL2CPP.Common.CFG;

    internal static class DeadBlockAnalysis
    {
        public static void MarkBlocksDeadIfNeeded(InstructionBlock[] instructionBlocks)
        {
            if (instructionBlocks.Count<InstructionBlock>() != 1)
            {
                foreach (InstructionBlock block in instructionBlocks)
                {
                    block.MarkIsDead();
                }
                instructionBlocks[0].MarkIsAliveRecursive();
            }
        }
    }
}

