using System.Collections.Generic;
using System.Linq;
using Model.LevelTaskLogic;
using UnityEngine;

namespace View.LevelTaskLogic
{
    public class LevelTasksView : MonoBehaviour
    {
        [SerializeField] private RectTransform _oneStarHeaderPrefab;
        [SerializeField] private RectTransform _twoStarsHeaderPrefab;
        [SerializeField] private RectTransform _treeStarsHeaderPrefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private LevelTaskViewFactory _taskViewFactory;

        private Dictionary<LevelScore, RectTransform> _headherPrefabs;
        private IEnumerable<LevelScore> _levelScores;
        private LevelTasks _levelTasks;

        public void Init(LevelTasks levelTasks)
        {
            _headherPrefabs = new Dictionary<LevelScore, RectTransform>()
            {
                {LevelScore.ONE_STAR, _oneStarHeaderPrefab},
                {LevelScore.TWO_STARS, _twoStarsHeaderPrefab},
                {LevelScore.TREE_STARS, _treeStarsHeaderPrefab},
            };
            _levelScores = _headherPrefabs.Keys;
            _levelTasks = levelTasks;

            Render();
        }

        private void Render()
        {
            foreach(LevelScore score in _levelScores)
            {
                IEnumerable<ILevelTask> tasks = _levelTasks[score];
                if(tasks.Any())
                {
                    Instantiate(_headherPrefabs[score], _container);
                    foreach(ILevelTask task in tasks)
                    {
                        LevelTaskView view = task.Accept(_taskViewFactory);
                        view.transform.SetParent(_container, false);
                    }
                }
            }
        }
    }
}