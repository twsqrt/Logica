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
        [SerializeField] private Transform _blockViewContainer;

        public void Init(IBlockData blockData, int limit)
        {
            _limitText.text = limit.ToString();
            BlockUIView view = blockData.AcceptFactory(_blockFactory);
            view.transform.SetParent(_blockViewContainer, false);
        }
    }
}