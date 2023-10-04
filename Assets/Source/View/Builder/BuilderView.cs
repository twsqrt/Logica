using Model;
using Presenter.Builder;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using View.Highlighters;
using View.MapLogic;

namespace View.BuilderLogic
{
    public class BuilderView : MonoBehaviour
    {
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

        public void Init(MapView mapView, BuilderPresenter presenter)
        {
            _mapView = mapView;
            _mapView.OnTileClicked += p => presenter.OnPositionSelected(p);

            _presenter = presenter;
            HighilghtPositions(presenter.CorrectPositions);
            _presenter.OnCorrectPositionsChanged += HighlightRefresh;
        }
    }
}