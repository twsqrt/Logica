using UnityEngine;
using Model.MapLogic;
using View.MapLogic;
using View.BuilderLogic;
using Config;
using Presenter;
using Model;

namespace EntryPointLogic
{
    public class EntryPoitn : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;

        private void Awake()
        {
            Map map = new Map(_mapConfig);
            _mapView.Init(map);

            TreeBlockBuilder builder = new TreeBlockBuilder(map);
            BuilderPresenter builderPresenter = new BuilderPresenter(builder);
            _builderView.Init(_mapView, builderPresenter, builder);
        }
    }
}