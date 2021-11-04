using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Windows.Storage;
using System.IO;

namespace DMPrepHelper.Theme
{
    public class ThemeFile : IDisposable
    {
        public ThemeFile(IStorageFile file)
        {
            backingArchive = file;
        }

        private async Task<Stream> ReadBackingArchive()
        {
            return await backingArchive.OpenStreamForReadAsync();
        }

        private async Task<ZipArchive> OpenArchive()
        {
            var istream = await ReadBackingArchive();
            return new ZipArchive(istream);
        }

        public async Task<string> GetText(DataFile file)
        {
            if (textCache.TryGetValue(file, out string cached))
            {
                return cached;
            }
            if (archive == null)
            {
                archive = await OpenArchive();
            }
            var filename = dataFiles[file];
            var entry = archive.GetEntry(filename);
            string output;
            using (StreamReader s = new StreamReader(entry.Open()))
            {
                output = await s.ReadToEndAsync();
                textCache[file] = output;
            }
            return output;
        }

        public async Task WriteText(DataFile file, string text)
        {
            if (archive == null)
            {
                archive = await OpenArchive();
            }
            var filename = dataFiles[file];
            var entry = archive.GetEntry(filename);
            using (StreamWriter s = new StreamWriter(entry.Open()))
            {
                await s.WriteAsync(text);
            }
        }

        private IStorageFile backingArchive;
        private ZipArchive archive;

        public void Dispose()
        {
            archive.Dispose();
        }

        Dictionary<DataFile, string> textCache = new Dictionary<DataFile, string>();

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
            //{DataFile.Rumor, "rumors.json" },
            {DataFile.SettlementRole, "settlementRoles.json" },
            {DataFile.SettlementType, "settlementTypes.json" }
        };
    }
}
