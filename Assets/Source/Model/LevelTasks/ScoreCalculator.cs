using System.Collections.Generic;
using Model.LevelStateLogic;
using UnityEngine.UIElements;

namespace Model.LevelTasksLogic
{
    public class ScoreCalculator
    {
        private readonly LevelState _levelState;
        private readonly LevelTasks _tasks;

        public ScoreCalculator(LevelState levelState, LevelTasks tasks)
        {
            _levelState = levelState;
            _tasks = tasks;
        }

        private bool IsAllTaskCompleted(IEnumerable<ILevelTask> tasks)
        {
            foreach(ILevelTask task in tasks)
            {
                if(task.CheckCompletion(_levelState) == false)
                    return false;
            }

            return true;
        }

        public LevelScore CalculateScore()
        {
            LevelScore result = LevelScore.NOT_FINISHED;
            foreach(LevelScore score in _tasks.Scores)
            {
                if(IsAllTaskCompleted(_tasks[score]))
                    result = score;
                else
                    return result;
            }

            return LevelScore.THREE_STARS;
        }
    }
}