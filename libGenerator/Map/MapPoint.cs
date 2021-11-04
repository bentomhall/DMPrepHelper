using libGenerator.Dungeon;

namespace libGenerator.Map
{
    public class MapPoint
    {
        private string name;
        private TerrainData t;
        private AdventureData dungeon;
        private ResourceData resources;

        public MapPoint(string name, TerrainData t, AdventureData dungeon, ResourceData resources)
        {
            this.name = name;
            this.t = t;
            this.dungeon = dungeon;
            this.resources = resources;
        }
    }
}