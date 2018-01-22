using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    /// <summary>
    /// TaskDialog is a kind of Windows dialog introduced with Windows Vista.
    /// </summary>
    [ToolboxBitmap("TaskDialog.bmp")]
    [ToolboxItem(true)]
    [DefaultEvent("Created")]
    [DefaultProperty("CustomButtons")]
    [Description("Shows a customizable dialog for Windows Vista")]
    public sealed class TaskDialog : Component {

        private NativeMethods.TASKDIALOGCONFIG NativeConfig;
        private Collection<TaskDialogButton> _CustomButtons;
        private Collection<TaskDialogRadioButton> _RadioButtons;
        private TaskDialogIcon _StandardIcon;
        private Icon _Icon;
        private TaskDialogIcon _StandardFooterIcon;
        private Icon _FooterIcon;
        private Icon _WindowIcon;
        private IntPtr _Handle;
        private TaskDialogButton _DefaultButton;
        private TaskDialogRadioButton _DefaultRadioButton;
        private DialogResult _DefaultCommonButton;
        private int _Minimum;
        private int _Maximum;
        private int _Value;
        private int _MarqueeSpeed;
        private bool _Marquee;
        private ProgressState _State;
        private bool _CheckboxChecked;
        private bool _CloseEnabled;
        private bool _AllowDialogCancelation;

        /// <summary>
        /// Initializes a new instance of TaskDialog
        /// </summary>
        public TaskDialog() : base() {
            NativeConfig = new NativeMethods.TASKDIALOGCONFIG();
            NativeConfig.cbSize = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOGCONFIG));
            NativeConfig.hInstance = Process.GetCurrentProcess().Handle;
            NativeConfig.pfCallback = new TaskDialogCallbackProc(DialogProc);
            _CustomButtons = new Collection<TaskDialogButton>();
            _RadioButtons = new Collection<TaskDialogRadioButton>();
            _DefaultCommonButton = DialogResult.None;
            _CloseEnabled = true;
            _Maximum = 100;
            _MarqueeSpeed = 100;
        }

        private int DialogProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, IntPtr referenceData) {
            switch (message) {
                case NativeMethods.TDN_CREATED:
                    _Handle = hWnd;
                    NativePostSetup(hWnd);
                    Created?.Invoke(this, EventArgs.Empty);
                    break;
                case NativeMethods.TDN_BUTTON_CLICKED:
                    if (ButtonClick != null) {
                        ButtonClickEventArgs e;
                        if (Enum.IsDefined(typeof(DialogResult), wParam.ToInt32())) {
                            e = new ButtonClickEventArgs((DialogResult) wParam.ToInt32(), false);
                        } else {
                            e = new ButtonClickEventArgs(FindButtonByID(wParam.ToInt32()), false);
                        }
                        ButtonClick(this, e);
                        if (e.Cancel) return NativeMethods.S_FALSE;
                    }
                    break;
                case NativeMethods.TDN_RADIO_BUTTON_CLICKED:
                    RadioButtonClick?.Invoke(this, new RadioButtonClickEventArgs(FindRadioButtonByID(wParam.ToInt32())));
                    break;
                case NativeMethods.TDN_VERIFICATION_CLICKED:
                    CheckboxCheck?.Invoke(this, new CheckboxCheckedEventArgs(NativeMethods.NativeToBool(wParam.ToInt32())));
                    break;
                case NativeMethods.TDN_HYPERLINK_CLICKED:
                    string Link = Marshal.PtrToStringAuto(lParam);
                    HyperlinkClick?.Invoke(this, new LinkClickedEventArgs(Link));
                    break;
                case NativeMethods.TDN_TIMER:
                    if (TimerTick != null) {
                        TimerTickEventArgs e = new TimerTickEventArgs(wParam.ToInt32());
                        TimerTick(this, e);
                        if (e.ResetPending) {
                            e.ResetPending = false;
                            return NativeMethods.S_FALSE;
                        }
                    }
                    break;
                case NativeMethods.TDN_EXPANDO_BUTTON_CLICKED:
                    ExpandCollapse?.Invoke(this, new ExpandCollapseEventArgs(NativeMethods.NativeToBool(wParam.ToInt32())));
                    break;
                case NativeMethods.TDN_HELP:
                    HelpRequested?.Invoke(this, EventArgs.Empty);
                    break;
                case NativeMethods.TDN_NAVIGATED:
                    Navigated?.Invoke(this, EventArgs.Empty);
                    break;
                case NativeMethods.TDN_DESTROYED:
                    _Handle = IntPtr.Zero;
                    Destroyed?.Invoke(this, EventArgs.Empty);
                    break;
            }
            return NativeMethods.S_OK;
        }

        #region Methods for show dialog

        private void NativePostSetup(IntPtr hWnd) {
            if (_WindowIcon != null) {
                NativeMethods.SendMessage(hWnd, NativeMethods.WM_SETICON, NativeMethods.ICON_BIG, _WindowIcon.Handle);
                NativeMethods.SendMessage(hWnd, NativeMethods.WM_SETICON, NativeMethods.ICON_SMALL, _WindowIcon.Handle);
            }
            foreach (TaskDialogButton Btn in _CustomButtons) {
                NativeMethods.SendMessage(hWnd, NativeMethods.TDM_ENABLE_BUTTON, Btn.ID, NativeMethods.BoolToNative(Btn.Enabled));
                NativeMethods.SendMessage(hWnd, NativeMethods.TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE, Btn.ID, NativeMethods.BoolToNative(Btn.Shield));
            }
            foreach (TaskDialogRadioButton Btn in _RadioButtons) {
                NativeMethods.SendMessage(hWnd, NativeMethods.TDM_ENABLE_RADIO_BUTTON, Btn.ID, NativeMethods.BoolToNative(Btn.Enabled));
            }
            if (UseProgressBar) {
                if (_Marquee) {
                    NativeMethods.SendMessage(hWnd, NativeMethods.TDM_SET_PROGRESS_BAR_MARQUEE, NativeMethods.BoolToNative(_Marquee), _MarqueeSpeed);
                } else {
                    NativeMethods.SendMessage(hWnd, NativeMethods.TDM_SET_PROGRESS_BAR_RANGE, 0, new IntPtr(NativeMethods.MakeLParam(_Minimum, _Maximum)));
                    long word = NativeMethods.MakeLParam(_Minimum, _Maximum);
                    NativeMethods.SendMessage(hWnd, NativeMethods.TDM_SET_PROGRESS_BAR_POS, _Value, 0);
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                    switch (_State) {
                        case ProgressState.Normal:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                            break;
                        case ProgressState.Error:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_ERROR, 0);
                            break;
                        case ProgressState.Paused:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_PAUSED, 0);
                            break;
                        default:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                            break;
                    }
                }
            }
            if (_AllowDialogCancelation) {
                NativeMethods.EnableMenuItem(NativeMethods.GetSystemMenu(_Handle, false), NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | (_CloseEnabled ? NativeMethods.MF_ENABLED : (NativeMethods.MF_DISABLED | NativeMethods.MF_GRAYED)));
            }
        }

        /// <summary>
        /// Shows the TaskDialog according to the properties.
        /// </summary>
        /// <returns>The TaskDialog result</returns>
        public TaskDialogResult ShowDialog() {
            if (!IsSupported) {
                throw new UnsupportedWindowsException("Windows Vista");
            }
            NativeConfig.dwFlags = 0;
            NativeConfig.hwndParent = NativeMethods.GetActiveWindow();
            if (_CustomButtons.Count > 0) {
                NativeMethods.TASKDIALOG_BUTTON[] NativeButtons = new NativeMethods.TASKDIALOG_BUTTON[_CustomButtons.Count];
                for (int i = 0; i < _CustomButtons.Count; i++) {
                    int ID = i + 100;
                    _CustomButtons[i].ID = ID;
                    _CustomButtons[i].SetParent(this);
                    NativeButtons[i] = _CustomButtons[i].NativeButton;
                }
                IntPtr NativeButtonsPtr = NativeMethods.StructArrayToPtr(NativeButtons, false);
                NativeConfig.pButtons = NativeButtonsPtr;
                NativeConfig.cButtons = _CustomButtons.Count;
            }
            if (_RadioButtons.Count > 0) {
                NativeMethods.TASKDIALOG_BUTTON[] NativeButtons = new NativeMethods.TASKDIALOG_BUTTON[_RadioButtons.Count];
                for (int i = 0; i < _RadioButtons.Count; i++) {
                    int ID = i + 1000;
                    _RadioButtons[i].ID = ID;
                    _RadioButtons[i].SetParent(this);
                    NativeButtons[i] = _RadioButtons[i].NativeButton;
                }
                IntPtr NativeButtonsPtr = NativeMethods.StructArrayToPtr(NativeButtons, false);
                NativeConfig.pRadioButtons = NativeButtonsPtr;
                NativeConfig.cRadioButtons = _RadioButtons.Count;
            }
            if (_DefaultButton != null) {
                NativeConfig.nDefaultButton = _DefaultButton.ID;
            } else if (_DefaultCommonButton != DialogResult.None) {
                NativeConfig.nDefaultButton = (int) _DefaultCommonButton;
            }
            if (_DefaultRadioButton != null) {
                NativeConfig.nDefaultRadioButton = _DefaultRadioButton.ID;
            } else {
                NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_NO_DEFAULT_RADIO_BUTTON;
            }
            if (_Icon != null) {
                NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_USE_HICON_MAIN;
                NativeMethods.IconUnion Union = new NativeMethods.IconUnion();
                Union.hIcon = _Icon.Handle;
                NativeConfig.mainIcon = Union;
            } else if (_StandardIcon != TaskDialogIcon.None) {
                NativeMethods.IconUnion Union = new NativeMethods.IconUnion();
                Union.pszIcon = (int) _StandardIcon;
                NativeConfig.mainIcon = Union;
            }
            if (_FooterIcon != null) {
                NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_USE_HICON_FOOTER;
                NativeMethods.IconUnion Union = new NativeMethods.IconUnion();
                Union.hIcon = _FooterIcon.Handle;
                NativeConfig.footerIcon = Union;
            } else if (_StandardFooterIcon != TaskDialogIcon.None) {
                NativeMethods.IconUnion Union = new NativeMethods.IconUnion();
                Union.pszIcon = (int)_StandardFooterIcon;
                NativeConfig.footerIcon = Union;
            }
            if (EnableHyperlinks) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_ENABLE_HYPERLINKS;
            if (UseCommandLinks) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_USE_COMMAND_LINKS;
            if (AllowDialogCancelation) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_ALLOW_DIALOG_CANCELLATION;
            if (ExpandedInFooterArea) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA;
            if (ExpandedByDefault) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_EXPANDED_BY_DEFAULT;
            if (CheckboxChecked) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_VERIFICATION_FLAG_CHECKED;
            if (UseProgressBar) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_SHOW_PROGRESS_BAR;
            if (UseProgressBar && _Marquee) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_SHOW_MARQUEE_PROGRESS_BAR;
            if (TimerEnabled) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_CALLBACK_TIMER;
            if (PositionRelativeToWindow) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW;
            if (RightToLeft) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_RTL_LAYOUT;
            if (Minimizable) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_CAN_BE_MINIMIZED;
            if (Width == 0) NativeConfig.dwFlags |= NativeMethods.TaskDialogFlags.TDF_SIZE_TO_CONTENT;
            int buttonID;
            int radioButtonID;
            bool check;
            int result = NativeMethods.TaskDialogIndirect(NativeConfig, out buttonID, out radioButtonID, out check);
            if (!NativeMethods.Succeeded(result)) {
                Marshal.ThrowExceptionForHR(result);
            }
            TaskDialogResult Result;
            if (Enum.IsDefined(typeof(DialogResult), buttonID)) {
                Result = new TaskDialogResult((DialogResult)buttonID);
            } else {
                TaskDialogButton Button = FindButtonByID(buttonID);
                if (Button != null) {
                    Result = new TaskDialogResult(Button);
                } else {
                    Result = new TaskDialogResult(DialogResult.None);
                }
            }
            TaskDialogRadioButton Radio = FindRadioButtonByID(radioButtonID);
            if (Radio != null) {
                Result.SetRadioButton(Radio);
            }
            Result.SetCheckboxChecked(check);
            return Result;
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <returns>The TaskDialog result</returns>
        public static TaskDialogCommonButton Show(string MainInstruction) {
            return ShowCore(null, null, MainInstruction, null, TaskDialogCommonButton.None, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="Owner">The window owner, this is optional</param>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(IWin32Window Owner, string MainInstruction) {
            return ShowCore(Owner, null, MainInstruction, null, TaskDialogCommonButton.None, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(string MainInstruction, string WindowTitle) {
            return ShowCore(null, WindowTitle, MainInstruction, null, TaskDialogCommonButton.None, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="Owner">The window owner, this is optional</param>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(IWin32Window Owner, string MainInstruction, string WindowTitle) {
            return ShowCore(Owner, WindowTitle, MainInstruction, null, TaskDialogCommonButton.None, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons) {
            return ShowCore(null, WindowTitle, MainInstruction, null, Buttons, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="Owner">The window owner, this is optional</param>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(IWin32Window Owner, string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons) {
            return ShowCore(Owner, WindowTitle, MainInstruction, null, Buttons, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <param name="Content">The dialog's text, also known as content</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons, string Content) {
            return ShowCore(null, WindowTitle, MainInstruction, Content, Buttons, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="Owner">The window owner, this is optional</param>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <param name="Content">The dialog's text, also known as content</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(IWin32Window Owner, string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons, string Content) {
            return ShowCore(Owner, WindowTitle, MainInstruction, Content, Buttons, TaskDialogIcon.None);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <param name="Content">The dialog's text, also known as content</param>
        /// <param name="Icon">The dialog's icon</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons, string Content, TaskDialogIcon Icon) {
            return ShowCore(null, WindowTitle, MainInstruction, Content, Buttons, Icon);
        }

        /// <summary>
        /// Shows the basic version of TaskDialog, for the advanced version, use TaskDialog as a object
        /// </summary>
        /// <param name="Owner">The window owner, this is optional</param>
        /// <param name="MainInstruction">The dialog's message</param>
        /// <param name="WindowTitle">The dialog's window title</param>
        /// <param name="Buttons">The dialog's buttons to show</param>
        /// <param name="Content">The dialog's text, also known as content</param>
        /// <param name="Icon">The dialog's icon</param>
        /// <returns></returns>
        public static TaskDialogCommonButton Show(IWin32Window Owner, string MainInstruction, string WindowTitle, TaskDialogCommonButton Buttons, string Content, TaskDialogIcon Icon) {
            return ShowCore(Owner, WindowTitle, MainInstruction, Content, Buttons, Icon);
        }

        private static TaskDialogCommonButton ShowCore(IWin32Window Owner, string WindowTitle, string MainInstruction, string Content, TaskDialogCommonButton Buttons, TaskDialogIcon Icon) {
            if (!IsSupported) {
                throw new UnsupportedWindowsException("Windows Vista");
            }
            IntPtr Handle = IntPtr.Zero;
            if (Owner == null) {
                Handle = NativeMethods.GetActiveWindow();
            } else {
                Handle = GetSafeHandle(Owner);
            }
            int buttonID;
            int result = NativeMethods.TaskDialog(Handle, Process.GetCurrentProcess().Handle, WindowTitle, MainInstruction, Content, Buttons, (int) Icon, out buttonID);
            return (TaskDialogCommonButton) buttonID;
        }

        #endregion

        /// <summary>
        /// Click a common button programmatically
        /// </summary>
        /// <param name="Button"></param>
        public void PerformClick(TaskDialogCommonButton Button) {
            if (_Handle != null) {
                NativeMethods.SendMessage(_Handle, NativeMethods.TDM_CLICK_BUTTON, (int) Button, 0);
            }
        }

        /// <summary>
        /// Recreates a task dialog with new contents, simulating the functionality of a multi-page wizard
        /// </summary>
        /// <remarks>
        /// It only works if dialog is shown
        /// </remarks>
        /// <param name="Dialog"></param>
        public void NavigateToPage(TaskDialog Dialog) {
            if (IsShown) {
                IntPtr TaskDialogPtr = IntPtr.Zero;
                Marshal.StructureToPtr(Dialog.NativeConfig, TaskDialogPtr, false);
                NativeMethods.SendMessage(_Handle, NativeMethods.TDM_NAVIGATE_PAGE, 0, TaskDialogPtr);
            }
        }

        private static IntPtr GetSafeHandle(IWin32Window window) {
            IntPtr hWnd = IntPtr.Zero;
            Control control = window as Control;
            if (control != null) {
                hWnd = control.Handle;
                return hWnd;
            } else {
                hWnd = window.Handle;
                if (hWnd == IntPtr.Zero || NativeMethods.IsWindow(hWnd)) {
                    return hWnd;
                } else {
                    throw new Win32Exception(NativeMethods.ERROR_INVALID_HANDLE);
                }
            }
        }

        private TaskDialogButton FindButtonByID(int ID) {
            foreach (TaskDialogButton Btn in _CustomButtons) {
                if (Btn.ID == ID) {
                    return Btn;
                }
            }
            return null;
        }

        private TaskDialogRadioButton FindRadioButtonByID(int ID) {
            foreach (TaskDialogRadioButton Btn in _RadioButtons) {
                if (Btn.ID == ID) {
                    return Btn;
                }
            }
            return null;
        }

        #region Public properties

        /// <summary>
        /// The window title
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The window title")]
        [Localizable(true)]
        public string WindowTitle {
            get {
                return NativeConfig.pszWindowTitle;
            }
            set {
                NativeConfig.pszWindowTitle = value;
                if (IsShown) {
                    NativeMethods.SetWindowText(_Handle, value);
                }
            }
        }

        /// <summary>
        /// The dialog's header text, also known as Main Instruction
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The dialog's header text, also known as Main Instruction")]
        [Localizable(true)]
        public string MainInstruction {
            get {
                return NativeConfig.pszMainInstruction;
            }
            set {
                NativeConfig.pszMainInstruction = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ELEMENT_TEXT, NativeMethods.TDE_MAIN_INSTRUCTION, value);
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's content
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the dialog's content")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        public string Content {
            get {
                return NativeConfig.pszContent;
            }
            set {
                NativeConfig.pszContent = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ELEMENT_TEXT, NativeMethods.TDE_CONTENT, value);
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's footer text
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the dialog's footer text")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        public string FooterText {
            get {
                return NativeConfig.pszFooter;
            }
            set {
                NativeConfig.pszFooter = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ELEMENT_TEXT, NativeMethods.TDE_FOOTER, value);
                }
            }
        }

        /// <summary>
        /// Returns/sets the additional information to show if Show Details is clicked
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the additional information to show if Show Details is clicked")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        public string ExpandedInformation {
            get {
                return NativeConfig.pszExpandedInformation;
            }
            set {
                NativeConfig.pszExpandedInformation = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ELEMENT_TEXT, NativeMethods.TDE_EXPANDED_INFORMATION, value);
                }
            }
        }

        /// <summary>
        /// Returns/sets a text that replaces the classic Show Details
        /// </summary>
        /// <remarks>
        /// If null or empty, the classic Show Details text will be used
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets a text that replaces the classic Show Details. If null or empty, the classic Show Details text will be used")]
        [Localizable(true)]
        public string ShowDetailsText {
            get {
                return NativeConfig.pszCollapsedControlText;
            }
            set {
                NativeConfig.pszCollapsedControlText = value;
            }
        }

        /// <summary>
        /// Returns/sets a text that replaces the classic Hide Details
        /// </summary>
        /// <remarks>
        /// If null or empty, the classic Hide Details text will be used
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets a text that replaces the classic Hide Details. If null or empty, the classic Hide Details text will be used")]
        [Localizable(true)]
        public string HideDetailsText {
            get {
                return NativeConfig.pszExpandedControlText;
            }
            set {
                NativeConfig.pszExpandedControlText = value;
            }
        }

        /// <summary>
        /// Returns/sets the dialog's common buttons like OK, Cancel, Retry, Yes, No and Close.
        /// </summary>
        /// <remarks>
        /// These buttons are not affected by UseCommandLinks property.
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(TaskDialogCommonButton.None)]
        [Description("Returns/sets the dialog's common buttons like OK, Cancel, Retry, Yes, No and Close. These buttons are not affected by UseCommandLinks property")]
        [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
        public TaskDialogCommonButton CommonButtons {
            get {
                return NativeConfig.dwCommonButtons;
            }
            set {
                if (NativeConfig.dwCommonButtons != value) {
                    NativeConfig.dwCommonButtons = value;
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's icon using standard icons with/without header
        /// </summary>
        /// <remarks>
        /// This property will be unset if Icon is set
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(TaskDialogIcon.None)]
        [Description("Returns/sets the dialog's icon using standard icons with/without header. This property will be unset if Icon is set")]
        public TaskDialogIcon StandardIcon {
            get {
                return _StandardIcon;
            }
            set {
                if (IsShown && Icon != null) return;
                _StandardIcon = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ICON, NativeMethods.TDIE_ICON_MAIN, NativeMethods.MakeIntResource((int)value));
                }
                if (value != TaskDialogIcon.None && Icon != null) {
                    Icon = null;
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's icon using a custom icon
        /// </summary>
        /// <remarks>
        /// This property will be unset if StandardIcon is set
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the dialog's icon using a custom icon. This property will be unset if StandardIcon is set")]
        public Icon Icon {
            get {
                return _Icon;
            }
            set {
                if (IsShown && StandardIcon != TaskDialogIcon.None) return;
                _Icon = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ICON, NativeMethods.TDIE_ICON_MAIN, value.Handle);
                }
                if (value != null && StandardIcon != TaskDialogIcon.None) {
                    StandardIcon = TaskDialogIcon.None;
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's footer icon using standard icons with/without header
        /// </summary>
        /// <remarks>
        /// This property will be unset if FooterIcon is set
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(TaskDialogIcon.None)]
        [Description("Returns/sets the dialog's footer icon using standard icons with/without header. This property will be unset if FooterIcon is set")]
        public TaskDialogIcon StandardFooterIcon {
            get {
                return _StandardFooterIcon;
            }
            set {
                if (IsShown && FooterIcon != null) return;
                _StandardFooterIcon = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ICON, NativeMethods.TDIE_ICON_MAIN, NativeMethods.MakeIntResource((int)value));
                }
                if (value != TaskDialogIcon.None && FooterIcon != null) {
                    FooterIcon = null;
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's footer icon using a custom icon
        /// </summary>
        /// <remarks>
        /// This property will be unset if StandardFooterIcon is set
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the dialog's footer icon using a custom icon. This property will be unset if StandardFooterIcon is set")]
        public Icon FooterIcon {
            get {
                return _FooterIcon;
            }
            set {
                if (IsShown && StandardFooterIcon != TaskDialogIcon.None) return;
                _FooterIcon = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_UPDATE_ICON, NativeMethods.TDIE_ICON_FOOTER, value.Handle);
                }
                if (value != null && StandardFooterIcon != TaskDialogIcon.None) {
                    StandardFooterIcon = TaskDialogIcon.None;
                }
            }
        }

        /// <summary>
        /// Returns/sets the icon next to window text
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the icon next to window text")]
        public Icon WindowIcon {
            get {
                return _WindowIcon;
            }
            set {
                _WindowIcon = value;
                if (IsShown) {
                    if (value != null) {
                        NativeMethods.SendMessage(_Handle, NativeMethods.WM_SETICON, NativeMethods.ICON_BIG, value.Handle);
                        NativeMethods.SendMessage(_Handle, NativeMethods.WM_SETICON, NativeMethods.ICON_SMALL, value.Handle);
                    } else {
                        NativeMethods.SendMessage(_Handle, NativeMethods.WM_SETICON, NativeMethods.ICON_BIG, IntPtr.Zero);
                        NativeMethods.SendMessage(_Handle, NativeMethods.WM_SETICON, NativeMethods.ICON_SMALL, IntPtr.Zero);
                    }
                }
            }
        }

        /// <summary>
        /// The TaskDialog's custom buttons
        /// </summary>
        /// <remarks>
        /// Unlike common buttons, custom buttons are able to be CommandLinks
        /// </remarks>
        [Category("Behavior")]
        [Description("The TaskDialog's custom buttons")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        public Collection<TaskDialogButton> CustomButtons {
            get {
                return _CustomButtons;
            }
        }

        /// <summary>
        /// The TaskDialog's radio buttons
        /// </summary>
        [Category("Behavior")]
        [Description("The TaskDialog's radio buttons")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        public Collection<TaskDialogRadioButton> RadioButtons {
            get {
                return _RadioButtons;
            }
        }

        /// <summary>
        /// Returns/sets the default button to be focused
        /// </summary>
        /// <remarks>
        /// This property will be unset if DefaultCommonButton is set
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("Returns/sets the default button to be focused. This property will be unset if DefaultCommonButton is set")]
        public TaskDialogButton DefaultButton {
            get {
                return _DefaultButton;
            }
            set {
                bool found = false;
                foreach (TaskDialogButton Btn in _CustomButtons) {
                    if (Btn == value) {
                        found = true;
                        _DefaultButton = value;
                        break;
                    }
                }
                if (!found) {
                    if (value != null) {
                        throw new TaskDialogButtonException();
                    } else {
                        _DefaultButton = value;
                    }
                }
                if (value != null && DefaultCommonButton != DialogResult.None) {
                    DefaultCommonButton = DialogResult.None;
                }
            }
        }

        /// <summary>
        /// Returns/sets the default button to be focused
        /// </summary>
        /// <remarks>
        /// This property will be unset if DefaultButton is set
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(DialogResult.None)]
        [Description("Returns/sets the default button to be focused. This property will be unset if DefaultButton is set")]
        public DialogResult DefaultCommonButton {
            get {
                return _DefaultCommonButton;
            }
            set {
                _DefaultCommonButton = value;
                if (value != DialogResult.None && DefaultButton != null) {
                    DefaultButton = null;
                }
            }
        }

        /// <summary>
        /// Returns/sets the radio button that is selected by default
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("Returns/sets the radio button that is selected by default")]
        public TaskDialogRadioButton SelectedRadioButton {
            get {
                return _DefaultRadioButton;
            }
            set {
                bool found = false;
                foreach (TaskDialogRadioButton Btn in _RadioButtons) {
                    if (Btn == value && !(Btn is TaskDialogButton)) {
                        found = true;
                        _DefaultRadioButton = value;
                        break;
                    }
                }
                if (!found) {
                    throw new TaskDialogButtonException();
                }
            }
        }

        /// <summary>
        /// Returns/sets the dialog's checkbox text
        /// </summary>
        /// <remarks>
        /// If null or empty, no checkbox will be shown
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Returns/sets the dialog's checkbox text. If null or empty, no checkbox will be shown")]
        [Localizable(true)]
        public string CheckboxText {
            get {
                return NativeConfig.pszVerificationText;
            }
            set {
                NativeConfig.pszVerificationText = value;
            }
        }

        /// <summary>
        /// Returns/sets the dialog's width
        /// </summary>
        /// <remarks>
        /// If value is 0, the dialog width is auto
        /// </remarks>
        [Category("WindowStyle")]
        [DefaultValue(0)]
        [Description("Returns/sets the dialog's width, if value is 0, the dialog width is auto")]
        public int Width {
            get {
                return NativeConfig.cxWidth;
            }
            set {
                NativeConfig.cxWidth = value;
            }
        }

        /// <summary>
        /// Set the minimum value of progress bar
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(0)]
        [Description("Set the minimum value of progress bar")]
        public int ProgressMinimum {
            get {
                return _Minimum;
            }
            set {
                _Minimum = value;
                if (IsShown) {
                    long range = NativeMethods.MakeLParam(value, ProgressMaximum);
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_RANGE, 0, new IntPtr(range));
                }
            }
        }

        /// <summary>
        /// Set the maximum value of progress bar
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(100)]
        [Description("Set the maximum value of progress bar")]
        public int ProgressMaximum {
            get {
                return _Maximum;
            }
            set {
                _Maximum = value;
                if (IsShown) {
                    long range = NativeMethods.MakeLParam(ProgressMinimum, value);
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_RANGE, 0, new IntPtr(range));
                }
            }
        }

        /// <summary>
        /// Set the progress current value
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(0)]
        [Description("Set the progress current value")]
        public int ProgressValue {
            get {
                return _Value;
            }
            set {
                if (value < ProgressMinimum || value > ProgressMaximum) {
                    throw new ArgumentOutOfRangeException("ProgressValue", "ProgressValue should be less or equals to ProgressMaximum and greater or equals to ProgressMinimum");
                }
                _Value = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_POS, value, 0);
                }
            }
        }

        /// <summary>
        /// Set the marquee animation speed
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(100)]
        [Description("Set the marquee animation speed")]
        public int ProgressMarqueeSpeed {
            get {
                return _MarqueeSpeed;
            }
            set {
                _MarqueeSpeed = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_MARQUEE, NativeMethods.BoolToNative(ProgressMarquee), value);
                }
            }
        }

        /// <summary>
        /// Set if progress bar has the marquee animation
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(false)]
        [Description("Set if progress bar has the marquee animation")]
        public bool ProgressMarquee {
            get {
                return _Marquee;
            }
            set {
                _Marquee = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_MARQUEE_PROGRESS_BAR, NativeMethods.BoolToNative(value), 0);
                }
            }
        }

        [Category("ProgressBar")]
        [DefaultValue(ProgressState.Normal)]
        [Description("Set the progress state")]
        public ProgressState ProgressState {
            get {
                return _State;
            }
            set {
                _State = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                    switch (value) {
                        case ProgressState.Normal:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                            break;
                        case ProgressState.Error:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_ERROR, 0);
                            break;
                        case ProgressState.Paused:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_PAUSED, 0);
                            break;
                        default:
                            NativeMethods.SendMessage(_Handle, NativeMethods.TDM_SET_PROGRESS_BAR_STATE, NativeMethods.PBST_NORMAL, 0);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns/sets if progress bar will be used in the dialog
        /// </summary>
        [Category("ProgressBar")]
        [DefaultValue(false)]
        [Description("Returns/sets if progress bar will be used in the dialog")]
        public bool UseProgressBar { get; set; }

        /// <summary>
        /// Returns/sets if &lt;a&gt; tags will be converted into hyperlinks
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Returns/sets if <a> tags will be converted into hyperlinks")]
        public bool EnableHyperlinks { get; set; }

        /// <summary>
        /// Returns/sets if custom buttons will be CommandLinks
        /// </summary>
        /// <remarks>
        /// Common buttons are not altered by this property
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Returns/sets if custom buttons will be CommandLinks. Common buttons are not altered by this property")]
        public bool UseCommandLinks { get; set; }

        /// <summary>
        /// Returns/sets if dialog can be cancelled by close button or Alt+F4
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Returns/sets if dialog can be cancelled by close button or Alt+F4")]
        public bool AllowDialogCancelation {
            get {
                return _AllowDialogCancelation;
            }
            set {
                _AllowDialogCancelation = value;
                if (!value && !CloseEnabled) CloseEnabled = true;
            }
        }

        /// <summary>
        /// Returns/sets if expanded information will be displayed at the bottom of footer instead of immediately after the content
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Returns/sets if expanded information will be displayed at the bottom of footer instead of immediately after the content")]
        public bool ExpandedInFooterArea { get; set; }

        /// <summary>
        /// eturns/sets if expanded information is expanded by default
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Returns/sets if expanded information is expanded by default")]
        public bool ExpandedByDefault { get; set; }

        /// <summary>
        /// Returns/sets if checkbox is checked by default
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Returns/sets if checkbox is checked by default")]
        public bool CheckboxChecked {
            get {
                return _CheckboxChecked;
            }
            set {
                _CheckboxChecked = value;
                if (IsShown) {
                    NativeMethods.SendMessage(_Handle, NativeMethods.TDM_CLICK_VERIFICATION, NativeMethods.BoolToNative(value), 0);
                }
            }
        }

        /// <summary>
        /// Returns/sets if TimerTick event is fired approximately every 200 milliseconds
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Returns/sets if TimerTick event is fired approximately every 200 milliseconds")]
        public bool TimerEnabled { get; set; }

        /// <summary>
        /// Returns/sets if dialog is positioned (centered) relative to parent window instead of relative to monitor
        /// </summary>
        [Category("Design")]
        [DefaultValue(false)]
        [Description("Returns/sets if dialog is positioned (centered) relative to parent window instead of relative to monitor")]
        public bool PositionRelativeToWindow { get; set; }

        /// <summary>
        /// Returns/sets if dialog's layout is Right-To-Left instead of Left-To-Right
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Returns/sets if dialog's layout is Right-To-Left instead of Left-To-Right")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// Returns/sets if dialog can be minimized
        /// </summary>
        [Category("WindowStyle")]
        [DefaultValue(false)]
        [Description("Returns/sets if dialog can be minimized")]
        public bool Minimizable { get; set; }

        /// <summary>
        /// Returns/sets if dialog's clsoe button is enabled
        /// </summary>
        /// <remarks>
        /// This property is ignored if AllowDialogCancelation is false
        /// </remarks>
        [Category("WindowStyle")]
        [DefaultValue(true)]
        [Description("Returns/sets if dialog's clsoe button is enabled")]
        public bool CloseEnabled {
            get {
                return _CloseEnabled;
            }
            set {
                _CloseEnabled = value;
                if (!value && !AllowDialogCancelation) AllowDialogCancelation = true;
                if (IsShown && AllowDialogCancelation) {
                    NativeMethods.EnableMenuItem(NativeMethods.GetSystemMenu(_Handle, false), NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | (value ? NativeMethods.MF_ENABLED : (NativeMethods.MF_DISABLED | NativeMethods.MF_GRAYED)));
                }
            }
        }

        /// <summary>
        /// Get the TaskDialog native handle
        /// </summary>
        /// <remarks>
        /// The handle is not null when dialog is shown
        /// </remarks>
        [Browsable(false)]
        public IntPtr Handle {
            get {
                return _Handle;
            }
        }

        /// <summary>
        /// Get if task dialog is shown
        /// </summary>
        [Browsable(false)]
        public bool IsShown {
            get {
                return _Handle != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Check if TaskDialog is supported (Windows Vista or later)
        /// </summary>
        [Browsable(false)]
        public static bool IsSupported {
            get {
                return Environment.OSVersion.Version >= new Version(6, 0);
            }
        }

        #endregion

        #region Public events

        /// <summary>
        /// Fires when dialog is created before it is displayed
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when dialog is created before it is displayed")]
        public event EventHandler Created;

        /// <summary>
        /// Fires when a dialog button is clicked
        /// </summary>
        [Category("Action")]
        [Description("Fires when a dialog button is clicked")]
        public event ButtonClickEventHandler ButtonClick;

        /// <summary>
        /// Fires when a dialog radio button is clicked
        /// </summary>
        [Category("Action")]
        [Description("Fires when a dialog radio button is clicked")]
        public event RadioButtonClickEventHandler RadioButtonClick;


        /// <summary>
        /// Fires when checkbox is checked/unchecked
        /// </summary>
        [Description("Fires when checkbox is checked/unchecked")]
        public event CheckboxCheckedEventHandler CheckboxCheck;

        [Category("Action")]
        [Description("Fires when a hyperlink is clicked")]
        public event LinkClickedEventHandler HyperlinkClick;

        /// <summary>
        /// Fires approximately every 200 milliseconds when TimerEnabled is true
        /// </summary>
        [Category("Behavior")]
        [Description("Fires approximately every 200 milliseconds when TimerEnabled is true")]
        public event TimerTickEventHandler TimerTick;

        /// <summary>
        /// Fires when expanded information is expanded or collapsed
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when expanded information is expanded or collapsed")]
        public event ExpandCollapseEventHandler ExpandCollapse;

        /// <summary>
        /// Fires when user presses F1 when dialog is focused
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when user presses F1 when dialog is focused")]
        public event EventHandler HelpRequested;

        [Description("Fires when a navigation has occurred")]
        public event EventHandler Navigated;

        /// <summary>
        /// Fires when dialog is destroyed
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when dialog is destroyed")]
        public event EventHandler Destroyed;

        #endregion

        protected override void Dispose(bool disposing) {
            if (disposing) {
                TaskDialogButton[] CButtons = new TaskDialogButton[_CustomButtons.Count];
                TaskDialogRadioButton[] RButtons = new TaskDialogRadioButton[_RadioButtons.Count];
                _CustomButtons.CopyTo(CButtons, 0);
                _RadioButtons.CopyTo(RButtons, 0);
                _CustomButtons.Clear();
                _CustomButtons = null;
                _RadioButtons.Clear();
                _RadioButtons = null;
                foreach (TaskDialogButton Btn in CButtons) Btn.Dispose();
                foreach (TaskDialogRadioButton Btn in RButtons) Btn.Dispose();
                _DefaultButton = null;
                _DefaultCommonButton = DialogResult.None;
                _DefaultRadioButton = null;
            }
            Marshal.FreeHGlobal(_Handle);
            if (IsShown) {
                PerformClick(TaskDialogCommonButton.Cancel);
            }
            base.Dispose(disposing);
        }

    }
}
