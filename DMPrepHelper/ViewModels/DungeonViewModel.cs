using libGenerator.Dungeon;

namespace DMPrepHelper.ViewModels
{
    public class DungeonViewModel : NotifyChangedBase
    {
        private string hasBoss = " with a boss monster";

        public DungeonViewModel(AdventureData d)
        {
            RawData = d;
            OnPropertyChanged(nameof(AdventureType));
            OnPropertyChanged(nameof(Region));
            OnPropertyChanged(nameof(Size));
            OnPropertyChanged(nameof(PrimaryMonster));
        }

        public AdventureData RawData { get; }

        public string AdventureType { get => $"Level {RawData.Level} {RawData.AdventureType} ({RawData.SubType})"; }
        public string Region { get => RawData.Region; }
        public string Size { get => $"A {RawData.Scale} site with {RawData.Size} areas located in {Region}"; }
        public string PrimaryMonster { get => $"Dominated by {RawData.PrimaryMonster} creatures{(RawData.HasBoss ? hasBoss: "")}."; }

    }
}
