using Model.TreeLogic;
using TMPro;
using UnityEngine;
using Presenter.BuilderLogic;

namespace View
{
    public class FormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        private BlockTree _tree;
        private TreeToStringConverter _converter;

        private void UpdateText()
            => _formulaText.text = _tree.IsEmpty ? string.Empty : _tree.Root.Accept(_converter);

        public void Init(BlockTree tree, BuilderPresenter builder, TreeToStringConverter conveter)
        {
            _tree = tree;
            _converter = conveter;
            builder.OnExecuted   += UpdateText;
        }
    }
}