using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibGenerator.Settlement;
using Newtonsoft.Json;

namespace DMPrepHelper.ViewModels
{
    public class CityConfigViewModel : ConfigBaseViewModel
    {
        private List<CityData> storedData;
        private string name;
        private string nation;
        private int population;
        private string size;
        private string region;
        private string terrain;
        private Dictionary<string, double> races = new Dictionary<string, double>();
        private int tech;
        private List<string> namePrefixes = new List<string>();
        private List<string> nameInfixes = new List<string>();
        private List<string> nameSuffixes = new List<string>();
        private string combiner ="";
        private DictItem selectedRaceItem;
        private string selectedCity;

        public CityConfigViewModel(StorageHelper s) : base(s)
        {
            storage = s;
            storedData = GetStoredData();
            
        }

        public new void SetItems()
        {
            ItemNames = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }

        public DictItem SelectedRaceItem { get => selectedRaceItem; set => SetProperty(ref selectedRaceItem, value); }
        public string SelectedCity { get => selectedCity; set => SetProperty(ref selectedCity, value); }

        public string NewRaceName { get; set; }
        public double NewRaceWeight { get; set; }
        public string NamePrefixes
        {
            get => string.Join(", ", namePrefixes);
            set
            {
                namePrefixes = value.Split(", ").ToList();
                OnPropertyChanged(nameof(NamePrefixes));
            }
        }

        public string NameInfixes
        {
            get => string.Join(", ", nameInfixes);
            set
            {
                namePrefixes = value.Split(", ").ToList();
                OnPropertyChanged(nameof(NameInfixes));
            }
        }

        public string NameSuffixes
        {
            get => string.Join(", ", nameSuffixes);
            set
            {
                namePrefixes = value.Split(", ").ToList();
                OnPropertyChanged(nameof(NameSuffixes));
            }
        }

        public ObservableCollection<DictItem> Races { get => new ObservableCollection<DictItem>(races.Select(x => new DictItem(x))); }
        public ObservableCollection<string> ItemNames { get => items; set => SetProperty(ref items, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Nation { get => nation; set => SetProperty(ref nation, value); }
        public int Population { get => population; set => SetProperty(ref population, value); }
        public string Size { get => size; set => SetProperty(ref size, value); }
        public string Region { get => region; set => SetProperty(ref region, value); }
        public string Terrain { get => terrain; set => SetProperty(ref terrain, value); }
        public int Tech { get => tech; set => SetProperty(ref tech, value); }
        public string Combiner { get => combiner; set => SetProperty(ref combiner, value); }

        private List<CityData> GetStoredData()
        {
            var data = storage.Deserialize<CityData>(DataFile.City);
            return data;
        }

        protected override void DidSelectItem(string item)
        {
            var data = storedData.First(x => x.Name == item);
            Name = item;
            Nation = data.Nation;
            Population = data.Population;
            Size = data.Size;
            Region = data.Region;
            races = data.Races;
            Tech = data.Tech;
            Terrain = data.Terrain;
            namePrefixes = data.Prefixes;
            nameInfixes = data.Infixes;
            nameSuffixes = data.Suffixes;
            Combiner = data.Combiner;
            OnPropertyChanged(nameof(Races));
            OnPropertyChanged(nameof(NamePrefixes));
            OnPropertyChanged(nameof(NameInfixes));
            OnPropertyChanged(nameof(NameSuffixes));
        }

        protected override void DidAddListItem(string p)
        {
            races[NewRaceName] = NewRaceWeight;
            OnPropertyChanged(nameof(Races));
        }

        protected override void DidRemoveListItem(string p)
        {

            races.Remove(selectedRaceItem.Key);
            OnPropertyChanged(nameof(Races));
        }

        protected override void DidAddItem()
        {
            storedData.Add(new CityData { Name = name, Combiner = combiner, Infixes = nameInfixes, Nation = nation, Population = population, Prefixes = namePrefixes, Races = races, Region = region, Size = size, Suffixes = nameSuffixes, Tech = tech, Terrain = terrain });
            OnPropertyChanged(nameof(ItemNames));
        }

        protected override void DidRemoveItem(string item)
        {
            storedData.Remove(storedData.First(x => x.Name == item));
            OnPropertyChanged(nameof(ItemNames));
        }

        protected override void DidSaveConfig()
        {
            var text = JsonConvert.SerializeObject(storedData);
            var dummy = storage.SaveConfigText(DataFile.City, text);
        }

    }

    public class DictItem
    {
        public DictItem(KeyValuePair<string, double> kvp)
        {
            Key = kvp.Key;
            Value = kvp.Value;
        }

        public string Key { get; set; }
        public double Value { get; set; }
        public string Representation { get => $"{Key} : {Value}"; }
    }

}
