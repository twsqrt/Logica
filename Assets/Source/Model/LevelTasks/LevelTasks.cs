using System.Collections.Generic;
using System.Linq;
using Configs.LevelConfigs.LevelTasksConfigs;
using Model.LevelStateLogic;
using Model.LevelTasks;

namespace Model.LevelTasksLogic
{
    public class LevelTasks
    {
        private readonly SortedDictionary<LevelScore, IEnumerable<ILevelTask>> _scoreTasks;

        public IEnumerable<ILevelTask> this[LevelScore score]
            => _scoreTasks[score];
        
        public IEnumerable<LevelScore> Scores 
            => _scoreTasks.Keys;

        public LevelTasks(LevelTasksConfig config, LevelTaskFactory factory)
        {
            var scoreTasks = new Dictionary<LevelScore, IEnumerable<ILevelTask>>();
            IEnumerable<LevelScore> scores = new[]
            {
                LevelScore.ONE_STAR, 
                LevelScore.TWO_STARS, 
                LevelScore.TREE_STARS
            };

            foreach(LevelScore score in scores)
            {
                IEnumerable<ITaskConfig> configs = config.GetTasks(score);
                if(score == LevelScore.ONE_STAR)
                    configs = configs.Concat(new[]{config.FormulaTask});

                scoreTasks.Add(score, configs.Select(c => c.Accept(factory)));
            }

            _scoreTasks = new SortedDictionary<LevelScore, IEnumerable<ILevelTask>>(scoreTasks);
        }
    }
}