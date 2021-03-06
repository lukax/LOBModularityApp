﻿#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Contract.ViewModel.Controls.Main;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IColumnToolViewModel)), PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class ColumnToolViewModel : BaseViewModel, IColumnToolViewModel, IPartImportsSatisfiedNotification {
        public string NotificationStatus { get; set; }
        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand TestDbConnection { get; set; }
        [Import] public Lazy<IEventAggregator> LazyEventAggregator { get; set; }
        [Import] public Lazy<IFluentNavigator> LazyFluentNavigator { get; set; }
        [Import] public Lazy<IRegionAdapter> LazyRegionAdapter { get; set; }
        [Import] public Lazy<INotificationToolViewModel> LazyNotificationViewModel { get; set; }
        [Import("TestDbConnection", typeof(Action<object>))] private Action<object> _testDbConnection;

        public void OnImportsSatisfied() {
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
            NotificationCommand = new DelegateCommand(ShowNotification);
            LogoutCommand = new DelegateCommand(Logout);
            TestDbConnection = new ActionCommand(_testDbConnection);
            NotificationStatus = Strings.UI_ToolTip_Notifications;
            InitWorker();
        }

        private void ShowOperations(object arg) {
            //var op = new ViewModelInfo {Type = ViewType.Op, ViewState = ViewState.List};
            //if(_regionAdapter.Contains(op, RegionName.TabRegion)) _regionAdapter.Remove(op, RegionName.TabRegion);
            //else _navigator.Init.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
            LazyFluentNavigator.Value.Init.ResolveView<IListOpViewModel>().AddToRegion(RegionName.TabRegion);
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowNotification(object o) {
            LazyNotificationViewModel.Value.IsVisible = !LazyNotificationViewModel.Value.IsVisible;
            //var op = new ViewModelInfo {
            //    Type = ViewType.NotificationTool,
            //    ViewState = ViewState.Tool
            //};
            //if(_regionAdapter.Contains(op, RegionName.BottomRegion)) _regionAdapter.Remove(op, RegionName.BottomRegion);
            //else
            //    _navigator.Init.ResolveView(op)
            //              .ResolveViewModel(op)
            //              .AddToRegion(RegionName.BottomRegion);
        }

        private void Logout(object o) { //_eventAggregator.GetEvent<CloseViewEvent>().Publish(new ViewModelInfo {Type = ViewType.Main}); 
        }

        private void InitWorker() {
            Worker.DoWork += UpdateStatus;
            Worker.RunWorkerAsync();
        }

        private async void UpdateStatus(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            do {
                await Task.Delay(1000);
                NotificationStatus = LazyNotificationViewModel.Value.Status;
            } while(!Worker.CancellationPending);
        }
    }
}