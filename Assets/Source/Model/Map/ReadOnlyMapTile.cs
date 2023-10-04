using System;
using Model.BlocksLogic;

namespace Model.MapLogic
{
    public class ReadOnlyMapTile
    {
        private readonly MapTile _tile;

        public event Action<IReadOnlyBlock> OnBlockPlaced;
        public event Action OnBlockRemoved; 

        public IReadOnlyBlock Block => _tile.Block;
        public bool IsOccupied => _tile.IsOccupied;

        public ReadOnlyMapTile(MapTile tile)
        {
            _tile = tile;

            _tile.OnBlockPlaced += b => OnBlockPlaced?.Invoke(b);
            _tile.OnBlockRemoved += () => OnBlockRemoved?.Invoke();
        }
    }
}