﻿#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>, IAlterSaleViewModel
    {
        public AlterSaleViewModel(Sale entity, IRepository repository) : base(entity, repository)
        {
        }

        public override OperationType OperationType
        {
            get { return OperationType.NewSale; }
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new Sale();
        }
    }
}