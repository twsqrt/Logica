namespace Model.BlockLogic.BlockDataLogic
{
    public interface IBlockData
    {
        T AcceptFactory<T>(IBlockDataBasedFactory<T> factory);
    }
}