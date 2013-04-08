﻿#region Usings

using System.Diagnostics.CodeAnalysis;
using LOB.Log.Interface;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Actions;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Modularity {
    [Module(ModuleName = "UICoreViewModule")]
    public class Module : IModule {

        private readonly IUnityContainer _container;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] // ReSharper disable NotAccessedField.Local
        private MainRegionController _mainRegionController;
// ReSharper restore NotAccessedField.Local

        public Module(IUnityContainer container) { _container = container; }

        public void Initialize() {
            _container.RegisterType<IFluentNavigator, FluentNavigator>();
            _container.RegisterType<IRegionAdapter, RegionAdapter>();

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionName.HeaderRegion, typeof(HeaderToolView));
            regionManager.RegisterViewWithRegion(RegionName.ColumnRegion, typeof(ColumnToolView));
            regionManager.RegisterViewWithRegion(RegionName.BottomRegion,
                                                 typeof(NotificationToolView));

            CloseTabItemAction.Container = _container.Resolve<IServiceLocator>();

            //Init controller
            _mainRegionController = _container.Resolve<MainRegionController>();
#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("UICoreViewModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}