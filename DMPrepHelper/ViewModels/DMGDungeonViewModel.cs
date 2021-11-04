using libGenerator.DMGDungeonBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMPrepHelper.ViewModels
{
    public class DMGDungeonViewModel : NotifyChangedBase
    {
        public DMGDungeonViewModel(DMGDungeon dungeon)
        {
            Dungeon = dungeon;
            Rooms = dungeon.Chambers.Select(c => new DMGChamberViewModel(c, dungeon.AdjacencyFor(c.Id)));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Rooms));
        }

        public DMGDungeon Dungeon { get; }
        public string Description { get => $"A {Dungeon.Purpose} dungeon with {Dungeon.Chambers.Count()} chambers"; }
        public IEnumerable<DMGChamberViewModel> Rooms { get; }
    }

    public class DMGChamberViewModel : NotifyChangedBase
    {
        public DMGChamberViewModel(DMGChamber chamber, HashSet<int> connections)
        {
            Chamber = chamber;
            var contents = new List<string> { $"Contents: {Chamber.ChamberContents}" };
            if (Chamber.Hazard.Length > 0)
            {
                contents.Add($"Hazard: {Chamber.Hazard}");
            }
            else if (Chamber.Obstacle.Length > 0)
            {
                contents.Add($"Obstacle: {Chamber.Obstacle}");
            }
            else if (Chamber.Trap.Length > 0)
            {
                contents.Add($"Trap: {Chamber.Trap}");
            }
            Contents = string.Join(Environment.NewLine, contents);
            Exits = string.Join(Environment.NewLine, chamber.Exits.Select(exit => exit.Description));
            var adjacency = string.Join(", ", connections.Select(id => id.ToString()));
            LeadsTo = $"Chamber opens to chambers {adjacency}";
            OnPropertyChanged(nameof(Chamber));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(Purpose));
            OnPropertyChanged(nameof(Contents));
            OnPropertyChanged(nameof(Exits));
            OnPropertyChanged(nameof(LeadsTo));
        }

        public DMGChamber Chamber { get; }
        public string Description { get => $"Chamber {Chamber.Id}: {Chamber.Description}"; }
        public string State { get => Chamber.ChamberCondition; }
        public string Purpose { get => Chamber.ChamberPurpose; }
        public string Hazard { get => Chamber.Hazard; }
        public string Contents { get; }
        public string Exits { get; }
        public string LeadsTo { get; }

        public bool HazardVisibility { get => Hazard.Length > 0; }
        public bool StateVisibility { get => State.Length > 0; }
    }
}
