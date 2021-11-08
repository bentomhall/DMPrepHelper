using libGenerator.Treasure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMPrepHelper.ViewModels
{
    public class DMGTreasureGenerator : NotifyChangedBase
    {
        private TreasureGenerator generator;
        private readonly StorageHelper storage;
        private int selectedTier = 0;
        private int numberToGenerate = 1;
        private ObservableCollection<DMGTreasureViewModel> viewModels = new ObservableCollection<DMGTreasureViewModel>();
        private ObservableCollection<DMGTreasureViewModel> selectedModels = new ObservableCollection<DMGTreasureViewModel>();
        private RelayCommand<object> exportCommand;
        private RelayCommand<object> generateCommand;

        public int SelectedTier { get => selectedTier; set => SetProperty(ref selectedTier, value); }
        public int NumberToGenerate { get => numberToGenerate; set => SetProperty(ref numberToGenerate, value); }

        public ObservableCollection<int> Tiers { get; } = new ObservableCollection<int> { 1, 2, 3, 4 };
        public ObservableCollection<DMGTreasureViewModel> ViewModels { get => viewModels; set => SetProperty(ref viewModels, value); }
        public ObservableCollection<DMGTreasureViewModel> SelectedViewModels { get => selectedModels; set => SetProperty(ref selectedModels, value); }

        public DMGTreasureGenerator(StorageHelper s)
        {
            storage = s;
            generator = s.GetTreasureGenerator();
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
           for (int i = 0; i < numberToGenerate; i++)
            {
                var details = generator.GetHoard(selectedTier + 1, true);
                ViewModels.Add(new DMGTreasureViewModel(details.Item1, details.Item2));
            }
        }

        private void Export()
        {
            //var dummy = storage.WriteFile(DMPrepHelper.Export.ExportTypes.DMGDungeon, SelectedViewModels.Select(s => s.Dungeon));
        }
    }

    public class DMGTreasureViewModel : NotifyChangedBase
    {
        private TreasureTable.CashReward cash;

        public DMGTreasureViewModel(TreasureTable.CashReward cash, List<string> items)
        {
            this.cash = cash;
            Items = items;
        }

        public string Cash { get => cash.Description; }
        public List<string> Items { get; }

        public bool HasItems { get => Items.Count > 0; }
    }
}
