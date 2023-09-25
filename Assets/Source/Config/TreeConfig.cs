using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Tree Config", menuName = "Config/Tree", order = 51)]
    public class TreeConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _rootPosition;
        
        public Vector2Int RootPosition => _rootPosition;
    }
}