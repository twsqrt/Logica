using Presenter.Builder;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.Highlighters;
using View.MapLogic;
using Zenject;

namespace View.Builder
{
    public class BuilderView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private MapView _mapView;
        private BuilderPresenter _presenter;
        private IEnumerable<IHighlighter> _activeHighlihghters;

        private void HighilghtPositions()
        {
            _activeHighlihghters = _presenter.CorrectPositions.Select(p => _mapView[p].Highlighter).ToArray();
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
            HighilghtPositions();
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

        [Inject] private void Init(MapView mapView, BuilderPresenter presenter)
        {
            _mapView = mapView;
            _presenter = presenter;

            _presenter.OnModeChanged += _ => HighlightRefresh();
            _presenter.OnExecuted += () => HighlightRefresh();

            HighilghtPositions();
        }
    }
}