namespace Model.LevelTasksLogic
{
    public interface ILevelTaskVisitor<T>
    {
        T Visit(FormulaTask formulaTask);
        T Visit(AmountTask amountTask);
    }
}