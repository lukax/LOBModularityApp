﻿#region Usings

using System.Windows;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Main
{
    /// <summary>
    ///     Interaction logic for MessageToolsView.xaml
    /// </summary>
    public partial class MessageToolsView : UserControl, IBaseView
    {
        public MessageToolsView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return this.DataContext as IBaseViewModel; }
            set
            {
                this.DataContext = value;
                var vm = value as MessageToolsViewModel;
                if (vm != null)
                    (vm).PropertyChanged += (sender, args) =>
                        {
                            if (vm.IsRestrictive)
                            {
                                BorderOuter.IsHitTestVisible = true;
                                GridInner.IsHitTestVisible = true;
                                BorderInner.IsHitTestVisible = true;
                                BorderInner.Opacity = .8;
                                ButtonClose.Visibility = Visibility.Visible;
                                Progress.Visibility = Visibility.Visible;
                                BorderOuter.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                BorderOuter.IsHitTestVisible = false;
                                GridInner.IsHitTestVisible = false;
                                BorderInner.IsHitTestVisible = false;
                                BorderInner.Opacity = .6;
                                ButtonClose.Visibility = Visibility.Hidden;
                                Progress.Visibility = Visibility.Hidden;
                                BorderOuter.Visibility = Visibility.Hidden;
                            }
                        };
            }
        }

        public string Header { get { return Strings.Header_Main_Message; } }
        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public OperationType OperationType
        {
            get { return OperationType.MessageTools; }
        }
    }
}