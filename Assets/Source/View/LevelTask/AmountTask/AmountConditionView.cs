using Model.BlockLogic.BlockDataLogic;
using TMPro;
using UnityEngine;
using View.BlockLogic;

namespace View.LevelTaskLogic.AmountTaskLogic
{
    public class AmountConditionView : MonoBehaviour
    {
        [SerializeField] private BlockUIViewFactory _blockFactory;
        [SerializeField] private TextMeshProUGUI _limitText;

        public void Init(IBlockData blockData, int limit)
        {
            _limitText.text = limit.ToString();
        }
    }
}