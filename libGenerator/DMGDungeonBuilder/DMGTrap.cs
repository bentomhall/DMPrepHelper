using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    class DMGTrap
    {
        public string GetTrap(Random r)
        {
            return severity.Match(r.NextDouble()) + " " + effects.Match(r.NextDouble()) + " triggered when " + trigger.Match(r.NextDouble());
        }

        private WeightedChoiceSet<string> trigger = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Stepped on (floor, stairs", 1 },
            {"Moved through (doorway, hallway)", 1 },
            {"Touched (doorknob, statue)", 1 },
            {"Opened (door, treasure chest)", 1 },
            {"Looked at (mural, arcane symbol)", 1 },
            {"Moved (cart, stone block)", 1 }
        });

        private WeightedChoiceSet<string> severity = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Setback", 2 },
            {"Dangerous", 3 },
            {"Deadly", 1 }
        });

        private WeightedChoiceSet<string> effects = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Magic missiles", 4 },
            {"Collapsing staircase creates a ramp that deposits characters into a pit at its lower end", 3 },
            {"Ceiling block falls, or entire ceiling collapses", 3 },
            {"Ceiling lowers slowly in locked room", 2 },
            {"Chute opens in floor", 2 },
            {"Clanging noise attracks nearby monsters", 2 },
            {"Touching an object triggers a disintegrate spell", 3 },
            {"Door or other object is coated with contact poison", 4 },
            {"Fire shoots out from a wall, floor, or object", 4 },
            {"Touching an object triggers a flesh to stone spell", 3 },
            {"Floor collapses or is an illusion", 3 },
            {"Vent releases gas: blinding, acidic, obscuring, paralyzing, poisonous, or sleep-inducing", 3 },
            {"Floor tiles are electrified", 3 },
            {"Glyph of warding", 4 },
            {"Huge wheeled statue rolls down corridor", 3 },
            {"Lightning bolt shoots from wall or object", 3 },
            {"Locked room floods with water or acid", 3 },
            {"Darts shoot out of an opened chest", 4 },
            {"A weapon, suit of armor, or rug animates and attacks when touched", 3 },
            {"Pendulum, either bladed or weighted as a maul, swings across the room or hall", 3 },
            {"Hidden pit opens beneath characters (25 percent chance that a black pudding or gelatinous cube fills the bottom of the pit", 5 },
            {"Hidden pit floods with acid or fire", 3 },
            {"Locking pit floods with water", 3 },
            {"Scything blade emerges from wall or object", 4 },
            {"Spears (possibly poisoned) spring out", 4 },
            {"Brittle stairs colapse over spikes", 3 },
            {"Thunderwave knocks characters into a pit or spikes", 4 },
            {"Steel or stone jaws restrain a character", 3 },
            {"Stone block smashes across hallway", 3},
            {"Symbol", 3 },
            {"Walls slide together", 3 }
        });
    }
}
