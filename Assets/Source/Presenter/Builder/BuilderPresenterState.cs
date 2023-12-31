using System;
using UnityEngine;

namespace Presenter.Builder
{
    public abstract class BuilderPresenterState
    {
        public event Action OnEnter;
        public event Action OnExit;

        public virtual void Enter()
            => OnEnter?.Invoke();

        public virtual void Exit()
            => OnExit?.Invoke();

        public abstract bool IsPositionCorrect(Vector2Int position);
        public abstract bool TryExecute(Vector2Int position);
    }
}