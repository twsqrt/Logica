namespace Model.BlocksLogic.BlocksData
{
    public interface IBlockDataBasedFactory<T>
    {
        T Create(ParameterData data);

        T Create(OperationData data);
    }
}