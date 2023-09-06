using System;
using Model.BlockLogic;
using UnityEngine;

namespace Model.MapLogic
{
    public class MapTile
    {
        private Block _block;

        public event Action<Block> OnBlockPlaced;
        public event Action OnBlockRemoved; 

        public Block Block => _block;
        public bool IsOccupied => _block != null;

        public MapTile()
        {
            _block = null;
        }

        public bool TryPlace(Block block)
        {
            if(IsOccupied)
                return false;
            
            _block = block;
            OnBlockPlaced?.Invoke(block);
            return true;
        }

        public void RemoveBlock()
        {
            _block?.Destroy();
            _block = null;
            OnBlockRemoved?.Invoke();
        }
    }
}