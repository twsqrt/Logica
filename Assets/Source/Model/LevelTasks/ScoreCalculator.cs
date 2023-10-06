using System.Collections.Generic;
using Model.LevelStateLogic;
using UnityEngine.UIElements;

namespace Model.LevelTasksLogic
{
    public class ScoreCalculator
    {
        private readonly LevelState _levelState;
        private readonly LevelTasks _tasks;
        private readonly IEnumerable<LevelScore> _allScores;

        public ScoreCalculator(LevelState levelState, LevelTasks tasks)
        {
            _levelState = levelState;
            _tasks = tasks;

            _allScores = new[]
            {
                LevelScore.ONE_STAR, 
                LevelScore.TWO_STARS, 
                LevelScore.TREE_STARS
            };
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
            foreach(LevelScore score in _allScores)
            {
                if(IsAllTaskCompleted(_tasks[score]))
                    result = score;
                else
                    return result;
            }

            return LevelScore.TREE_STARS;
        }
    }
}