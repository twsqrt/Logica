namespace Model.LevelTasksLogic
{
    public interface ILevelTask
    {
        bool CheckCompletion();
        public T Accept<T>(ILevelTaskVisitor<T> visitor);
    }
}