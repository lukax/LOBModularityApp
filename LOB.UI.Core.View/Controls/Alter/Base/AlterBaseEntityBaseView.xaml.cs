#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base
{
    public partial class AlterBaseEntityBaseView : UserControl, IBaseView
    {
        private string _header;

        public AlterBaseEntityBaseView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}