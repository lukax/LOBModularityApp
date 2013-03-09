﻿#region Usings

using System;
using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Base
{
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel
    {
        private String _header;

        public String Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public abstract void InitializeServices();
        public abstract void Refresh();
    }
}