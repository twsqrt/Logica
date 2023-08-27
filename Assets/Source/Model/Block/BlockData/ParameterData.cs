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
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return _id == (obj as ParameterData).Id;
        }

        public override int GetHashCode()
            => _id.GetHashCode();

        public T AcceptFactory<T>(IBlockDataBasedFactory<T> factory)
            => factory.Create(this);
    }
}