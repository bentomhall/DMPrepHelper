using System;
using System.Collections.Generic;

namespace libGenerator.Map
{
    public class TerrainData
    {
        public string Name { get; set; } = "";
        public int TrackDC { get; set; } = 10;
        public int FoodDC { get; set; } = 10;
        public int ThreatIndex
        {
            get => threat;
            set
            {
                if (value < 0 || value > 4)
                {
                    throw new ArgumentException("Threat values must be between 0 and 4 inclusive.");
                }
                threat = value;
            }
        }

        public string ThreatDescription
        {
            get
            {
                return threatDescriptions[threat];
            }

        }

        private readonly Dictionary<int, string> threatDescriptions = new Dictionary<int, string> 
        {   { 0, "No significant threat" }, 
            { 1, "Scattered and weak threats" }, 
            { 2, "Frequent but weak threats" }, 
            { 3, "Scattered but strong threats" }, 
            { 4, "Frequent and strong threats" } 
        };

        private int threat = 0;
    }
}