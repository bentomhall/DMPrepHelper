﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGenerator.Dungeon;
using LibGenerator.NPC;
using LibGenerator.Settlement;
using LibGenerator.Core;

namespace DMPrepHelper.ViewModels
{
    public class ConfigItemViewModel : ConfigBaseViewModel
    {
        private string configText;
        private string helpText;
        private DataFile configType;

        public ConfigItemViewModel(StorageHelper s, DataFile type) : base(s)
        {
            configType = type;
            configText = GetData();
        }

        public string ConfigText { get => configText; set => SetProperty(ref configText, value); }
        public string HelpText { get => helpText; set => SetProperty(ref helpText, value); }

        private string GetData()
        {
            return storage.GetConfigText(configType);
        }

        protected override void DidSaveConfig()
        {
            var dummy = storage.SaveConfigText(configType, configText);
        }

        protected override void DidAddItem()
        {
            string json;
            switch (configType)
            {
        case DataFile.City:
                json = storage.GetBlankEntry<CityData>();
                break;
            case DataFile.Dungeon:
                json = storage.GetBlankEntry<LocationData>();
                break;
            case DataFile.ItemRank:
                json = storage.GetBlankEntry<ItemData>();
                break;
            case DataFile.Nation:
                json = storage.GetBlankEntry<NationData>();
                break;
            case DataFile.NpcName:
                json = storage.GetBlankEntry<NationData>();
                break;
            case DataFile.Race:
                json = storage.GetBlankEntry<CultureData>();
                break;
            case DataFile.Region:
                json = storage.GetBlankEntry<RegionData>();
                break;
            case DataFile.SettlementRole:
                json = storage.GetBlankEntry<SettlementRole>();
                break;
            case DataFile.SettlementType:
                json = storage.GetBlankEntry<SettlementData>();
                break;
            default:
                return;
            }
                
            ConfigText = configText.Insert(configText.Length - 1, json);
            return;
        }
    }


}