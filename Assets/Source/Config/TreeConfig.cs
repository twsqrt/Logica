using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "TreeConfig", menuName = "Config/TreeConfig", order = 51)]
    public class TreeConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _rootPosition;
        
        public Vector2Int RootPosition => _rootPosition;
    }
}