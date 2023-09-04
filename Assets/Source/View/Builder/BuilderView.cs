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

        private MapView _mapView;
        private BuilderPresenter _presenter;

        private IEnumerable<IHighlighter> _activeHighlihghters;

        private void HighilghtPositions(IEnumerable<Vector2Int> positions)
        {
            _activeHighlihghters = positions.Select(p => _mapView[p].Highlighter).ToArray();
            foreach(IHighlighter highlighter in _activeHighlihghters)
                highlighter.HighlightEnable();
        }

        private void HighlightDisable()
        {
            foreach(IHighlighter highlighter in _activeHighlihghters)
                highlighter.HighlightDisable();
        }

        private void HighlightRefresh(IEnumerable<Vector2Int> positions)
        {
            HighlightDisable();
            HighilghtPositions(positions);
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if(_mapView.TryGetPosition(ray, out Vector2Int position))
                    _presenter.OnPositionSelected(position);
            }
        }

        public void Init(MapView mapView, BuilderPresenter presenter)
        {
            _mapView = mapView;

            _presenter = presenter;
            HighilghtPositions(presenter.CorrectPositions);
            _presenter.OnCorrectPositionsChanged += HighlightRefresh;
        }
    }
}