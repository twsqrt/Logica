using Configs;
using Model.TreeLogic;

namespace Mappers
{
    public class FormulaMapper
    {
        private readonly FormulaParser _parser;
        private readonly TreeToFormula _treeConverter;

        public FormulaMapper(ParameterNamesConfig parametersConfig)
        {
            _treeConverter = new TreeToFormula(parametersConfig);
            _parser = new FormulaParser(parametersConfig);
        }

        public string From(string configString)
            => _parser.Parse(configString);

        public string From(BlockTree tree)
            => _treeConverter.Convert(tree);
    }
}