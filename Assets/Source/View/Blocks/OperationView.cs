using Model.BlocksLogic.OperationBlocksLogic;
using UnityEngine;
using View.Blocks.ViewData;

namespace View.Blocks
{
    public class OperationView : BlockView 
    {
        [SerializeField] private SpriteRenderer _operation;

        public void Init(OperationViewData viewData, IReadOnlyOperationBlock operation)
        {
            base.Init(viewData, operation);
            _operation.sprite = viewData.OperationSprite;
        }
    }
}