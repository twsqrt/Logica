using System.Collections.Generic;
using System.Linq;
using Config;
using Extensions;
using UnityEngine;

namespace Model.MapLogic
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Vector2Int _executionPosition;
        private readonly MapTile[] _tiles;

        public int Width => _width;
        public int Height => _height;
        public Vector2Int ExecutionPosition => _executionPosition;

        public Map(MapConfig config)
        {
            _width = config.Widht;
            _height = config.Height;
            _executionPosition = config.ExecutionPoint;

            _tiles = new MapTile[_width * _height]; 
            for(int i = 0; i < _tiles.Length; i++)
                _tiles[i] = new MapTile();
        }

        public MapTile this[int x, int y]
        {
            get => _tiles[x + y * _width];
            private set
            {
                _tiles[x + y * _width] = value;
            }
        }

        public MapTile this[Vector2Int position]
        {
            get => this[position.x, position.y];
            private set
            {
                this[position.x, position.y] = value;
            }
        }

        public static IEnumerable<Vector2Int> GetVicinity(Vector2Int position)
        {
            var vicinity = new[]{Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            for(int i = 0; i < vicinity.Length; i++)
                vicinity[i] += position;
            
            return vicinity;
        }

        public IEnumerable<Vector2Int> GetVicinityInMap(Vector2Int position)
            => GetVicinity(position).Where(p => PositionInMap(p));

        public bool PositionInMap(int x, int y)
            => x.IsBetween(0, _width - 1) && y.IsBetween(0, _height - 1);

        public bool PositionInMap(Vector2Int position)
            => PositionInMap(position.x, position.y);

        public bool CanPlace(Vector2Int position)
        {
            if(PositionInMap(position.x, position.y))
                return this[position].IsOccupied == false;
            return false;
        }
    }
}