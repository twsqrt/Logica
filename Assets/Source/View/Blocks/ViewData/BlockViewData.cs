using UnityEngine;

namespace View.Blocks.ViewData
{
    public class BlockViewData : ScriptableObject
    {
        [SerializeField] private Sprite _blockSprite;
        [SerializeField] private Color _blockColor;

        public Sprite BlockSprite => _blockSprite;
        public Color BlockColor => _blockColor;
    }
}