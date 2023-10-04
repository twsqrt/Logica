using Model.BlocksLogic;
using TMPro;
using UnityEngine;
using View.Blocks.ViewData;

namespace View.Blocks
{
    public class ParameterView : BlockView
    {
        [SerializeField] private TextMeshPro _parameterName;

        public void Init(ParameterViewData viewData, string parameterName, ParameterBlock parameter)
        {
            base.Init(viewData, parameter);
            _parameterName.color = viewData.ParameterColor;
            _parameterName.text = parameterName;
        }
    }
}