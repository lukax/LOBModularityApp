﻿#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.List.Base
{
    public interface IListServiceViewModel<T> : IListBaseEntityViewModel<T> where T : Service
    {
    }
}