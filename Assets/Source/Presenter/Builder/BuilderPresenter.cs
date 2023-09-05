using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Model.MapLogic;

namespace Presenter.BuilderLogic
{
    public class BuilderPresenter
    {
        private readonly Dictionary<BuilderPresenterStateType, BuilderPresenterState> _states;
        private readonly IEnumerable<Vector2Int> _allPositions;

        private BuilderPresenterState _currentState; 
        private BuilderPresenterStateType _currentStateType;
        private IEnumerable<Vector2Int> _correctPositions;

        public IEnumerable<Vector2Int> CorrectPositions
        {
            get => _correctPositions;
            private set
            {
                _correctPositions = value;
                OnCorrectPositionsChanged?.Invoke(value);
            }
        }

        private void EnterCurrentState()
        {
            _currentState.Enter();
            CorrectPositions = _currentState.GetCorrectPositions(_allPositions);
        }


        public event Action<BuilderPresenterStateType> OnStateChanged;
        public event Action<IEnumerable<Vector2Int>> OnCorrectPositionsChanged;

        public BuilderPresenter(Map map, BuilderPresenterState placing, BuilderPresenterState removing)
        {
            IEnumerable<int> widthRange = Enumerable.Range(0, map.Width);
            IEnumerable<int> heightRange = Enumerable.Range(0, map.Height);
            _allPositions = widthRange.SelectMany( _ => heightRange, (x, y) => new Vector2Int(x, y));

            _states = new Dictionary<BuilderPresenterStateType, BuilderPresenterState>()
            {
                {BuilderPresenterStateType.PLACING, placing},
                {BuilderPresenterStateType.REMOVING, removing},
            };

            _currentStateType = BuilderPresenterStateType.PLACING;
            _currentState = _states[_currentStateType];
            EnterCurrentState();
        }

        public void ChangeState(BuilderPresenterStateType newStateType)
        {
            if(_currentStateType != newStateType)
            {
                _currentState.Exit();

                _currentStateType = newStateType;
                _currentState = _states[newStateType];
                EnterCurrentState();

                OnStateChanged?.Invoke(newStateType);
            }
        }

        public void OnPositionSelected(Vector2Int position)
        {
            if(_currentState.TryExecute(position))
                CorrectPositions = _currentState.GetCorrectPositions(_allPositions);
        }
    }
}