﻿#region Usings

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Threading;

//using System.Windows.Threading;

#endregion

namespace LOB.Core.Util {
    public class MThreadObservableCollection<T> : ObservableCollection<T> {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            NotifyCollectionChangedEventHandler eh = CollectionChanged;
            if(eh != null) {
                Dispatcher dispatcher =
                        (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
                         let dpo = nh.Target as DispatcherObject
                         where dpo != null
                         select dpo.Dispatcher).FirstOrDefault();

                if(dispatcher != null && dispatcher.CheckAccess() == false) dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => OnCollectionChanged(e)));
                else foreach(NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()) nh.Invoke(this, e);
            }
        }
    }
}