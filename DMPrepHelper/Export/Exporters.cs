using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGenerator.NPC;
using LibGenerator.Settlement;
using LibGenerator.Dungeon;
using Windows.Storage;

namespace DMPrepHelper.Export
{
    public class ExportBase<T> : IExporter<T>
    {
        public string Marshal(IEnumerable<T> t)
        {
            string separator = $"{Environment.NewLine}------------------------{Environment.NewLine}";
            var data = t.Select(x => FormatData(x));
            return string.Join(separator, data);
        }

        // dummy implementation
        string FormatData(object obj) { return ""; }
    }


    public class PersonExporter : ExportBase<PersonData>
    {
        private string FormatData(PersonData d)
        {
            var output = new List<string>()
            {
                $"Name: {d.Name}"
            };
            if (string.IsNullOrEmpty(d.Subrace))
            {
                output.Add($"Demographics: {d.Age} {d.Gender} {d.Race} from {d.Nation} ({d.Culture})");
            }
            else
            {
                output.Add($"Demographics: {d.Age} {d.Gender} {d.Race} ({d.Subrace}) from {d.Nation} ({d.Culture})");
            }
            output.Add($"Profession: {d.Profession}");
            output.Add($"Religious habits: {d.Religion}");
            output.Add($"Personality: {d.Personality}");
            return string.Join(Environment.NewLine, output);
        }
    }

    public class DungeonExporter : ExportBase<AdventureData>
    {
        private string FormatData(AdventureData d)
        {
            throw new NotImplementedException();
        }
    }

    public class SettlementExporter : ExportBase<SettlementData>
    {
        private string FormatData(SettlementData d)
        {
            throw new NotImplementedException();
        }
    }
}
