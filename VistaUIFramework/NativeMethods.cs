//--------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using MyAPKapp.VistaUIFramework.TaskDialog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {

    #region Delegate callback procedures

    internal delegate int TaskDialogCallbackProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, IntPtr referenceData);
    [return: MarshalAs(UnmanagedType.Bool)]
    internal delegate bool EnumResTypeProc(IntPtr hModule, IntPtr lpszType, IntPtr lParam);
    [return: MarshalAs(UnmanagedType.Bool)]
    internal delegate bool EnumResNameProc(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);

    #endregion

    /// <summary>
    /// NativeMethods is an internal class that contains all unmanaged native classes, interfaces, enums, structs, methods, macros, etc.
    /// </summary>
    internal sealed class NativeMethods {

        /// <summary>
        /// No constructors allowed, <see cref="NativeMethods"/> is a container of static elements
        /// </summary>
        private NativeMethods() {}

        #region Native constants

        public const int TRUE = 1;
        public const int FALSE = 0;
        public const int SC_CLOSE = 0xF060;
        public const int IMAGE_BITMAP = 0;
        public const int IMAGE_ICON = 1;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_INVALIDARG = 0x8007005;
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

        /* GWL VARIABLES */
        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;

        /* WM VARIABLES */
        public const int WM_USER = 0x0400;
        public const int WM_PAINT = 0x000F;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_CTLCOLORSTATIC = 0x0138;
        public const int WM_SETICON = 0x0080;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NOTIFY = 0x004E;
        public const int WM_REFLECT = WM_USER + 0x1C00;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        public const int WM_DWMNCRENDERINGCHANGED = 0x031F;
        public const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        public const int WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONDBLCLK = 0x0203;

        /* WS VARIABLES */
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_BORDER = 0x00800000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_LAYOUTRTL = 0x00400000;
        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const int WS_EX_TOPMOST = 0x00000008;

        /* CS VARIABLES */
        public const int CS_VREDRAW = 0x0001;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_OWNDC = 0x0020;
        public const int CS_CLASSDC = 0x0040;
        public const int CS_PARENTDC = 0x0080;
        public const int CS_NOCLOSE = 0x0200;
        public const int CS_SAVEBITS = 0x0800;
        public const int CS_BYTEALIGNCLIENT = 0x1000;
        public const int CS_BYTEALIGNWINDOW = 0x2000;
        public const int CS_GLOBALCLASS = 0x4000;

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
        public const int LVM_GETGROUPINFOBYINDEX = LVM_FIRST + 153;
        public const int LVM_SETGROUPINFO = LVM_FIRST + 147;
        public const int LVGS_COLLAPSIBLE = 0x00000008;
        public const int LVGF_STATE = 0x00000004;
        public const int LVGF_GROUPID = 0x00000010;

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

        /* CCS VARIABLES */
        public const int CCS_TOP = 0x00000001;
        public const int CCS_NOMOVEY = 0x00000002;
        public const int CCS_BOTTOM = 0x00000003;
        public const int CCS_NORESIZE = 0x00000004;
        public const int CCS_NOPARENTALIGN = 0x00000008;
        public const int CCS_ADJUSTABLE = 0x00000020;
        public const int CCS_NODIVIDER = 0x00000040;
        public const int CCS_VERT = 0x00000080;
        public const int CCS_LEFT = CCS_VERT | CCS_TOP;
        public const int CCS_RIGHT = CCS_VERT | CCS_BOTTOM;
        public const int CCS_NOMOVEX = CCS_VERT | CCS_NOMOVEY;

        /* ICC VARIABLES */
        public const int ICC_COOL_CLASSES = 0x00000400;
        public const int ICC_INTERNET_CLASSES = 0x00000800;

        /* RBS VARIABLES */
        public const int RBS_TOOLTIPS = 0x00000100;
        public const int RBS_VARHEIGHT = 0x00000200;
        public const int RBS_BANDBORDERS = 0x00000400;
        public const int RBS_FIXEDORDER = 0x00000800;
        public const int RBS_REGISTERDROP = 0x00001000;
        public const int RBS_AUTOSIZE = 0x00002000;
        public const int RBS_VERTICALGRIPPER = 0x00004000;
        public const int RBS_DBLCLKTOGGLE = 0x00008000;

        /* RB VARIABLES */
        public const int RB_SETBARINFO = WM_USER + 4;
        public const int RB_DELETEBAND = WM_USER + 2;
        public const int RB_HITTEST = WM_USER + 8;
        public const int RB_GETRECT = WM_USER + 9;
        public const int RB_GETBANDCOUNT = WM_USER + 12;
        public const int RB_INSERTBANDA = WM_USER + 1;
        public const int RB_INSERTBANDW = WM_USER + 10;
        public const int RB_SETBANDINFOA = WM_USER + 6;
        public const int RB_SETBANDINFOW = WM_USER + 11;
        public static readonly int RB_INSERTBAND = IsAnsi ? RB_INSERTBANDA : RB_INSERTBANDW;
        public static readonly int RB_SETBANDINFO = IsAnsi ? RB_SETBANDINFOA : RB_SETBANDINFOW;

        /* RBBIM VARIABLES */
        public const int RBIM_IMAGELIST = 0x00000001;
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

        /* RBBS VARIABLES */
        public const int RBBS_BREAK = 0x00000001;
        public const int RBBS_FIXEDSIZE = 0x00000002;
        public const int RBBS_CHILDEDGE = 0x00000004;
        public const int RBBS_HIDDEN = 0x00000008;
        public const int RBBS_NOVERT = 0x00000010;
        public const int RBBS_FIXEDBMP = 0x00000020;
        public const int RBBS_VARIABLEHEIGHT = 0x00000040;
        public const int RBBS_GRIPPERALWAYS = 0x00000080;
        public const int RBBS_NOGRIPPER = 0x00000100;
        public const int RBBS_USECHEVRON = 0x00000200;
        public const int RBBS_HIDETITLE = 0x00000400;
        public const int RBBS_TOPALIGN = 0x00000800;

        /* RBN VARIABLES */
        public const int RBN_FIRST = 0 - 831;
        public const int RBN_HEIGHTCHANGE = (RBN_FIRST - 0);
        public const int RBN_GETOBJECT = (RBN_FIRST - 1);
        public const int RBN_LAYOUTCHANGED = (RBN_FIRST - 2);
        public const int RBN_AUTOSIZE = (RBN_FIRST - 3);
        public const int RBN_BEGINDRAG = (RBN_FIRST - 4);
        public const int RBN_ENDDRAG = (RBN_FIRST - 5);
        public const int RBN_DELETINGBAND = (RBN_FIRST - 6);
        public const int RBN_DELETEDBAND = (RBN_FIRST - 7);
        public const int RBN_CHILDSIZE = (RBN_FIRST - 8);
        public const int RBN_CHEVRONPUSHED = (RBN_FIRST - 10);
        public const int RBN_SPLITTERDRAG = (RBN_FIRST - 11);
        public const int RBN_MINMAX = (RBN_FIRST - 21);
        public const int RBN_AUTOBREAK = (RBN_FIRST - 22);

        /* NM VARIABLES */
        public const int NM_FIRST = 0 - 0;
        public const int NM_OUTOFMEMORY = NM_FIRST - 1;
        public const int NM_CLICK = NM_FIRST - 2;
        public const int NM_DBLCLK = NM_FIRST - 3;
        public const int NM_RETURN = NM_FIRST - 4;
        public const int NM_RCLICK = NM_FIRST - 5;
        public const int NM_RDBLCLK = NM_FIRST - 6;
        public const int NM_SETFOCUS = NM_FIRST - 7;
        public const int NM_KILLFOCUS = NM_FIRST - 8;
        public const int NM_CUSTOMDRAW = NM_FIRST - 12;
        public const int NM_HOVER = NM_FIRST - 13;
        public const int NM_NCHITTEST = NM_FIRST - 14;
        public const int NM_KEYDOWN = NM_FIRST - 15;
        public const int NM_RELEASEDCAPTURE = NM_FIRST - 16;
        public const int NM_SETCURSOR = NM_FIRST - 17;
        public const int NM_CHAR = NM_FIRST - 18;
        public const int NM_TOOLTIPSCREATED = NM_FIRST - 19;
        public const int NM_LDOWN = NM_FIRST - 20;
        public const int NM_RDOWN = NM_FIRST - 21;
        public const int NM_THEMECHANGED = NM_FIRST - 22;

        /* STGM VARIABLES */
        public const int STGM_READ = 0x00000000;
        public const int STGM_WRITE = 0x00000001;
        public const int STGM_READWRITE = 0x00000002;

        /* STATE SYSTEM VARIABLES */
        public const int STATE_SYSTEM_UNAVAILABLE = 0x00000001;
        public const int STATE_SYSTEM_SELECTED = 0x00000002;
        public const int STATE_SYSTEM_FOCUSED = 0x00000004;
        public const int STATE_SYSTEM_PRESSED = 0x00000008;
        public const int STATE_SYSTEM_CHECKED = 0x00000010;
        public const int STATE_SYSTEM_MIXED = 0x00000020;
        public const int STATE_SYSTEM_INDETERMINATE = STATE_SYSTEM_MIXED;
        public const int STATE_SYSTEM_READONLY = 0x00000040;
        public const int STATE_SYSTEM_HOTTRACKED = 0x00000080;
        public const int STATE_SYSTEM_DEFAULT = 0x00000100;
        public const int STATE_SYSTEM_EXPANDED = 0x00000200;
        public const int STATE_SYSTEM_COLLAPSED = 0x00000400;
        public const int STATE_SYSTEM_BUSY = 0x00000800;
        public const int STATE_SYSTEM_FLOATING = 0x00001000;
        public const int STATE_SYSTEM_MARQUEED = 0x00002000;
        public const int STATE_SYSTEM_ANIMATED = 0x00004000;
        public const int STATE_SYSTEM_INVISIBLE = 0x00008000;
        public const int STATE_SYSTEM_OFFSCREEN = 0x00010000;
        public const int STATE_SYSTEM_SIZEABLE = 0x00020000;
        public const int STATE_SYSTEM_MOVEABLE = 0x00040000;
        public const int STATE_SYSTEM_SELFVOICING = 0x00080000;
        public const int STATE_SYSTEM_FOCUSABLE = 0x00100000;
        public const int STATE_SYSTEM_SELECTABLE = 0x00200000;
        public const int STATE_SYSTEM_LINKED = 0x00400000;
        public const int STATE_SYSTEM_TRAVERSED = 0x00800000;
        public const int STATE_SYSTEM_MULTISELECTABLE = 0x01000000;
        public const int STATE_SYSTEM_EXTSELECTABLE = 0x02000000;
        public const int STATE_SYSTEM_ALERT_LOW = 0x04000000;
        public const int STATE_SYSTEM_ALERT_MEDIUM = 0x08000000;
        public const int STATE_SYSTEM_ALERT_HIGH = 0x10000000;
        public const int STATE_SYSTEM_PROTECTED = 0x20000000;
        public const int STATE_SYSTEM_VALID = 0x3FFFFFFF;

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

        /// <summary>
        /// The flags for the <see cref="DTTOPTS.dwFlags"/> parameter
        /// </summary>
        [Flags]
        public enum DTT : uint {

            /// <summary>
            /// The crText member value is valid.
            /// </summary>
            TextColor = (1 << 0),

            /// <summary>
            /// The crBorder member value is valid.
            /// </summary>
            BorderColor = (1 << 1),

            /// <summary>
            /// The crShadow member value is valid.
            /// </summary>
            ShadowColor = (1 << 2),

            /// <summary>
            /// The iTextShadowType member value is valid.
            /// </summary>
            ShadowType = (1 << 3),

            /// <summary>
            /// The ptShadowOffset member value is valid.
            /// </summary>
            ShadowOffset = (1 << 4),

            /// <summary>
            /// The iBorderSize member value is valid.
            /// </summary>
            BorderSize = (1 << 5),

            /// <summary>
            /// The iFontPropId member value is valid.
            /// </summary>
            FontProp = (1 << 6),

            /// <summary>
            /// The iColorPropId member value is valid.
            /// </summary>
            ColorProp = (1 << 7),

            /// <summary>
            /// The iStateId member value is valid.
            /// </summary>
            StateID = (1 << 8),

            /// <summary>
            /// The pRect parameter of the DrawThemeTextEx function that uses this structure will be used as both an in and an out parameter. After the function returns, the pRect parameter will contain the rectangle that corresponds to the region calculated to be drawn.
            /// </summary>
            CalcRect = (1 << 9),

            /// <summary>
            /// The fApplyOverlay member value is valid.
            /// </summary>
            ApplyOverlay = (1 << 10),

            /// <summary>
            /// The iGlowSize member value is valid.
            /// </summary>
            GlowSize = (1 << 11),

            /// <summary>
            /// The pfnDrawTextCallback member value is valid.
            /// </summary>
            Callback = (1 << 12),

            /// <summary>
            /// Draws text with antialiased alpha. Use of this flag requires a top-down DIB section. This flag works only if the HDC passed to function DrawThemeTextEx has a top-down DIB section currently selected in it. For more information, see Device-Independent Bitmaps.
            /// </summary>
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

        public enum KNOWNDESTCATEGORY {
            KDC_FREQUENT = 1,
            KDC_RECENT = KDC_FREQUENT + 1
        }

        public enum SHARD {
            SHARD_PIDL = 0x00000001,
            SHARD_PATHA = 0x00000002,
            SHARD_PATHW = 0x00000003,
            SHARD_APPIDINFO = 0x00000004,
            SHARD_APPIDINFOIDLIST = 0x00000005,
            SHARD_LINK = 0x00000006,
            SHARD_APPIDINFOLINK = 0x00000007,
            SHARD_SHELLITEM = 0x00000008
        }

        #endregion

        #region Containers

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

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct REBARBANDINFO {
            public int cbSize;
            public int fMask;
            public int fStyle;
            public int clrFore;
            public int clrBack;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpText;
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
            public IntPtr lParam;
            public int cxHeader;
            public RECT rcChevronLocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct REBARINFO {
            public int cbSize;
            public int fMask;
            public IntPtr himl;
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

            public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

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

            public Point Location {
                get { return new Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public Size Size {
                get { return new Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator Rectangle(RECT r) {
                return new Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(Rectangle r) {
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

            public Rectangle Rect {
                get { return new Rectangle(X, Y, Width, Height); }
            }

            public override bool Equals(object obj) {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is Rectangle)
                    return Equals(new RECT((Rectangle)obj));
                return false;
            }

            public override int GetHashCode() {
                return ((Rectangle)this).GetHashCode();
            }

            public override string ToString() {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }

        /// <summary>
        /// Defines the options for the DrawThemeTextEx function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DTTOPTS {

            /// <summary>
            /// Size of the structure.
            /// </summary>
            public int dwSize;

            /// <summary>
            /// A combination of flags that specify whether certain values of the DTTOPTS structure have been specified, and how to interpret these values. This member can be a combination of the following.
            /// </summary>
            public DTT dwFlags;

            /// <summary>
            /// Specifies the color of the text that will be drawn.
            /// </summary>
            public int crText;

            /// <summary>
            /// Specifies the color of the outline that will be drawn around the text.
            /// </summary>
            public int crBorder;

            /// <summary>
            /// Specifies the color of the shadow that will be drawn behind the text.
            /// </summary>
            public int crShadow;

            /// <summary>
            /// Specifies the type of the shadow that will be drawn behind the text. This member can have one of the following values.
            /// </summary>
            public TEXTSHADOWTYPE iTextShadowType;

            /// <summary>
            /// Specifies the amount of offset, in logical coordinates, between the shadow and the text.
            /// </summary>
            public Point ptShadowOffset;

            /// <summary>
            /// Specifies the radius of the outline that will be drawn around the text.
            /// </summary>
            public int iBorderSize;

            /// <summary>
            /// Specifies an alternate font property to use when drawing text. For a list of possible values, see GetThemeSysFont.
            /// </summary>
            public int iFontPropId;

            /// <summary>
            /// Specifies an alternate color property to use when drawing text. If this value is valid and the corresponding flag is set in dwFlags, this value will override the value of crText. See the values listed in GetSysColor for the nIndex parameter.
            /// </summary>
            public int iColorPropId;

            /// <summary>
            /// Specifies an alternate state to use. This member is not used by DrawThemeTextEx.
            /// </summary>
            public int iStateId;

            /// <summary>
            /// If true, text will be drawn on top of the shadow and outline effects. If false, just the shadow and outline effects will be drawn.
            /// </summary>
            [MarshalAs(UnmanagedType.Bool)]
            public bool fApplyOverlay;

            /// <summary>
            /// Specifies the size of a glow that will be drawn on the background prior to any text being drawn.
            /// </summary>
            public int iGlowSize;

            /// <summary>
            /// Pointer to callback function for DrawThemeTextEx.
            /// </summary>
            public int pfnDrawTextCallback;

            /// <summary>
            /// Parameter for callback back function specified by pfnDrawTextCallback.
            /// </summary>
            public IntPtr lParam;
        }

        /// <summary>
        /// The <b>BITMAPINFO</b> structure contains information about the dimensions and color format of a device-independent bitmap (DIB).
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO {

            /// <summary>
            /// Specifies the number of bytes required by the structure. This value does not include the size of the color table or the size of the color masks, if they are appended to the end of structure.
            /// </summary>
            /// <remarks>
            /// </remarks>
            public int biSize;

            /// <summary>
            /// Specifies the width of the bitmap, in pixels. For information about calculating the stride of the bitmap.
            /// </summary>
            public int biWidth;

            /// <summary>
            /// <para>Specifies the height of the bitmap, in pixels.</para>
            /// <list type="bullet">
            /// <item><description>For uncompressed RGB bitmaps, if <see cref="BITMAPINFO.biHeight"/> is positive, the bitmap is a bottom-up DIB with the origin at the lower left corner. If <see cref="BITMAPINFO.biHeight"/> is negative, the bitmap is a top-down DIB with the origin at the upper left corner.</description></item>
            /// <item><description>For YUV bitmaps, the bitmap is always top-down, regardless of the sign of <see cref="BITMAPINFO.biHeight"/>. Decoders should offer YUV formats with postive <see cref="BITMAPINFO.biHeight"/>, but for backward compatibility they should accept YUV formats with either positive or negative <see cref="BITMAPINFO.biHeight"/>.</description></item>
            /// <item><description>For compressed formats, <see cref="BITMAPINFO.biHeight"/> must be positive, regardless of image orientation.</description></item>
            /// </list>
            /// </summary>
            public int biHeight;

            /// <summary>
            /// Specifies the number of planes for the target device. This value must be set to 1.
            /// </summary>
            public ushort biPlanes;

            /// <summary>
            /// Specifies the number of bits per pixel (bpp). For uncompressed formats, this value is the average number of bits per pixel. For compressed formats, this value is the implied bit depth of the uncompressed image, after the image has been decoded.
            /// </summary>
            public ushort biBitCount;

            /// <summary>
            /// For compressed video and YUV formats, this member is a FOURCC code, specified as a DWORD in little-endian order. For example, YUYV video has the FOURCC 'VYUY' or 0x56595559. For more information, see FOURCC Codes.
            /// </summary>
            public BitmapCompressionMode biCompression;

            /// <summary>
            /// Specifies the size, in bytes, of the image. This can be set to 0 for uncompressed RGB bitmaps.
            /// </summary>
            public uint biSizeImage;

            /// <summary>
            /// Specifies the horizontal resolution, in pixels per meter, of the target device for the bitmap.
            /// </summary>
            public int biXPelsPerMeter;

            /// <summary>
            /// Specifies the vertical resolution, in pixels per meter, of the target device for the bitmap.
            /// </summary>
            public int biYPelsPerMeter;

            /// <summary>
            /// Specifies the number of color indices in the color table that are actually used by the bitmap. See Remarks for more information.
            /// </summary>
            public uint biClrUsed;

            /// <summary>
            /// Specifies the number of color indices that are considered important for displaying the bitmap. If this value is zero, all colors are important.
            /// </summary>
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
        [ComImport]
        public interface IShellLink {
            /// <summary>Retrieves the path and file name of a Shell link object</summary>
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATA pfd, SLGP_FLAGS fFlags);
            /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
            void GetIDList(out IntPtr ppidl);
            /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
            void SetIDList(IntPtr pidl);
            /// <summary>Retrieves the description string for a Shell link object</summary>
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            /// <summary>Sets the name of the working directory for a Shell link object</summary>
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            /// <summary>Sets the command-line arguments for a Shell link object</summary>
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            /// <summary>Retrieves the hot key for a Shell link object</summary>
            void GetHotkey(out short pwHotkey);
            /// <summary>Sets a hot key for a Shell link object</summary>
            void SetHotkey(short wHotkey);
            /// <summary>Retrieves the show command for a Shell link object</summary>
            void GetShowCmd(out int piShowCmd);
            /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
            void SetShowCmd(int iShowCmd);
            /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                int cchIconPath, out int piIcon);
            /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            /// <summary>Sets the relative path to the Shell link object</summary>
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
            void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);
            /// <summary>Sets the path and file name of a Shell link object</summary>
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IPropertyStore {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            int GetCount([Out] out uint propertyCount);
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            int GetAt([In] uint propertyIndex, out PROPERTYKEY key);
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            int GetValue([In] ref PROPERTYKEY key, [Out] PROPVARIANT pv);
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            int SetValue([In] ref PROPERTYKEY key, [In] PROPVARIANT pv);
            [PreserveSig]
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            int Commit();
        }

        [Guid("45e2b4ae-b1c3-11d0-b92f-00a0c90312e1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IShellLinkDataList {
            void AddDataBlock(IntPtr pDataBlock);
            void CopyDataBlock(uint dwSig, out IntPtr ppDataBlock);
            void RemoveDataBlock(uint dwSig);
            void GetFlags(out SHELL_LINK_DATA_FLAGS pdwFlags);
            void SetFlags(SHELL_LINK_DATA_FLAGS dwFlags);
        }

        [Guid("6332DEBF-87B5-4670-90C0-5E57B408A49E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface ICustomDestinationList {
            void SetAppID([MarshalAs(UnmanagedType.LPWStr)] string pszAppID);
            [PreserveSig]
            int BeginList(out uint cMaxSlots, ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out object ppv);
            [PreserveSig]
            int AppendCategory([MarshalAs(UnmanagedType.LPWStr)] string pszCategory, [MarshalAs(UnmanagedType.Interface)] IObjectArray poa);
            void AppendKnownCategory(KNOWNDESTCATEGORY category);
            [PreserveSig]
            int AddUserTasks([MarshalAs(UnmanagedType.Interface)] IObjectArray poa);
            void CommitList();
            void GetRemovedDestinations(ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out object ppv);
            void DeleteList([MarshalAs(UnmanagedType.LPWStr)] string pszAppID);
            void AbortList();
        }

        [Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IObjectArray {
            void GetCount(out uint pcObjects);
            void GetAt(uint uiIndex, ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out object ppv);
        }

        [Guid("5632B1A4-E38A-400A-928A-D4CD63230295")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IObjectCollection {
            [PreserveSig]
            void GetCount(out uint cObjects);
            [PreserveSig]
            void GetAt(uint iIndex, ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out object ppvObject);
            void AddObject([MarshalAs(UnmanagedType.Interface)] object pvObject);
            void AddFromArray([MarshalAs(UnmanagedType.Interface)] IObjectArray poaSource);
            void RemoveObject(uint uiIndex);
            void Clear();
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
            public int fMask = 0x00000010;
            public int dwStyle = 0x04000000;
            public uint cyMax;
            public IntPtr hbrBack = IntPtr.Zero;
            public int dwContextHelpID;
            public IntPtr dwMenuData = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMREBARAUTOBREAK {
            public NMHDR hdr;
            public int uBand;
            public uint wID;
            public IntPtr lParam;
            public uint uMsg;
            public uint fStyleCurrent;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAutoBreak;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMRBAUTOSIZE {
            public NMHDR hdr;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fChanged;
            public RECT rcTarget;
            public RECT rcActual;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMREBAR {
            public NMHDR hdr;
            public uint dwMask;
            public int uBand;
            public uint fStyle;
            public uint wID;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMREBARCHEVRON {
            public NMHDR hdr;
            public int uBand;
            public uint wID;
            public IntPtr lParam;
            public RECT rc;
            public IntPtr lParamNM;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMREBARCHILDSIZE {
            public NMHDR hdr;
            public int uBand;
            public uint wID;
            public RECT rcChild;
            public RECT rcBand;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMREBARSPLITTER {
            public NMHDR hdr;
            public RECT rcSizing;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMMOUSE {
            public NMHDR hdr;
            public UIntPtr dwItemSpec;
            public UIntPtr dwItemData;
            public Point pt;
            public IntPtr dwHitInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RBHITTESTINFO {
            public Point pt;
            public RebarHitTest flags;
            public int iBand;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PROPERTYKEY {
            public Guid fmtid;
            public uint pid;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct PROPVARIANT {
            public ushort vt;
            public ushort wReserved1;
            public ushort wReserved2;
            public ushort wReserved3;
            public IntPtr p;
            public int p2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct CY {
            public uint Lo;
            public int Hi;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct BSTRBLOB {
            public int cbSize;
            public IntPtr pData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct BLOB {
            public int cbSize;
            public IntPtr pBlobData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct CArray {
            public uint cElems;
            public IntPtr pElems;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVGROUP {
            public int cbSize;
            public int mask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszHeader;
            public int cchHeader;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;
            public int cchFooter;
            public int iGroupId;
            public int stateMask;
            public int state;
            public int uAlign;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSubtitle;
            public int cchSubtitle;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszTask;
            public int cchTask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescriptionTop;
            public int cchDescriptionTop;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescriptionBottom;
            public int cchDescriptionBottom;
            public int iTitleImage;
            public int iExtendedImage;
            public int iFirstItem;
            public int cItems;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSubsetTitle;
            public int cchSubsetTitle;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Converts multiple structures into a single handle
        /// </summary>
        /// <param name="structs">Source structures</param>
        /// <param name="deleteOld">Would it delete the old memory?</param>
        /// <returns>Destination handle (IntPtr)</returns>
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

        /// <summary>
        /// Convert a managed bool into an unmanaged BOOL
        /// </summary>
        /// <param name="boolean">Self-explanatory (true or false)</param>
        /// <returns>TRUE (1) for true and FALSE (0) for false</returns>
        public static int BoolToNative(bool boolean) {
            return boolean ? TRUE : FALSE;
        }

        /// <summary>
        /// Convert an unmanaged BOOL into a managed bool
        /// </summary>
        /// <param name="NativeBool">TRUE (1 or non-zero) or FALSE (0)</param>
        /// <returns>A managed bool data type</returns>
        public static bool NativeToBool(int NativeBool) {
            return NativeBool != FALSE;
        }

        /// <summary>
        /// Convert an unmanaged BOOL into a managed bool
        /// </summary>
        /// <param name="NativeBool">TRUE (1 or non-zero) or FALSE (0)</param>
        /// <returns>A managed bool data type</returns>
        public static bool NativeToBool(IntPtr NativeBool) {
            return NativeToBool(NativeBool.ToInt32());
        }

        /// <summary>
        /// It detects if system is 32 (x86) or 64 (x64) bits before using GetWindowLong/GetWindowLongPtr
        /// </summary>
        /// <param name="hWnd">The handle of a window (HWND)</param>
        /// <param name="nIndex">The index of the property to change</param>
        /// <returns>It returns the requested value</returns>
        public static IntPtr GetWindowLongBits(IntPtr hWnd, int nIndex) {
            if (Is64Bits) {
                return GetWindowLongPtr(hWnd, nIndex);
            } else {
                return new IntPtr(GetWindowLong(hWnd, nIndex));
            }
        }

        /// <summary>
        /// It detects if system is 32 (x86) or 64 (x64) bits before using SetWindowLong/SetWindowLongPtr
        /// </summary>
        /// <param name="hWnd">The handle of a window (HWND)</param>
        /// <param name="nIndex">The index of the property to change</param>
        /// <param name="dwNewLong">The handle of the new value</param>
        /// <returns>It returns the previous value</returns>
        public static IntPtr SetWindowLongBits(IntPtr hWnd, int nIndex, IntPtr dwNewLong) {
            if (Is64Bits) {
                return SetWindowLongPtr(hWnd, nIndex, dwNewLong);
            } else {
                return new IntPtr(SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32()));
            }
        }

        /// <summary>
        /// It detects if system is 32 (x86) or 64 (x64) bits before using SetWindowLong/SetWindowLongPtr
        /// </summary>
        /// <param name="hWnd">The handle of a window (HWND)</param>
        /// <param name="nIndex">The index of the property to change</param>
        /// <param name="dwNewLong">The new value</param>
        /// <returns>It returns the previous value</returns>
        public static int SetWindowLongBits(IntPtr hWnd, int nIndex, int dwNewLong) {
            if (Is64Bits) {
                return SetWindowLongPtr(hWnd, nIndex, new IntPtr(dwNewLong)).ToInt32();
            } else {
                return SetWindowLong(hWnd, nIndex, dwNewLong);
            }
        }

        /// <summary>
        /// Check if the system is x64 (64-bits) or x86 (32-bits)
        /// </summary>
        public static bool Is64Bits {
            get {
                return IntPtr.Size == 8;
            }
        }

        /// <summary>
        /// Check if the system uses Ansi or Unicode (Wide multi-byte characters)
        /// </summary>
        public static bool IsAnsi {
            get {
                return Marshal.SystemDefaultCharSize == 1;
            }
        }

        #endregion

        #region Methods

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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref REBARBANDINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref REBARINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref RBHITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [Out] out RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref LVGROUP lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, LVGROUP lParam);

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

        [DllImport("comctl32.dll", EntryPoint = "InitCommonControlsEx", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
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

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)] string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
   int cxDesired, int cyDesired, uint fuLoad);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hTheme"></param>
        /// <param name="hdc"></param>
        /// <param name="iPartId"></param>
        /// <param name="iStateId"></param>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <param name="flags"></param>
        /// <param name="rect"></param>
        /// <param name="poptions"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The <see cref="CreateDIBSection(IntPtr, ref BITMAPINFO, uint, out IntPtr, IntPtr, uint)"/> method creates a DIB that applications can write to directly. The function gives you a pointer to the location of the bitmap bit values. You can supply a handle to a file-mapping object that the function will use to create the bitmap, or you can let the system allocate the memory for the bitmap.
        /// </summary>
        /// <param name="hdc">A handle to a device context. If the value of iUsage is DIB_PAL_COLORS, the function uses this device context's logical palette to initialize the DIB colors.</param>
        /// <param name="pbmi">A pointer to a <see cref="BITMAPINFO"/> structure that specifies various attributes of the DIB, including the bitmap dimensions and colors.</param>
        /// <param name="usage">The type of data contained in the bmiColors array member of the BITMAPINFO structure pointed to by pbmi (either logical palette indexes or literal RGB values). The following values are defined.</param>
        /// <param name="ppvBits">A pointer to a variable that receives a pointer to the location of the DIB bit values.</param>
        /// <param name="hSection">
        /// <para>A handle to a file-mapping object that the function will use to create the DIB. This parameter can be NULL.</para>
        /// <para>If hSection is not NULL, it must be a handle to a file-mapping object created by calling the CreateFileMapping function with the PAGE_READWRITE or PAGE_WRITECOPY flag. Read-only DIB sections are not supported. Handles created by other means will cause CreateDIBSection to fail.</para>
        /// <para>If hSection is not NULL, the CreateDIBSection function locates the bitmap bit values at offset dwOffset in the file-mapping object referred to by hSection. An application can later retrieve the hSection handle by calling the GetObject function with the HBITMAP returned by CreateDIBSection.</para>
        /// <para>If hSection is NULL, the system allocates memory for the DIB. In this case, the CreateDIBSection function ignores the dwOffset parameter. An application cannot later obtain a handle to this memory. The dshSection member of the DIBSECTION structure filled in by calling the GetObject function will be NULL.</para>
        /// </param>
        /// <param name="dwOffset">The offset from the beginning of the file-mapping object referenced by hSection where storage for the bitmap bit values is to begin. This value is ignored if hSection is NULL. The bitmap bit values are aligned on doubleword boundaries, so dwOffset must be a multiple of the size of a DWORD.</param>
        /// <returns>
        /// <para>If the method succeeds, the return value is a handle to the newly created DIB, and *ppvBits points to the bitmap bit values.</para>
        /// <para>If the function fails, the return value is NULL, and *ppvBits is NULL.</para>
        /// </returns>
        /// <remarks>
        /// <para>As noted above, if hSection is NULL, the system allocates memory for the DIB. The system closes the handle to that memory when you later delete the DIB by calling the <see cref="DeleteObject(IntPtr)"/> function. If hSection is not NULL, you must close the hSection memory handle yourself after calling <see cref="DeleteObject(IntPtr)"/> to delete the bitmap.</para>
        /// <para>You cannot paste a DIB section from one application into another application.</para>
        /// <para>CreateDIBSection does not use the BITMAPINFOHEADER parameters biXPelsPerMeter or biYPelsPerMeter and will not provide resolution information in the BITMAPINFO structure.</para>
        /// <para>You need to guarantee that the GDI subsystem has completed any drawing to a bitmap created by CreateDIBSection before you draw to the bitmap yourself. Access to the bitmap must be synchronized. Do this by calling the GdiFlush function. This applies to any use of the pointer to the bitmap bit values, including passing the pointer in calls to functions such as SetDIBits.</para>
        /// <para><b>ICM:</b> No color management is done.</para>
        /// </remarks>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi, uint usage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

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

        [DllImport("Shell32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SHAddToRecentDocs(SHARD flag, [MarshalAs(UnmanagedType.LPWStr)] string path);

        [DllImport("Shell32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SHAddToRecentDocs(SHARD flag, IntPtr pidl);

        [DllImport("Shell32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SHAddToRecentDocs(SHARD flag, IShellLink pidl);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentProcessExplicitAppUserModelID([Out, MarshalAs(UnmanagedType.LPWStr)] out string AppId);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppId);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumResourceTypes(IntPtr hModule, EnumResTypeProc lpEnumFunc, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern bool EnumResourceNames(IntPtr hModule, string lpszType, EnumResNameProc lpEnumFunc, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern bool EnumResourceNames(IntPtr hModule, int dwID, EnumResNameProc lpEnumFunc, IntPtr lParam);

        #endregion

        #region Macros

        /// <summary>
        /// In a packed int/dword, it gets the high integer value
        /// </summary>
        /// <param name="value">The packed integer/long</param>
        /// <param name="size">The size of the packed integer</param>
        /// <returns>Gets the high integer value of a packed integer</returns>
        public static int GetHiWord(long value, int size) {
            return (short)(value >> size);
        }

        /// <summary>
        /// In a packed int/dword, it gets the low integer value
        /// </summary>
        /// <param name="value">The packed integer/long</param>
        /// <returns>Gets the low integer value of a packed integer</returns>
        public static int GetLoWord(long value) {
            return (short)(value & 0xFFFF);
        }

        /// <summary>
        /// Makes a number by merging the low and high value into a packed int/long
        /// </summary>
        /// <param name="low">The low value</param>
        /// <param name="high">The high value</param>
        /// <returns></returns>
        public static long MakeLParam(int low, int high) {
            return (high << 16) + low;
        }
        
        /// <summary>
        /// Check if a result has been successful
        /// </summary>
        /// <param name="hr">HResult number</param>
        /// <returns>Success</returns>
        public static bool Succeeded(int hr) {
            return hr >= S_OK;
        }

        /// <summary>
        /// Check if a result has been failed
        /// </summary>
        /// <param name="hr">HResult number</param>
        /// <returns>Failure</returns>
        public static bool Failed(int hr) {
            return hr < S_OK;
        }

        /// <summary>
        /// Check if a resource is int or string
        /// </summary>
        /// <param name="value">UINT or LPCTSTR</param>
        /// <returns>Resource is int or string</returns>
        public static bool IsIntResource(IntPtr value) {
            if (((uint)value) > ushort.MaxValue)
                return false;
            return true;
        }

        /// <summary>
        /// Make a resource based on a integer
        /// </summary>
        /// <param name="resource">Int resource</param>
        /// <returns>Returns a managed int</returns>
        public static int MakeIntResource(int resource) {
            return (ushort) resource;
        }

        /// <summary>
        /// Make a resource based on a integer
        /// </summary>
        /// <param name="resource">Int resource</param>
        /// <param name="sharpMode">Return a #int or a processed int</param>
        /// <returns></returns>
        public static string MakeIntResource(int resource, bool sharpMode) {
            if (sharpMode) {
                return "#" + resource;
            } else {
                return MakeIntResource(resource).ToString();
            }
        }

        /// <summary>
        /// Returns a native exception for a last win32 error
        /// </summary>
        /// <returns></returns>
        public static Win32Exception NativeException() {
            return new Win32Exception(Marshal.GetLastWin32Error());
        }

        #endregion

    }
}
