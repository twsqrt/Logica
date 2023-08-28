using UnityEngine;
using View.HighlighterLogic;

namespace View.BlockDataLogic
{
    public class BlockDataView : MonoBehaviour
    {
        [SerializeField] private ColorHighlighter _highlighter;

        public IHighlighter Highlighter => _highlighter;

        public virtual void Init()
        {
            _highlighter.Init();
        }
    }
}