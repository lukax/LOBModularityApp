#region Usings

using System.Diagnostics.CodeAnalysis;

#endregion

[assembly:
    SuppressMessage("Microsoft.Usage", "CA2241:Provide correct arguments to formatting methods", Scope = "member",
        Target =
            "MahApps.Metro.Controls.TransitioningContentControl.#OnTransitionPropertyChanged(System.Windows.DependencyObject,System.Windows.DependencyPropertyChangedEventArgs)"
        )]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2241:Provide correct arguments to formatting methods", Scope = "member",
        Target = "MahApps.Metro.Controls.TransitioningContentControl.#OnApplyTemplate()")]
[assembly:
    SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Scope = "member",
        Target = "MahApps.Metro.Native.UnsafeNativeMethods.#SetClassLongPtr64(System.IntPtr,System.Int32,System.IntPtr)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Scope = "type",
        Target = "MahApps.Metro.Controls.WindowCommands")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member",
        Target = "MahApps.Metro.Controls.WindowCommands.#ClosingWindow")]