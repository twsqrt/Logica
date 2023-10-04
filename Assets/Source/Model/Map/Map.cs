using Configs.LevelConfigs;
using Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Model.MapLogic
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;
        private readonly MapTile[] _tiles;

        public int Width => _width;
        public int Height => _height;

        public Map(MapConfig config)
        {
            _width = config.Width;
            _height = config.Height;

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

        public bool PositionInMap(Vector2Int position)
            => position.x.IsBetween(0, _width - 1) 
            && position.y.IsBetween(0, _height - 1);

        public IEnumerable<Vector2Int> GetVicinity(Vector2Int position)
        {
            var vicinityOffsets = new[]{ Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            foreach(Vector2Int vicinityOffset in vicinityOffsets)
            {
                Vector2Int vicinityPosition = position + vicinityOffset;
                if(PositionInMap(vicinityPosition))
                    yield return vicinityPosition;
            }
        }

        public ReadOnlyMap AsReadOnly()
            => new ReadOnlyMap(this);
    }
}