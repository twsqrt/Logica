using UnityEngine;
using UnityEngine.UI;
using View.Blocks.ViewData;

namespace View.Blocks.UI
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