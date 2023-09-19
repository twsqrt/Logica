using Model.MapLogic;
using Model.BlockLogic;
using Model.TreeLogic;
using TMPro;
using UnityEngine;
using Presenter.BuilderLogic;

namespace View
{
    public class FormulaView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        private TreeToStringConverter _converter;

        private void WriteFromRoot(Block root)
            => _formulaText.text = root != null ? root.Accept(_converter) : string.Empty;

        public void Init(Map map, BuilderPresenter builder, TreeToStringConverter conveter)
        {
            _converter = conveter;
            MapTile rootTile = map[map.RootPosition];
            builder.OnExecuted += () => WriteFromRoot(rootTile.Block);
        }
    }
}