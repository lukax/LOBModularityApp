﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    [Export(typeof(IBaseView<IListProductViewModel>))]
    [ViewInfo(ViewType.Product, new[] {ViewState.List, ViewState.QuickSearch})]
    public partial class ListProductView : IBaseView<IListProductViewModel> {
        public ListProductView() { InitializeComponent(); }

        [Import] public IListProductViewModel ViewModel {
            get { return DataContext as IListProductViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public int Index { get; set; }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}