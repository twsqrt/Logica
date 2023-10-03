using Model.BlockLogic.BlockDataLogic;
using TMPro;
using UnityEngine;
using View.BlockLogic;

namespace View.LevelTaskLogic.AmountTaskLogic
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