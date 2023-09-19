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
        
        private TreeVerifier _treeVerifier;
        private bool _isTreeCorrect;

        public bool IsTreeCorrect
        {
            get => _isTreeCorrect;
            private set 
            {
                _isTreeCorrect = value;

                if(_isTreeCorrect) 
                    _highlighter.HighlightEnable();
                else
                    _highlighter.HighlightDisable();
            }
        }

        private void CheckTree(Block root)
            => IsTreeCorrect = root != null && root.Accept(_treeVerifier);

        public void Init(Map map, BuilderPresenter builderPresenter, TreeVerifier treeVerifier)
        {
            _treeVerifier = treeVerifier;
            MapTile rootTile = map[map.RootPosition];
            builderPresenter.OnExecuted += () => CheckTree(rootTile.Block);

            CheckTree(rootTile.Block);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}