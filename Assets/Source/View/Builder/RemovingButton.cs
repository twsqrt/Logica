using Presenter.BuilderLogic;
using UnityEngine.EventSystems;
using UnityEngine;
using View.HighlighterLogic;
using Zenject;

namespace Veiw.BuilderLogic
{
    public class RemovingButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ComponentHighlighter _highlighter;
        private BuilderPresenter _builderPresenter;

        [Inject] private void Init(BuilderPresenter builderPresenter, RemovingPresenter removingPresenter)
        {
            _builderPresenter = builderPresenter;

            removingPresenter.OnEnter += _highlighter.HighlightEnable;
            removingPresenter.OnExit += _highlighter.HighlightDisable;  
        }

        public void OnPointerClick(PointerEventData eventData)
            => _builderPresenter.ChangeState(BuilderPresenterStateType.REMOVING);
    }
}