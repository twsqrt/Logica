using System.Collections.Generic;
using UnityEngine;

namespace View.Highlighters
{
    public class ColorHighlighter : MonoBehaviour, IHighlighter
    {
        [SerializeField] private Color _highlightColor;
        [SerializeField] [Range(0f, 1f)] private float _lerpCoefficient;
 
        private Dictionary<SpriteRenderer, Color> _sprites;

        public void Register(SpriteRenderer sprite)
        {
            _sprites.Add(sprite, sprite.color);
        }

        public void Init()
        {
            _sprites = new Dictionary<SpriteRenderer, Color>();
        }

        public void HighlightEnable()
        {
            foreach(var (renderer, defaultColor) in _sprites)
                renderer.color = Color.Lerp(defaultColor, _highlightColor, _lerpCoefficient);
        }

        public void HighlightDisable()
        {
            foreach(var (renderer, defaultColor) in _sprites)
                renderer.color = defaultColor;
        }
    }
}