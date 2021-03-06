﻿#region Usings

using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Actions {
    public class CloseTabItemAction : TriggerAction<DependencyObject> {
        public static readonly DependencyProperty TabControlProperty = DependencyProperty.Register("TabControl", typeof(TabControl),
                                                                                                   typeof(CloseTabItemAction),
                                                                                                   new PropertyMetadata(default(TabControl)));

        public static readonly DependencyProperty TabItemProperty = DependencyProperty.Register("TabItem", typeof(TabItem), typeof(CloseTabItemAction),
                                                                                                new PropertyMetadata(default(TabItem)));

        public TabControl TabControl {
            get { return (TabControl)GetValue(TabControlProperty); }
            set { SetValue(TabControlProperty, value); }
        }

        public TabItem TabItem {
            get { return (TabItem)GetValue(TabItemProperty); }
            set { SetValue(TabItemProperty, value); }
        }

        public static IRegionAdapter RegionAdapter { get; set; }

        protected override void Invoke(object parameter) {
            if(TabControl.Items.Contains(TabItem))
                if(RegionAdapter != null) {
                    // ReSharper disable SuspiciousTypeConversion.Global
                    var view = TabItem as IBaseView<IBaseViewModel>;
                    // ReSharper restore SuspiciousTypeConversion.Global
                    if(view != null) RegionAdapter.Remove(view);
                }
                else TabControl.Items.Remove(TabItem);
            else if(TabControl.Items.Contains(TabItem.Content))
                if(RegionAdapter != null) {
                    var view = TabItem.Content as IBaseView<IBaseViewModel>;
                    if(view != null) RegionAdapter.Remove(view);
                }
                else TabControl.Items.Remove(TabItem.Content);
        }
    }
}