using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "ParameterViewData", menuName = "View/ViewData/ParameterViewData", order = 51)]
    public class ParameterViewData : BlockViewData
    {
        [SerializeField] private Color _parameterColor;

        public Color ParameterColor => _parameterColor;
    }
}