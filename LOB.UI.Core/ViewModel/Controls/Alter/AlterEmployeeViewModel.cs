﻿#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {

        private readonly IEventAggregator _eventAggregator;

        [InjectionConstructor] public AlterEmployeeViewModel(Employee entity, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _eventAggregator = eventAggregator;
        }

        public override void InitializeServices() {
            ClearEntity(null);
        }

        public override void Refresh() {
            ClearEntity(null);
        }

        public override OperationType OperationType {
            get { return OperationType.AlterEmployee; }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

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
            Entity = new Employee {};

        }

    }
}