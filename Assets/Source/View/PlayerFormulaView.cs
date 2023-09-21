using TMPro;
using UnityEngine;
using Presenter.BuilderLogic;
using Presenter;

namespace View
{
    public class PlayerFormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        public void Init(PlayerFormulaPresenter formulaPresenter, BuilderPresenter builder)
        {
            builder.OnExecuted += () => _formulaText.text = formulaPresenter.GetFormulaString();
        }
    }
}