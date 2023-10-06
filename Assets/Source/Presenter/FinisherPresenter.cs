using Model.LevelTasksLogic;
using UnityEngine; 

namespace Presenter
{
    public class FinisherPresenter
    {
        private readonly ScoreCalculator _scoreCalculator;
        
        public FinisherPresenter(ScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public void TryFinishLevel()
        {
            LevelScore score = _scoreCalculator.CalculateScore();
            if(score == LevelScore.NOT_FINISHED)
                Debug.Log("Level not finished!");
            else
                Debug.Log($"Level finished with score: {score}");
        }
    }
}