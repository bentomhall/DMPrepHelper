using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibGenerator.Core;

namespace LibGenerator.NPC
{
    public class NPCGenerator
    {

        public NPCGenerator(Task<List<CultureData>> c, Task<List<NameData>> n, Task<List<string>> personalities, List<string> proffs, List<NationData> nations)
        {
            asyncCulture = c;
            asyncNames = n;
            asyncPersonalities = personalities;
            professions = proffs;
            this.nations = nations;
        }

        public PersonData GenerateNPC(string nation)
        {
            
            if (names == null)
            {
                if (!asyncNames.IsCompleted) { asyncNames.Wait(); }
                names = asyncNames.Result;
            }
            if (cultures == null)
            {
                if (!asyncCulture.IsCompleted) { asyncCulture.Wait(); }
                cultures = asyncCulture.Result;
            }
            if (personalities == null)
            {
                if (!asyncPersonalities.IsCompleted) { asyncPersonalities.Wait(); }
                personalities = asyncPersonalities.Result;
            }
            var race = nations.First(x => x.Name == nation).GetRace();

            var culture = cultures.First(x => x.Matches(race, nation));
            var gender = culture.GetGender();
            var religion = culture.GetReligion();
            var subrace = culture.GetSubrace();
            var nameList = names.First(x => x.Culture.ToLower() == culture.Culture.ToLower());
            var name = nameList.GetName(gender);
            var age = r.Choice(new List<string>() { "young", "middle-aged", "old", "ancient" });
            return new PersonData()
            {
                Name = name,
                Nation = nation,
                Culture = culture.Culture,
                Gender = gender,
                Religion = religion,
                Race = race,
                Subrace = subrace,
                Personality = r.Choice(personalities),
                Profession = r.Choice(professions),
                Age = age
            };
        }

        public PersonData GenerateNPCforCity(NationData nation)
        {
            
            if (names == null)
            {
                if (!asyncNames.IsCompleted) { asyncNames.Wait(); }
                names = asyncNames.Result;
            }
            if (cultures == null)
            {
                if (!asyncCulture.IsCompleted) { asyncCulture.Wait(); }
                cultures = asyncCulture.Result;
            }
            if (personalities == null)
            {
                if (!asyncPersonalities.IsCompleted) { asyncPersonalities.Wait(); }
                personalities = asyncPersonalities.Result;
            }
            var race = nation.GetRace();
            var culture = cultures.First(x => x.Matches(race, nation.Name));
            var gender = culture.GetGender();
            var religion = culture.GetReligion();
            var subrace = culture.GetSubrace();
            var nameList = names.First(x => x.Culture == culture.Culture);
            var name = nameList.GetName(gender);
            var age = r.Choice(new List<string>() { "young", "middle-aged", "old", "ancient" });
            return new PersonData()
            {
                Name = name,
                Nation = nation.Name,
                Culture = culture.Culture,
                Gender = gender,
                Religion = religion,
                Race = race,
                Subrace = subrace,
                Personality = r.Choice(personalities),
                Profession = r.Choice(professions),
                Age = age
            };
        }

        public IEnumerable<string> GetValidNations()
        {
            return nations.Select(x => x.Name);
        }

        private List<PersonData> npcs = new List<PersonData>();
        private List<CultureData> cultures;
        private Task<List<CultureData>> asyncCulture;
        private Task<List<NameData>> asyncNames;
        private Task<List<string>> asyncPersonalities;
        private List<NameData> names;
        private List<string> personalities;
        private List<string> professions;
        private List<NationData> nations;
        private Random r = new Random();
    }
}
