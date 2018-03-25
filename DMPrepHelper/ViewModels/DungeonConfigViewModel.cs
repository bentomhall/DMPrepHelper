using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibGenerator.Dungeon;
using Newtonsoft.Json;

namespace DMPrepHelper.ViewModels
{
    public class DungeonConfigViewModel : ConfigBaseViewModel
    {
        private List<LocationData> storedData;
        private string name;
        private string scale;
        private ObservableCollection<int> sizes;
        private ObservableCollection<string> subtypes;
        private ObservableCollection<string> ages;
        private double bossProbability;
        private Dictionary<string, double> lairChance;
        private bool canHaveSublocations;
        private string newSubtype;
        private string newAge;
        private int newSize;

        public DungeonConfigViewModel(StorageHelper s) : base(s)
        {
            storedData = GetStoredData();
            items = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }
        public ObservableCollection<string> ItemNames { get => items; set => SetProperty(ref items, value); }

        public string SelectedType { get; set; }
        public int SelectedSize { get; set; }
        public string SelectedSubType { get; set; }
        public string SelectedAge { get; set; }
        public int NewSizeItem { get => newSize; set => SetProperty(ref newSize, value); }
        public string NewSubtypeItem { get => newSubtype; set => SetProperty(ref newSubtype, value); }
        public string NewAgeItem { get => newAge; set => SetProperty(ref newAge, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Scale { get => scale; set => SetProperty(ref scale, value); }
        public double BossProbability { get => bossProbability; set => SetProperty(ref bossProbability,value); }
        public bool CanHaveSublocations { get => canHaveSublocations; set => SetProperty(ref canHaveSublocations, value); }
        public ObservableCollection<int> Sizes { get => sizes; set => SetProperty(ref sizes, value); }
        public ObservableCollection<string> Subtypes { get => subtypes; set => SetProperty(ref subtypes, value); }
        public ObservableCollection<string> Ages { get => ages; set => SetProperty(ref ages, value); }

        private List<LocationData> GetStoredData()
        {
            return storage.Deserialize<LocationData>(DataFile.Dungeon);
        }

        protected override void DidAddItem()
        {
            var item = new LocationData { Name = name, Scale = scale, Sizes = sizes.ToList(), Subtypes = subtypes.ToList(), Ages = ages.ToList(), HasBoss = bossProbability, HasSublocations = canHaveSublocations, LairChance = lairChance };
            storedData.Add(item);
            OnPropertyChanged(nameof(ItemNames));
        }

        protected override void DidRemoveItem(string item)
        {
            storedData.Remove(storedData.First(x => x.Name == item));
            OnPropertyChanged(nameof(ItemNames));
        }

        protected override void DidSelectItem(string item)
        {
            var location = storedData.First(x => x.Name == item);
            Name = location.Name;
            Scale = location.Scale;
            Sizes = new ObservableCollection<int>(location.Sizes);
            Subtypes = new ObservableCollection<string>(location.Subtypes);
            Ages = new ObservableCollection<string>(location.Ages);
            BossProbability = location.HasBoss;
            CanHaveSublocations = location.HasSublocations;
            lairChance = location.LairChance;
        }

        protected override void DidSaveConfig()
        {
            var dummy = storage.SaveConfigText(DataFile.Dungeon, storedData);
        }

        public override void DidAddListItem(string lst)
        {
            switch (lst)
            {
                case "size":
                    Sizes.Add(NewSizeItem);
                    break;
                case "subtype":
                    Subtypes.Add(NewSubtypeItem);
                    break;
                case "age":
                    Ages.Add(NewAgeItem);
                    break;
                default:
                    break;
            }
            return;
        }

        public override void DidRemoveListItem(string lst)
        {
            switch (lst)
            {
                case "size":
                    Sizes.Remove(SelectedSize);
                    break;
                case "subtype":
                    Subtypes.Remove(SelectedSubType);
                    break;
                case "age":
                    Ages.Remove(SelectedAge);
                    break;
                default:
                    break;
            }
            return;
        }

        private new void SetItems()
        {
            ItemNames = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }
    }
}
