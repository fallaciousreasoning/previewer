using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SHDocVw;

namespace Previewer
{
    /// <summary>
    /// Allows you to pick up the currently selected file from an explorer window
    /// </summary>
    public static class SelectionDetector
    {
        private const string EXPLORER_NAME = "explorer";

        public static bool IsExplorerWindowActive()
        {
            var activeHandle = GetForegroundWindow();
            return new SHDocVw.ShellWindows().Cast<InternetExplorer>().Any(window => window.HWND == (int) activeHandle);
        }

        public static bool SelectedAndExplorerActive()
        {
            var activeHandle = GetForegroundWindow();
            return new ShellWindows().Cast<InternetExplorer>().Any(window => window.HWND == (int) activeHandle && ((Shell32.IShellFolderViewDual2) window.Document).SelectedItems().Count > 0);
        }

        /// <summary>
        /// Gets a list of all the currently selected files/folders
        /// </summary>
        /// <returns>A list of currently selected files</returns>
        public static List<string> SelectedPaths()
        {
            var activeHandle = GetForegroundWindow();

            var selected = new List<string>();
            foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindows())
            {
                if (window.HWND != (int)activeHandle) continue;

                var filename = Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                if (filename.ToLowerInvariant() == EXPLORER_NAME)
                {
                    Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();
                    foreach (Shell32.FolderItem item in items)
                    {
                        selected.Add(item.Path);
                    }
                }
            }
            return selected;
        }

        /// <summary>
        /// Gets the first selected item from explorer
        /// </summary>
        /// <returns>A path to a selected file in explorer</returns>
        public static string SelectedPath()
        {
            return SelectedPaths().FirstOrDefault();
        }

        /// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
