using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    class DMGContents
    {
        public DMGContents()
        {
            var r = random.NextDouble();
            Contents = contents.Match(r);
            Hazard = "";
            Trap = "";
            Obstacle = "";
            if (MathUtil.isBetween(r, 51, 58) || MathUtil.isBetween(r, 89, 94))
            {
                Hazard = hazards.Match(random.NextDouble());
            }
            else if (MathUtil.isBetween(r, 59, 63))
            {
                Obstacle = obstacles.Match(random.NextDouble());
            }
            else if (MathUtil.isBetween(r, 64, 76))
            {
                Trap = trapGenerator.GetTrap(random);
            } 
            else if (MathUtil.isBetween(r, 77, 80))
            {
                //tricks are not yet implemented;
            }
            
        }

        public string Contents { get; private set; }

        public string Hazard { get; private set; }

        public string Obstacle { get; private set; }

        public string Trap { get; private set; }

        private readonly Random random = new Random();

        private readonly DMGTrap trapGenerator = new DMGTrap();

        private WeightedChoiceSet<string> obstacles = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Antilife aura with radius 1d10x10 ft, while in aura, living creatures cannot regain hit points", 1 },
            {"Battering winds reduce speed by half, impose disadvantage on ranged attack rolls", 1 },
            {"Blade barrier blocks passage", 1 },
            {"Cave-in", 5 },
            {"Chasm 1d4 x 10 ft wide and 2d6 x 10 ft deep, possibly connected to other levels of the dungeon", 4 },
            {"Flooding leaves 2d10 ft of water in the area, create new upward-sloping passages, raised floors, or rising stairs to contain the water", 2 },
            {"Lava flows through the area (50 percent chance of a stone bridge crossing it)", 1 },
            {"Overgrown mushrooms block progress and must be hacked down (25 percent chance of a mold or fungus dungeon hazard hidden among them", 1 },
            {"Poisonous gas (deals 1d6 poison damage per minute of exposure", 1 },
            {"Reverse gravity effect causes creatures to fall toward the ceiling", 1 },
            {"Wall of fire blocks passage", 1 },
            {"Wall of force blocks passage", 1 }
        });

        private WeightedChoiceSet<string> hazards = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Brown mold", 3 },
            {"Green slime", 5 },
            {"Shrieker", 2 },
            {"Spiderwebs", 5 },
            {"Violet fungus", 2 },
            {"Yellow mold", 3 }
        });

        private WeightedChoiceSet<string> contents = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Monster (dominant inhabitant)", 8 },
            {"Monster (dominant inhabitant) with treasure", 7 },
            {"Monster (pet or allied creature)", 12 },
            {"Monster (pet or allied creature) guarding treasure", 6 },
            {"Monster (random creature)", 9 },
            {"Monster (random creature) with treasure", 8 },
            {"Dungeon hazard with incidental treasure", 8 },
            {"Obstacle", 5 },
            {"Trap", 10 },
            {"Trap protecting treasure", 3 },
            {"Trick", 4 },
            {"Empty room", 8 },
            {"Empty room with dungeon hazard", 6 },
            {"Empty room with treasure", 6 }
        });
    }
}
