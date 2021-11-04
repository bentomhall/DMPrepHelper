using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Newtonsoft.Json;

namespace DMPrepHelper.Theme
{
    public class ThemeReader
    {
        public ThemeReader()
        {
            rootFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<ThemeFile> LoadTheme(string name, bool isRegistered)
        {
            StorageFile file;
            if (!isRegistered)
            {
                file = await GetThemeFile(name);
            }
            else
            {
                file = await GetRegisteredTheme(name);
            }
            return new ThemeFile(file);
        }

        private async Task<StorageFile> GetThemeFile(string name)
        {
            var picker = new FileOpenPicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                ViewMode = PickerViewMode.List
            };
            picker.FileTypeFilter.Add(".setting");
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                throw new OperationCanceledException("File Picking Canceled");
            }
            var folder = dataFolder ?? await GetDataDirectory();
            await RegisterTheme(name, file.Name);  
            return await file.CopyAsync(folder);
        }

        private async Task<StorageFolder> GetDataDirectory()
        {
            dataFolder = await rootFolder.TryGetItemAsync(ThemeFolder) as StorageFolder;
            if (dataFolder == null)
            {
                dataFolder = await rootFolder.CreateFolderAsync(ThemeFolder);
            }
            return dataFolder;
        }

        private async Task RegisterTheme(string name, string filename)
        {
            var themeRegistry = await dataFolder.GetFileAsync("themes.json");
            var entry = new ThemeRegistryEntry() { Name = name, Filename = filename };
            var jsonText = JsonConvert.SerializeObject(entry);
            await FileIO.AppendTextAsync(themeRegistry, jsonText);
        }

        private async Task<StorageFile> GetRegisteredTheme(string name)
        {
            if (registryCache.TryGetValue(name, out ThemeRegistryEntry entry))
            {
                return await (dataFolder ?? await GetDataDirectory()).GetFileAsync(entry.Filename);
            }
            else
            {
                var registry = await (dataFolder ?? await GetDataDirectory()).GetFileAsync("themes.json");
                var data = await FileIO.ReadTextAsync(registry);
                var registryEntries = JsonConvert.DeserializeObject<IEnumerable<ThemeRegistryEntry>>(data);
                var filename = registryEntries.First(x => x.Name == name).Filename;
                return await dataFolder.GetFileAsync(filename);
            }
        }

        Dictionary<string, ThemeRegistryEntry> registryCache = new Dictionary<string, ThemeRegistryEntry>();
        StorageFolder dataFolder;
        StorageFolder rootFolder;
        string ThemeFolder = "Themes";

        
    }

    public class ThemeRegistryEntry
    {
        public string Name { get; set; }
        public string Filename { get; set; }
    }
}
