using System.Collections.Generic;
using Model.BlocksLogic;
using UnityEngine;

namespace Model.MapLogic
{
    public class MapVicinity
    {
        private readonly static Vector2Int[] vectorOffsets 
            = new[]{ Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        private readonly Vector2Int _center;
        private readonly Dictionary<Direction, Vector2Int> _positions;

        public Vector2Int Center => _center;
        public IReadOnlyDictionary<Direction, Vector2Int> Positions => _positions;

        private Direction DirectionFromOffset(Vector2Int offset)
        => (offset.x, offset.y) switch
        {
            (1, 0) => Direction.RIGHT,
            (-1, 0) => Direction.LEFT,
            (0, 1) => Direction.UP,
            (0, -1) => Direction.DOWN,
            _ => Direction.NONE,
        };

        public MapVicinity(Map map, Vector2Int center)
        {
            _center = center;

            _positions = new Dictionary<Direction, Vector2Int>();
            foreach(Vector2Int offset in vectorOffsets)
            {
                Vector2Int position = center + offset;
                if(map.PositionInMap(position))
                {
                    Direction fromCenter = DirectionFromOffset(offset);
                    _positions.Add(fromCenter, position);
                }
            }
        }
    }
}