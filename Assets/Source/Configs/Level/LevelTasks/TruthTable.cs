using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class TruthTable
    {
        private class Comparer : IEqualityComparer<IEnumerable<bool>>
        {
            public bool Equals(IEnumerable<bool> x, IEnumerable<bool> y)
                => x.SequenceEqual(y);

            public int GetHashCode(IEnumerable<bool> obj)
                => obj.Sum(b => b.GetHashCode());
        }

        private Dictionary<IEnumerable<bool>, bool> _table;

        public bool this[IEnumerable<bool> parameters]
            => _table[parameters];

        public TruthTable(Dictionary<IEnumerable<bool>, bool> table)
        {
            _table = new Dictionary<IEnumerable<bool>, bool>(table, new Comparer());
        }
    }
}