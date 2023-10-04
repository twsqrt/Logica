using System;
using Model.BlocksLogic;
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

        public void PlaceBlock(Block block)
        {
            if(IsOccupied)
                throw new InvalidOperationException();
            
            _block = block;
            OnBlockPlaced?.Invoke(block);
        }

        public void RemoveBlock()
        {
            _block?.Destroy();
            _block = null;
            OnBlockRemoved?.Invoke();
        }
    }
}