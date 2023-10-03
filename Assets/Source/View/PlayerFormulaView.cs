using Model.TreeLogic;
using Presenter;
using TMPro;
using UnityEngine;
using Zenject;

namespace View
{
    public class PlayerFormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        [Inject] private void Init(BlockTree tree, PlayerFormulaPresenter formulaPresenter)
        {
            tree.OnChanged += () => _formulaText.text = formulaPresenter.GetFormulaString();
        }
    }
}