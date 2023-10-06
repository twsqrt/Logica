using Model.BlocksLogic.BlocksData;
using TMPro;
using UnityEngine;
using View.Blocks.UI;

namespace View.LevelTasksLogic.AmountSaveTaskLogic
{
    public class AmountConditionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _limitText;
        [SerializeField] private Transform _blockViewContainer;

        public void Init(BlockUIViewFactory blockViewFactory, IBlockData blockData, int limit)
        {
            _limitText.text = limit.ToString();
            BlockUIView view = blockData.AcceptFactory(blockViewFactory);
            view.transform.SetParent(_blockViewContainer, false);
        }
    }
}