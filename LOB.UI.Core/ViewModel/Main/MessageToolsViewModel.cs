﻿#region Usings

using System.Windows.Input;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class MessageToolsViewModel : BaseViewModel
    {
        private IUnityContainer container = null;
        private IEventAggregator eventAggregator = null;

        #region Props

        private bool _isRestrictive;
        private string _message = "Please wait...";

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public bool IsRestrictive
        {
            get { return _isRestrictive; }
            set
            {
                _isRestrictive = value;
                OnPropertyChanged();
            }
        }

        #endregion Message

        #region CloseExecute Command

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return this._closeCommand ?? (this._closeCommand = new DelegateCommand(CloseExecute, () => CanClose)); }

            set { this._closeCommand = value; }
        }

        public bool CanClose { get; set; }

        public void CloseExecute()
        {
            this.eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        #endregion CloseExecute Command

        [InjectionConstructor]
        public MessageToolsViewModel(IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = this.container.Resolve<IEventAggregator>();
        }

        public override OperationType OperationType
        {
            get { return OperationType.MessageTools; }
        }

        public void Initialize(string message, bool canClose, bool isRestrictive)
        {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}