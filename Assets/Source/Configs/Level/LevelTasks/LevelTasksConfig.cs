using System;
using System.Collections.Generic;
using System.Linq;
using Model.LevelTasksLogic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class LevelTasksConfig
    {
        [JsonProperty("formulaTask")] private FormulaTaskConfig _formulaTask;
        [JsonProperty("oneStarTasks")] private TypedTaskConfig[] _oneStarTasks;
        [JsonProperty("twoStarsTasks")] private TypedTaskConfig[] _twoStarsTasks;
        [JsonProperty("treeStarsTasks")] private TypedTaskConfig[] _treeStarsTasks;
        
        public FormulaTaskConfig FormulaTask => _formulaTask;

        public IEnumerable<ITaskConfig> ScoreTasks(LevelScore levelScore)
        {
            IEnumerable<TypedTaskConfig> typedTasks = levelScore switch
            {
                LevelScore.ONE_STAR => _oneStarTasks,
                LevelScore.TWO_STARS => _twoStarsTasks,
                LevelScore.TREE_STARS => _treeStarsTasks,
                _ => throw new ArgumentException(),
            };

            return typedTasks.Select(t => t.TaskConfig);
        }
    }
}