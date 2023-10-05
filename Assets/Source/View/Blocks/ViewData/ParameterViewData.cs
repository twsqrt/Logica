using UnityEngine;

namespace View.Blocks.ViewData
{
    [CreateAssetMenu(fileName = "ParameterBlock View Data", menuName = "Data/ParameterBlock View", order = 51)]
    public class ParameterViewData : BlockViewData
    {
        [SerializeField] private Color _parameterColor;

        public Color ParameterColor => _parameterColor;
    }
}