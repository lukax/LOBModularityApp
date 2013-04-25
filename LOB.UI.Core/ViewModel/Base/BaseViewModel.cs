﻿#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel.Base {
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel {
        private ViewSubState _subState;

        [Import("ViewId")] public Guid Id { get; protected set; }
        public virtual string Header {
            get { return Strings.Common_Title; }
        }
        protected static CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }
        protected BackgroundWorker Worker { get; private set; }
        public ViewState ViewState { get; private set; }

        protected BaseViewModel() { Worker = new BackgroundWorker(); }
        public abstract void InitializeServices();
        public abstract void Refresh();

        protected virtual ViewState ChangeState(ViewState changeState) {
            if(changeState != default(ViewState)) ViewState = changeState;
            return ViewState;
        }
        protected virtual void Lock() { _subState = ViewSubState.Locked; }
        protected virtual void Unlock() { _subState = ViewSubState.Unlocked; }
        public virtual bool IsUnlocked {
            get { return _subState == ViewSubState.Unlocked; }
        }
        public virtual bool IsChild { get; protected set; }

        //public bool Equals(BaseViewModel other) { return Id == other.Id; }
        public abstract void Dispose();
    }

    [PartCreationPolicy(CreationPolicy.NonShared)] //INFO: Makes property Id not shared across views
    internal class BaseViewModelHelper {
        [Export("ViewId")] public Guid Id {
            get { return Guid.NewGuid(); }
        }

        [Export] public BackgroundWorker Worker {
            get { return new BackgroundWorker(); }
        }
    }
}