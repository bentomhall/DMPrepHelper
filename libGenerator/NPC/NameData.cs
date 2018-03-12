using System;
using System.Collections.Generic;
using System.Text;
using LibGenerator.Core;

namespace LibGenerator.NPC
{
    public class NameData
    {
        public string Culture { get; set; }
        public List<string> Male { get; set; }
        public List<string> Female { get; set; }
        public string Combiner { get; set; }
        public List<string> Family { get; set; }
        public string Base { get; set; }

        private Random r = new Random();

        private string RandomName(string gender)
        {
            switch (gender)
            {
                case "male":
                    return r.Choice(Male);
                case "female":
                    return r.Choice(Female);
                default:
                    return r.Choice(Male);
            }
        }

        public string GetName(string gender)
        {
            var given = RandomName(gender);
            return $"{given} {Combiner} {r.Choice(Family)}";
        }


    }
}
