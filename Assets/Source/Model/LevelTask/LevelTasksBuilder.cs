using System.Collections.Generic;
using System.Linq;

namespace Model.LevelTaskLogic
{
    public class LevelTasksBuilder
    {
        private List<(LevelScore, ILevelTask)> _tasks;

        public LevelTasksBuilder()
        {
            _tasks = new List<(LevelScore, ILevelTask)>();
        }

        public LevelTasksBuilder StartBuilding()
        {
            _tasks.Clear();
            return this;
        } 

        public LevelTasksBuilder Register(LevelScore score, ILevelTask task)
        {
            _tasks.Add((score, task));
            return this;
        }

        public LevelTasksBuilder RegisterForOneStar(ILevelTask task)
            => Register(LevelScore.ONE_STAR, task);

        public LevelTasksBuilder RegisterForTwoStars(ILevelTask task)
            => Register(LevelScore.TWO_STARS, task);

        public LevelTasksBuilder RegisterForTreeStars(ILevelTask task)
            => Register(LevelScore.TREE_STARS, task);
        
        public LevelTasks Build()
        {
            Dictionary<LevelScore, IEnumerable<ILevelTask>> dictionary = _tasks
                .GroupBy(t => t.Item1)
                .ToDictionary(g => g.Key, g => g.Select(g => g.Item2));
            
            return new LevelTasks(dictionary);
        }
    }
}