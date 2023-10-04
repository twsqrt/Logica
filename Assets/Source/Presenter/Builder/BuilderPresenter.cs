using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Model.MapLogic;

namespace Presenter.Builder
{
    public class BuilderPresenter
    {
        private readonly BuilderPresenterState _placingState;
        private readonly BuilderPresenterState _removingState;
        private readonly IEnumerable<Vector2Int> _allPositions;

        private BuildingMode _currentMode;
        private BuilderPresenterState _currentState; 

        public IEnumerable<Vector2Int> CorrectPositions 
            => _allPositions.Where(_currentState.IsPositionCorrect);
        
        public BuildingMode Mode => _currentMode;

        public event Action<BuildingMode> OnModeChanged;
        public event Action OnExecuted;

        public BuilderPresenter(Map map, BuilderPresenterState placing, BuilderPresenterState removing)
        {
            IEnumerable<int> widthRange = Enumerable.Range(0, map.Width);
            IEnumerable<int> heightRange = Enumerable.Range(0, map.Height);
            _allPositions = widthRange.SelectMany( _ => heightRange, (x, y) => new Vector2Int(x, y));

            _placingState = placing;
            _removingState = removing;
            _currentState = placing;
            _currentMode = BuildingMode.PLACING;
            _currentState.Enter();
        }

        public void SetMode(BuildingMode mode)
        {
            _currentState.Exit();

            if(mode == BuildingMode.PLACING)
                _currentState = _placingState;
            else
                _currentState = _removingState;
            
            _currentState.Enter();
            _currentMode = mode;
            OnModeChanged.Invoke(mode);
        }

        public void OnPositionSelected(Vector2Int position)
        {
            if(_currentState.TryExecute(position))
                OnExecuted?.Invoke();
        }
    }
}