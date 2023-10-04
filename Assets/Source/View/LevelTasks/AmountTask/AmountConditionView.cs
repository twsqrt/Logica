using Model.BlocksLogic.BlocksData;
using TMPro;
using UnityEngine;
using View.Blocks.UI;

namespace View.LevelTasksLogic.AmountTaskLogic
{
    public class AmountConditionView : MonoBehaviour
    {
        [SerializeField] private BlockUIViewFactory _blockFactory;
        [SerializeField] private TextMeshProUGUI _limitText;
        [SerializeField] private Transform _blockViewContainer;

        public void Init(IBlockData blockData, int limit)
        {
            _limitText.text = limit.ToString();
            BlockUIView view = blockData.AcceptFactory(_blockFactory);
            view.transform.SetParent(_blockViewContainer, false);
        }
    }
}