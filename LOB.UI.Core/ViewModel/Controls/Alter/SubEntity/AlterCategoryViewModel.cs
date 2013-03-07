﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public class AlterCategoryViewModel : AlterServiceViewModel
    {
        public AlterCategoryViewModel(Category entity, IRepository repository) : base(entity, repository)
        {
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow.BeginTransaction())
            {
                Debug.Write("Saving changes...");
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}