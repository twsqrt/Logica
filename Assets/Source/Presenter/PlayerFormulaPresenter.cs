using Model.BlockLogic;
using Model.MapLogic;
using Model.TreeLogic;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly MapTile _rootTile;
        private readonly TreeToStringConverter _converter;

        public string GetFormulaString()
        {
            Block root = _rootTile.Block;
            return root != null ? root.Accept(_converter) : string.Empty;
        }

        public PlayerFormulaPresenter(Map map, TreeToStringConverter conveter)
        {
            _rootTile = map[map.RootPosition];
            _converter = conveter;
        }
    }
}