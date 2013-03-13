﻿#region Usings

using System.Collections.Generic;
using System.Windows.Controls;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    public class RegionAdapter : IRegionAdapter
    {
        private readonly IUnityContainer _container;
        private readonly IDictionary<string, object> _regions = new Dictionary<string, object>();

        [InjectionConstructor]
        public RegionAdapter(IUnityContainer container)
        {
            _container = container;
        }

        public IRegionAdapter RegisterRegion(string name, object region)
        {
            _regions.Add(name, region);
            return this;
        }

        public IRegionAdapter AddView<TView>(TView view, string regionName, string title = "IsDefault")
            where TView : class
        {
            object region = _regions[regionName];
            if (region is IBaseView)
            {
                ((IBaseView) region).Header = title;
            }
            if (region is ContentControl)
            {
                ((ContentControl) region).Content = view;
            }

            return this;
        }
    }
}