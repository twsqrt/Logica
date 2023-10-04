using TMPro;
using UnityEngine;
using View.Blocks.ViewData;

namespace View.Blocks.UI
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