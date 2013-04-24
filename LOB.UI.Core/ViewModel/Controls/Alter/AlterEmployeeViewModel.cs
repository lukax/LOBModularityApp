﻿#region Usings

using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterEmployeeViewModel))]
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {}
}