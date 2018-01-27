using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// Shortcuts are .lnk files that redirects the Windows Explorer to directories or files
    /// </summary>
    public class Shortcut {

        private NativeMethods.IShellLink link;
        private NativeMethods.IShellLinkDataList list;
        private NativeMethods.IPersistFile file;

        /// <summary>
        /// Creates a new Shortcut instance, recommended for new .lnk files
        /// </summary>
        /// <param name="LnkDestPath">The path where the .lnk file will be created</param>
        public Shortcut(string LnkDestPath) {
            this.LnkDestPath = LnkDestPath;
            link = (NativeMethods.IShellLink) new ShellLink();
            list = (NativeMethods.IShellLinkDataList) link;
            file = (NativeMethods.IPersistFile) link;
        }

        private Shortcut(string LnkSourcePath, bool loaded = false) : this(LnkSourcePath) {
            if (loaded) {
                int result = file.Load(LnkDestPath, NativeMethods.STGM_READWRITE);
                if (!NativeMethods.Succeeded(result)) {
                    Marshal.ThrowExceptionForHR(result);
                }
            }
        }

        /// <summary>
        /// Open an existing .lnk shortcut
        /// </summary>
        /// <param name="LnkSourcePath">The path of the .lnk file</param>
        /// <returns>Creates a new istance of Shortcut with the properties of the .lnk file</returns>
        public static Shortcut Load(string LnkSourcePath) {
            Shortcut shortcut = new Shortcut(LnkSourcePath, true);
            return shortcut;
        }

        /// <summary>
        /// The path where the .lnk file will be created (in case file doesn't exist)
        /// </summary>
        public string LnkDestPath { get; }

        /// <summary>
        /// The path where Windows Explorer will redirect to (without arguments). Environment variables are allowed
        /// </summary>
        public string DestinationPath {
            get {
                StringBuilder builder = new StringBuilder(NativeMethods.MAX_PATH);
                NativeMethods.WIN32_FIND_DATA data = new NativeMethods.WIN32_FIND_DATA();
                link.GetPath(builder, NativeMethods.MAX_PATH, out data, NativeMethods.SLGP_FLAGS.SLGP_RAWPATH);
                return builder.ToString();
            }
            set {
                link.SetPath(value);
            }
        }

        /// <summary>
        /// The arguments for the path
        /// </summary>
        public string Arguments {
            get {
                StringBuilder builder = new StringBuilder(NativeMethods.MAX_PATH);
                link.GetArguments(builder, NativeMethods.MAX_PATH);
                return builder.ToString();
            }
            set {
                link.SetArguments(value);

            }
        }

        /// <summary>
        /// The shortcut link description
        /// </summary>
        public string Description {
            get {
                StringBuilder builder = new StringBuilder(1024);
                link.GetDescription(builder, 1024);
                return builder.ToString();
            }
            set {
                link.SetDescription(value);
            }
        }

        /// <summary>
        /// The working directory (only directory)
        /// </summary>
        public string WorkingDirectory {
            get {
                StringBuilder builder = new StringBuilder(1024);
                link.GetWorkingDirectory(builder, 1024);
                return builder.ToString();
            }
            set {
                link.SetWorkingDirectory(value);
            }
        }

        /// <summary>
        /// The shortcut's icon path (without the index)
        /// </summary>
        public string IconPath {
            get {
                StringBuilder builder = new StringBuilder(NativeMethods.MAX_PATH);
                link.GetIconLocation(builder, NativeMethods.MAX_PATH, out int dummy);
                return builder.ToString();
            }
            set {
                link.SetIconLocation(value, IconIndex);
            }
        }

        /// <summary>
        /// The icon index, the default value is 0 so it's not mandatory to set this property
        /// </summary>
        public int IconIndex {
            get {
                StringBuilder dummy = new StringBuilder(NativeMethods.MAX_PATH);
                link.GetIconLocation(dummy, NativeMethods.MAX_PATH, out int index);
                dummy = null;
                return index;
            }
            set {
                link.SetIconLocation(IconPath, value);
            }
        }

        /// <summary>
        /// Set if shortcut will start normal, maximized or minimized
        /// </summary>
        public ShowModes ShowMode {
            get {
                int ShowCmd = 1;
                link.GetShowCmd(out ShowCmd);
                return (ShowModes) (ShowCmd - 1);
            }
            set {
                int ShowCmd = (int) value;
                link.SetShowCmd(ShowCmd + 1);
            }
        }

        public System.Windows.Forms.Keys HotKey {
            get {
                link.GetHotkey(out short key);
                return (System.Windows.Forms.Keys) key;
            }
            set {
                link.SetHotkey((short) value);
            }
        }

        /// <summary>
        /// Set if the shortcut will execute the program as administrator
        /// </summary>
        public bool RunAsAdmin {
            get {
                list.GetFlags(out NativeMethods.SHELL_LINK_DATA_FLAGS flags);
                return (flags & NativeMethods.SHELL_LINK_DATA_FLAGS.SLDF_RUNAS_USER) != 0;
            }
            set {
                list.GetFlags(out NativeMethods.SHELL_LINK_DATA_FLAGS flags);
                if (value) {
                    if ((flags & NativeMethods.SHELL_LINK_DATA_FLAGS.SLDF_RUNAS_USER) == 0) {
                        flags |= NativeMethods.SHELL_LINK_DATA_FLAGS.SLDF_RUNAS_USER;
                        list.SetFlags(flags);
                    }
                } else {
                    if ((flags & NativeMethods.SHELL_LINK_DATA_FLAGS.SLDF_RUNAS_USER) != 0) {
                        flags &= ~(NativeMethods.SHELL_LINK_DATA_FLAGS.SLDF_RUNAS_USER);
                        list.SetFlags(flags);
                    }
                }
            }
        }

        /// <summary>
        /// The show modes
        /// </summary>
        public enum ShowModes {
            Normal, Minimized, Maximized
        }

        /// <summary>
        /// Once the properties are set, you can save the shortcut .lnk file
        /// </summary>
        public void Save() {
            int saveResult = file.Save(LnkDestPath, true);
            if (NativeMethods.Succeeded(saveResult)) {
                int completedResult = file.SaveCompleted(LnkDestPath);
                if (!NativeMethods.Succeeded(completedResult)) {
                    Marshal.ThrowExceptionForHR(completedResult);
                }
            } else {
                Marshal.ThrowExceptionForHR(saveResult);
            }
        }

        [Guid("00021401-0000-0000-C000-000000000046")]
        [ClassInterface(ClassInterfaceType.None)]
        [ComImport()]
        private class ShellLink { }

    }
}
