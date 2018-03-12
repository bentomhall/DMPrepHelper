using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGenerator.Dungeon;
using LibGenerator.Core;

namespace DMPrepHelper.ViewModels
{
    class RegionDataViewModel : NotifyChangedBase
    {
        private RegionData data;
        private ObservableCollection<string> adventureTypes;
        private WeightedChoiceSet monsters;

        RegionDataViewModel(RegionData d)
        {
            data = d;
            adventureTypes = new ObservableCollection<string>(d.AdventureTypes);
            monsters = new WeightedChoiceSet(data.Monsters);
        }

        public string Name
        {
            get => data.Name;
            set
            {
                data.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Tier
        {
            get => data.Tier;
            set
            {
                data.Tier = value;
                OnPropertyChanged(nameof(Tier));
            }
        }

       public ObservableCollection<string> AdventureTypes
       {
            get => adventureTypes;
       }

       public Dictionary<string, double> Monsters { get => data.Monsters; }

       private void AddAdventureType(string newType)
        {
            adventureTypes.Add(newType);
            data.AdventureTypes.Add(newType);
        }

        private void RemoveAdventureType(string key)
        {
            adventureTypes.Remove(key);
            data.AdventureTypes.Remove(key);
        }

        private void AddOrModifyMonsterType(string key, double value)
        {
            monsters.AddOrModify(key, value);
            data.SetMonsters(monsters);
            OnPropertyChanged(nameof(Monsters));
        }

        private void RemoveMonsterType(string key)
        {
            monsters.Remove(key);
            data.SetMonsters(monsters);
            OnPropertyChanged(nameof(Monsters));
        }
    }
}
