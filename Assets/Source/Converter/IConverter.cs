namespace Converter
{
    public interface IConverter<in Tin, out Tout>
    {
        Tout Convert(Tin input);
    }
}