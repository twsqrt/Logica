using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View.HighlighterLogic
{
    public class ColorHighlighter : MonoBehaviour, IHighlighter
    {
        [SerializeField] private Color _highlightColor;
        [SerializeField] [Range(0f, 1f)] private float _lerpCoefficient;
 
        private Dictionary<MeshRenderer, Color> _defaultColors;

        public void Init()
        {
            IEnumerable<MeshRenderer> renderers = GetComponentsInChildren<MeshRenderer>();
            _defaultColors = renderers.ToDictionary(r => r, r => r.material.color);
        }

        public void HighlightEnable()
        {
            foreach(var (renderer, defaultColor) in _defaultColors)
                renderer.material.color = Color.Lerp(defaultColor, _highlightColor, _lerpCoefficient);
        }

        public void HighlightDisable()
        {
            foreach(var (renderer, defaultColor) in _defaultColors)
                renderer.material.color = defaultColor;
        }
    }
}