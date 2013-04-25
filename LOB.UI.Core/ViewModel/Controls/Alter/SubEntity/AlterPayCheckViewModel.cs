﻿#region Usings

using System.ComponentModel.Composition;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterPayCheckViewModel))]
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {}
}