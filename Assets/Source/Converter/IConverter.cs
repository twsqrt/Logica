namespace Converter
{
    public interface IConverter<in Tin, out Tout>
    {
        Tout Converter(Tin input);
    }
}