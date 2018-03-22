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
    class CultureConfigViewModel : ConfigBaseViewModel
    {
        private List<CultureData> storedData;

        public CultureConfigViewModel(StorageHelper s) : base(s)
        {
            storedData = GetData();
            items = new ObservableCollection<string>(storedData.Select(x => x.Race));
        }

        private List<CultureData> GetData()
        {
            return storage.Deserialize<CultureData>(DataFile.Race);
        }

        protected override void DidAddItem()
        {
            base.DidAddItem();
        }

        protected override void DidAddListItem(string p)
        {
            base.DidAddListItem(p);
        }

        protected override void DidRemoveItem(string p)
        {
            base.DidRemoveItem(p);
        }

        protected override void DidRemoveListItem(string p)
        {
            base.DidRemoveListItem(p);
        }

        protected override void DidSaveConfig()
        {
            base.DidSaveConfig();
        }

        protected override void DidSelectItem(string p)
        {
            base.DidSelectItem(p);
        }
    }
}
