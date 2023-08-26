using UnityEngine;
using Model.BlockLogic.LogicOperationLogic;
using Model;

namespace Presenter
{
    public class BuilderPresenter
    {
        private readonly TreeBlockBuilder _model;

        public BuilderPresenter(TreeBlockBuilder model)
        {
            _model = model;
        }

        public bool TryPlace(Vector2Int position, LogicOperationType blockType)
            => _model.TryPlace(position, blockType);
    }
}