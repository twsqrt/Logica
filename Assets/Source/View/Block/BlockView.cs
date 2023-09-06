using System.Collections.Generic;
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
        [SerializeField] private SpriteRenderer _arrowPrefab;

        public IHighlighter Highlighter => _highlighter;

        protected void Init(BlockViewData data, Block block)
        {
            _blockSprite.sprite = data.BlockSprite;
            _blockSprite.color = data.BlockColor;

            block.OnDestroy += _ => Destroy(gameObject);

            _highlighter.Init();
            _highlighter.Register(_blockSprite);

            if(block.Context.HasParent)
            {
                BlockSide connectionSide = block.Context.ParentConnectionSide;

                SpriteRenderer arrow = Instantiate(_arrowPrefab, transform);

                Vector2Int position =  BlockSideMapper.PositionFromBlockSide(connectionSide);
                arrow.transform.localPosition = new Vector3(position.x, position.y, 0f) * 0.5f;

                float sideAngle = BlockSideMapper.AngleFromBlockSide(connectionSide);
                arrow.transform.eulerAngles = new Vector3(0f, 0f, sideAngle + 180f);

                _highlighter.Register(arrow);
            }
        }
    }
}