using System.Collections.Generic;
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
                Instantiate(_headherPrefabs[score], _container);
                foreach(ILevelTask task in _levelTasks[score])
                {
                    LevelTaskView view = task.Accept(_taskViewFactory);
                    view.transform.SetParent(_container, false);
                }
            }
        }
    }
}