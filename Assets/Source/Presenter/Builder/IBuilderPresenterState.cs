using System;
using System.Collections.Generic;
using UnityEngine;
using Model.BuilderLogic;

namespace Presenter.BuilderLogic
{
    public abstract class BuilderPresenterState
    {
        public event Action OnEnter;
        public event Action OnExit;

        public virtual void Enter()
            => OnEnter?.Invoke();

        public virtual void Exit()
            => OnExit?.Invoke();

        public abstract bool TryExecute(Vector2Int position);
        public abstract IEnumerable<Vector2Int> GetCorrectPositions(IEnumerable<Vector2Int> positions);
    }
}