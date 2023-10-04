using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "ParameterBlock View Data", menuName = "Data/ParameterBlock View", order = 51)]
    public class ParameterViewData : BlockViewData
    {
        [SerializeField] private Color _parameterColor;

        public Color ParameterColor => _parameterColor;
    }
}