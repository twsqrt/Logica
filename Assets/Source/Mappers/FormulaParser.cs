using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configs;
using Model.BlocksLogic.OperationBlocksLogic;
using UnityEngine;

namespace Mappers
{
    [CreateAssetMenu(fileName = "Formula Parser", menuName = "Formula Parser", order = 51)]
    public class FormulaParser : ScriptableObject
    {
        [Serializable] private class Tag
        {
            public string Word;
            public LogicOperationType OperationType;
        }

        [SerializeField] private FormulaConfig _formulaConfig;
        [SerializeField] private Tag[] _tags;
        [SerializeField] private string _parameterTagStringFormat; 

        private Dictionary<string, LogicOperationType> _tagsDictionary = null;

        private void InitDictionary()
            => _tagsDictionary = _tags.ToDictionary(t => t.Word, t => t.OperationType);

        private void ReplaceOperations(StringBuilder sb)
        {
            foreach(var (tag, OperationType) in _tagsDictionary)
            {
                char symbol = _formulaConfig.GetOperationChar(OperationType);
                sb.Replace(tag, symbol.ToString());
            }
        }

        private void ReplaceParameters(StringBuilder sb)
        {
            foreach(var (id, name) in _formulaConfig.ParameterNames.ToDictionary())
            {
                string tag = string.Format(_parameterTagStringFormat, id);
                sb.Replace(tag, name);
            }
        }


        public string Parse(string input)
        {
            if(_tagsDictionary == null)
                InitDictionary();

            var sb = new StringBuilder(input);
            ReplaceOperations(sb);
            ReplaceParameters(sb);

            return sb.ToString();
        }
    }
}