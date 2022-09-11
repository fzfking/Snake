using UnityEngine;

namespace Sources.Architecture
{
    public class MapSize
    {
        public int Size { get; private set; }
        private static readonly string Name = "Map size";

        private MapSize(int size)
        {
            Size = size;
        }
        

        public static MapSize CreateNew(int size)
        {
            var newMapSize = new MapSize(size);
            return newMapSize;
        }

        public static MapSize Load()
        {
            var newMapSize = new MapSize(PlayerPrefs.GetInt(Name, 4));
            return newMapSize;
        }

        public static void Save(MapSize mapSize)
        {
            PlayerPrefs.SetInt(Name, mapSize.Size);
        }
    }
}