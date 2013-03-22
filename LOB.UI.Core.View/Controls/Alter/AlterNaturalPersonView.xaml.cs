﻿#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterNaturalPersonView : UserControl, IBaseView
    {
        private string _header;

        public AlterNaturalPersonView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterNaturalPersonViewModel; }
            set
            {
                DataContext = value;
                var localViewModel = value as IAlterNaturalPersonViewModel;
                if (localViewModel != null)
                {
                    UcAlterPersonView.DataContext = value;
                    UcAlterPersonView.UcAlterAddressView.DataContext = localViewModel.AlterAddressViewModel;
                    UcAlterPersonView.UcAlterContactInfoView.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header { get { return Strings.Header_Alter_NaturalPerson; } }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public OperationType OperationType
        {
            get { return OperationType.AlterNaturalPerson; }
        }
    }
}