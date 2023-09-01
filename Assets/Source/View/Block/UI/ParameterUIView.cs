using Model.BlockLogic.BlockDataLogic;
using TMPro;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public class ParameterUIView : BlockUIView
    {
        [SerializeField] private TextMeshPro _tmp;

        public void Init(ParameterViewData viewData, string parameterName)
        {
            base.Init(viewData);

            _tmp.color = viewData.ParameterColor;
            _tmp.text = parameterName;
        }
    }
}