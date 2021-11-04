using System;
using System.Collections.Generic;
using System.Text;
using libGenerator;
using libGenerator.Dungeon;

namespace libGenerator.Map
{
    public class MapLocationGenerator
    {
        public MapLocationGenerator(DungeonGenerator dungeonGenerator, string region)
        {
            this.dungeonGenerator = dungeonGenerator;
            this.region = region;
        }

        public MapPoint generateLocation(TerrainData t)
        {
            string name = generateNameForLocation(region);
            var dungeon = dungeonGenerator.GenerateAdventure(region);
            var resources = selectResourcesForTerrain(t);
            return new MapPoint(name, t, dungeon, resources);
        }

        private string generateNameForLocation(string region)
        {
            throw new NotImplementedException();
        }

        private ResourceData selectResourcesForTerrain(TerrainData t)
        {
            return new ResourceData();
        }

        private readonly DungeonGenerator dungeonGenerator;
        private readonly string region; 
    }
}
