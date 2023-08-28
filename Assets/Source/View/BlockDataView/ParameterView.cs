using Model.BlockLogic.BlockDataLogic;
using TMPro;
using UnityEngine;

namespace View.BlockDataLogic
{
    public class ParameterView : BlockDataView
    {
        [SerializeField] private TextMeshPro _tmp;

        public void Init(string parameterName)
        {
            Init();
            _tmp.text = parameterName;
        }
    }
}