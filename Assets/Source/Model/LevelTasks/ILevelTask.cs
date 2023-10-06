using Model.LevelStateLogic;

namespace Model.LevelTasksLogic
{
    public interface ILevelTask
    {
        bool CheckCompletion(LevelState levelState);
        public T Accept<T>(ILevelTaskVisitor<T> visitor);
    }
}