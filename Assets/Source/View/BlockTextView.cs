using Model.BlockLogic;
using Model.BuilderLogic;
using Model.TreeConverterLogic;
using TMPro;
using UnityEngine;

namespace View
{
    public class BlockTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;

        private TreeToStringConverter _converter;

        private void WriteFromRoot(Block root)
            => _tmp.text = root != null ? root.Accept(_converter) : string.Empty;

        public void Init(BlockBuilder builder, TreeToStringConverter conveter)
        {
            _converter = conveter;

            builder.OnPlaced += _ => WriteFromRoot(builder.Root);
            builder.OnRemoved += _ => WriteFromRoot(builder.Root);
        }
    }
}