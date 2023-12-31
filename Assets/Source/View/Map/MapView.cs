using UnityEngine;
using Model.MapLogic;
using System;
using Zenject;

namespace View.MapLogic
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private MeshCollider _quad;
        [SerializeField] private Transform _tileContainer;
        [SerializeField] private MapTileView _tilePrefab;
        [SerializeField] private float _scale;

        private const int MAP_COLLIDER_LAYER = 1 << 3; 

        private int _width;
        private int _height;
        private MapTileView[] _tiles;

        public int Width => _width;
        public int Height => _height;

        public MapTileView this[int x, int y]
        {
            get => _tiles[x + _width * y];
            private set 
            {
                _tiles[x + _width * y] = value;
            }
        }

        public MapTileView this[Vector2Int position] 
            => this[position.x, position.y];

        private void Render(ReadOnlyMap map)
        {
            Vector3 offset = new Vector3(map.Width - 1f, map.Height - 1f, 0f) * 0.5f * _scale;
            for(int i =0; i < map.Width; i++)
            {
                for(int j =0; j < map.Height; j++)
                {
                    MapTileView view = Instantiate(_tilePrefab, _tileContainer);
                    view.transform.localPosition = new Vector3(i, j, 0f) * _scale - offset;
                    view.Init(map[i, j]);

                    this[i, j] = view;
                }
            }
        }

        [Inject] private void Init(ReadOnlyMap map)
        {
            _width = map.Width;
            _height = map.Height;
            _tiles = new MapTileView[_width * _height];

            _quad.transform.localScale = new Vector3(_width, _height, 1f) * _scale;
            Render(map);
        }

        public bool TryGetPosition(Ray ray, out Vector2Int position)
        {
            if(Physics.Raycast(ray, out RaycastHit hit, 64f, MAP_COLLIDER_LAYER))
            {
                Vector3 offset = _quad.transform.position - new Vector3(_width, _height, 0f) * 0.5f * _scale;
                Vector3 positionOnQuad = hit.point - offset;

                position = new Vector2Int((int)(positionOnQuad.x/_scale), (int)(positionOnQuad.y/_scale));
                return true;
            }

            position = Vector2Int.zero;
            return false;
        }
    }
}