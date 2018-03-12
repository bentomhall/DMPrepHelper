using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMPrepHelper.ViewModels
{
    public class ConfigEditorViewModel : NotifyChangedBase
    {
        private StorageHelper storage;
        private ConfigLabel selectedItem;
        private Dictionary<string, DataFile> fileTypes = new Dictionary<string, DataFile>()
        {
            {"Cities", DataFile.City },
            {"Dungeons", DataFile.Dungeon },
            {"Items", DataFile.ItemRank },
            {"Nations", DataFile.Nation },
            {"Names", DataFile.NpcName },
            {"Personalities", DataFile.Personality },
            {"Professions", DataFile.Profession },
            {"Cultures", DataFile.Race },
            {"Regions", DataFile.Region },
            {"Rumors", DataFile.Rumor },
            {"Settlement Types", DataFile.SettlementType },
            {"Settlement Roles", DataFile.SettlementRole }
        };
        private RelayCommand<object> selectItem;
        private RelayCommand<object> saveSelected;
        private bool canExecute = true;
        private string displayText;


        public ConfigEditorViewModel(StorageHelper s)
        {
            storage = s;
            Labels = new ObservableCollection<ConfigLabel>()
            {
                new ConfigLabel{ ConfigType=DataFile.City, Icon="MapPin", Label="Cities", Command=SelectItemCommand},
                new ConfigLabel{ ConfigType=DataFile.Dungeon, Icon="ReportHacked", Label="Dungeons", Command=SelectItemCommand},
                new ConfigLabel{ ConfigType=DataFile.ItemRank, Icon="Switch", Label="Item Ranks", Command=SelectItemCommand},
                new ConfigLabel{ ConfigType=DataFile.Nation, Icon="World", Label="Nations", Command=SelectItemCommand},
                new ConfigLabel{ ConfigType=DataFile.NpcName, Icon="AddFriend", Label="Names", Command=SelectItemCommand}
            };
            selectedItem = new ConfigLabel();
        }

        public ConfigLabel SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                DidSelectItem();
            }
        }

        public string DisplayText { get => displayText; set => SetProperty(ref displayText, value); }

        public ICommand SelectItemCommand
        {
            get
            {
                if (selectItem == null)
                {
                    selectItem = new RelayCommand<object>(param => DidSelectItem(), param => canExecute);
                }
                return selectItem;
            }
        }

        public ICommand SaveItemCommand
        {
            get
            {
                if (saveSelected == null)
                {
                    saveSelected = new RelayCommand<object>(param => DidSaveItem().RunSynchronously(), param => canExecute);
                }
                return saveSelected;
            }
        }

        public ObservableCollection<ConfigLabel> Labels { get; private set; }


        private void DidSelectItem()
        {
            var label = selectedItem.Label;
            var dataType = selectedItem.ConfigType;
            DisplayText = storage.GetConfigText(dataType);
            return;
        }

        private async Task DidSaveItem()
        {
            var dataType = selectedItem.ConfigType;
            await storage.SaveConfigText(dataType, displayText);
            
        }






    }

    public class ConfigLabel : NotifyChangedBase
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public DataFile ConfigType { get; set; }
        public ICommand Command { get; set; }
    }
}
