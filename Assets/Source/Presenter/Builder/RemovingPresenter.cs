using System.Collections.Generic;
using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Presenter.Builder
{
    public class RemovingPresenter : BuilderPresenterState
    {
        private readonly Map _map;

        public RemovingPresenter(Map map)
        {
            _map = map;
        }

        public override bool IsPositionCorrect(Vector2Int position)
        {
            MapTile tile = _map[position];
            return tile.IsOccupied && tile.Block.HasOperands() == false;
        }

        public override bool TryExecute(Vector2Int position)
        {
            if(IsPositionCorrect(position))
            {
                _map[position].RemoveBlock();
                return true;
            }

            return false;
        }
    }
}