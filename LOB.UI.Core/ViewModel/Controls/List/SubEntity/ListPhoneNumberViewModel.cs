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
    public class ListPhoneNumberViewModel : ListBaseEntityViewModel<PhoneNumber>, IListPhoneNumberViewModel {
        public ListPhoneNumberViewModel(PhoneNumber entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<PhoneNumber, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Number.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Type.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Description.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public override void Refresh() { Search = ""; }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.PhoneNumber, State = ViewState.List};
    }
}