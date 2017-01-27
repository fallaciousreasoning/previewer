using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Shell32;
using SHDocVw;

namespace Previewer.Core
{
    /// <summary>
    /// Allows you to pick up the currently selected file from an explorer window
    /// </summary>
    public static class SelectionDetector
    {
        private const string EXPLORER_NAME = "explorer";

        /// <summary>
        /// Determines whether the currently active window is an explorer window
        /// </summary>
        /// <returns></returns>
        public static bool IsExplorerWindowActive()
        {
            var activeHandle = GetForegroundWindow();
            return new SHDocVw.ShellWindows().Cast<InternetExplorer>().Any(window => window.HWND == (int) activeHandle);
        }

        /// <summary>
        /// Determines if the currently active window is an explorer window AND that there is at least one file selected
        /// </summary>
        /// <returns></returns>
        public static bool SelectedAndExplorerActive()
        {
            var activeHandle = GetForegroundWindow();
            return new ShellWindows().Cast<InternetExplorer>().Any(window => window.HWND == (int) activeHandle && ((Shell32.IShellFolderViewDual2) window.Document).SelectedItems().Count > 0);
        }

        /// <summary>
        /// Gets a list of all the currently selected files/folders
        /// </summary>
        /// <returns>A list of currently selected files</returns>
        public static List<FolderItem> SelectedPaths()
        {
            var activeHandle = GetForegroundWindow();

            var selected = new List<FolderItem>();
            foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindows())
            {
                if (window.HWND != (int)activeHandle) continue;

                var filename = Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                if (filename.ToLowerInvariant() == EXPLORER_NAME)
                {
                    Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();
                    foreach (Shell32.FolderItem item in items)
                    {
                        selected.Add(FromShellFolderItem(item));
                    }
                }
            }
            GetItemsInSelectedPath();
            return selected;
        }

        /// <summary>
        /// Gets the first selected item from explorer
        /// </summary>
        /// <returns>A path to a selected file in explorer</returns>
        public static FolderItem SelectedPath()
        {
            return SelectedPaths().FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of items in the currently selected path
        /// </summary>
        /// <returns>Items in the selected path</returns>
        public static List<FolderItem> GetItemsInSelectedPath()
        {
            var activeHandle = GetForegroundWindow();

            var inDir = new List<FolderItem>();
            foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindows())
            {
                if (window.HWND != (int) activeHandle) continue;

                var filename = Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                if (filename != EXPLORER_NAME) continue;

                var document = (Shell32.IShellFolderViewDual2) window.Document;
                var items = document.Folder.Items();
                
                foreach (Shell32.FolderItem item in items)
                    inDir.Add(FromShellFolderItem(item));
            }
            return inDir;
        }

        /// <summary>
        /// Converts a Shell32.FolderItem to a FolderItem
        /// </summary>
        /// <param name="folderItem">The shell folder item</param>
        /// <returns>The folder item</returns>
        private static FolderItem FromShellFolderItem(Shell32.FolderItem folderItem)
        {
            return new FolderItem()
            {
                IsFolder = folderItem.IsFolder,
                Path = folderItem.Path,
                Name = folderItem.Name,
                Size = folderItem.Size,
                ModifiedDate = folderItem.ModifyDate
            };
        }

        /// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
