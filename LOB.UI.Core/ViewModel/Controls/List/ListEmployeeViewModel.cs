﻿#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListEmployeeViewModel))]
    public sealed class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>, IListEmployeeViewModel {
        public override Expression<Func<Employee, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Title.ToUpper().Contains(Search.ToUpper()) ||
                         arg.FirstName.ToUpper().Contains(Search.ToUpper()) || arg.LastName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.NickName.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(Search.ToUpper()) || arg.NickName.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}