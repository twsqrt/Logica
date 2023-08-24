using UnityEngine;
using Model.LogicBlockLogic;
using View.BuilderLogic;
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

        public bool TryPlace(Vector2Int position, LogicBlockType blockType)
            => _model.TryPlace(position, blockType);
    }
}