using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibGenerator.Dungeon;

namespace DMPrepHelper.ViewModels
{
    class DungeonGeneratorViewModel : NotifyChangedBase
    {
        private int number = 1;
        private string region = "";
        private ObservableCollection<string> regions;
        private DungeonGenerator generator = new DungeonGenerator();
        private bool CanExecute => (!String.IsNullOrWhiteSpace(SelectedRegion));
        private RelayCommand<object> generateCommand;
        private ObservableCollection<DungeonViewModel> vms;

        public ObservableCollection<string> Regions
        {
            get
            {
                if (regions == null) { regions = new ObservableCollection<string>(generator.GetValidRegions()); }
                return regions;
            }
            set
            {
                SetProperty(ref regions, value);
                SelectedRegion = value[0];
            }
        }

        public ObservableCollection<DungeonViewModel> ViewModels { get => (vms ?? new ObservableCollection<DungeonViewModel>()); set => SetProperty(ref vms, value); }

        public int AdventuresToGenerate { get => number; set => SetProperty(ref number, value); }
        public string SelectedRegion { get => region; set => SetProperty(ref region, value); }
        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                {
                    generateCommand = new RelayCommand<object>(param => CreateVM(), param => CanExecute);
                }
                return generateCommand;
            }
        }

        private void CreateVM()
        {
            if (vms == null) { vms = new ObservableCollection<DungeonViewModel>(); }
            for (int i=0; i < number; i++)
            {
                var vm = new DungeonViewModel(generator.GenerateAdventure(SelectedRegion));
                vms.Add(vm);
            }
            ViewModels = vms;
        }

    }
}
