using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Model.BlocksLogic.OperationBlocksLogic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Formula Configs", menuName = "Configs/Formula", order = 51)]
    public class FormulaConfig : ScriptableObject
    {
        [Serializable] private class OperationCharacter
        {
            public LogicOperationType Type;
            public string HexUnicode;
        }

        [SerializeField] private OperationCharacter[] _characters; 
        [SerializeField] private char _unknownOperation;
        [SerializeField] private ParameterNamesConfig _parameterNames;

        private Dictionary<LogicOperationType, char> _charactersDictionary = null;

        public ParameterNamesConfig ParameterNames => _parameterNames;

        public char ParseHexUnicode(string HexUnicode)
        {
            int code = int.Parse(HexUnicode, NumberStyles.HexNumber);
            return Convert.ToChar(code);
        }

        public char GetOperationChar(LogicOperationType type)
        {
            if(_charactersDictionary == null)
                _charactersDictionary = _characters.ToDictionary(c => c.Type, c => ParseHexUnicode(c.HexUnicode));
            if(_charactersDictionary.TryGetValue(type, out char character))
                return character;
            return _unknownOperation;
        }
    }
}