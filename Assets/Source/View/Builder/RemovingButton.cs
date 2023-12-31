using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;
using Presenter.Builder;
using View.Highlighters;

namespace View.Builder
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
            => _builderPresenter.SetMode(BuildingMode.REMOVING);
    }
}