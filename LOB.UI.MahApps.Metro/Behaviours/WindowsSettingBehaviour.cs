#region Usings

using System.Windows.Interactivity;
using MahApps.Metro.Controls;

#endregion

namespace MahApps.Metro.Behaviours
{
    public class WindowsSettingBehaviour : Behavior<MetroWindow>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject != null && AssociatedObject.SaveWindowPosition)
            {
                // save with custom settings class or use the default way
                var windowPlacementSettings = AssociatedObject.WindowPlacementSettings ??
                                              new WindowApplicationSettings(AssociatedObject);
                WindowSettings.SetSave(AssociatedObject, windowPlacementSettings);
            }
        }
    }
}