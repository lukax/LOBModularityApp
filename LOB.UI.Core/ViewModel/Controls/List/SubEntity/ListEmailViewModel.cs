﻿#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public class ListEmailViewModel : ListBaseEntityViewModel<Email>, IListEmailViewModel {

        public ListEmailViewModel(Email entity, IRepository repository,
            IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<Email, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Value.ToString().ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() { Operation = _operation; }

        public override void Refresh() { throw new NotImplementedException(); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Email,
            State = UIOperationState.List
        };

    }
}