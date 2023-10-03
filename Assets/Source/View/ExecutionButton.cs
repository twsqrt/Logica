using Presenter;
using UnityEngine.EventSystems;
using UnityEngine;
using View.HighlighterLogic;
using Model.TreeLogic;
using Zenject;

namespace View
{
    public class ExecutionButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UIColorHighlighter _highlighter;

        private ExecutionPresenter _presenter;

        private void UpdateButtonView()
        {
            if(_presenter.CanExecute())
                _highlighter.HighlightEnable();
            else
                _highlighter.HighlightDisable();
        }

        [Inject] private void Init(BlockTree tree, ExecutionPresenter presenter)
        {
            _presenter = presenter;
            tree.OnChanged += UpdateButtonView;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _presenter.TryExecute();
        }
    }
}