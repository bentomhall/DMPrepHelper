using System;
using System.Collections.Generic;
using System.Text;
using LibGenerator.NPC;
using LibGenerator.Settlement;

namespace libGenerator.Nation
{
    public class PolityData
    {
        public string Name { get; set; } = "";
        public string GovernmentName { get; set; } = "";
        public List<PersonData> Leaders { get; set; } = new List<PersonData>();
        public List<Settlement> MajorSettlements { get; set; } = new List<Settlement>();
        public int Population { get; set; } = 0;
        public Dictionary<string, double> Races { get; set; } = new Dictionary<string, double>();
        public string EconomicSystem { get; set; } = "";
        public string ForeignPolicy { get; set; } = "";
        public string SocialAttitude { get; set; } = "";
        public string LandArea { get; set; } = "";
    }
}
