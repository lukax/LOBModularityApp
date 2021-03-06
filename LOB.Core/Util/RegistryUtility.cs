﻿#region Usings

using System;
using Microsoft.Win32;

#endregion

namespace LOB.Core.Util {
    public class RegistryUtility {
        /// <summary>
        ///     An useful class to read/write/delete/count registry keys
        /// </summary>
        public class ModifyRegistry {
            private RegistryKey _baseRegistryKey = Registry.LocalMachine;

            public ModifyRegistry() {
                SubKey = "SOFTWARE\\LOBAPP";
                ShowError = false;
            }

            /// <summary>
            ///     A property to show or hide error messages
            ///     (default = false)
            /// </summary>
            public bool ShowError { get; set; }

            /// <summary>
            ///     A property to set the SubKey value
            ///     (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
            /// </summary>
            public string SubKey { get; set; }

            /// <summary>
            ///     A property to set the BaseRegistryKey value.
            ///     (default = Registry.LocalMachine)
            /// </summary>
            public RegistryKey BaseRegistryKey {
                get { return _baseRegistryKey; }
                set { _baseRegistryKey = value; }
            }

            /// <summary>
            ///     To read a registry key.
            ///     input: KeyName (string)
            ///     output: value (string)
            /// </summary>
            public string Read(string keyName) {
                // Opening the registry key
                RegistryKey rk = _baseRegistryKey;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(SubKey);
                // If the RegistrySubKey doesn't exist -> (null)
                if(sk1 == null) return null;
                try {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(keyName.ToUpper());
                } catch(Exception e) {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Reading registry " + keyName.ToUpper());
                    return null;
                }
            }

            /// <summary>
            ///     To write into a registry key.
            ///     input: KeyName (string) , Value (object)
            ///     output: true or false
            /// </summary>
            public bool Write(string keyName, object value) {
                try {
                    // Setting
                    RegistryKey rk = _baseRegistryKey;
                    // I have to use CreateSubKey 
                    // (create or open it if already exits), 
                    // 'cause OpenSubKey open a subKey as read-only
                    RegistryKey sk1 = rk.CreateSubKey(SubKey);
                    // Save the value
                    if(sk1 != null) sk1.SetValue(keyName.ToUpper(), value);

                    return true;
                } catch(Exception e) {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Writing registry " + keyName.ToUpper());
                    return false;
                }
            }

            /// <summary>
            ///     To delete a registry key.
            ///     input: KeyName (string)
            ///     output: true or false
            /// </summary>
            public bool DeleteKey(string keyName) {
                try {
                    // Setting
                    RegistryKey rk = _baseRegistryKey;
                    RegistryKey sk1 = rk.CreateSubKey(SubKey);
                    // If the RegistrySubKey doesn't exists -> (true)
                    if(sk1 == null) return true;
                    sk1.DeleteValue(keyName);

                    return true;
                } catch(Exception e) {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Deleting SubKey " + SubKey);
                    return false;
                }
            }

            /// <summary>
            ///     To delete a sub key and any child.
            ///     input: void
            ///     output: true or false
            /// </summary>
            public bool DeleteSubKeyTree() {
                try {
                    // Setting
                    RegistryKey rk = _baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(SubKey);
                    // If the RegistryKey exists, I delete it
                    if(sk1 != null) rk.DeleteSubKeyTree(SubKey);

                    return true;
                } catch(Exception e) {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Deleting SubKey " + SubKey);
                    return false;
                }
            }

            /// <summary>
            ///     Retrive the count of subkeys at the current key.
            ///     input: void
            ///     output: number of subkeys
            /// </summary>
            public int SubKeyCount() {
                try {
                    // Setting
                    RegistryKey rk = _baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(SubKey);
                    // If the RegistryKey exists...
                    if(sk1 != null) return sk1.SubKeyCount;
                    return 0;
                } catch(Exception e) {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Retriving subkeys of " + SubKey);
                    return 0;
                }
            }

            /// <summary>
            ///     Retrive the count of values in the key.
            ///     input: void
            ///     output: number of keys
            /// </summary>
            public int ValueCount() {
                try {
                    // Setting
                    RegistryKey rk = _baseRegistryKey;
                    RegistryKey sk1 = rk.OpenSubKey(SubKey);
                    // If the RegistryKey exists...
                    if(sk1 != null) return sk1.ValueCount;
                    return 0;
                } catch(Exception e) {
                    ShowErrorMessage(e, "Retriving keys of " + SubKey);
                    return 0;
                }
            }

            private void ShowErrorMessage(Exception e, string title) { throw new NotImplementedException(); }
        }
    }
}