﻿#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    [InheritedExport]
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListBaseEntityViewModel<T> where T : BaseEntity {
        private int _updateInterval;
        private Expression<Func<T, bool>> _searchCriteria;
        private ViewModelInfo _viewModelInfo;
        public virtual Expression<Func<T, bool>> SearchCriteria {
            get {
                try {
                    var converted = Convert.ToInt32(Search);
                    return _searchCriteria ?? (arg => arg.Code == converted);
                } catch(FormatException) {
                    return arg => false;
                }
            }
            set { _searchCriteria = value; }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand IncludeCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand FetchCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public T Entity { get; set; }
        public IEnumerable<T> Entities { get; set; }
        public string Search { get; set; }
        [Import] protected Lazy<IRepository> RepositoryLazy { get; private set; }
        [Import] protected Lazy<IEventAggregator> EventAggregatorLazy { get; private set; }
        public override ViewModelInfo Info {
            get {
                return _viewModelInfo ??
                       (_viewModelInfo = new ViewModelInfo {IsChild = true, ViewState = ViewState.Add, ViewSubState = ViewSubState.Locked});
            }
            set { _viewModelInfo = value; }
        }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregatorLazy.Value.GetEvent<NotificationEvent>(); }
        }
        [Import] protected Lazy<Notification> NotificationLazy { get; private set; }

        protected ListBaseEntityViewModel() {
            SearchCommand = new DelegateCommand(SearchExecute);
            IncludeCommand = new DelegateCommand(Include);
            AddCommand = new DelegateCommand(Save, CanSave);
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);
            CloseCommand = new DelegateCommand(Exit);
            Search = "";
        }

        private void Include(object o) {
            //EventAggregatorLazy.GetEvent<IncludeEntityEvent>().Publish(Entity);
        }

        protected virtual void SearchExecute(object obj) { if(Worker.CancellationPending) if(!Worker.IsBusy) Worker.RunWorkerAsync(); }

        private void Exit(object obj) {
            //EventAggregatorLazy.GetEvent<CloseViewEvent>().Publish(ViewModelInfo);
        }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void InitializeServices() {
            Worker.DoWork += WorkerUpdateList;
            Worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Constantly update the list async every 1000 miliseconds
        /// </summary>
        private void WorkerUpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            //TODO: Dynamic set based on selected tab
            RepositoryLazy.Value.Uow.OnError += (o, s) => {
                                                    NotificationEvent.Publish(
                                                        NotificationLazy.Value.Message(s.Description)
                                                                        .Detail(s.ErrorMessage)
                                                                        .Progress(-1)
                                                                        .State(NotificationState.Error));
                                                    //Worker.CancelAsync();
                                                    Info.SubState(ViewSubState.Locked);
                                                };
            do {
                if(RepositoryLazy.Value.Uow.TestConnection())
                    using(RepositoryLazy.Value.Uow.BeginTransaction())
                        if(!worker.CancellationPending) {
                            EventAggregatorLazy.Value.GetEvent<NotificationEvent>()
                                               .Publish(
                                                   NotificationLazy.Value.Message(Strings.Notification_List_Updating)
                                                                   .Progress(10)
                                                                   .State(NotificationState.Info));
                            IList<T> localList = string.IsNullOrEmpty(Search)
                                                     ? (RepositoryLazy.Value.GetAll<T>()).ToList()
                                                     : (RepositoryLazy.Value.GetAll(SearchCriteria)).ToList();
                            EventAggregatorLazy.Value.GetEvent<NotificationEvent>()
                                               .Publish(NotificationLazy.Value.Message(Strings.Notification_List_Updating).Progress(70));
                            if(Entities == null || !localList.SequenceEqual(Entities)) {
                                Entities = new ObservableCollection<T>(localList);
                                EventAggregatorLazy.Value.GetEvent<NotificationEvent>()
                                                   .Publish(NotificationLazy.Value.Message(Strings.Notification_List_Updating).Progress(100));
                            }
                            else
                                EventAggregatorLazy.Value.GetEvent<NotificationEvent>()
                                                   .Publish(
                                                       NotificationLazy.Value.Message(Strings.Notification_List_Updated)
                                                                       .Progress(-1)
                                                                       .State(NotificationState.Ok));
                            Info.SubState(ViewSubState.Unlocked);
                        }
                Thread.Sleep(2000);
            } while(!Worker.CancellationPending);
        }

        protected virtual void Save(object arg) { }

        protected virtual bool CanSave(object arg) { return Entity != null; }

        protected virtual void Update(object arg) { }

        protected virtual bool CanUpdate(object arg) { return Entity != null; }

        protected virtual void Delete(object arg) { RepositoryLazy.Value.Delete(Entity); }

        protected virtual bool CanDelete(object arg) { return Entity != null; }

        public override void Refresh() { Search = ""; }

        protected virtual void Fetch(object arg = null) {
            Entities = null;
            if(!Worker.IsBusy) Worker.RunWorkerAsync();
        }
        #region Implementation of IDisposable

        ~ListBaseEntityViewModel() { Dispose(false); }
        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if(Worker.WorkerSupportsCancellation) Worker.CancelAsync();
            if(!disposing) return;
            Worker.Dispose();
        }

        #endregion
    }
}