using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private BindingList<CityViewModel> storedData;
        private string name;
        private string nation;
        private int population;
        private string size;
        private string region;
        private string terrain;
        private ObservableCollection<DictItem> races = new ObservableCollection<DictItem> { new DictItem() };
        private int tech;
        private List<string> namePrefixes = new List<string>();
        private List<string> nameInfixes = new List<string>();
        private List<string> nameSuffixes = new List<string>();
        private string combiner = "";
        private DictItem selectedRaceItem;
        private CityViewModel selectedCity;
        private Guid guid;
        //private ObservableCollection<CityViewModel> cities;

        public CityConfigViewModel(StorageHelper s) : base(s)
        {
            storage = s;
            storedData = new BindingList<CityViewModel>(GetStoredData().Select(x => new CityViewModel(x)).ToList());
            //cities = new ObservableCollection<CityViewModel>(storedData.Select(x => new CityViewModel(x)));
        }

        public new void SetItems()
        {
            ItemNames = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }

        public BindingList<CityViewModel> Items
        {
            get => storedData;
            /*
            get => cities;
            set
            {
                storedData = value.Select(x => x.Data).ToList();
                cities = value;
                OnPropertyChanged(nameof(Items));
            }
            */
        }

        public DictItem SelectedRaceItem { get => selectedRaceItem; set => SetProperty(ref selectedRaceItem, value); }
        public CityViewModel SelectedCity { get => selectedCity; set => SetProperty(ref selectedCity, value); }

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

        public ObservableCollection<DictItem> Races { get => races; set => SetProperty(ref races, value); }
        public ObservableCollection<string> ItemNames { get => items; set => SetProperty(ref items, value); }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
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
            if (string.IsNullOrEmpty(item))
            {
                return;
            }
            var data = storedData.First(x => x.Name == item).Data;
            guid = data.Guid;
            Name = item;
            Nation = data.Nation;
            Population = data.Population;
            Size = data.Size;
            Region = data.Region;
            if (data.Races != null)
            {
                Races = new ObservableCollection<DictItem>(data.Races.Select(x => new DictItem(x)));
            }
            else
            {
                Races = new ObservableCollection<DictItem> { new DictItem() };
            }
            
            Tech = data.Tech;
            Terrain = data.Terrain;
            namePrefixes = data.Prefixes;
            nameInfixes = data.Infixes;
            nameSuffixes = data.Suffixes;
            Combiner = data.Combiner;
            OnPropertyChanged(nameof(NamePrefixes));
            OnPropertyChanged(nameof(NameInfixes));
            OnPropertyChanged(nameof(NameSuffixes));
        }

        public override void DidAddListItem(string p)
        {
            Races.Add(new DictItem());
            //races[NewRaceName] = NewRaceWeight;
            //OnPropertyChanged(nameof(Races));
        }

        public override void DidRemoveListItem(string p)
        {

            Races.Remove(selectedRaceItem);
            //OnPropertyChanged(nameof(Races));
        }

        protected override void DidAddItem()
        {
            CityData newItem;
            if (selectedCity == null && !string.IsNullOrEmpty(Name))
            {
                newItem = new CityData { Name = name, Combiner = combiner, Infixes = nameInfixes, Nation = nation, Population = population, Prefixes = namePrefixes, Region = region, Size = size, Suffixes = nameSuffixes, Tech = tech, Terrain = terrain };
                newItem.Races = new Dictionary<string, double>(races.Select(x => x.ToKeyValuePair()));

            }
            else
            {
                DidEditItem();
                newItem = new CityData();
                Clear();
            }
            var newModel = new CityViewModel(newItem);
            storedData.Add(newModel);
        }

        protected override void DidRemoveItem(string item)
        {
            Items.Remove(selectedCity);
            storedData.Remove(selectedCity);
        }

        protected override void DidSaveConfig()
        {
            if (selectedCity != null) { DidEditItem(); }
            var text = JsonConvert.SerializeObject(storedData);
            var dummy = storage.SaveConfigText(DataFile.City, text);
        }

        private void Clear()
        {
            Name = "";
            Nation = "";
            Population = 0;
            Size = "";
            Region = "";
            Races = new ObservableCollection<DictItem>
            {
                new DictItem()
            };
            Tech = 0;
            Terrain = "";
            namePrefixes = new List<string>();
            nameInfixes = new List<string>();
            nameSuffixes = new List<string>();
            Combiner = "";
            OnPropertyChanged(nameof(NamePrefixes));
            OnPropertyChanged(nameof(NameInfixes));
            OnPropertyChanged(nameof(NameSuffixes));
        }

        public void DidEditItem(string itemName = "")
        {
            if (guid == null || selectedCity == null || itemName == selectedCity.Data.Name)
            {
                return;
            }
            var model = storedData.First(x => x.Data.Guid == guid);
            var item = model.Data;
            item.Name = Name;
            item.Nation = Nation;
            item.Population = Population;
            item.Prefixes = namePrefixes;
            item.Infixes = nameInfixes;
            item.Races = new Dictionary<string, double>(races.Select(x => x.ToKeyValuePair()));
            item.Region = Region;
            item.Tech = Tech;
            item.Terrain = Terrain;
            item.Combiner = Combiner;
            //model.Data = item;
            //cities = new ObservableCollection<CityViewModel>(storedData.Select(x => new CityViewModel(x)));
            
            return;
        }

        private bool ShouldUpdate<T>(T old, T val)
        {
            if (old == null) { return false; }
            return old.Equals(val);
        }

    }

    public class DictItem
    {
        public DictItem()
        {
            Key = "";
            Value = 0.0;
        }
        public DictItem(KeyValuePair<string, double> kvp)
        {
            Key = kvp.Key;
            Value = kvp.Value;
        }

        public string Key { get; set; }
        public double Value { get; set; }
        public string Representation { get => $"{Key} : {Value}"; }
        public KeyValuePair<string, double> ToKeyValuePair()
        {
            return new KeyValuePair<string, double>(Key, Value);
        }
    }

    public class CityViewModel : NotifyChangedBase
    {
        private CityData data;
        private string name;

        public CityData Data { get => data; set => SetProperty(ref data, value); }
        public string Name { get => data.Name;
            set
            {
                data.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public CityViewModel(CityData d)
        {
            data = d;
        }
    }

}
