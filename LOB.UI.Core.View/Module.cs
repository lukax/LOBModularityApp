﻿#region Usings

using System.Diagnostics.CodeAnalysis;
using LOB.Log.Interface;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Controller;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View
{
    [Module(ModuleName = "UICoreViewModule")]
    public class Module : IModule
    {
        private readonly IUnityContainer _container;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] private MainRegionController
            _mainRegionController;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IFluentNavigator, FluentNavigator>();
            _container.RegisterType<IRegionAdapter, RegionAdapter>();
            _container.RegisterInstance(CommandService.Default);
            _container.RegisterInstance<MessageToolsView>(new MessageToolsView()
                {
                    ViewModel = _container.Resolve<MessageToolsViewModel>()
                });


            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionName.HeaderRegion, typeof (HeaderToolsView));
            regionManager.RegisterViewWithRegion(RegionName.ColumnRegion, typeof (ColumnToolsView));
            //regionManager.RegisterViewWithRegion(RegionName.TabRegion, typeof (AlterCustomerView));
            //regionManager.Regions.Add(RegionName.TabRegion, new Region());
            //regionManager.AddToRegion(RegionName.TabRegion, typeof (AlterCategoryView));

            //Init controller
            _mainRegionController = _container.Resolve<MainRegionController>();
#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("UICoreViewModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}