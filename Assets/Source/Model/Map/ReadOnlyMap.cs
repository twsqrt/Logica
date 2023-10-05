using UnityEngine;

namespace Model.MapLogic
{
    public class ReadOnlyMap
    {
        private readonly Map _map;

        public int Width => _map.Width;
        public int Height => _map.Height;

        public ReadOnlyMap(Map map)
        {
            _map = map;
        }

        
        public ReadOnlyMapTile this[int x, int y]
            => _map[x, y].AsReadOnly();

        public ReadOnlyMapTile this[Vector2Int position]
            => _map[position].AsReadOnly();

        public bool PositionInMap(Vector2Int position)
            => _map.PositionInMap(position);

        public MapVicinity GetVicinity(Vector2Int position)
            => _map.GetVicinity(position);
    }
}