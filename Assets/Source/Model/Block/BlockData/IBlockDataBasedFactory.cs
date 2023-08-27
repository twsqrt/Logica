namespace Model.BlockLogic.BlockDataLogic
{
    public interface IBlockDataBasedFactory<T>
    {
        T Create(ParameterData data);

        T Create(OperationData data);
    }
}