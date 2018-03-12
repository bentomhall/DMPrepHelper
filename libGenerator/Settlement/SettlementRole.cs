using System.Collections.Generic;

namespace LibGenerator.Settlement
{
    public class SettlementRole
    {
        public string Name { get; set; }
        public List<Specialty> Specialties { get; set; }
    }

    public class Specialty
    {
        public string Name { get; set; }
        public int Modifier { get; set; }
    }
}