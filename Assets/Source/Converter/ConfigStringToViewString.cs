namespace Converter
{
    public class ConfigStringToViewString : IConverter<string, string>
    {
        public string Converter(string input)
            => input;
    }
}