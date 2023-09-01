using Model.BlockLogic;
using Model.MapLogic;
using UnityEngine;
using View.BlockLogic;
using View.HighlighterLogic;

namespace View.MapLogic
{
    public class MapTileView : MonoBehaviour
    {
        [SerializeField] private BlockViewFactory _blockFactory;
        [SerializeField] private ImageHighlighter _highlighter;

        public IHighlighter Highlighter => _highlighter;

        public void Init(MapTile tile)
        {
            tile.OnBlockChange += OnBlockChangedHandler;
        }

        private void OnBlockChangedHandler(Block block)
        {
            BlockView blockView = block.Accept(_blockFactory);
            blockView.transform.SetParent(transform, false);
        }
    }
}