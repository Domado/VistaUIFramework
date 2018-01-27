﻿using MyAPKapp.VistaUIFramework.TaskDialog;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {

    internal delegate int TaskDialogCallbackProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, IntPtr referenceData);

    /// <summary>
    /// NativeMethods is an internal class that contains all unmanaged native classes, interfaces, enums, structs, methods, macros, etc.
    /// </summary>
    internal class NativeMethods {

        #region Native constants

        public const int TRUE = 1;
        public const int FALSE = 0;
        public const int SC_CLOSE = 0xF060;
        public const int CS_NOCLOSE = 0x0200;
        public const int IMAGE_BITMAP = 0;
        public const int IMAGE_ICON = 1;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;
        public const int GWL_EXSTYLE = (-20);
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int ERROR_INVALID_HANDLE = 6;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const int MAX_PATH = 260;
        public const string WC_IPADDRESS = "SysIPAddress32";
        public const string REBARCLASSNAME = "ReBarWindow32";

        /* MF VARIABLES */
        public const int MF_BYCOMMAND = 0x0000;
        public const int MF_ENABLED = 0x0000;
        public const int MF_DISABLED = 0x0002;
        public const int MF_GRAYED = 0x0001;

        /* WM VARIABLES */
        public const int WM_USER = 0x0400;
        public const int WM_PAINT = 0x000F;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_CTLCOLORSTATIC = 0x0138;
        public const int WM_SETICON = 0x0080;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        public const int WM_DWMNCRENDERINGCHANGED = 0x031F;
        public const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        public const int WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321;

        /* WS VARIABLES */
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_BORDER = 0x00800000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_LAYOUTRTL = 0x00400000;
        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;

        /* SW VARIABLES */
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        /* EDIT VARIABLES */
        public const int ECM_FIRST = 0x1500;
        public const int EM_SETCUEBANNER = ECM_FIRST + 1;

        /* COMBOBOX VARIABLES */
        public const int CBM_FIRST = 0x1700;
        public const int CB_SETCUEBANNER = CBM_FIRST + 3;

        /* CLASS VARIABLES */
        public const int CS_VREDRAW = 0x0001;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_GLOBALCLASS = 0x4000;

        /* BUTTON VARIABLES */
        public const int BCM_FIRST = 0x1600;
        public const int BCM_SETNOTE = BCM_FIRST + 0x0009;
        public const int BCM_SETDROPDOWNSTATE = BCM_FIRST + 0x0006;
        public const int BCM_SETSHIELD = BCM_FIRST + 0x000C;
        public const int BS_COMMANDLINK = 0x000E;
        public const int BS_DEFCOMMANDLINK = 0x000F;
        public const int BS_PUSHBUTTON = 0x0000;
        public const int BS_DEFPUSHBUTTON = 0x0001;
        public const int BS_SPLITBUTTON = 0x000C;
        public const int BS_DEFSPLITBUTTON = 0x000D;
        public const int BS_ICON = 0x00000040;
        public const int BM_SETIMAGE = 0x00F7;

        /* PROGRESS BAR VARIABLES */
        public const int PBM_SETSTATE = WM_USER + 16;
        public const int PBS_SMOOTHREVERSE = 0x10;
        public const int PBST_NORMAL = 0x0001;
        public const int PBST_ERROR = 0x0002;
        public const int PBST_PAUSED = 0x0003;

        /* LISTVIEW VARIABLES */
        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        public const int LVS_EX_DOUBLEBUFFER = 0x00010000;

        /* TREEVIEW VARIABLES */
        public const int TV_FIRST = 0x1100;
        public const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        public const int TVS_EX_AUTOHSCROLL = 0x0020;
        public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        public const int TVS_EX_DOUBLEBUFFER = 0x0004;

        /* MENU VARIABLES */
        public const int MIM_STYLE = 0x00000010;
        public const int MNS_CHECKORBMP = 0x04000000;
        public const int MIIM_BITMAP = 0x00000080;

        /* TASK DIALOG VARIABLES */
        public const int TDM_NAVIGATE_PAGE = WM_USER + 101;
        public const int TDM_CLICK_BUTTON = WM_USER + 102;
        public const int TDM_SET_MARQUEE_PROGRESS_BAR = WM_USER + 103;
        public const int TDM_SET_PROGRESS_BAR_STATE = WM_USER + 104;
        public const int TDM_SET_PROGRESS_BAR_RANGE = WM_USER + 105;
        public const int TDM_SET_PROGRESS_BAR_POS = WM_USER + 106;
        public const int TDM_SET_PROGRESS_BAR_MARQUEE = WM_USER + 107;
        public const int TDM_SET_ELEMENT_TEXT = WM_USER + 108;
        public const int TDM_CLICK_RADIO_BUTTON = WM_USER + 110;
        public const int TDM_ENABLE_BUTTON = WM_USER + 111;
        public const int TDM_ENABLE_RADIO_BUTTON = WM_USER + 112;
        public const int TDM_CLICK_VERIFICATION = WM_USER + 113;
        public const int TDM_UPDATE_ELEMENT_TEXT = WM_USER + 114;
        public const int TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE = WM_USER + 115;
        public const int TDM_UPDATE_ICON = WM_USER + 116;
        public const int TDN_CREATED = 0;
        public const int TDN_NAVIGATED = 1;
        public const int TDN_BUTTON_CLICKED = 2;
        public const int TDN_HYPERLINK_CLICKED = 3;
        public const int TDN_TIMER = 4;
        public const int TDN_DESTROYED = 5;
        public const int TDN_RADIO_BUTTON_CLICKED = 6;
        public const int TDN_DIALOG_CONSTRUCTED = 7;
        public const int TDN_VERIFICATION_CLICKED = 8;
        public const int TDN_HELP = 9;
        public const int TDN_EXPANDO_BUTTON_CLICKED = 10;
        public const int TDE_CONTENT = 0;
        public const int TDE_EXPANDED_INFORMATION = 1;
        public const int TDE_FOOTER = 2;
        public const int TDE_MAIN_INSTRUCTION = 3;
        public const int TDIE_ICON_MAIN = 0;
        public const int TDIE_ICON_FOOTER = 1;

        /* REBAR VARIABLES */
        public const int ICC_COOL_CLASSES = 0x00000400;
        public const int ICC_BAR_CLASSES = 0x00000004;
        public const int CCS_NODIVIDER = 0x00000040;
        public const int RBS_VARHEIGHT = 0x00000200;
        public const int RBS_BANDBORDERS = 0x00000400;
        public const int RB_INSERTBANDA = WM_USER + 1;
        public const int RB_DELETEBAND = WM_USER + 2;
        public const int RB_INSERTBANDW = WM_USER + 10;
        public const int RB_SETBANDINFOA = WM_USER + 6;
        public const int RB_SETBANDINFOW = WM_USER + 11;
        public static readonly int RB_INSERTBAND = (Marshal.SystemDefaultCharSize == 1) ? RB_INSERTBANDA : RB_INSERTBANDW;
        public static readonly int RB_SETBANDINFO = (Marshal.SystemDefaultCharSize == 1) ? RB_SETBANDINFOA : RB_SETBANDINFOW;
        public const int RBBIM_STYLE = 0x00000001;
        public const int RBBIM_COLORS = 0x00000002;
        public const int RBBIM_TEXT = 0x00000004;
        public const int RBBIM_IMAGE = 0x00000008;
        public const int RBBIM_CHILD = 0x00000010;
        public const int RBBIM_CHILDSIZE = 0x00000020;
        public const int RBBIM_SIZE = 0x00000040;
        public const int RBBIM_BACKGROUND = 0x00000080;
        public const int RBBIM_ID = 0x00000100;
        public const int RBBIM_IDEALSIZE = 0x00000200;
        public const int RBBIM_LPARAM = 0x00000400;
        public const int RBBIM_HEADERSIZE = 0x00000800;
        public const int RBBIM_CHEVRONLOCATION = 0x00001000;
        public const int RBBIM_CHEVRONSTATE = 0x00002000;

        /* STGM VARIABLES */
        public const int STGM_READ = 0x00000000;
        public const int STGM_WRITE = 0x00000001;
        public const int STGM_READWRITE = 0x00000002;

        #endregion

        #region Native resources

        public const int IDC_HAND = 32649;

        #endregion

        #region Native enums

        [Flags]
        public enum THUMBBUTTONFLAGS {
            THBF_ENABLED = 0,
            THBF_DISABLED = 0x1,
            THBF_DISMISSONCLICK = 0x2,
            THBF_NOBACKGROUND = 0x4,
            THBF_HIDDEN = 0x8,
            THBF_NONINTERACTIVE = 0x10
        }

        [Flags]
        public enum THUMBBUTTONMASK {
            THB_BITMAP = 0x1,
            THB_ICON = 0x2,
            THB_TOOLTIP = 0x4,
            THB_FLAGS = 0x8
        }

        public enum StockObjects {
            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = NULL_BRUSH,
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 16,
            DEFAULT_GUI_FONT = 17,
            DC_BRUSH = 18,
            DC_PEN = 19
        }

        [Flags]
        public enum LoadLibraryFlags : uint {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        [Flags]
        public enum DTT : uint {
            TextColor = (1 << 0),
            BorderColor = (1 << 1),
            ShadowColor = (1 << 2),
            ShadowType = (1 << 3),
            ShadowOffset = (1 << 4),
            BorderSize = (1 << 5),
            FontProp = (1 << 6),
            ColorProp = (1 << 7),
            StateID = (1 << 8),
            CalcRect = (1 << 9),
            ApplyOverlay = (1 << 10),
            GlowSize = (1 << 11),
            Callback = (1 << 12),
            Composited = (1 << 13)
        }

        public enum BitmapCompressionMode : uint {
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5
        }

        public enum TEXTSHADOWTYPE : int {
            None = 0,
            Single = 1,
            Continuous = 2,
        }

        /// <summary>
        ///     Specifies a raster-operation code. These codes define how the color data for the
        ///     source rectangle is to be combined with the color data for the destination
        ///     rectangle to achieve the final color.
        /// </summary>
        public enum TernaryRasterOperations : uint {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            /// <summary>dest = source AND dest</summary>
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062,
            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows 
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CAPTUREBLT = 0x40000000
        }

        [Flags]
        public enum TaskDialogFlags {
            TDF_ENABLE_HYPERLINKS = 0x0001,
            TDF_USE_HICON_MAIN = 0x0002,
            TDF_USE_HICON_FOOTER = 0x0004,
            TDF_ALLOW_DIALOG_CANCELLATION = 0x0008,
            TDF_USE_COMMAND_LINKS = 0x0010,
            TDF_USE_COMMAND_LINKS_NO_ICON = 0x0020,
            TDF_EXPAND_FOOTER_AREA = 0x0040,
            TDF_EXPANDED_BY_DEFAULT = 0x0080,
            TDF_VERIFICATION_FLAG_CHECKED = 0x0100,
            TDF_SHOW_PROGRESS_BAR = 0x0200,
            TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x0400,
            TDF_CALLBACK_TIMER = 0x0800,
            TDF_POSITION_RELATIVE_TO_WINDOW = 0x1000,
            TDF_RTL_LAYOUT = 0x2000,
            TDF_NO_DEFAULT_RADIO_BUTTON = 0x4000,
            TDF_CAN_BE_MINIMIZED = 0x8000,
            TDF_NO_SET_FOREGROUND = 0x00010000,
            TDF_SIZE_TO_CONTENT = 0x01000000
        }

        [Flags]
        public enum SHGSI : uint {
            SHGSI_ICONLOCATION = 0,
            SHGSI_ICON = 0x000000100,
            SHGSI_SYSICONINDEX = 0x000004000,
            SHGSI_LINKOVERLAY = 0x000008000,
            SHGSI_SELECTED = 0x000010000,
            SHGSI_LARGEICON = 0x000000000,
            SHGSI_SMALLICON = 0x000000001,
            SHGSI_SHELLICONSIZE = 0x000000004
        }

        [Flags]
        public enum SLGP_FLAGS {
            SLGP_SHORTPATH = 0x1,
            SLGP_UNCPRIORITY = 0x2,
            SLGP_RAWPATH = 0x4,
            SLGP_RELATIVEPRIORITY = 0x8
        }

        [Flags]
        public enum SLR_FLAGS {
            SLR_NONE = 0,
            SLR_NO_UI = 0x1,
            SLR_ANY_MATCH = 0x2,
            SLR_UPDATE = 0x4,
            SLR_NOUPDATE = 0x8,
            SLR_NOSEARCH = 0x10,
            SLR_NOTRACK = 0x20,
            SLR_NOLINKINFO = 0x40,
            SLR_INVOKE_MSI = 0x80,
            SLR_NO_UI_WITH_MSG_PUMP = 0x101,
            SLR_OFFER_DELETE_WITHOUT_FILE = 0x200,
            SLR_KNOWNFOLDER = 0x400,
            SLR_MACHINE_IN_LOCAL_TARGET = 0x800,
            SLR_UPDATE_MACHINE_AND_SID = 0x1000,
            SLR_NO_OBJECT_ID = 0x2000
        }

        [Flags]
        public enum SHELL_LINK_DATA_FLAGS {
            SLDF_DEFAULT = 0x00000000,
            SLDF_HAS_ID_LIST = 0x00000001,
            SLDF_HAS_LINK_INFO = 0x00000002,
            SLDF_HAS_NAME = 0x00000004,
            SLDF_HAS_RELPATH = 0x00000008,
            SLDF_HAS_WORKINGDIR = 0x00000010,
            SLDF_HAS_ARGS = 0x00000020,
            SLDF_HAS_ICONLOCATION = 0x00000040,
            SLDF_UNICODE = 0x00000080,
            SLDF_FORCE_NO_LINKINFO = 0x00000100,
            SLDF_HAS_EXP_SZ = 0x00000200,
            SLDF_RUN_IN_SEPARATE = 0x00000400,
            SLDF_HAS_LOGO3ID = 0x00000800,
            SLDF_HAS_DARWINID = 0x00001000,
            SLDF_RUNAS_USER = 0x00002000,
            SLDF_HAS_EXP_ICON_SZ = 0x00004000,
            SLDF_NO_PIDL_ALIAS = 0x00008000,
            SLDF_FORCE_UNCNAME = 0x00010000,
            SLDF_RUN_WITH_SHIMLAYER = 0x00020000,
            SLDF_FORCE_NO_LINKTRACK = 0x00040000,
            SLDF_ENABLE_TARGET_METADATA = 0x000800000,
            SLDF_DISABLE_LINK_PATH_TRACKING = 0x00100000,
            SLDF_DISABLE_KNOWNFOLDER_RELATIVE_TRACKING = 0x00200000,
            SLDF_NO_KF_ALIAS = 0x00400000,
            SLDF_ALLOW_LINK_TO_LINK = 0x00800000,
            SLDF_UNALIAS_ON_SAVE = 0x01000000,
            SLDF_PREFER_ENVIRONMENT_PATH = 0x02000000,
            SLDF_KEEP_LOCAL_IDLIST_FOR_UNC_TARGET = 0x04000000,
            SLDF_PERSIST_VOLUME_ID_RELATIVE = 0x08000000,
            SLDF_VALID = 0x0FFFF7FF,
            SLDF_RESERVED = 0x8000000
        }

        [Flags]
        public enum DWM_BB {
            Enable = 1,
            BlurRegion = 2,
            TransitionMaximized = 4
        }

        #endregion

        #region Containers

        /* STRUCTURES, CLASSES AND INTERFACES */

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        public struct THUMBBUTTON {
            public const int Clicked = 0x1800;
            [MarshalAs(UnmanagedType.U4)]
            public THUMBBUTTONMASK dwMask;
            public int iId;
            public int iBitmap;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szTip;
            public THUMBBUTTONFLAGS dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INITCOMMONCONTROLSEX {
            public int dwSize;
            public uint dwICC;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct REBARBANDINFO {
            public int cbSize;
            public int fMask;
            public int fStyle;
            public int clrFore;
            public int clrBack;
            public IntPtr lpText;
            public int cch;
            public int iImage;
            public IntPtr hwndChild;
            public int cxMinChild;
            public int cyMinChild;
            public int cx;
            public IntPtr hbmBack;
            public int wID;
            public int cyChild;
            public int cyMaxChild;
            public int cyIntegral;
            public int cxIdeal;
            public int lParam;
            public int cxHeader;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom) {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r) {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r) {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2) {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2) {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r) {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj) {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode() {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString() {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DTTOPTS {
            public int dwSize;
            public DTT dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public TEXTSHADOWTYPE iTextShadowType;
            public System.Drawing.Point ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public BitmapCompressionMode biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        /// <summary>
        /// Extends ITaskbarList2 by exposing methods that support the unified launching and switching taskbar button
        /// functionality added in Windows 7. This functionality includes thumbnail representations and switch
        /// targets based on individual tabs in a tabbed application, thumbnail toolbars, notification and
        /// status overlays, and progress indicators.
        /// </summary>
        [ComImport,
        Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITaskbarList3 {
            // ITaskbarList

            /// <summary>
            /// Initializes the taskbar list object. This method must be called before any other ITaskbarList methods can be called.
            /// </summary>
            void HrInit();

            /// <summary>
            /// Adds an item to the taskbar.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be added to the taskbar.</param>
            void AddTab(IntPtr hWnd);

            /// <summary>
            /// Deletes an item from the taskbar.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be deleted from the taskbar.</param>
            void DeleteTab(IntPtr hWnd);

            /// <summary>
            /// Activates an item on the taskbar. The window is not actually activated; the window's item on the taskbar is merely displayed as active.
            /// </summary>
            /// <param name="hWnd">A handle to the window on the taskbar to be displayed as active.</param>
            void ActivateTab(IntPtr hWnd);

            /// <summary>
            /// Marks a taskbar item as active but does not visually activate it.
            /// </summary>
            /// <param name="hWnd">A handle to the window to be marked as active.</param>
            void SetActiveAlt(IntPtr hWnd);

            // ITaskbarList2

            /// <summary>
            /// Marks a window as full-screen
            /// </summary>
            /// <param name="hWnd"></param>
            /// <param name="fFullscreen"></param>
            void MarkFullscreenWindow(IntPtr hWnd, int fFullscreen);

            /// <summary>
            /// Displays or updates a progress bar hosted in a taskbar button to show the specific percentage
            /// completed of the full operation.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose associated taskbar button is being used as
            /// a progress indicator.</param>
            /// <param name="ullCompleted">An application-defined value that indicates the proportion of the
            /// operation that has been completed at the time the method is called.</param>
            /// <param name="ullTotal">An application-defined value that specifies the value ullCompleted will
            /// have when the operation is complete.</param>
            void SetProgressValue(IntPtr hWnd, ulong ullCompleted, ulong ullTotal);

            /// <summary>
            /// Sets the type and state of the progress indicator displayed on a taskbar button.
            /// </summary>
            /// <param name="hWnd">The handle of the window in which the progress of an operation is being
            /// shown. This window's associated taskbar button will display the progress bar.</param>
            /// <param name="tbpFlags">Flags that control the current state of the progress button. Specify
            /// only one of the following flags; all states are mutually exclusive of all others.</param>
            void SetProgressState(IntPtr hWnd, TaskBarProgressState tbpFlags);

            /// <summary>
            /// Informs the taskbar that a new tab or document thumbnail has been provided for display in an
            /// application's taskbar group flyout.
            /// </summary>
            /// <param name="hWndTab">Handle of the tab or document window. This value is required and cannot
            /// be NULL.</param>
            /// <param name="hWndMDI">Handle of the application's main window. This value tells the taskbar
            /// which application's preview group to attach the new thumbnail to. This value is required and
            /// cannot be NULL.</param>
            void RegisterTab(IntPtr hWndTab, IntPtr hWndMDI);

            /// <summary>
            /// Removes a thumbnail from an application's preview group when that tab or document is closed in the application.
            /// </summary>
            /// <param name="hWndTab">The handle of the tab window whose thumbnail is being removed. This is the same
            /// value with which the thumbnail was registered as part the group through ITaskbarList3::RegisterTab.
            /// This value is required and cannot be NULL.</param>
            void UnregisterTab(IntPtr hWndTab);

            /// <summary>
            /// Inserts a new thumbnail into a tabbed-document interface (TDI) or multiple-document interface
            /// (MDI) application's group flyout or moves an existing thumbnail to a new position in the
            /// application's group.
            /// </summary>
            /// <param name="hWndTab">The handle of the tab window whose thumbnail is being placed. This value
            /// is required, must already be registered through ITaskbarList3::RegisterTab, and cannot be NULL.</param>
            /// <param name="hWndInsertBefore">The handle of the tab window whose thumbnail that hwndTab is
            /// inserted to the left of. This handle must already be registered through ITaskbarList3::RegisterTab.
            /// If this value is NULL, the new thumbnail is added to the end of the list.</param>
            void SetTabOrder(IntPtr hWndTab, IntPtr hWndInsertBefore);

            /// <summary>
            /// Informs the taskbar that a tab or document window has been made the active window.
            /// </summary>
            /// <param name="hWndTab">Handle of the active tab window. This handle must already be registered
            /// through ITaskbarList3::RegisterTab. This value can be NULL if no tab is active.</param>
            /// <param name="hWndMDI">Handle of the application's main window. This value tells the taskbar
            /// which group the thumbnail is a member of. This value is required and cannot be NULL.</param>
            /// <param name="tbatFlags">None, one, or both of the following values that specify a thumbnail
            /// and peek view to use in place of a representation of the specific tab or document.</param>
            void SetTabActive(IntPtr hWndTab, IntPtr hWndMDI, int tbatFlags);

            /// <summary>
            /// Adds a thumbnail toolbar with a specified set of buttons to the thumbnail image of a window in a
            /// taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose thumbnail representation will receive the toolbar.
            /// This handle must belong to the calling process.</param>
            /// <param name="cButtons">The number of buttons defined in the array pointed to by pButton. The maximum
            /// number of buttons allowed is 7.</param>
            /// <param name="pButton">A pointer to an array of THUMBBUTTON structures. Each THUMBBUTTON defines an
            /// individual button to be added to the toolbar. Buttons cannot be added or deleted later, so this must
            /// be the full defined set. Buttons also cannot be reordered, so their order in the array, which is the
            /// order in which they are displayed left to right, will be their permanent order.</param>
            void ThumbBarAddButtons(
            IntPtr hWnd,
            int cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButton);

            void ThumbBarUpdateButtons(
            IntPtr hWnd,
            int cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButton);

            /// <summary>
            /// Specifies an image list that contains button images for a toolbar embedded in a thumbnail image of a
            /// window in a taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose thumbnail representation contains the toolbar to be
            /// updated. This handle must belong to the calling process.</param>
            /// <param name="himl">The handle of the image list that contains all button images to be used in the toolbar.</param>
            void ThumbBarSetImageList(IntPtr hWnd, IntPtr himl);

            /// <summary>
            /// Applies an overlay to a taskbar button to indicate application status or a notification to the user.
            /// </summary>
            /// <param name="hWnd">The handle of the window whose associated taskbar button receives the overlay.
            /// This handle must belong to a calling process associated with the button's application and must be
            /// a valid HWND or the call is ignored.</param>
            /// <param name="hIcon">The handle of an icon to use as the overlay. This should be a small icon,
            /// measuring 16x16 pixels at 96 dots per inch (dpi). If an overlay icon is already applied to the
            /// taskbar button, that existing overlay is replaced.</param>
            /// <param name="pszDescription">A pointer to a string that provides an alt text version of the
            /// information conveyed by the overlay, for accessibility purposes.</param>
            void SetOverlayIcon(IntPtr hWnd, IntPtr hIcon, string pszDescription);

            /// <summary>
            /// Specifies or updates the text of the tooltip that is displayed when the mouse pointer rests on an
            /// individual preview thumbnail in a taskbar button flyout.
            /// </summary>
            /// <param name="hWnd">The handle to the window whose thumbnail displays the tooltip. This handle must
            /// belong to the calling process.</param>
            /// <param name="pszTip">The pointer to the text to be displayed in the tooltip. This value can be NULL,
            /// in which case the title of the window specified by hwnd is used as the tooltip.</param>
            void SetThumbnailTooltip(IntPtr hWnd, string pszTip);

            /// <summary>
            /// Selects a portion of a window's client area to display as that window's thumbnail in the taskbar.
            /// </summary>
            /// <param name="hWnd">The handle to a window represented in the taskbar.</param>
            /// <param name="prcClip">A pointer to a RECT structure that specifies a selection within the window's
            /// client area, relative to the upper-left corner of that client area. To clear a clip that is already
            /// in place and return to the default display of the thumbnail, set this parameter to NULL.</param>
            void SetThumbnailClip(IntPtr hWnd, IntPtr prcClip);
        }

        /// <summary>The IShellLink interface allows Shell links to be created, modified, and resolved</summary>
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport()]
        public interface IShellLink {
            /// <summary>Retrieves the path and file name of a Shell link object</summary>
            int GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATA pfd, SLGP_FLAGS fFlags);
            /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
            int GetIDList(out IntPtr ppidl);
            /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
            int SetIDList(IntPtr pidl);
            /// <summary>Retrieves the description string for a Shell link object</summary>
            int GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
            int SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
            int GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            /// <summary>Sets the name of the working directory for a Shell link object</summary>
            int SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
            int GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            /// <summary>Sets the command-line arguments for a Shell link object</summary>
            int SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            /// <summary>Retrieves the hot key for a Shell link object</summary>
            int GetHotkey(out short pwHotkey);
            /// <summary>Sets a hot key for a Shell link object</summary>
            int SetHotkey(short wHotkey);
            /// <summary>Retrieves the show command for a Shell link object</summary>
            int GetShowCmd(out int piShowCmd);
            /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
            int SetShowCmd(int iShowCmd);
            /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
            int GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                int cchIconPath, out int piIcon);
            /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
            int SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            /// <summary>Sets the relative path to the Shell link object</summary>
            int SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
            int Resolve(IntPtr hwnd, SLR_FLAGS fFlags);
            /// <summary>Sets the path and file name of a Shell link object</summary>
            int SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [Guid("45e2b4ae-b1c3-11d0-b92f-00a0c90312e1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport()]
        public interface IShellLinkDataList {
            int AddDataBlock(IntPtr pDataBlock);
            int CopyDataBlock(uint dwSig, out IntPtr ppDataBlock);
            int RemoveDataBlock(uint dwSig);
            int GetFlags(out SHELL_LINK_DATA_FLAGS pdwFlags);
            int SetFlags(SHELL_LINK_DATA_FLAGS dwFlags);
        }

        [Guid("0000010b-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport()]
        public interface IPersistFile {
            int GetClassID(out Guid pClassID);
            int IsDirty();
            int Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
            int Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);
            int SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
            int GetCurFile([In, MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct TASKDIALOGCONFIG {
            public int cbSize;
            public IntPtr hwndParent;
            public IntPtr hInstance;
            public TaskDialogFlags dwFlags;
            public TaskDialogCommonButton dwCommonButtons;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszWindowTitle;
            public IconUnion mainIcon;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMainInstruction;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszContent;
            public int cButtons;
            public IntPtr pButtons;
            public int nDefaultButton;
            public int cRadioButtons;
            public IntPtr pRadioButtons;
            public int nDefaultRadioButton;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszVerificationText;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedInformation;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedControlText;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCollapsedControlText;
            public IconUnion footerIcon;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;
            public TaskDialogCallbackProc pfCallback;
            public IntPtr lpCallbackData;
            public int cxWidth;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct TASKDIALOG_BUTTON {
            public int nButtonID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszButtonText;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
        public struct IconUnion {
            [FieldOffset(0)]
            public IntPtr hIcon;

            [FieldOffset(0)]
            public int pszIcon;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO {
            public int cbSize;
            public IntPtr hwnd;
            public int dwFlags;
            public int uCount;
            public int dwTimeout;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHSTOCKICONINFO {
            public int cbSize;
            public IntPtr hIcon;
            public int iSysIconIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DATABLOCK_HEADER {
            public int cbSize;
            public int dwSignature;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct EXP_DARWIN_LINK {
            public DATABLOCK_HEADER dbh;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDarwinID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szwDarwinID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND {
            public DWM_BB dwFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fEnable;
            public IntPtr hRgnBlur;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fTransitionOnMaximized;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO_T_RW {
            public int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T_RW));
            public int fMask = 0x00000080; //MIIM_BITMAP = 0x00000080
            public int fType;
            public int fState;
            public int wID;
            public IntPtr hSubMenu = IntPtr.Zero;
            public IntPtr hbmpChecked = IntPtr.Zero;
            public IntPtr hbmpUnchecked = IntPtr.Zero;
            public IntPtr dwItemData = IntPtr.Zero;
            public IntPtr dwTypeData = IntPtr.Zero;
            public int cch;
            public IntPtr hbmpItem = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUINFO {
            public int cbSize = Marshal.SizeOf(typeof(MENUINFO));
            public int fMask = 0x00000010; //MIM_STYLE;
            public int dwStyle = 0x04000000; //MNS_CHECKORBMP;
            public uint cyMax;
            public IntPtr hbrBack = IntPtr.Zero;
            public int dwContextHelpID;
            public IntPtr dwMenuData = IntPtr.Zero;
        }

        #endregion

        #region Utilities

        public static IntPtr StructArrayToPtr(TASKDIALOG_BUTTON[] structs, bool deleteOld) {
            IntPtr initialPtr = Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(TASKDIALOG_BUTTON)) * structs.Length);
            IntPtr currentPtr = initialPtr;
            foreach (TASKDIALOG_BUTTON button in structs) {
                Marshal.StructureToPtr(button, currentPtr, false);
                currentPtr = (IntPtr)((int)currentPtr + Marshal.SizeOf(button));
            }
            return initialPtr;
        }

        public static int BoolToNative(bool boolean) {
            return boolean ? TRUE : FALSE;
        }

        public static bool NativeToBool(int NativeBool) {
            return NativeBool == 1;
        }

        public static bool NativeToBool(IntPtr NativeBool) {
            return NativeToBool(NativeBool.ToInt32());
        }

        #endregion

        #region Methods

        /* METHODS */

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableComposition(int uCompositionAction);

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref DWM_BLURBEHIND blurBehind);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetColorizationColor(out int ColorizationColor, [MarshalAs(UnmanagedType.Bool)] out bool ColorizationOpaqueBlend);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, int pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, int pszSubIdList);

        // This helper static method is required because the 32-bit version of user32.dll does not contain this API
        // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
        // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong) {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("comctl32.dll", EntryPoint = "InitCommonControlsEx", CallingConvention = CallingConvention.StdCall)]
        public static extern bool InitCommonControlsEx(ref INITCOMMONCONTROLSEX iccex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, MENUITEMINFO_T_RW lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetMenuInfo(HandleRef hMenu, MENUINFO lpcmi);

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(StockObjects fnObject);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
   int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int length, int flags, ref RECT rect, ref DTTOPTS poptions);

        /// <summary>
        ///        Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc">A handle to an existing DC. If this handle is NULL,
        ///        the function creates a memory DC compatible with the application's current screen.</param>
        /// <returns>
        ///        If the function succeeds, the return value is the handle to a memory DC.
        ///        If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi,
   uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        /// <summary>Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.</summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="hgdiobj">A handle to the object to be selected.</param>
        /// <returns>
        ///   <para>If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced. If the selected object is a region and the function succeeds, the return value is one of the following values.</para>
        ///   <para>SIMPLEREGION - Region consists of a single rectangle.</para>
        ///   <para>COMPLEXREGION - Region consists of more than one rectangle.</para>
        ///   <para>NULLREGION - Region is empty.</para>
        ///   <para>If an error occurs and the selected object is not a region, the return value is <c>NULL</c>. Otherwise, it is <c>HGDI_ERROR</c>.</para>
        /// </returns>
        /// <remarks>
        ///   <para>This function returns the previously selected object of the specified type. An application should always replace a new object with the original, default object after it has finished drawing with the new object.</para>
        ///   <para>An application cannot select a single bitmap into more than one DC at a time.</para>
        ///   <para>ICM: If the object being selected is a brush or a pen, color management is performed.</para>
        /// </remarks>
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        /// <summary>
        ///    Performs a bit-block transfer of the color data corresponding to a
        ///    rectangle of pixels from the specified source device context into
        ///    a destination device context.
        /// </summary>
        /// <param name="hdc">Handle to the destination device context.</param>
        /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        /// <param name="hdcSrc">Handle to the source device context.</param>
        /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        /// <param name="dwRop">A raster-operation code.</param>
        /// <returns>
        ///    <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        /// <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>
        ///   <para>If the function succeeds, the return value is nonzero.</para>
        ///   <para>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
        /// </returns>
        /// <remarks>
        ///   <para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
        ///   <para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
        /// </remarks>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        /// <summary>Deletes the specified device context (DC).</summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <returns><para>If the function succeeds, the return value is nonzero.</para><para>If the function fails, the return value is zero.</para></returns>
        /// <remarks>An application must not delete a DC whose handle was obtained by calling the <c>GetDC</c> function. Instead, it must call the <c>ReleaseDC</c> function to free the DC.</remarks>
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC([In] IntPtr hdc);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int TaskDialog([In] IntPtr hWndParent, [In] IntPtr hInstance, [MarshalAs(UnmanagedType.LPWStr), In] string pszWindowTitle, [MarshalAs(UnmanagedType.LPWStr), In] string pszMainInstruction, [MarshalAs(UnmanagedType.LPWStr), In] string pszContent, [In] TaskDialogCommonButton dwCommonButtons, [In] int pszIcon, [Out] out int pnButton);

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern int TaskDialogIndirect([In] TASKDIALOGCONFIG pTaskConfig, [Out] out int pnButton, [Out] out int pnRadioButton, [MarshalAs(UnmanagedType.Bool), Out] out bool pfverificationFlagChecked);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        ///     Changes the text of the specified window's title bar (if it has one). If the specified window is a control, the
        ///     text of the control is changed. However, SetWindowText cannot change the text of a control in another application.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633546%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hwnd">C++ ( hWnd [in]. Type: HWND )<br />A handle to the window or control whose text is to be changed.</param>
        /// <param name="lpString">C++ ( lpString [in, optional]. Type: LPCTSTR )<br />The new title or control text.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.<br />
        ///     To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     If the target window is owned by the current process, <see cref="SetWindowText" /> causes a WM_SETTEXT message to
        ///     be sent to the specified window or control. If the control is a list box control created with the WS_CAPTION style,
        ///     however, <see cref="SetWindowText" /> sets the text for the control, not for the list box entries.<br />To set the
        ///     text of a control in another process, send the WM_SETTEXT message directly instead of calling
        ///     <see cref="SetWindowText" />. The <see cref="SetWindowText" /> function does not expand tab characters (ASCII code
        ///     0x09). Tab characters are displayed as vertical bar(|) characters.<br />For an example go to
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms644928%28v=vs.85%29.aspx#sending">
        ///     Sending a
        ///     Message.
        ///     </see>
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bInvert);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        ///     priority to the thread that created the foreground window than it does to other threads.
        ///     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
        /// </param>
        /// <returns>
        ///     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
        ///     brought to the foreground.
        /// </returns>
        /// <remarks>
        ///     The system restricts which processes can set the foreground window. A process can set the foreground window only if
        ///     one of the following conditions is true:
        ///     <list type="bullet">
        ///     <listheader>
        ///         <term>Conditions</term><description></description>
        ///     </listheader>
        ///     <item>The process is the foreground process.</item>
        ///     <item>The process was started by the foreground process.</item>
        ///     <item>The process received the last input event.</item>
        ///     <item>There is no foreground process.</item>
        ///     <item>The process is being debugged.</item>
        ///     <item>The foreground process is not a Modern Application or the Start Screen.</item>
        ///     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        ///     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        ///     <item>No menus are active.</item>
        ///     </list>
        ///     <para>
        ///     An application cannot force a window to the foreground while the user is working with another window.
        ///     Instead, Windows flashes the taskbar button of the window to notify the user.
        ///     </para>
        ///     <para>
        ///     A process that can set the foreground window can enable another process to set the foreground window by
        ///     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
        ///     the foreground window the next time the user generates input, unless the input is directed at that process, or
        ///     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
        ///     </para>
        ///     <para>
        ///     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
        ///     function.
        ///     </para>
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("Shell32.dll", SetLastError = false)]
        public static extern int SHGetStockIconInfo(StockIcon siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);

        #endregion

        #region Macros

        /* WINDOWS API ELEMENTS */

        public static int GetHiWord(long value, int size) {
            return (short)(value >> size);
        }

        public static int GetLoWord(long value) {
            return (short)(value & 0xFFFF);
        }

        public static long MakeLParam(int low, int high) {
            return (high << 16) + low;
        }

        public static bool Succeeded(int hr) {
            return hr == S_OK;
        }

        public static int MakeIntResource(int resource) {
            return (ushort) resource;
        }

        public static string MakeIntResource(int resource, bool sharpMode) {
            if (sharpMode) {
                return "#" + resource;
            } else {
                return MakeIntResource(resource).ToString();
            }
        }

        public static Win32Exception NativeException() {
            return new Win32Exception(Marshal.GetLastWin32Error());
        }

        #endregion

    }
}
