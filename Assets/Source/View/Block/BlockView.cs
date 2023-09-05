using Model.BlockLogic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;
using View.HighlighterLogic;

namespace View.BlockLogic
{
    public abstract class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _blockSprite;
        [SerializeField] private SpriteHighlighter _highlighter;
        [SerializeField] private Transform _arrowPrefab;

        public IHighlighter Highlighter => _highlighter;

        private void OnBlockRemoveHandler()
            => Destroy(gameObject);

        protected void Init(BlockViewData data, Block block)
        {
            _blockSprite.sprite = data.BlockSprite;
            _blockSprite.material.color = data.BlockColor;

            block.OnRemove += _ => OnBlockRemoveHandler();

            BlockSide connectionSide = block.Context.ConnectionSide;
            if(connectionSide != BlockSide.UNDEFINED)
            {
                Transform arrow = Instantiate(_arrowPrefab, transform);

                Vector2Int position =  BlockSideMapper.PositionFromBlockSide(connectionSide);
                arrow.localPosition = new Vector3(position.x, position.y, 0f) * 0.5f;

                float angle = BlockSideMapper.AngleFromBlockSide(connectionSide);
                arrow.eulerAngles = new Vector3(0f, 0f, angle);
            }

            _highlighter.Init();
        }
    }
}