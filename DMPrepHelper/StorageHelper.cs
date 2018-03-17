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
using DMPrepHelper.Theme;
using System.Diagnostics;

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
            var file = await GetModifiedFile(fileType);
            string text;
            if (file == null)
            {
                var defaultFile = await GetDefaultFile(fileType);
                text = await FileIO.ReadTextAsync(defaultFile);
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(dataFiles[fileType]);
                await FileIO.WriteTextAsync(file, text);
            }
            else
            {
                text = await FileIO.ReadTextAsync(file);
            }
            localFiles[fileType] = file;
            return text;
        }

        async Task<StorageFile> GetModifiedFile(DataFile fileType)
        {
            if (localFiles.TryGetValue(fileType, out StorageFile savedFile))
            {
                return savedFile;
            }
            var folder = ApplicationData.Current.LocalFolder;
            var filename = dataFiles[fileType];
            StorageFile file = await folder.TryGetItemAsync(filename) as StorageFile;
            return file;

        }

        async Task<StorageFile> GetDefaultFile(DataFile fileType)
        {
            var filename = new Uri(@"ms-appx:////Assets/" + dataFiles[fileType]);
            return await StorageFile.GetFileFromApplicationUriAsync(filename);
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

        public DungeonGenerator GetDungeonGenerator()
        {
            if (dGenerator == null)
            {
                var regions = Deserialize<RegionData>(DataFile.Region);
                var locations = Deserialize<LocationData>(DataFile.Dungeon);
                dGenerator = new DungeonGenerator(regions, locations);
            }
            return dGenerator;
        }

        public string GetConfigText(DataFile type)
        {
            return dataText[type].Result;
        }

        public async Task SaveConfigText(DataFile type, string text)
        {
            var file = localFiles[type];
            await FileIO.WriteTextAsync(file, text, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            return;
        }

        private SettlementGenerator sGenerator;
        private NPCGenerator nPCGenerator;
        private DungeonGenerator dGenerator;

        private async Task<List<T>> DeserializeAsync<T>(DataFile type)
        {
            var data = dataText[type];
            return await data.ContinueWith(x => JsonConvert.DeserializeObject<List<T>>(x.Result));
        }

        private List<T> Deserialize<T>(DataFile type)
        {
            var data = dataText[type];
            if (!data.IsCompletedSuccessfully)
            {
                data.Wait(-1); //will block thread.
            }
            return JsonConvert.DeserializeObject<List<T>>(data.Result);
        }

        private async Task<StorageFile> ChooseFileLocation(Export.ExportTypes type)
        {
            string filename;
            switch (type)
            {
                case Export.ExportTypes.Person:
                    filename = "generated NPC";
                    break;
                case Export.ExportTypes.Dungeon:
                    filename = "generated location";
                    break;
                case Export.ExportTypes.Settlement:
                    filename = "generated settlement";
                    break;
                default:
                    filename = "generated data";
                    break;
            }

            var picker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary,
                SuggestedFileName = filename
            };
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            var file = await picker.PickSaveFileAsync();
            return file;
        }

        public async Task WriteFile<T>(Export.ExportTypes export, IEnumerable<T> data)
        {
            var file = await ChooseFileLocation(export);
            var writer = new Export.ExportWriter(file);
            switch (export)
            {
                case Export.ExportTypes.Dungeon:
                    await WriteDungeon(writer, data as IEnumerable<AdventureData>);
                    break;
                case Export.ExportTypes.Person:
                    await WriteNPC(writer, data as IEnumerable<PersonData>);
                    break;
                case Export.ExportTypes.Settlement:
                    await WriteSettlement(writer, data as IEnumerable<Settlement>);
                    break;
            }
            return;

        }

        private async Task WriteNPC(Export.ExportWriter e, IEnumerable<PersonData> data)
        {
            var exporter = new Export.PersonExporter();
            await e.WriteFile(exporter, data);
        }

        private async Task WriteSettlement(Export.ExportWriter e, IEnumerable<Settlement> data)
        {
            var exporter = new Export.SettlementExporter();
            await e.WriteFile(exporter, data);
        }

        private async Task WriteDungeon(Export.ExportWriter e, IEnumerable<AdventureData> data)
        {
            var exporter = new Export.DungeonExporter();
            await e.WriteFile(exporter, data);
        }

        Dictionary<DataFile, StorageFile> localFiles = new Dictionary<DataFile, StorageFile>();
        Dictionary<DataFile, Task<string>> dataText = new Dictionary<DataFile, Task<string>>();
        ThemeFile theme;
        ThemeReader themeReader = new ThemeReader();
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
