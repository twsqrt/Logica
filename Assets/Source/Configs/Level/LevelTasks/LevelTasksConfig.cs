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
        [JsonProperty("threeStarsTasks")] private TypedTaskConfig[] _threeStarsTasks;
        
        public FormulaTaskConfig FormulaTask => _formulaTask;

        public IEnumerable<ITaskConfig> GetTasks(LevelScore levelScore)
        {
            IEnumerable<TypedTaskConfig> typedTasks = levelScore switch
            {
                LevelScore.ONE_STAR => _oneStarTasks,
                LevelScore.TWO_STARS => _twoStarsTasks,
                LevelScore.THREE_STARS => _threeStarsTasks,
                _ => throw new ArgumentException(),
            };

            if(typedTasks == null)
                return Enumerable.Empty<ITaskConfig>(); 
            return typedTasks.Select(t => t.TaskConfig);
        }
    }
}