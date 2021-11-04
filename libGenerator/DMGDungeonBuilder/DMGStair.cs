using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public class DMGStair : IContinuation
    {
        public DMGStair(string description, bool followStairs)
        {
            Description = description;
            if (!followStairs) { FurtherContinuation = ContinuationType.Terminus; }
        }
        public ContinuationType FurtherContinuation { get; }
        public string Description { get; }
    }
}
