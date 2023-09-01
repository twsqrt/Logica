using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "OperationViewData", menuName = "View/ViewData/OperationViewData", order = 51)]
    public class OperationViewData : BlockViewData
    {
        [SerializeField] private Sprite _operationSprite;

        public Sprite OperationSprite => _operationSprite;
    }
}