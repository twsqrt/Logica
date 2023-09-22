using View.HighlighterLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using Model.TreeLogic;
using Model.MapLogic;
using Presenter.BuilderLogic;
using Model.BlockLogic;
using System;

namespace View
{
    public class ExecutionButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UIColorHighlighter _highlighter;

        private BlockTree _tree;
        private bool _isCorrect;

        public event Action OnClick;

        private void UpdateHiglighting()
        {
            _isCorrect = _tree.IsCorrect();
            if(_isCorrect)
                _highlighter.HighlightEnable();
            else
                _highlighter.HighlightDisable();
        }

        public void Init(BlockTree tree, BuilderPresenter builderPresenter)
        {
            _tree = tree;
            _isCorrect = false;

            builderPresenter.OnExecuted += UpdateHiglighting;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isCorrect)
                OnClick?.Invoke();
        }
    }
}