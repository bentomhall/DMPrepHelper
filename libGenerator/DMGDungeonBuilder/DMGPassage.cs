using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public class DMGPassage : IContinuation
    {

        public DMGPassage(string location)
        {
            var random = new Random();
            var x = random.Next(1, 13);
            if (x == 11)
            {
                Description = $"A passage {location} leading to a dead end.";
                FurtherContinuation = ContinuationType.Terminus;
            }
            else if (x == 12)
            {
                Description = $"A door {location} that opens into a dead-end passage.";
                FurtherContinuation = ContinuationType.Terminus;
            }
            else if (x >= 6 )
            {
                Description = $"A door {location} that opens into another chamber.";
                FurtherContinuation = ContinuationType.Door;
            }
            else
            {
                Description = $"A passage {location} that leads to another chamber.";
                FurtherContinuation = ContinuationType.Passage;
            }
            contents = new DMGContents();

            if (Hazards.Length != 0) { Description += $" Hazard: {Hazards}"; }
            if (Obstacles.Length != 0) { Description += $" Obstacle: {Obstacles}"; }
            if (Traps.Length != 0) { Description += $" Trap: {Traps}"; }
        }

        private readonly DMGContents contents;

        public string Hazards { get => contents.Hazard; }
        public string Obstacles { get => contents.Obstacle; }
        public string Traps { get => contents.Trap; }

        public ContinuationType FurtherContinuation { get; }
        public string Description { get; }

    }
}
