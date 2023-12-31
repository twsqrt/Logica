using System.Collections.Generic;
using System.Linq;
using Model.LevelTasksLogic;
using UnityEngine;
using Zenject;

namespace View.LevelTasksLogic
{
    public class LevelTasksView : MonoBehaviour
    {
        [SerializeField] private LevelTaskViewFactory _taskViewFactory;
        [SerializeField] private RectTransform _oneStarHeaderPrefab;
        [SerializeField] private RectTransform _twoStarsHeaderPrefab;
        [SerializeField] private RectTransform _threeStarsHeaderPrefab;
        [SerializeField] private Transform _container;

        private Dictionary<LevelScore, RectTransform> _headherPrefabs;
        private IEnumerable<LevelScore> _levelScores;
        private LevelTasks _levelTasks;

        [Inject] private void Init(LevelTasks levelTasks)
        {
            _levelTasks = levelTasks;

            _headherPrefabs = new Dictionary<LevelScore, RectTransform>()
            {
                {LevelScore.ONE_STAR, _oneStarHeaderPrefab},
                {LevelScore.TWO_STARS, _twoStarsHeaderPrefab},
                {LevelScore.THREE_STARS, _threeStarsHeaderPrefab},
            };

            _levelScores = _headherPrefabs.Keys;
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