using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "Parameter View Data", menuName = "Data/Parameter View", order = 51)]
    public class ParameterViewData : BlockViewData
    {
        [SerializeField] private Color _parameterColor;

        public Color ParameterColor => _parameterColor;
    }
}