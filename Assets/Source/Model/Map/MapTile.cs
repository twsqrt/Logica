using System;
using Model.BlockLogic;
using UnityEngine;

namespace Model.MapLogic
{
    public class MapTile
    {
        private Block _block;

        public event Action<Block> OnBlockChange;

        public Block Block
        {
            get => _block;
            set
            {
                _block?.TryRemove();
                _block = value;
                OnBlockChange?.Invoke(value);
            }
        }

        public MapTile()
        {
            _block = null;
        }

        public bool IsOccupied => Block != null;

        public void RemoveBlock() => Block = null;
    }
}