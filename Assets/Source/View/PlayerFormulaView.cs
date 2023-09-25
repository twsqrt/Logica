using Model.TreeLogic;
using Presenter;
using TMPro;
using UnityEngine;

namespace View
{
    public class PlayerFormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        public void Init(PlayerFormulaPresenter formulaPresenter)
        {
            formulaPresenter.OnFormulaTextChanged += t => _formulaText.text = t;
        }
    }
}