using System;
using Configs.LevelConfigs.LevelTasksConfigs;
using Model.TreeLogic;

namespace Mappers
{
    public class DelegateMapper
    {
        private readonly DelegateParser _parser;
        private readonly TreeToDelegate _treeConverter;

        public DelegateMapper(FormulaTaskConfig formulaTaskConfig)
        {
            _treeConverter = new TreeToDelegate(formulaTaskConfig);
            _parser = new DelegateParser();
        }

        public Delegate From(string delegateString)
            => _parser.Parse(delegateString);

        public Delegate From(BlockTree tree)
            => _treeConverter.Convert(tree);
    }
}