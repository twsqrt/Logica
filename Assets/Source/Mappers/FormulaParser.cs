using System.Collections.Generic;
using System.Text;
using Configs;

namespace Mappers
{
    public class FormulaParser
    {
        private const string PARAMETER_TAG_FORMAT = "<param={0}>";

        private readonly ParameterNamesConfig _parameterNamesConifg;
        private readonly Dictionary<string, char> _operationTags;

        public FormulaParser(ParameterNamesConfig parameterNamesConfig)
        {
            _parameterNamesConifg = parameterNamesConfig;

            _operationTags = new Dictionary<string, char>()
            {
                {"<not>", '\u00AC'},
                {"<or>", '\u2228'},
                {"<and>", '\u2227'},
                {"<xor>", '\u2295'},
                {"<nor>", '\u2192'},
            };
        }

        private void ReplaceOperations(StringBuilder sb)
        {
            foreach(var (tag, operationSymbol) in _operationTags)
                sb.Replace(tag, operationSymbol.ToString());
        }

        private void ReplaceParameters(StringBuilder sb)
        {
            foreach(var (id, name) in _parameterNamesConifg.ToDictionary())
            {
                string tag = string.Format(PARAMETER_TAG_FORMAT, id);
                sb.Replace(tag, name);
            }
        }

        public string Parse(string input)
        {
            var sb = new StringBuilder(input);

            ReplaceOperations(sb);
            ReplaceParameters(sb);

            return sb.ToString();
        }
    }
}