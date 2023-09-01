using Model.BlockLogic.BlockDataLogic;
using Model;
using System;
using UnityEngine;

namespace Presenter.BuilderLogic
{
    public class BuilderPresenter
    {
        private readonly TreeBlockBuilder _builder;

        private IBlockData _currentData;

        public event Action<IBlockData> OnDataSelected;

        public BuilderPresenter(TreeBlockBuilder builder)
        {
            _builder = builder;
            _currentData = null;
        }

        public void SelectData(IBlockData data)
        {
            _currentData = data;
            OnDataSelected?.Invoke(data);
        }


        public bool TryPlace(Vector2Int position)
        {
            if(_currentData == null)
                return false;
            return _builder.TryPlace(position, _currentData);
        }
    }
}