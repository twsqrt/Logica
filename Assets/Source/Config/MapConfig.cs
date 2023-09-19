using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Config/MapConfig", order = 51)]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private Vector2Int _rootPosition;

        public int Widht => _width;
        public int Height => _height;
        public Vector2Int RootPosition => _rootPosition;
    }
}