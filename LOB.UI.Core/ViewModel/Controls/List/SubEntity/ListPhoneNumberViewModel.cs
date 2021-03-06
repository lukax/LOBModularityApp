﻿#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListPhoneNumberViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListPhoneNumberViewModel : ListBaseEntityViewModel<PhoneNumber>, IListPhoneNumberViewModel {
        public override Expression<Func<PhoneNumber, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Number.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) || arg.Type.ToString().ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Detail.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}