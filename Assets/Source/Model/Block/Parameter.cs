using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public class Parameter : Block
    {
        private readonly int _id;

        public int Id => _id;

        public Parameter(int id, BlockPositionContext context) : base(context)
        {
            _id = id;
        }

        public override bool CanAppend(Vector2Int operandPosition) => false;

        public override void Append(Block operand)
        {
            throw new InvalidOperationException();
        }

        public override bool IsCorrectTree() => true;

        public override bool CanBeRemoved() => true;

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}