using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework.Taskbar {

    /// <summary>
    /// The <see cref="JumpListLink"/> is a link based on <see cref="Shortcut"/> made for <see cref="JumpList"/>s
    /// </summary>
    public class JumpListLink : Shortcut {

        private string _Title;

        /// <summary>
        /// Initializes a new instance of <see cref="JumpListLink"/>
        /// </summary>
        /// <param name="Title">The title of the <see cref="JumpListLink"/></param>
        /// <param name="DestPath">The destination path (for example: an executable)</param>
        public JumpListLink(string Title, string DestPath) : base(true, DestPath) {
            this.Title = Title;
        }

        /// <summary>
        /// Gets or sets the title of the <see cref="JumpListLink"/>
        /// </summary>
        public string Title {
            get {
                return _Title;
            }
            set {
                if (_Title != value) {
                    _Title = value;
                    NativeMethods.PROPERTYKEY PKEY_Title = new NativeMethods.PROPERTYKEY {
                        fmtid = new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"),
                        pid = 2
                    };
                    IntPtr IntStr = Marshal.StringToCoTaskMemAuto(value);
                    NativeMethods.PROPVARIANT prop = new NativeMethods.PROPVARIANT {
                        vt = (ushort) VarEnum.VT_LPWSTR,
                        p = IntStr
                    };
                    NativeMethods.IPropertyStore store = (NativeMethods.IPropertyStore) NativeInterface;
                    store.SetValue(ref PKEY_Title, prop);
                    store = null;
                    Marshal.FreeHGlobal(IntStr);
                }
            }
        }

        /// <summary>
        /// DON'T CALL THIS METHOD, THIS WAS MEANT FOR .LNK FILES, JUMPLISTLINK IS A LINK BUT IT'S NOT A .LNK FILE
        /// </summary>
        /// <param name="LnkSourcePath">DON'T CALL THIS METHOD</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("DON'T CALL THIS METHOD")]
        private static new Shortcut Load(string LnkSourcePath) {
            throw new NotImplementedException("DON'T CALL THIS METHOD");
        }

        /// <summary>
        /// DON'T CALL THIS METHOD, THIS WAS MEANT FOR .LNK FILES, JUMPLISTLINK IS A LINK BUT IT'S NOT A .LNK FILE
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("DON'T CALL THIS METHOD")]
        private new void Save() {
            throw new NotImplementedException("DON'T CALL THIS METHOD");
        }

    }
}