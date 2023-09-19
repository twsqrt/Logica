using UnityEngine;

namespace View.HighlighterLogic
{
    public class SpriteHighlighter : MonoBehaviour, IHighlighter
    {
        [SerializeField] private SpriteRenderer _spriteRender;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _highlightSprite;

        public void HighlightEnable()
        {
            _spriteRender.sprite = _highlightSprite;
        }

        public void HighlightDisable()
        {
            _spriteRender.sprite = _defaultSprite;
        }
    }
}