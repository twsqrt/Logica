using UnityEngine;
using UnityEngine.UI;

namespace View.HighlighterLogic
{
    public class UIColorHighlighter : MonoBehaviour, IHighlighter
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _highlightColor;

        public void HighlightEnable()
            => _image.color = _highlightColor;

        public void HighlightDisable()
            => _image.color = _defaultColor;
    }
}