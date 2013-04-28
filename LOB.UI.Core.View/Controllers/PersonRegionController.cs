﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Controller;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.Event.View;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Contract.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Controllers {
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonRegionController : IPartImportsSatisfiedNotification, IBaseController {
        [Import] protected IEventAggregator EventAggregator { get; set; }
        [Import] protected IBaseView<IAlterAddressViewModel> AlterAddressView { get; set; }
        [Import] protected IBaseView<IAlterContactInfoViewModel> AlterContactInfoView { get; set; }
        [Import] protected IRegionManager RegionManager { get; set; }
        [Import] protected Lazy<IRegionAdapter> RegionAdapter { get; set; }
        [Import] public IAlterPersonViewModel ViewModel { get; set; }

        public void OnImportsSatisfied() {
            EventAggregator.GetEvent<SetupChildViewEvent>()
                           .Publish(new SetupChildPayload(AlterAddressView.ViewModel.Id, ViewModel.Id, ViewModel.Entity));
            EventAggregator.GetEvent<SetupChildViewEvent>()
                           .Publish(new SetupChildPayload(AlterContactInfoView.ViewModel.Id, ViewModel.Id, ViewModel.Entity));

            RegionManager.RegisterViewWithRegion("AddressRegion", () => AlterAddressView);
            RegionManager.RegisterViewWithRegion("ContactInfoRegion", () => AlterContactInfoView);

            AlterAddressView.ViewModel.InitializeServices();
            AlterContactInfoView.ViewModel.InitializeServices();
        }

        public void Dispose() {
            RegionAdapter.Value.Remove(AlterAddressView);
            RegionAdapter.Value.Remove(AlterContactInfoView);
            ViewModel.Dispose();
        }
    }
}