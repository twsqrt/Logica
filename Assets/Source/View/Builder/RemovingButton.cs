using Presenter.BuilderLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using View.HighlighterLogic;

namespace Veiw.BuilderLogic
{
    public class RemovingButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ComponentHighlighter _highlighter;
        private BuilderPresenter _builderPresenter;

        public void Init(BuilderPresenter builderPresenter, RemovingPresenter removingPresenter)
        {
            _builderPresenter = builderPresenter;

            removingPresenter.OnEnter += _highlighter.HighlightEnable;
            removingPresenter.OnExit += _highlighter.HighlightDisable;  
        }

        public void OnPointerClick(PointerEventData eventData)
            => _builderPresenter.ChangeState(BuilderPresenterStateType.REMOVING);
    }
}