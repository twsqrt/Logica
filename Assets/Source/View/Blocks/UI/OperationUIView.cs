using UnityEngine;
using View.Blocks.ViewData;
using UnityEngine.UI;

namespace View.Blocks.UI
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