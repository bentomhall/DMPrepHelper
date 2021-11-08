using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.Treasure
{
    public class TreasureGenerator
    {
        public TreasureGenerator(List<TreasureTable> individualTreasures, List<TreasureTable> hoards, List<ItemTableData> itemTables)
        {
            this.individualTreasures = individualTreasures;
            this.hoards = hoards;
            this.itemTables = itemTables;
        }

        public TreasureTable.CashReward GetIndividualTreasure(int tier)
        {
            throw new NotImplementedException("Individual Treasure Not implemented");
            //var treasureTable = individualTreasures.Find(x => x.Tier == tier);
            //return treasureTable.GenerateTreasure(itemTables, false).Item1;
        }

        public Tuple<TreasureTable.CashReward, List<string>> GetHoard(int tier, bool withTreasure)
        {
            var treasureTable = hoards.Find(x => x.Tier == tier);
            return treasureTable.GenerateTreasure(itemTables, withTreasure);
        }

        private readonly List<TreasureTable> individualTreasures;
        private readonly List<TreasureTable> hoards;
        private readonly List<ItemTableData> itemTables;
    }
}
