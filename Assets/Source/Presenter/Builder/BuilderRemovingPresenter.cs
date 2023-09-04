using System.Collections.Generic;
using System.Linq;
using Model.BuilderLogic;
using UnityEngine;

namespace Presenter.BuilderLogic
{
    public class BuilderRemovingPresenter : BuilderPresenterState
    {
        public BuilderRemovingPresenter(BlockBuilder builder) : base(builder) {}

        public override IEnumerable<Vector2Int> GetCorrectPositions(IEnumerable<Vector2Int> positions)
            => Enumerable.Empty<Vector2Int>();

        public override bool TryExecute(Vector2Int position)
            => _builder.TryRemove(position);
    }
}