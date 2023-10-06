using Presenter;
using UnityEngine.EventSystems;
using UnityEngine;
using View.Highlighters;
using Model.TreeLogic;
using Zenject;

namespace View
{
    public class TreeExecutionButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UIColorHighlighter _highlighter;

        private FinisherPresenter _finisherPresenter;

        private void HighlighterUpdate(bool shouldTurnOn)
        {
            if(shouldTurnOn)
                _highlighter.HighlightEnable();
            else
                _highlighter.HighlightDisable();
        }

        [Inject] private void Init(BlockTree tree, FinisherPresenter finisherPresenter)
        {
            _finisherPresenter = finisherPresenter;
            tree.OnChanged += () => HighlighterUpdate(tree.IsCorrect());
        }

        public void OnPointerClick(PointerEventData eventData)
            => _finisherPresenter.TryFinishLevel();
    }
}