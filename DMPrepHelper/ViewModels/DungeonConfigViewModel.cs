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
    public class DungeonConfigViewModel : NotifyChangedBase, IConfigDisplay
    {
        private StorageHelper storage;
        private List<LocationData> storedData;
        private ObservableCollection<string> items;
        private string name;
        private string scale;
        private ObservableCollection<int> sizes;
        private ObservableCollection<string> subtypes;
        private ObservableCollection<string> ages;
        private double bossProbability;
        private Dictionary<string, double> lairChance;
        private bool canHaveSublocations;

        public DungeonConfigViewModel(StorageHelper s)
        {
            storage = s;
            storedData = GetStoredData();
            

        }
        public ObservableCollection<string> ItemNames { get => items; set => SetProperty(ref items, value); }


        public int SelectedSize { get; set; }
        public string SelectedSubType { get; set; }
        public string SelectedAge { get; set; }
        public int NewSizeItem { get; set; }
        public string NewSubtypeItem { get; set; }
        public string NewAgeItem { get; set; }

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

        private void DidAddItem()
        {
            var item = new LocationData { Name = name, Scale = scale, Sizes = sizes.ToList(), Subtypes = subtypes.ToList(), Ages = ages.ToList(), HasBoss = bossProbability, HasSublocations = canHaveSublocations, LairChance = lairChance };
            storedData.Add(item);
            OnPropertyChanged(nameof(ItemNames));
        }

        private void DidRemoveItem(string item)
        {
            storedData.Remove(storedData.First(x => x.Name == item));
            OnPropertyChanged(nameof(ItemNames));
        }

        private void DidSelectItem(string item)
        {
            var location = storedData.First(x => x.Name == item);
            name = location.Name;
            scale = location.Scale;
            sizes = new ObservableCollection<int>(location.Sizes);
            subtypes = new ObservableCollection<string>(location.Subtypes);
            ages = new ObservableCollection<string>(location.Ages);
            bossProbability = location.HasBoss;
            canHaveSublocations = location.HasSublocations;
            lairChance = location.LairChance;
            OnPropertyChanged(null);
        }

        private void DidSaveConfig()
        {
            var text = JsonConvert.SerializeObject(storedData);
            var dummy = storage.SaveConfigText(DataFile.Dungeon, text);
        }

        private void DidAddListItem(string lst)
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
        }

        private void DidRemoveListItem(string lst)
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
        }

        public void SetItems()
        {
            ItemNames = new ObservableCollection<string>(storedData.Select(x => x.Name));
        }

        #region Commands


        private RelayCommand<object> addItemCommand;
        private RelayCommand<string> removeItemCommand;
        private RelayCommand<string> selectItemCommand;
        private RelayCommand<object> saveConfigCommand;
        private RelayCommand<string> addListItemCommand;
        private RelayCommand<string> removeListItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand<object>(p => DidAddItem());
                }
                return addItemCommand;
            }
        }
        public ICommand RemoveItemCommand
        {
            get
            {
                if (removeItemCommand == null)
                {
                    removeItemCommand = new RelayCommand<string>(p => DidRemoveItem(p));
                }
                return removeItemCommand;
            }
        }
        public ICommand SelectItemCommand
        {
            get
            {
                if (selectItemCommand == null)
                {
                    selectItemCommand = new RelayCommand<string>(p => DidSelectItem(p));
                }
                return selectItemCommand;
            }
        }
        public ICommand SaveConfigCommand
        {
            get
            {
                if (saveConfigCommand == null)
                {
                    saveConfigCommand = new RelayCommand<object>(p => DidSaveConfig());
                }
                return saveConfigCommand;
            }
        }
        public ICommand AddListItemCommand
        {
            get
            {
                if (addListItemCommand == null)
                {
                    addListItemCommand = new RelayCommand<string>(p => DidAddListItem(p));
                }
                return addListItemCommand;
            }
        }
        public ICommand RemoveListItemCommand
        {
            get
            {
                if (removeListItemCommand == null)
                {
                    removeListItemCommand = new RelayCommand<string>(p => DidRemoveListItem(p));
                }
                return removeListItemCommand;
            }
        }
        #endregion
    }
}
