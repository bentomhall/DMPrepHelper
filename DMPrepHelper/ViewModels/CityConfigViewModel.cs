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
    public class CityConfigViewModel : NotifyChangedBase, IConfigDisplay
    {
        private StorageHelper storage;
        private List<CityData> storedData;
        private ObservableCollection<string> items;
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
        private RelayCommand<object> addCityCommand;
        private RelayCommand<string> selectCityCommand;
        private RelayCommand<string> removeCityCommand;
        private RelayCommand<object> addRaceCommand;
        private RelayCommand<DictItem> removeRaceCommand;
        private RelayCommand<object> saveCommand;
        private string selectedRaceItem;

        public CityConfigViewModel(StorageHelper s)
        {
            storage = s;
            storedData = GetStoredData();
            items = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }

        public string SelectedRaceItem { get => selectedRaceItem; set => SetProperty(ref selectedRaceItem, value); }

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
        public ICommand AddItemCommand
        {
            get
            {
                if (addCityCommand == null)
                {
                    addCityCommand = new RelayCommand<object>(param => AddCityItem(param));
                }
                return addCityCommand;
            }
        }
        public ICommand RemoveItemCommand
        {
            get
            {
                if (removeCityCommand == null)
                {
                    removeCityCommand = new RelayCommand<string>(param => RemoveCityItem(param));
                }
                return removeCityCommand;
            }
        }
        public ICommand SelectItemCommand
        {
            get
            {
                if (selectCityCommand == null)
                {
                    selectCityCommand = new RelayCommand<string>(param => DidSelectItem(param));
                }
                return selectCityCommand;
            }
        }
        public ICommand AddRaceCommand
        {
            get
            {
                if (addRaceCommand == null)
                {
                    addRaceCommand = new RelayCommand<object>(param => AddRaceItem());
                }
                return addRaceCommand;
            }
        }
        public ICommand RemoveRaceCommand
        {
            get
            {
                if (removeRaceCommand == null)
                {
                    removeRaceCommand = new RelayCommand<DictItem>(param => RemoveRaceItem(param));
                }
                return removeRaceCommand;
            }
        }
        public ICommand SaveConfigCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand<object>(param => SaveData());
                }
                return saveCommand;
            }
        }

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
            return storage.Deserialize<CityData>(DataFile.City);
        }

        private void DidSelectItem(string item)
        {
            var data = storedData.First(x => x.Name == item);
            name = item;
            nation = data.Nation;
            population = data.Population;
            size = data.Size;
            region = data.Region;
            races = data.Races;
            tech = data.Tech;
            terrain = data.Terrain;
            namePrefixes = data.Prefixes;
            nameInfixes = data.Infixes;
            nameSuffixes = data.Suffixes;
            Combiner = data.Combiner;
            OnPropertyChanged(null);
        }

        private void AddRaceItem()
        {
            races[NewRaceName] = NewRaceWeight;
            OnPropertyChanged(nameof(Races));
        }

        private void RemoveRaceItem(DictItem item)
        {
            races.Remove(item.Key);
            OnPropertyChanged(nameof(Races));
        }

        private void AddCityItem(object o)
        {
            storedData.Add(new CityData { Name = name, Combiner = combiner, Infixes = nameInfixes, Nation = nation, Population = population, Prefixes = namePrefixes, Races = races, Region = region, Size = size, Suffixes = nameSuffixes, Tech = tech, Terrain = terrain });
            OnPropertyChanged(nameof(ItemNames));
        }

        private void RemoveCityItem(string item)
        {
            storedData.Remove(storedData.First(x => x.Name == item));
            OnPropertyChanged(nameof(ItemNames));
        }

        private void SaveData()
        {
            var text = JsonConvert.SerializeObject(storedData);
            storage.SaveConfigText(DataFile.City, text);
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
