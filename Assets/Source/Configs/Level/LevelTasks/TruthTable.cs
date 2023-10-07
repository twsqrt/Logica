using System.Collections.Generic;
using System.Linq;
using Configs.LevelConfigs.JsonConverters;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    [JsonConverter(typeof(TruthTableConverter))]
    public class TruthTable
    {
        private class Comparer : IEqualityComparer<IEnumerable<bool>>
        {
            public bool Equals(IEnumerable<bool> x, IEnumerable<bool> y)
                => x.SequenceEqual(y);

            public int GetHashCode(IEnumerable<bool> obj)
                => obj.Sum(b => b.GetHashCode());
        }

        private readonly Dictionary<IEnumerable<bool>, bool> _table;

        public bool this[IEnumerable<bool> parameters]
            => _table[parameters];

        public TruthTable(Dictionary<IEnumerable<bool>, bool> table)
        {
            _table = new Dictionary<IEnumerable<bool>, bool>(table, new Comparer());
        }
    }
}