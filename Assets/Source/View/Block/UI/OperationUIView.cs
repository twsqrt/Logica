using UnityEngine;
using View.BlockLogic.ViewDataLogic;
using UnityEngine.UI;

namespace View. BlockLogic
{
    public class OperationUIView : BlockUIView
    {
        [SerializeField] private Image _operationImage;

        public void Init(OperationViewData viewData)
        {
            base.Init(viewData);

            _operationImage.sprite = viewData.OperationSprite;
        }
    }
}