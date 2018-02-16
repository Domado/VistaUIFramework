//--------------------------------------------------------------------
// <copyright file="RebarBand.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class RebarBand : Component {

        internal Rebar parent;
        private Control prevParent;
        private bool _AcceptVertical = true;
        private Color _BackColor = SystemColors.Control;
        private Image _BackgroundImage;
        private bool _BackgroundFixed;
        private bool _Break;
        private Point _ChevronLocation = Point.Empty;
        private Control _Child;
        private int _ChildHeight = -1;
        private bool _FixedSize;
        private Color _ForeColor = SystemColors.ControlText;
        private RebarGripStyle _GripStyle = RebarGripStyle.Auto;
        private int _HeaderWidth = -1;
        private int _ImageIndex = -1;
        private string _ImageKey;
        private int _IdealWidth = -1;
        private int _IntegralWidth = -1;
        private int _MaximumChildHeight = -1;
        private Size _MinimumChildSize = Size.Empty;
        private object _Tag;
        private string _Text;
        private bool _TitleVisible = true;
        private bool _TopAlign;
        private bool _UseBackAndFore;
        private bool _UseChevron;
        private int _Width = -1;
        private bool _VariantHeight;
        private bool _Visible = true;

        public RebarBand() : base() {}

        public RebarBand(string Text) : this() {
            this.Text = Text;
        }

        public RebarBand(Control Child) : this() {
            this.Child = Child;
        }

        internal NativeMethods.REBARBANDINFO BuildBandInfo() {
            NativeMethods.REBARBANDINFO info = new NativeMethods.REBARBANDINFO {
                cbSize = Marshal.SizeOf(typeof(NativeMethods.REBARBANDINFO))
            };
            if (!string.IsNullOrEmpty(Text)) {
                info.fMask |= NativeMethods.RBBIM_TEXT;
                info.lpText = Text;
            }
            if (GripStyle != RebarGripStyle.Auto || !Visible || !AcceptVertical || UseChevron || TopAlign)
                info.fMask |= NativeMethods.RBBIM_STYLE;
            if (!MinimumChildSize.IsEmpty || MaximumChildHeight != -1 || ChildHeight != -1 || IntegralHeight != -1)
                info.fMask |= NativeMethods.RBBIM_CHILDSIZE;
            if (UseBackAndFore) {
                info.fMask |= NativeMethods.RBBIM_COLORS;
                info.clrBack = ColorTranslator.ToWin32(BackColor);
                info.clrFore = ColorTranslator.ToWin32(ForeColor);
            }
            if (BackgroundImage != null) {
                info.fMask |= NativeMethods.RBBIM_BACKGROUND;
                info.hbmBack = new Bitmap(BackgroundImage).GetHbitmap();
            }
            if (!ChevronLocation.IsEmpty) {
                info.fMask |= NativeMethods.RBBIM_CHEVRONLOCATION;
                info.rcChevronLocation = new NativeMethods.RECT(new Rectangle(ChevronLocation, Size.Empty));
            }
            if (Child != null && parent != null) {
                prevParent = Child.Parent;
                Child.Parent = parent;
                info.fMask |= NativeMethods.RBBIM_CHILD;
                info.hwndChild = Child.Handle;
            }
            if (MinimumChildSize.Width != 0) info.cxMinChild = MinimumChildSize.Width;
            if (MinimumChildSize.Height != 0) info.cyMinChild = MinimumChildSize.Height;
            if (ChildHeight != -1) info.cyChild = ChildHeight;
            if (MaximumChildHeight != -1) info.cyMaxChild = MaximumChildHeight;
            if (IntegralHeight != -1) info.cyIntegral = IntegralHeight;
            if (HeaderWidth != -1) {
                info.fMask |= NativeMethods.RBBIM_HEADERSIZE;
                info.cxHeader = HeaderWidth;
            }
            if (IdealWidth != -1) {
                info.fMask |= NativeMethods.RBBIM_IDEALSIZE;
                info.cxIdeal = IdealWidth;
            }
            if (Tag != null) {
                GCHandle handle = GCHandle.Alloc(Tag);
                info.fMask |= NativeMethods.RBBIM_LPARAM;
                info.lParam = (IntPtr) handle;
                handle.Free();
            }
            if (GripStyle == RebarGripStyle.Hidden)
                info.fStyle |= NativeMethods.RBBS_NOGRIPPER;
            else if (GripStyle == RebarGripStyle.Visible)
                info.fStyle |= NativeMethods.RBBS_GRIPPERALWAYS;
            if (!Visible) info.fStyle |= NativeMethods.RBBS_HIDDEN;
            if (!AcceptVertical) info.fStyle |= NativeMethods.RBBS_NOVERT;
            if (!TitleVisible) info.fStyle |= NativeMethods.RBBS_HIDETITLE;
            if (UseChevron) info.fStyle |= NativeMethods.RBBS_USECHEVRON;
            if (TopAlign) info.fStyle |= NativeMethods.RBBS_TOPALIGN;
            if (FixedSize) info.fStyle |= NativeMethods.RBBS_FIXEDSIZE;
            if (Break) info.fStyle |= NativeMethods.RBBS_BREAK;
            if (VariantHeight) info.fStyle |= NativeMethods.RBBS_VARIABLEHEIGHT;
            if (parent.ImageList != null) {
                if (ImageIndex != -1 && parent.ImageList.Images[ImageIndex] != null) {
                    info.fMask |= NativeMethods.RBBIM_IMAGE;
                    info.iImage = ImageIndex;
                } else if (ImageKey != null && parent.ImageList.Images.IndexOfKey(ImageKey) != -1 && parent != null) {
                    info.fMask |= NativeMethods.RBBIM_IMAGE;
                    info.iImage = parent.ImageList.Images.IndexOfKey(ImageKey);
                }
            }
            if (_Width != -1) {
                info.fMask |= NativeMethods.RBBIM_SIZE;
                info.cx = Width;
            }
            return info;
        }

        #region Public properties

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets if band is shown even if rebar is vertical")]
        public bool AcceptVertical {
            get {
                return _AcceptVertical;
            }
            set {
                if (_AcceptVertical != value) {
                    _AcceptVertical = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Control")]
        [Description("The background color of the band")]
        public Color BackColor {
            get {
                return _BackColor;
            }
            set {
                if (_BackColor != value) {
                    _BackColor = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The background image of the band")]
        public Image BackgroundImage {
            get {
                return _BackgroundImage;
            }
            set {
                if (_BackgroundImage != value) {
                    _BackgroundImage = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Gets or sets if background image is fixed when band is resized")]
        public bool BackgroundFixed {
            get {
                return _BackgroundFixed;
            }
            set {
                if (_BackgroundFixed != value) {
                    _BackgroundFixed = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if band is on a new line")]
        public bool Break {
            get {
                return _Break;
            }
            set {
                if (_Break != value) {
                    _Break = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(typeof(Point), "0, 0")]
        [Description("The location of the chevron")]
        public Point ChevronLocation {
            get {
                return _ChevronLocation;
            }
            set {
                if (_ChevronLocation != value) {
                    _ChevronLocation = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("The child control of the band")]
        public Control Child {
            get {
                return _Child;
            }
            set {
                if (_Child != value) {
                    if (parent != null && value == parent) throw new ArgumentException("Bands cannot contain the parent", "Child");
                    if (value is System.Windows.Forms.Form) throw new ArgumentException("Bands cannot contain a form", "Child");
                    if (value is Rebar && DesignMode && MessageBox.Show("You are about to add another Rebar as the band child. You don't need to do that. Do you want to continue anyway?", "Rebar child", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
                    if (value == null && _Child != null) {
                        _Child.Disposed -= ChildDisposed;
                        if (prevParent != null) _Child.Parent = prevParent;
                    }
                    _Child = value;
                    if (_Child != null) _Child.Dock = DockStyle.None;
                    UpdateBand(true);
                    if (_Child != null && MinimumChildSize.IsEmpty && DesignMode) MinimumChildSize = new Size(0, _Child.Height);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(-1)]
        [Description("The initial height of the child control")]
        public int ChildHeight {
            get {
                return _ChildHeight;
            }
            set {
                if (_ChildHeight != value) {
                    if (value < -1 || value > MaximumChildHeight) throw new ArgumentOutOfRangeException("hildHeight", "Height out of bounds");
                    _ChildHeight = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if the band can't be resized")]
        public bool FixedSize {
            get {
                return _FixedSize;
            }
            set {
                if (_FixedSize != value) {
                    _FixedSize = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "ControlText")]
        [Description("The text color of the band")]
        public Color ForeColor {
            get {
                return _ForeColor;
            }
            set {
                if (_ForeColor != value) {
                    _ForeColor = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(RebarGripStyle.Auto)]
        [Description("Gets or sets if the grip is auto. or forced")]
        public RebarGripStyle GripStyle {
            get {
                return _GripStyle;
            }
            set {
                if (_GripStyle != value) {
                    _GripStyle = value;
                    UpdateBand(true);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(-1)]
        [Description("")]
        public int HeaderWidth {
            get {
                return _HeaderWidth;
            }
            set {
                if (_HeaderWidth != value) {
                    _HeaderWidth = value;
                    UpdateBand(false);
                }
            }
        }

        [TypeConverter(typeof(ImageIndexConverter))]
        [Category("Appearance")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
        [DefaultValue(-1)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        [Description("The index of the image that Rebar will show")]
        public int ImageIndex {
            get {
                return _ImageIndex;
            }
            set {
                if (_ImageIndex != value) {
                    if (value < -1) throw new ArgumentOutOfRangeException("ImageIndex", "Index out of bounds");
                    if (ImageKey != null) ImageKey = null;
                    _ImageIndex = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(ImageKeyConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        [Description("The index of the image that Rebar will show")]
        public string ImageKey {
            get {
                return _ImageKey;
            }
            set {
                if (_ImageKey != value) {
                    if (ImageIndex != -1) ImageIndex = -1;
                    _ImageKey = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(-1)]
        [Description("The ideal width of the band. If the band is maximized, rebar will attempt to use this width")]
        public int IdealWidth {
            get {
                return _IdealWidth;
            }
            set {
                if (_IdealWidth != value) {
                    _IdealWidth = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(-1)]
        [Description("The step value by which the band can grow or shrink")]
        public int IntegralHeight {
            get {
                return _IntegralWidth;
            }
            set {
                if (_IntegralWidth != value) {
                    _IntegralWidth = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(-1)]
        [Description("The maximum height of the child control")]
        public int MaximumChildHeight {
            get {
                return _MaximumChildHeight;
            }
            set {
                if (_MaximumChildHeight != value) {
                    if (value < -1) throw new ArgumentOutOfRangeException("MaximumChildHeight", "Height out of bounds");
                    _MaximumChildHeight = value;
                    if (ChildHeight > value) ChildHeight = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(typeof(Size), "0, 0")]
        [Description("The minimum size of the child control")]
        public Size MinimumChildSize {
            get {
                return _MinimumChildSize;
            }
            set {
                if (_MinimumChildSize != value) {
                    _MinimumChildSize = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Data")]
        [DefaultValue(null)]
        [Localizable(false)]
        [Bindable(true)]
        [TypeConverter(typeof(StringConverter))]
        [Description("Gets or sets the object that contains data about the component")]
        public object Tag {
            get {
                return _Tag;
            }
            set {
                if (_Tag != value) {
                    _Tag = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The text of the band")]
        public string Text {
            get {
                return _Text ?? string.Empty;
            }
            set {
                if (string.IsNullOrEmpty(value))
                    value = null;
                if ((value == null && _Text != null) || (value != null && (_Text == null || _Text != value))) {
                    _Text = value;
                    UpdateBand(ContainsMnemonic(_Text));
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets if band title is hidden")]
        public bool TitleVisible {
            get {
                return _TitleVisible;
            }
            set {
                if (_TitleVisible != value) {
                    _TitleVisible = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Gets or sets if BackColor and ForeColor is valid")]
        public bool UseBackAndFore {
            get {
                return _UseBackAndFore;
            }
            set {
                if (_UseBackAndFore != value) {
                    _UseBackAndFore = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if band is kept in top row")]
        public bool TopAlign {
            get {
                return _TopAlign;
            }
            set {
                if (_TopAlign != value) {
                    _TopAlign = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if band uses chevron buttons")]
        public bool UseChevron {
            get {
                return _UseChevron;
            }
            set {
                if (_UseChevron != value) {
                    _UseChevron = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Design")]
        [DefaultValue(-1)]
        [Description("The length of the band, in pixels")]
        public int Width {
            get {
                return _Width;
            }
            set {
                if (_Width != value) {
                    _Width = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if band is resized by the Rebar")]
        public bool VariantHeight {
            get {
                return _VariantHeight;
            }
            set {
                if (_VariantHeight != value) {
                    _VariantHeight = value;
                    UpdateBand(false);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets if the band is visible")]
        public bool Visible {
            get {
                return _Visible;
            }
            set {
                if (_Visible != value) {
                    _Visible = value;
                    UpdateBand(false);
                }
            }
        }

        /// <summary>
        /// The parent control that contains the band
        /// </summary>
        [Browsable(false)]
        public Rebar Parent {
            get {
                return parent;
            }
        }

        #endregion

        #region Internal, private and overriden members

        private int FindBandIndex() {
            for (int i = 0; i < parent.Bands.Count; i++)
                if (parent.Bands[i] == this) return i;
            return -1;
        }

        private void UpdateBand(bool Recreate) {
            if (parent != null) {
                int index = FindBandIndex();
                if (index != -1)
                    parent.CollectionSetBand(index, this, Recreate);
            }
        }

        private bool ContainsMnemonic(string text) {
            if (text != null) {
                int textLength = text.Length;
                int firstAmpersand = text.IndexOf('&', 0);
                if (firstAmpersand >= 0 && firstAmpersand <= textLength - 2) {
                    int secondAmpersand = text.IndexOf('&', firstAmpersand + 1);
                    if (secondAmpersand == -1) return true;
                }
            }
            return false;
        }

        private void ChildDisposed(object sender, EventArgs e) {
            Child = null;
        }

        #endregion

        public enum RebarGripStyle {
            Auto, Visible, Hidden
        }

        protected override void Dispose(bool disposing) {
            if (disposing && parent != null) {
                if (Child != null && prevParent != null) {
                    Child.Parent = prevParent;
                    Child = null;
                }
                int index = FindBandIndex();
                if (index != -1) parent.Bands.RemoveAt(index);
            }
            base.Dispose(disposing);
        }

    }

}
