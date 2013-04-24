﻿#region Usings

using System.ComponentModel.Composition;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterCategoryViewModel))]
    public sealed class AlterCategoryViewModel : AlterBaseEntityViewModel<Category>, IAlterCategoryViewModel {}
}