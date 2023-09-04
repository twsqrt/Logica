using Model;
using Presenter.BuilderLogic;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using View.HighlighterLogic;
using View.MapLogic;

namespace View.BuilderLogic
{
    public class BuilderView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private IEnumerable<Vector2Int> _allPositions;
        private MapView _mapView;
        private BuilderPresenter _presenter;
        private Func<Vector2Int, bool> _placeRule;

        private IEnumerable<IHighlighter> _activeHighlihghters;

        public void Init(MapView mapView, BuilderPresenter presenter, TreeBlockBuilder treeBlockBuilder)
        {
            _mapView = mapView;
            _presenter = presenter;

            _placeRule = treeBlockBuilder.CanPlace;

            IEnumerable<int> widthRange = Enumerable.Range(0, _mapView.Width);
            IEnumerable<int> heightRange = Enumerable.Range(0, _mapView.Height);
            _allPositions = widthRange.SelectMany( _ => heightRange, (x, y) => new Vector2Int(x, y));

            HighlightCorrectPositions();
        }

        private void HighlightCorrectPositions()
        {
            _activeHighlihghters = _allPositions.Where(_placeRule).Select(p => _mapView[p].Highlighter).ToArray();
            foreach(IHighlighter highlighter in _activeHighlihghters)
                highlighter.HighlightEnable();
        }

        private void HighlightDisable()
        {
            foreach(IHighlighter highlighter in _activeHighlihghters)
                highlighter.HighlightDisable();
        }

        private void HighlightRefresh()
        {
            HighlightDisable();
            HighlightCorrectPositions();
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if(_mapView.TryGetPosition(ray, out Vector2Int position))
                {
                    if(_presenter.TryPlace(position))
                        HighlightRefresh();
                }
            }
        }
    }
}