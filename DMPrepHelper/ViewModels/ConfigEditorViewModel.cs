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
        private string selectedItem;
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
        private RelayCommand<string> selectItem;
        private RelayCommand<object> saveSelected;
        private bool canExecute = true;
        private string displayText;


        public ConfigEditorViewModel(StorageHelper s)
        {
            storage = s;
        }

        public string DisplayText { get => displayText; set => SetProperty(ref displayText, value); }

        public ICommand SelectItemCommand
        {
            get
            {
                if (selectItem == null)
                {
                    selectItem = new RelayCommand<string>(DidSelectItem);
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
                    saveSelected = new RelayCommand<object>(param => DidSaveItem(), param => canExecute);
                }
                return saveSelected;
            }
        }

        public ObservableCollection<ConfigLabel> Labels { get; private set; }


        private void DidSelectItem(string label)
        {
            selectedItem = label;
            var dataType = fileTypes[label];
            DisplayText = storage.GetConfigText(dataType);
            return;
        }

        private async Task DidSaveItem()
        {
            var dataType = fileTypes[selectedItem];
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
