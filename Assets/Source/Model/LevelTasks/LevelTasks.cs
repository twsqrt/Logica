using System.Collections.Generic;
using System.Linq;
using Model.LevelStateLogic;

namespace Model.LevelTasksLogic
{
    public class LevelTasks
    {
        private readonly SortedDictionary<LevelScore, IEnumerable<ILevelTask>> _scoreTasks;

        public IEnumerable<ILevelTask> this[LevelScore score]
        {
            get 
            {
                if(_scoreTasks.TryGetValue(score, out IEnumerable<ILevelTask> tasks))
                    return tasks;
                return Enumerable.Empty<ILevelTask>();
            }
        }

        public LevelTasks(Dictionary<LevelScore, IEnumerable<ILevelTask>> scoreTasks)
        {
            _scoreTasks = new SortedDictionary<LevelScore, IEnumerable<ILevelTask>>(scoreTasks);
        }
    }
}