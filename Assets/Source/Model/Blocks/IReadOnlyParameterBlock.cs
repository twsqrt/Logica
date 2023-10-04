namespace Model.BlocksLogic
{
    public interface IReadOnlyParameterBlock : IReadOnlyBlock
    {
        int Id { get; }
    }
}