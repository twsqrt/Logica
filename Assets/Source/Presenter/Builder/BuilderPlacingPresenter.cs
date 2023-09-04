using System;
using System.Collections.Generic;
using System.Linq;
using Model.BuilderLogic;
using Model.BlockLogic.BlockDataLogic;
using UnityEngine;

namespace Presenter.BuilderLogic
{
    public class BuilderPlacingPresenter : BuilderPresenterState
    {
        private IBlockData _currentData;
        private bool _isDataSelected;

        public event Action<IBlockData> OnDataSelected;

        private void ResetData()
        {
            _currentData = null;
            _isDataSelected = false;
        }

        public BuilderPlacingPresenter(BlockBuilder builder) : base(builder)
        {
            ResetData();
        }

        public void SelectBlockData(IBlockData data)
        {
            _currentData = data;
            if(data != null)
            {
                _isDataSelected = true;
                OnDataSelected?.Invoke(data);
            }
        }

        public override void Exit()
        {
            base.Exit();
            ResetData();
        }

        public override IEnumerable<Vector2Int> GetCorrectPositions(IEnumerable<Vector2Int> positions)
            => positions.Where(_builder.CanPlace);

        public override bool TryExecute(Vector2Int position)
        {
            if(_isDataSelected == false)
                return false;

            return _builder.TryPlace(position, _currentData);
        }
    }
}