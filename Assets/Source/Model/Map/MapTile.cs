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

        public MapTile()
        {
            _block = null;
        }

        public Block Block => _block;
        public bool IsOccupied => _block != null;

        public bool TryPlaceBlock(Block block)
        {
            if(IsOccupied)
                return false;
            
            _block = block;
            OnBlockPlaced?.Invoke(block);
            return true;
        }

        public bool TryRemoveBlock()
        {
            if(IsOccupied == false || _block.TryRemove() == false)
                return false;
            
            _block = null;
            OnBlockRemoved?.Invoke();
            return true;
        }
    }
}