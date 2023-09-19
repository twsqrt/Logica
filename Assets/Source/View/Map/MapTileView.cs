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
        [SerializeField] private SpriteHighlighter _tileHighlighter;

        private IHighlighter _currentHighlighter;

        public IHighlighter Highlighter => _currentHighlighter;

        private void OnBlockPlacedHandler(Block block)
        {
            BlockView blockView = block.Accept(_blockFactory);
            blockView.transform.SetParent(transform, false);

            _currentHighlighter = blockView.Highlighter;
        }

        private void OnBlockRemovedHandler()
        {
            _currentHighlighter = _tileHighlighter;

        }

        public void Init(MapTile tile)
        {
            _currentHighlighter = _tileHighlighter;

            tile.OnBlockPlaced += OnBlockPlacedHandler;
            tile.OnBlockRemoved += OnBlockRemovedHandler;
        }
    }
}