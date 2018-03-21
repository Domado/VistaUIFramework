using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework.Taskbar {

    /// <summary>
    /// The <see cref="JumpList"/> is a feature introduced with Windows 7. The <see cref="JumpList"/> contains tasks, categories and recent files
    /// </summary>
    public class JumpList {

        private static JumpList helper;
        private NativeMethods.ICustomDestinationList dest;
        private bool included;

        private JumpList() {
            dest = (NativeMethods.ICustomDestinationList) new JumpListInstance();
            Guid guid = new Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9");
            int result = dest.BeginList(out uint cMaxSlots, ref guid, out object ppv);
            MaxVisibleSlots = (int)cMaxSlots;
            if (NativeMethods.Failed(result)) {
                throw Marshal.GetExceptionForHR(result);
            }
        }

        /// <summary>
        /// Add a file to the 'Recent' category
        /// </summary>
        /// <param name="Shortcut">The <see cref="JumpListLink"/> as a shortcut, you'll be able to customize arguments, icon, working directory, etc.</param>
        public static void AddToRecentFiles(JumpListLink Shortcut) {
            NativeMethods.SHAddToRecentDocs(NativeMethods.SHARD.SHARD_LINK, Shortcut.NativeInterface);
        }

        /// <summary>
        /// Add a file to the 'Recent' category
        /// </summary>
        /// <param name="Path">The path to the file</param>
        public static void AddToRecentFiles(string Path) {
            if (Marshal.SystemDefaultCharSize == 1) {
                NativeMethods.SHAddToRecentDocs(NativeMethods.SHARD.SHARD_PATHA, Path);
            } else {
                NativeMethods.SHAddToRecentDocs(NativeMethods.SHARD.SHARD_PATHW, Path);
            }
        }

        /// <summary>
        /// Add <see cref="JumpListLink"/>s to the Tasks category, user tasks cannot be unpinned
        /// </summary>
        /// <param name="Tasks">The list of tasks to add</param>
        public void AddUserTasks(params JumpListLink[] Tasks) {
            if (Tasks == null) throw new ArgumentNullException("Tasks", "The array is null");
            if (Tasks.Length == 0) throw new ArgumentException("Tasks", "The array is empty");
            NativeMethods.IObjectCollection collection = (NativeMethods.IObjectCollection) new CollectionInstance();
            foreach (JumpListLink Link in Tasks) {
                collection.AddObject(Link.NativeInterface);
            }
            NativeMethods.IObjectArray array = (NativeMethods.IObjectArray) collection;
            int result = dest.AddUserTasks(array);
            if (NativeMethods.Failed(result)) {
                throw Marshal.GetExceptionForHR(result);
            }
        }

        /// <summary>
        /// Add a category of <see cref="JumpListLink"/>s, the links are unpinnable
        /// </summary>
        /// <param name="Title">The title of the category</param>
        /// <param name="Links">The links category will contain</param>
        public void AddCategory(string Title, params JumpListLink[] Links) {
            NativeMethods.IObjectCollection collection = (NativeMethods.IObjectCollection) new CollectionInstance();
            foreach (JumpListLink Link in Links) {
                collection.AddObject(Link.NativeInterface);
            }
            NativeMethods.IObjectArray array = (NativeMethods.IObjectArray) collection;
            int result = dest.AppendCategory(Title, array);
            if (NativeMethods.Failed(result)) {
                throw Marshal.GetExceptionForHR(result);
            }
        }

        /// <summary>
        /// Delete all tasks and categories and start the <see cref="JumpListLink"/> from scratch
        /// </summary>
        public void Delete() {
            try {
                dest.DeleteList(null);
            } catch (Exception) {}
        }

        /// <summary>
        /// The <see cref="JumpList"/> will include the Recent category
        /// </summary>
        public void IncludeRecentCategory() {
            if (!included) {
                dest.AppendKnownCategory(NativeMethods.KNOWNDESTCATEGORY.KDC_RECENT);
                included = true;
            }
        }

        /// <summary>
        /// Commit the changes to the <see cref="JumpListLink"/>
        /// </summary>
        public void Commit() {
            dest.CommitList();
        }

        /// <summary>
        /// Discard any changes made
        /// </summary>
        public void Discard() {
            dest.AbortList();
        }

        /// <summary>
        /// Gets the amount of visible slots according to user settings
        /// </summary>
        public int MaxVisibleSlots { get; private set; }

        /// <summary>
        /// Gets the instance of the <see cref="JumpList"/>, if Windows version is earlier than Windows 7, an exception will be throwed. Call <see cref="IsSupported"/> property to check if version is Windows 7 or later
        /// </summary>
        public static JumpList Instance {
            get {
                if (!IsSupported) {
                    throw new UnsupportedWindowsException("Windows 7");
                }
                if (helper == null) helper = new JumpList();
                return helper;
            }
        }

        /// <summary>
        /// Check if Windows version supports <see cref="JumpList"/> methods.
        /// </summary>
        public static bool IsSupported {
            get {
                return Environment.OSVersion.Version >= new Version(6, 1);
            }
        }

        [Guid("77F10CF0-3DB5-4966-B520-B7C54FD35ED6")]
        [ClassInterface(ClassInterfaceType.None)]
        [ComImport]
        private class JumpListInstance { }

        [Guid("2D3468C1-36A7-43B6-AC24-D3F02FD9607A")]
        [ClassInterface(ClassInterfaceType.None)]
        [ComImport]
        private class CollectionInstance { }

    }
}