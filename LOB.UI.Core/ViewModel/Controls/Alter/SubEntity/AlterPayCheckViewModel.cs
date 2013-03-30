﻿#region Usings

using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {

        private readonly IPayCheckFacade _payCheckFacade;
        private readonly IEventAggregator _eventAggregator;

        public AlterPayCheckViewModel(PayCheck entity, IPayCheckFacade payCheckFacade, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _payCheckFacade = payCheckFacade;
            _eventAggregator = eventAggregator;
        }

        public override void InitializeServices() {
            ClearEntity(null);
        }

        public override void Refresh() {
            ClearEntity(null);
        }

        public override OperationType OperationType {
            get { return OperationType.AlterPayCheck; }
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void Cancel(object arg) {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType);
        }

        protected override void QuickSearch(object arg) {
            _eventAggregator.GetEvent<QuickSearchEvent>().Publish(OperationType);
        }

        protected override void ClearEntity(object arg) {
            Entity = new PayCheck {Bonus = 0, Code = 0, CurrentSalary = 0, Ps = ""};
        }

    }
}