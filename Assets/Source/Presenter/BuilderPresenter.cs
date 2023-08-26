using UnityEngine;
using Model.BlockLogic.LogicOperationLogic;
using Model;
using Model.BlockLogic.BlockDataLogic;

namespace Presenter
{
    public class BuilderPresenter
    {
        private readonly TreeBlockBuilder _model;

        //temp
        private readonly IBlockData _blockData;

        public BuilderPresenter(TreeBlockBuilder model)
        {
            _model = model;
            _blockData = new OperationData(LogicOperationType.OR);
        }

        public bool TryPlace(Vector2Int position)
            => _model.TryPlace(position, _blockData);
    }
}