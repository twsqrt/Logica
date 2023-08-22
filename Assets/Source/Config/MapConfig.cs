using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Config/MapConfig", order = 51)]
    public class MapConfig :ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private Vector2Int _executionPoint;

        public int Widht => _width;
        public int Height => _height;
        public Vector2Int ExecutionPoint => _executionPoint;
    }
}