﻿#region Usings

using System;
using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface ITabProp
    {
        String Header { get; set; }
        int? Index { get; set; }
    }
}