using System;
using System.Collections.Generic;
using UnityEngine;
using Model.BuilderLogic;

namespace Presenter.BuilderLogic
{
    public abstract class BuilderPresenterState
    {
        protected readonly BlockBuilder _builder;
        public event Action OnEnter;
        public event Action OnExit;
        public abstract bool TryExecute(Vector2Int position);
        public abstract IEnumerable<Vector2Int> GetCorrectPositions(IEnumerable<Vector2Int> positions);

        public BuilderPresenterState(BlockBuilder builder)
        {
            _builder = builder;
        }

        public virtual void Enter()
            => OnEnter?.Invoke();

        public virtual void Exit()
            => OnExit?.Invoke();
    }
}