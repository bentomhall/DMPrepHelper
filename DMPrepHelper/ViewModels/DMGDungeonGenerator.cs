using libGenerator.DMGDungeonBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMPrepHelper.ViewModels
{
    public class DMGDungeonGenerator : NotifyChangedBase
    {
        public ObservableCollection<string> PurposeDescriptions = new ObservableCollection<string> { "Death Trap", "Lair", "Mine", "Maze", "Planar Gate", "Stronghold", "Temple or Shrine", "Tomb", "Treasure Vault", "General" };

        public DMGDungeonGenerator(StorageHelper s)
        {
            storage = s;
        }

        public int MaxRooms { get => maxRooms; set => SetProperty(ref maxRooms, value); }

        public int SelectedType { get => selectedType; set => SetProperty(ref selectedType, value); }

        public ObservableCollection<DMGDungeonViewModel> ViewModels { get => viewModels; set => SetProperty(ref viewModels, value); }

        public ObservableCollection<DMGDungeonViewModel> SelectedViewModels { 
            get
            {
                if (selectedVMs == null) { selectedVMs = new ObservableCollection<DMGDungeonViewModel>(); }
                return selectedVMs;
            }
            set => SetProperty(ref selectedVMs, value);
        }

        
        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                {
                    generateCommand = new RelayCommand<object>(param => Generate());
                }
                return generateCommand;
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand<object>(param => Export());
                }
                return exportCommand;
            }
        }



        private void Generate()
        {
            var purpose = GetPurpose();
            var dungeon = new DMGDungeon(maxRooms, purpose);
            var vm = new DMGDungeonViewModel(dungeon);
            viewModels.Add(vm);
        }

        private void Export()
        {
            var dummy = storage.WriteFile(DMPrepHelper.Export.ExportTypes.DMGDungeon, SelectedViewModels.Select(s => s.Dungeon));
        }

        private int maxRooms = 5;
        private int selectedType = 0;
        private RelayCommand<object> generateCommand;
        private RelayCommand<object> exportCommand;
        private StorageHelper storage;

        private PurposeType GetPurpose()
        {
            return (PurposeType)selectedType;
        }
        private ObservableCollection<DMGDungeonViewModel> viewModels = new ObservableCollection<DMGDungeonViewModel>();
        private ObservableCollection<DMGDungeonViewModel> selectedVMs;
    }
}
