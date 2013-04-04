﻿#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterNaturalPersonView : IBaseView {

        public AlterNaturalPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterNaturalPersonViewModel; }
            set {
                DataContext = value;
                var localViewModel = value as IAlterNaturalPersonViewModel;
                if(localViewModel != null) {
                    ViewAlterPerson.DataContext = value;
                    ViewAlterPerson.ViewAlterAddress.DataContext =
                        localViewModel.AlterAddressViewModel;
                    ViewAlterPerson.ViewAlterContactInfo.DataContext =
                        localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_NaturalPerson; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }

    }
}