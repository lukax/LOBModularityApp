﻿#region Usings

using System;
using System.Windows.Input;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class MessageToolViewModel : BaseViewModel, IMessageToolViewModel {
        private readonly IEventAggregator _eventAggregator;
        #region Props

        public string Message { get; set; }

        public bool IsRestrictive { get; set; }

        #endregion Description
        #region CloseExecute Command

        private ICommand _closeCommand;

        public ICommand CloseCommand {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand(CloseExecute, () => CanClose)); }
            set { _closeCommand = value; }
        }

        public bool CanClose { get; set; }

        public void CloseExecute() {
            //_eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        #endregion CloseExecute Command
        public MessageToolViewModel(IEventAggregator eventAggregator) {
            Message = "Please wait...";
            _eventAggregator = eventAggregator;
        }

        private ViewID _viewID = new ViewID {Type = ViewType.MessageTool, State = ViewState.Internal};

        public override ViewID ViewID {
            get { return _viewID; }
            set { _viewID = value; }
        }

        public void Initialize(string message, bool canClose, bool isRestrictive) {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
        }

        public override void InitializeServices() { }

        public override void Refresh() { }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}