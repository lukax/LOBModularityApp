﻿#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterLegalPersonViewModel : AlterBaseEntityViewModel<LegalPerson>,
                                                    IAlterLegalPersonViewModel {

// ReSharper disable NotAccessedField.Local
        private readonly AlterPersonViewModel _alterPersonViewModel;
// ReSharper restore NotAccessedField.Local
        private readonly IEventAggregator _eventAggregator;
        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Service,
            State = UIOperationState.Add
        };

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        [InjectionConstructor]
        public AlterLegalPersonViewModel(LegalPerson entity,
            AlterPersonViewModel alterPersonViewModel, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _alterPersonViewModel = alterPersonViewModel;
            _eventAggregator = eventAggregator;
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) { Entity = new LegalPerson {}; }

    }
}