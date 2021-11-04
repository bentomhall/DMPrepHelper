using System.Collections.Generic;

namespace libGenerator.Settlement
{
    public class SettlementRole
    {
        public string Name { get; set; } = "";
        public List<Specialty> Specialties { get; set; } = new List<Specialty>();
    }

    public class Specialty
    {
        public string Name { get; set; } = "";
        public int Modifier { get; set; } = 0;
    }
}