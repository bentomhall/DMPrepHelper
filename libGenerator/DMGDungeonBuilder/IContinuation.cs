using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public interface IContinuation
    {
        ContinuationType FurtherContinuation { get; }
        string Description { get; }
    }

    public class Terminus : IContinuation
    {
        public Terminus(string description)
        {

            FurtherContinuation = ContinuationType.Terminus;
            Description = description;
        }
        public ContinuationType FurtherContinuation { get; }
        public string Description { get; }
    }

    public enum ContinuationType
    {
        Chamber,
        Passage,
        Door,
        Terminus,
        Mixed
    }
}
