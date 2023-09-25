using System.Collections.Generic;

namespace Model.LevelTaskLogic
{
    public class LevelTasks
    {
        private readonly SortedDictionary<LevelScore, IEnumerable<ILevelTask>> _scoreTasks;

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