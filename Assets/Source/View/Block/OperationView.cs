using Model.BlocksLogic;
using Model.BlocksLogic.OperationBlocksLogic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public class OperationView : BlockView 
    {
        [SerializeField] private SpriteRenderer _operation;

        public void Init(OperationViewData viewData, LogicOperation operation)
        {
            base.Init(viewData, operation);
            _operation.sprite = viewData.OperationSprite;
        }
    }
}