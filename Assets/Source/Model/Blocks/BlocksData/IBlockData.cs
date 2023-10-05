namespace Model.BlocksLogic.BlocksData
{
    public interface IBlockData
    {
        T AcceptFactory<T>(IBlockDataBasedFactory<T> factory);
    }
}