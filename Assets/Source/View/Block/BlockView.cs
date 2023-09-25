using System.Collections.Generic;
using Converter;
using Extensions;
using Model.BlockLogic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;
using View.HighlighterLogic;

namespace View.BlockLogic
{
    public abstract class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _blockSprite;
        [SerializeField] private ColorHighlighter _highlighter;
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
                Direction directionToParent = block.Context.DirectionToParent;

                SpriteRenderer arrow = Instantiate(_arrowPrefab, transform);

                Vector2Int offset =  directionToParent.ToVector();
                arrow.transform.localPosition = new Vector3(offset.x, offset.y, 0f) * 0.5f;

                float angle = directionToParent.Angle();
                arrow.transform.eulerAngles = new Vector3(0f, 0f, angle + 180f);

                _highlighter.Register(arrow);
            }
        }
    }
}