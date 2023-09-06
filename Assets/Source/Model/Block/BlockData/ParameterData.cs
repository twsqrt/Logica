namespace Model.BlockLogic.BlockDataLogic
{
    public readonly struct ParameterData : IBlockData
    {
        private readonly int _id;

        public int Id => _id;

        public ParameterData(int id)
        {
            _id = id;
        }

        public T AcceptFactory<T>(IBlockDataBasedFactory<T> factory)
            => factory.Create(this);
    }
}