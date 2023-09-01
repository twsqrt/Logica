using UnityEngine;

namespace View.HighlighterLogic
{
    public class ComponentHighlighter : MonoBehaviour, IHighlighter
    {
        [SerializeField] MonoBehaviour _component;

        public void HighlightEnable()
            => _component.enabled = true;

        public void HighlightDisable()
            => _component.enabled = false;
    }
}