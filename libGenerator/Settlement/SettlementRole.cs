using System.Collections.Generic;

namespace LibGenerator.Settlement
{
    public class SettlementRole
    {
        public string Name { get; set; } = "";
        public List<Specialty> Specialties { get; set; } = new List<Specialty>() { new Specialty { Name="Specialty", Modifier=0} };
    }

    public class Specialty
    {
        public string Name { get; set; } = "";
        public int Modifier { get; set; } = 0;
    }
}