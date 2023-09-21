using View.HighlighterLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using Model.TreeLogic;
using Model.MapLogic;
using Presenter.BuilderLogic;
using Model.BlockLogic;
using Presenter;
using Unity.VisualScripting;

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

        }
    }
}