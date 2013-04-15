﻿#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using Category = LOB.Domain.SubEntity.Category;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {
        private readonly IProductFacade _productFacade;
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Service, State = ViewState.Add};

        [InjectionConstructor]
        public AlterProductViewModel(Product entity, IProductFacade productFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) {
            _productFacade = productFacade;
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            ClearEntity(null);

            Worker.DoWork += UpdateCategoryList;
            Worker.RunWorkerAsync();
        }

        public override void Refresh() { ClearEntity(null); }

        private async void UpdateCategoryList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;

            do {
                await Task.Delay(2000); // TODO: Configuration based update time
                Categories = Repository.GetAll<Category>().ToList();
            } while(!worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            var op = new ViewID().State(ViewState.QuickSearch).Type(ViewType.Category);
            EventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        private void ExecuteAlterCategory(object o) {
            var op =
                new ViewID().Type(ViewType.Category)
                            .State(Entity.Category.Equals(_productFacade.GenerateEntity().Category) ? ViewState.Add : ViewState.Update);
            op.ViewModel = this;
            EventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _productFacade.CanAdd(out results);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _productFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override bool CanCancel(object arg) { return true; }

        protected override void ClearEntity(object args) {
            Entity = _productFacade.GenerateEntity();
            _productFacade.Entity = (Entity);
        }
    }
}