using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using LibGenerator.NPC;
using LibGenerator.Dungeon;
using LibGenerator.Settlement;

namespace DMPrepHelper
{
    public class StorageHelper
    {
        public StorageHelper()
        {
            foreach (DataFile value in Enum.GetValues(typeof(DataFile)))
            {
                dataText[value] = GetConfigData(value);
            }
        }

        async Task<string> GetConfigData(DataFile fileType)
        {
            //var localFolder = ApplicationData.Current.LocalFolder;
            var filename = new Uri("ms-appx:////Assets/" + dataFiles[fileType]);
            var file = await StorageFile.GetFileFromApplicationUriAsync(filename);
            return await FileIO.ReadTextAsync(file);
        }

        public NPCGenerator GetNPCGenerator()
        {
            if (nPCGenerator == null) { 
                var culture = DeserializeAsync<CultureData>(DataFile.Race);
                var names = DeserializeAsync<NameData>(DataFile.NpcName);
                var personalities = DeserializeAsync<string>(DataFile.Personality);
                var professions = Deserialize<string>(DataFile.Profession);
                var nations = Deserialize<NationData>(DataFile.Nation);
                nPCGenerator = new NPCGenerator(culture, names, personalities, professions, nations);
            }
            return nPCGenerator;
        }

        public SettlementGenerator GetSettlementGenerator()
        {
            if (sGenerator == null)
            {
                var npc = GetNPCGenerator();
                var cities = Deserialize<CityData>(DataFile.City);
                var items = Deserialize<ItemData>(DataFile.ItemRank);
                var settlements = Deserialize<SettlementData>(DataFile.SettlementType);
                var roles = Deserialize<SettlementRole>(DataFile.SettlementRole);
                sGenerator = new SettlementGenerator(cities, items, settlements, roles, npc);
            }

            return sGenerator;
        }

        public string GetConfigText(DataFile type)
        {
            return dataText[type].Result;
        }

        public async Task SaveConfigText(DataFile type, string text)
        {
            //var filename = new Uri("ms-appx:////Assets/" + dataFiles[type]);
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("Assets/" + dataFiles[type]);
            await FileIO.WriteTextAsync(file, text);
            return;
        }

        private SettlementGenerator sGenerator;
        private NPCGenerator nPCGenerator;

        private Task<List<T>> DeserializeAsync<T>(DataFile type)
        {
            var data = dataText[type];
            return new Task<List<T>>(() => JsonConvert.DeserializeObject<List<T>>(data.Result));
        }

        private List<T> Deserialize<T>(DataFile type)
        {
            var data = dataText[type];
            if (!data.IsCompleted)
            {
                data.Wait();
            }
            return JsonConvert.DeserializeObject<List<T>>(data.Result);
        }


        Dictionary<DataFile, Task<string>> dataText = new Dictionary<DataFile, Task<string>>();

        Dictionary<DataFile, string> dataFiles = new Dictionary<DataFile, string>
        {
            {DataFile.City, "cityData.json" },
            {DataFile.Dungeon, "dungeonData.json" },
            {DataFile.ItemRank, "itemRanks.json" },
            {DataFile.Nation, "nations.json" },
            {DataFile.NpcName, "npcNames.json" },
            {DataFile.Personality, "personality.json" },
            {DataFile.Profession, "professions.json" },
            {DataFile.Race, "races.json" },
            {DataFile.Region, "regionData.json" },
            {DataFile.Rumor, "rumors.json" },
            {DataFile.SettlementRole, "settlementRoles.json" },
            {DataFile.SettlementType, "settlementTypes.json" }
        };
    }

    public enum DataFile
    {
        City,
        Dungeon,
        ItemRank,
        NpcName,
        Nation,
        Personality,
        Profession,
        Race,
        Region,
        Rumor,
        SettlementRole,
        SettlementType

    }
}
