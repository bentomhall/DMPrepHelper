using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibGenerator.NPC;

namespace DMPrepHelper.ViewModels
{
    public class CultureConfigViewModel : ConfigBaseViewModel
    {
        private List<CultureData> storedData;
        private string nation;
        private string race;
        private ObservableCollection<RaceConfigModel> displayItems;
        private ObservableCollection<string> cultures;
        private ObservableCollection<DictItem> genders;
        private ObservableCollection<string> religions;
        private ObservableCollection<DictItem> subraces;

        private RaceConfigModel selectedItem;
        private string selectedCulture;
        private DictItem selectedGender;
        private string selectedReligion;
        private DictItem selectedSubrace;

        public CultureConfigViewModel(StorageHelper s) : base(s)
        {
            storedData = GetData();
            items = new ObservableCollection<string>(storedData.Select(x => x.Race));
            displayItems = new ObservableCollection<RaceConfigModel>(storedData.Select(x => new RaceConfigModel(x)));
            SelectedItem = displayItems[0];
        }

        public string Race { get => race; set => SetProperty(ref race, value); }
        public string Nation { get => nation; set => SetProperty(ref nation, value); }
        public ObservableCollection<RaceConfigModel> ItemNames { get => displayItems; set => SetProperty(ref displayItems, value); }
        public ObservableCollection<string> Cultures { get => cultures; set => SetProperty(ref cultures, value); }
        public ObservableCollection<DictItem> Genders { get => genders; set => SetProperty(ref genders, value); }
        public ObservableCollection<DictItem> Subraces { get => subraces; set => SetProperty(ref subraces, value); }
        public ObservableCollection<string> Religions { get => religions; set => SetProperty(ref religions, value); }

        public RaceConfigModel SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }
        public string SelectedCulture { get => selectedCulture; set => SetProperty(ref selectedCulture, value); }
        public DictItem SelectedGender { get => selectedGender; set => SetProperty(ref selectedGender, value); }
        public string SelectedReligion { get => selectedReligion; set => SetProperty(ref selectedReligion, value); }
        public DictItem SelectedSubrace { get => selectedSubrace; set => SetProperty(ref selectedSubrace, value); }

        private List<CultureData> GetData()
        {
            return storage.Deserialize<CultureData>(DataFile.Race);
        }

        protected override void DidAddItem()
        {
            base.DidAddItem();
        }

        public override void DidAddListItem(string p)
        {
            base.DidAddListItem(p);
        }

        protected override void DidRemoveItem(string p)
        {
            base.DidRemoveItem(p);
        }

        public override void DidRemoveListItem(string p)
        {
            base.DidRemoveListItem(p);
        }

        protected override void DidSaveConfig()
        {
            base.DidSaveConfig();
        }

        protected override void DidSelectItem(string p)
        {
            var selected = SelectedItem;
            var item = storedData.First(x => x.Matches(selected.Race, selected.Nation));
            
            Race = item.Race;
            Nation = item.Nation;
            
            Cultures = new ObservableCollection<string>(item.Culture);
            Genders = new ObservableCollection<DictItem>(item.Gender.Select(x => new DictItem(x)));
            Subraces = new ObservableCollection<DictItem>(item.Subrace.Select(x => new DictItem(x)));
            Religions = new ObservableCollection<string>(item.Religiosity);
        }
    }

    public class RaceConfigModel : NotifyChangedBase
    {
        CultureData data;
        public RaceConfigModel(CultureData d) => data = d;

        public string DisplayName { get => $"{data.Race} from {data.Nation}"; }
        public string Race { get => data.Race; }
        public string Nation { get => data.Nation; }
    }
}
