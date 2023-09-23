using Model.TreeLogic;
using Presenter;
using TMPro;
using UnityEngine;

namespace View
{
    public class PlayerFormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        public void Init(BlockTree tree, PlayerFormulaPresenter formulaPresenter)
        {
            tree.OnChanged += () => _formulaText.text = formulaPresenter.GetFormulaString();
        }
    }
}