﻿#region Usings

using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListPersonViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListPersonViewModel : ListBaseEntityViewModel<Person>, IListPersonViewModel {
        public new Expression<Func<Person, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Notes.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}