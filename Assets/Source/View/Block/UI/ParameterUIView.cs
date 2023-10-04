using Model.BlocksLogic.BlocksData;
using TMPro;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public class ParameterUIView : BlockUIView
    {
        [SerializeField] private TextMeshProUGUI _parameterName;

        public void Init(ParameterViewData viewData, string parameterName)
        {
            base.Init(viewData);

            _parameterName.color = viewData.ParameterColor;
            _parameterName.text = parameterName;
        }
    }
}