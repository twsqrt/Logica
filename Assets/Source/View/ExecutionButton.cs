using Presenter.BuilderLogic;
using Presenter;
using UnityEngine.EventSystems;
using UnityEngine;
using View.HighlighterLogic;

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

        public void Init(BuilderPresenter builder, ExecutionPresenter presenter)
        {
            _presenter = presenter;

            builder.OnExecuted += UpdateButtonView;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _presenter.TryExecute();
        }
    }
}