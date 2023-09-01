using Model.BlockLogic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    public abstract class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _blockSprite;
        [SerializeField] private Transform _arrowPrefab;

        private void OnBlockRemoveHandler()
            => Destroy(this);

        protected void Init(BlockViewData data, Block block)
        {
            _blockSprite.sprite = data.BlockSprite;
            _blockSprite.material.color = data.BlockColor;

            block.OnRemove += _ => OnBlockRemoveHandler();

            BlockSide connectionSide = block.Context.ConnectionSide;
            if(connectionSide != BlockSide.UNDEFINED)
            {
                Debug.Log($"Commection side: {connectionSide}");
                Transform arrow = Instantiate(_arrowPrefab, transform);

                Vector2Int position =  BlockSideMapper.PositionFromBlockSide(connectionSide);
                Debug.Log($"Position: {position}");
                arrow.localPosition = new Vector3(position.x, position.y, 0f) * 0.5f;

                float angle = BlockSideMapper.AngleFromBlockSide(connectionSide);
                Debug.Log($"Angle: {angle}");
                arrow.eulerAngles = new Vector3(0f, 0f, angle);
            }
        }
    }
}