namespace Model.BlockLogic.BlockDataLogic
{
    public class ParameterData : IBlockData
    {
        private readonly int _id;

        public int Id => _id;

        public ParameterData(int id)
        {
            _id = id;
        }

        public Block AcceptFactory(BlockFactory facotry)
            =>  facotry.Create(this);
    }
}