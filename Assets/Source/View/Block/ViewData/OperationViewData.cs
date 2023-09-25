using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "Operation View Data", menuName = "Data/Operation View", order = 51)]
    public class OperationViewData : BlockViewData
    {
        [SerializeField] private Sprite _operationSprite;

        public Sprite OperationSprite => _operationSprite;
    }
}