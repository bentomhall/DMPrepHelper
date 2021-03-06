﻿using System;
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
        private RelayCommand<object> exportCommand;
        private ObservableCollection<SettlementViewModel> vms = new ObservableCollection<SettlementViewModel>();
        private bool CanGenerate => (!String.IsNullOrEmpty(selected) && ! String.IsNullOrEmpty(selectedCity));
        private int number = 1;
        private ObservableCollection<SettlementViewModel> selectedViewModels = new ObservableCollection<SettlementViewModel>();
        private StorageHelper storageHelper;

        public SettlementGeneratorViewModel(StorageHelper storage)
        {
            storageHelper = storage;
            generator = storage.GetSettlementGenerator();
            Sizes = new ObservableCollection<string>(generator.GetPossibleSettlementTypes());
            Cities = new ObservableCollection<string>(generator.GetPossibleCities());
        }

        public ObservableCollection<SettlementViewModel> SelectedViewModels
        {
            get => selectedViewModels;
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
                try
                {
                    var settlement = generator.GenerateSettlement(selected, selectedCity);
                    SettlementModels.Add(new SettlementViewModel(settlement));
                    DisplayError = false;
                } catch (Exception)
                {
                    ErrorText = "An error occurred. Please check the following for mismatched keys: Culture, Name, Nation, City, Items, Town Type, and Town Role.";
                    DisplayError = true;
                    return;
                }
                
            }
            
        }

        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null) { exportCommand = new RelayCommand<object>(param => ExportSelected()); }
                return exportCommand;
            }
        }

        private void ExportSelected()
        {
            var data = selectedViewModels.Select(x => x.RawData);
            var dummy = storageHelper.WriteFile(Export.ExportTypes.Settlement, data);
        }
    }
}
