﻿#region Usings

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public class ListLegalPersonViewModel : ListBaseEntityViewModel<LegalPerson>,
                                            IListLegalPersonViewModel {

        [InjectionConstructor]
        public ListLegalPersonViewModel(LegalPerson entity, IRepository repository,
            EventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        private CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public new Expression<Func<LegalPerson, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.TradingName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.CorporateName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.CNPJ.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.InscEstadual.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.InscMunicipal.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CorporateName.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() { Operation = _operation; }

        public override void Refresh() { throw new NotImplementedException(); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.LegalPerson,
            State = UIOperationState.List
        };

    }
}