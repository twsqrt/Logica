using Model.MapLogic;
using UnityEngine;
using View.BuilderLogic;
using View.LogicBlockLogic;

namespace View.MapLogic
{
    public class MapTileView : MonoBehaviour
    {
        //template solution
        [SerializeField] private LogicBlockView _logicBlockPrefab;
        [SerializeField] private ImageHighlighter _highlighter;

        private LogicBlockView _blockView;

        public IHighlighter Highlighter => _highlighter;

        public void Init(MapTile tile)
        {
            //tile.OnBlockChange += _ => OnBlockChangedHandler();
        }

        private void OnBlockChangedHandler()
        {
            if(_blockView != null)
                Destroy(_blockView);
            
            _blockView = Instantiate(_logicBlockPrefab, transform);
        }
    }
}