namespace Converter
{
    public class ConfigStringToViewString : IConverter<string, string>
    {
        public string Convert(string input)
            => input;
    }
}