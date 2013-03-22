#region Usings

using System;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base
{
    public partial class AlterPersonView : UserControl, IBaseView
    {
        private string _header;

        public AlterPersonView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterPersonViewModel<Person>; }
            set
            {
                DataContext = value;
                var localViewModel = value as IAlterPersonViewModel<Person>;
                if (localViewModel != null)
                {
                    UcAlterAddressView.DataContext = localViewModel.AlterAddressViewModel;
                    UcAlterContactInfoView.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }


        public string Header { get { return Strings.Header_Alter_Person; } }

        public int? Index { get; set; }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public OperationType OperationType
        {
            get { return OperationType.AlterPerson; }
        }
    }
}