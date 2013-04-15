﻿#region Usings

using System;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Sell {
    public partial class SellServiceView : UserControl, IBaseView {
        public SellServiceView() { InitializeComponent(); }

        public IBaseViewModel ViewModel { get; set; }

        public string Header {
            get { return Strings.UI_Header_Sell_Service; }
        }

        public int Index { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}