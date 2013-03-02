﻿#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using System;
using System.Linq.Expressions;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public sealed class ListEmployeeViewModel : ListNaturalPersonViewModel
    {

        [ImportingConstructor]
        public ListEmployeeViewModel(Employee employee, IRepository repository)
            : base(employee, repository)
        {
        }

        public new Expression<Func<Employee, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                               arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Title.ToUpper().Contains(Search.ToUpper())
                            || arg.FirstName.ToUpper().Contains(Search.ToUpper())
                            || arg.LastName.ToUpper().Contains(Search.ToUpper())
                            || arg.NickName.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Notes.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Rg.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Cpf.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }

        protected override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}