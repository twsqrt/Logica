using View.HighlighterLogic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class ExecutionButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UIColorHighlighter _highlighter;

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}