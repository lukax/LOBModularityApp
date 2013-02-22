﻿#region Usings

using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public interface IAlterEntity
    {
        ICommand SaveChangesCommand { get; set; }
    }

    [InheritedExport]
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterEntity where T : BaseEntity
    {
        private CrudOperationType _typeOfOperation;

        [ImportingConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            //Entity = entity;
            //Repository = repository;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
        }

        protected IRepository Repository { get; set; }
        public ICommand CancelCommand { get; set; }
        public int? CancelIndex { get; set; }

        #region Props

        private T _entity;

        protected T Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;
                _entity = value;
                OnPropertyChanged();
            }
        }

        public int Code
        {
            get { return Entity != null ? Entity.Code : default(int); }
        }

        #endregion

        public ICommand SaveChangesCommand { get; set; }

        public abstract bool CanSaveChanges(object arg);
        public abstract bool CanCancel(object arg);

        public virtual void SaveChanges(object arg)
        {
            Debug.Write("Saving changes...");
            Repository.SaveOrUpdate(Entity);
            Cancel(arg);
        }

        public virtual void Cancel(object arg)
        {
            Messenger.Default.Send(CancelIndex, "Cancel");
        }

        public abstract override void InitializeServices();
        public abstract override void Refresh();
    }
}