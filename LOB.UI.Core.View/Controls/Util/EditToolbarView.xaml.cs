﻿#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    /// <summary>
    ///     Interaction logic for EditToolbarView.xaml
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EditToolbarView {
        public EditToolbarView() { InitializeComponent(); }
    }
}