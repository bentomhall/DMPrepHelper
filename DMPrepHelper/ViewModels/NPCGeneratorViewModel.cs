using System.Collections.ObjectModel;
using System.Windows.Input;
using LibGenerator.NPC;

namespace DMPrepHelper.ViewModels
{
    public class NPCGeneratorViewModel : NotifyChangedBase
    {
        private NPCGenerator generator;
        private bool canGenerate = true;
        private RelayCommand<object> generateCommand;
        private ObservableCollection<string> nations;
        private ObservableCollection<PersonViewModel> models = new ObservableCollection<PersonViewModel>() { };
        private string selected;
        private int number = 1;

        public ObservableCollection<string> Nations
        {
            get => nations;
            set
            {
                SetProperty(ref nations, value);
                selected = nations[0];
            }
        }
        public ObservableCollection<PersonViewModel> ViewModels { get => models; set => SetProperty(ref models, value); }

        public string SelectedNation
        {
            get => selected;
            set => SetProperty(ref selected, value);
        }

        public int Number { get => number; set => SetProperty(ref number, value); }

        public NPCGeneratorViewModel(StorageHelper storage)
        {
            generator = storage.GetNPCGenerator();
            Nations = new ObservableCollection<string>(generator.GetValidNations());
        }

        public bool CanGenerate { get => canGenerate; }

        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                {
                    generateCommand = new RelayCommand<object>(param => CreateNPCs(), param => canGenerate);
                }
                return generateCommand;
            }
        }

        private void CreateNPCs()
        {
            var viewModels = new ObservableCollection<PersonViewModel>(); 
            for (int i = 0; i < number; i++)
            {
                var npc = generator.GenerateNPC(selected);
                viewModels.Add(new PersonViewModel(npc));
            }
            ViewModels = viewModels;
            OnPropertyChanged(nameof(ViewModels));
        }
    }
}
