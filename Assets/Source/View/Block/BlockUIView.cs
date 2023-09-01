using UnityEngine;
using UnityEngine.UI;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public abstract class BlockUIView : MonoBehaviour
    {
        [SerializeField] private Image _blockImage;

        protected void Init(BlockViewData viewData)
        {
            _blockImage.sprite = viewData.BlockSprite;
            _blockImage.color = viewData.BlockColor;
        }
    }
}