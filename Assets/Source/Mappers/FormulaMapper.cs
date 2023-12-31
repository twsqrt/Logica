using Configs;
using Model.TreeLogic;

namespace Mappers
{
    public class FormulaMapper
    {
        private readonly FormulaParser _parser;
        private readonly TreeToFormula _treeConverter;

        public FormulaMapper(FormulaConfig formulaConfig, FormulaParser parser)
        {
            _treeConverter = new TreeToFormula(formulaConfig);
            _parser = parser;
        }

        public string From(string configString)
            => _parser.Parse(configString);

        public string From(BlockTree tree)
            => _treeConverter.Convert(tree);
    }
}