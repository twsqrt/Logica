using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Model.LevelTaskLogic
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

        private bool IsAllTaskCompleted(IEnumerable<ILevelTask> tasks)
        {
            foreach(ILevelTask task in tasks)
            {
                if(task.CheckCompletion() == false)
                    return false;
            }

            return true;
        }

        public LevelScore CalculateScore()
        {
            LevelScore result = LevelScore.NOT_FINISHED;
            foreach(var (score, tasks) in _scoreTasks)
            {
                if(IsAllTaskCompleted(tasks))
                    result = score;
                else
                    return result;
            }

            return LevelScore.TREE_STARS;
        }
    }
}