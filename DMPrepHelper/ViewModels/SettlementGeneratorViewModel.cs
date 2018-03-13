using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LibGenerator.Settlement;

namespace DMPrepHelper.ViewModels
{
    public class SettlementGeneratorViewModel : NotifyChangedBase
    {
        private SettlementGenerator generator;
        private ObservableCollection<string> sizes;
        private string selected;
        private string selectedCity;
        private ObservableCollection<string> cities;
        private RelayCommand<object> generateCommand;
        private RelayCommand<object> selectAllCommand;
        private RelayCommand<object> exportCommand;
        private ObservableCollection<SettlementViewModel> vms = new ObservableCollection<SettlementViewModel>();
        private bool CanGenerate => (!String.IsNullOrEmpty(selected) && ! String.IsNullOrEmpty(selectedCity));
        private int number = 1;
        private ObservableCollection<SettlementViewModel> selectedViewModels = new ObservableCollection<SettlementViewModel>();
        private StorageHelper storageHelper;

        /*
        public SettlementGeneratorViewModel()
        {
            Sizes = new ObservableCollection<string>(generator.GetPossibleSettlementTypes());
            Cities = new ObservableCollection<string>(generator.GetPossibleCities());
        }*/

        public SettlementGeneratorViewModel(StorageHelper storage)
        {
            storageHelper = storage;
            generator = storage.GetSettlementGenerator();
            Sizes = new ObservableCollection<string>(generator.GetPossibleSettlementTypes());
            Cities = new ObservableCollection<string>(generator.GetPossibleCities());
        }

        public ObservableCollection<SettlementViewModel> SelectedViewModels
        {
            get
            {
                return selectedViewModels;
            }
            set => SetProperty(ref selectedViewModels, value);
        }


        public int Number { get => number; set => SetProperty(ref number, value); }

        public ObservableCollection<string> Sizes
        {
            get => sizes;
            set
            {
                SetProperty(ref sizes, value);
                SelectedSize = value[0];
            }
        }

        public ObservableCollection<string> Cities
        {
            get => cities;
            set
            {
                SetProperty(ref cities, value);
                SelectedCity = value[0];
            }
        }
   

        public string SelectedSize { get => selected; set => SetProperty(ref selected, value); }
        public string SelectedCity { get => selectedCity; set => SetProperty(ref selectedCity, value); }

        public ObservableCollection<SettlementViewModel> SettlementModels { get => vms; set => SetProperty(ref vms, value); }

        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                {
                    generateCommand = new RelayCommand<object>(param => CreateVM(), param => CanGenerate);
                }
                return generateCommand;
            }
        }

        private void CreateVM()
        {
            SettlementModels.Clear();
            for (int i=0; i < Number; i++)
            {
                var settlement = generator.GenerateSettlement(selected, selectedCity);
                SettlementModels.Add(new SettlementViewModel(settlement));
            }
            
        }

        public ICommand SelectAllCommand
        {
            get
            {
                if (selectAllCommand == null) { selectAllCommand = new RelayCommand<object>(param => DidSelectAll()); }
                return selectAllCommand;
            }
        }

        public void DidSelectAll()
        {
            SelectedViewModels = vms;
        }

        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null) { exportCommand = new RelayCommand<object>(param => ExportSelected()); }
                return exportCommand;
            }
        }

        private async Task ExportSelected()
        {
            var data = selectedViewModels.Select(x => x.RawData);
            await storageHelper.WriteFile(Export.ExportTypes.Settlement, data);
        }
    }
}
