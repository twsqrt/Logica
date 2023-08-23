using System;
using Model.LogicBlockLogic;
using UnityEditor;

namespace Model.MapLogic
{
    public class MapTile
    {
        private LogicBlock _block;

        public event Action<LogicBlock> OnBlockChange;

        public LogicBlock Block
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