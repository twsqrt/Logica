using Model.LogicBlockLogic;
using UnityEditor;

namespace Model.MapLogic
{
    public class MapTile
    {
        private LogicBlock _block;

        public LogicBlock Block
        {
            get => _block;
            set
            {
                _block?.TryRemove();
                _block = value;
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