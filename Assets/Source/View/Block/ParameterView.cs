using Model.BlocksLogic;
using TMPro;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public class ParameterView : BlockView
    {
        [SerializeField] private TextMeshPro _parameterName;

        public void Init(ParameterViewData viewData, string parameterName, Parameter parameter)
        {
            base.Init(viewData, parameter);
            _parameterName.color = viewData.ParameterColor;
            _parameterName.text = parameterName;
        }
    }
}