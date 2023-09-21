using Model.BlockLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.BuilderLogic;
using System;

namespace Presenter
{
    public class ExecutionPresenter
    {
        private readonly MapTile _rootTile;
        private readonly TreeVerifier _treeVerifier;

        public bool CanExecute()
        {
            Block root = _rootTile.Block;
            return root != null && root.Accept(_treeVerifier);
        }

        public ExecutionPresenter(Map map, TreeVerifier treeVerifier)
        {
            _rootTile = map[map.RootPosition];
            _treeVerifier = treeVerifier;
        }
    }
}