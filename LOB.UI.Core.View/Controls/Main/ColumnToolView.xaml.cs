﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Main;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for ColumnToolsView.xaml
    /// </summary>
    [Export(typeof(IBaseView<IColumnToolViewModel>)), Export]
    [ViewInfo(ViewType.ColumnTool, ViewState.Other)]
    public partial class ColumnToolView : IBaseView<IColumnToolViewModel> {
        public ColumnToolView() { InitializeComponent(); }

        [Import] public IColumnToolViewModel ViewModel {
            get { return DataContext as IColumnToolViewModel; }
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