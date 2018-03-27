using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGenerator.Core;

namespace LibGenerator.NPC
{
    public class CultureData
    {
        public string Race { get; set; } = "";
        public string Nation { get; set; } = "";
        public List<string> Culture { get; set; } = new List<string>();
        public Dictionary<string, double> Gender { get; set; } = new Dictionary<string, double>() { { "male", 0.5 }, { "female", 0.5 } };
        public List<string> Religiosity { get; set; } = new List<string>();
        public Dictionary<string, double> Subrace { get; set; } = new Dictionary<string, double>() { { "N/A", 1.0} };

        private WeightedChoiceSet genders;
        private WeightedChoiceSet subraces;

        public bool Matches(string race, string nation)
        {
            return (race.ToLower() == Race.ToLower() && nation.ToLower() == Nation.ToLower());
        }

        public string GetGender()
        {
            if (genders == null) 
            {
                genders = new WeightedChoiceSet(Gender);    
            }
            var roll = r.NextDouble();
            return genders.Match(roll);
        }

        public string GetCulture()
        {
            return r.Choice(Culture);
        }

        public string GetReligion()
        {
            return r.Choice(Religiosity);
        }

        public string GetSubrace()
        {
            if (subraces == null)
            {
                subraces = new WeightedChoiceSet(Subrace);
            }
            if (Subrace.Count == 0)
            {
                return "";
            }
            var roll = r.NextDouble();
            var subrace = subraces.Match(roll);
            return subrace == "N/A"? "": subrace;
        }

        private Random r = new Random();
    }
}
