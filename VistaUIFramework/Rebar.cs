//--------------------------------------------------------------------
// <copyright file="Rebar.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// Rebar is a bar that acts as a container of bands
    /// </summary>
    /// <remarks>
    /// Rebar is a beta feature and some glitches could occur
    /// </remarks>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ToolboxBitmap(typeof(ToolBar))]
    [Designer(typeof(RebarDesigner))]
    [DefaultProperty("Bands")]
    [Description("Rebar (beta) is a bar that acts as a container of bands")]
    public class Rebar : Control {

        private bool _AutoSize;
        private RebarBandCollection _Bands;
        private bool _BandSeparator = true;
        private bool _Divider = true;
        private ImageList _ImageList;
        private Orientation _Orientation = Orientation.Horizontal;
        private bool _VariantHeight;
        private RebarBand[] BandArray;
        private int BandCount = 0;

        /// <summary>
        /// Initializates a new instance of the <code>Rebar</code> class with default settings
        /// </summary>
        public Rebar() : base() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.FixedWidth, false);
            SetStyle(ControlStyles.FixedHeight, AutoSize);
            Dock = DockStyle.Top;
            TabStop = false;
            _Bands = new RebarBandCollection(this);
        }

        #region Forced visibility of properties and events

        [Browsable(true)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize {
            get {
                return _AutoSize;
            }
            set {
                if (_AutoSize != value) {
                    _AutoSize = value;
                    if (Dock == DockStyle.Left || Dock == DockStyle.Right) {
                        SetStyle(ControlStyles.FixedWidth, AutoSize);
                        SetStyle(ControlStyles.FixedHeight, false);
                    } else {
                        SetStyle(ControlStyles.FixedHeight, AutoSize);
                        SetStyle(ControlStyles.FixedWidth, false);
                    }
                    RecreateHandle();
                    OnAutoSizeChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler AutoSizeChanged { add => base.AutoSizeChanged += value; remove => base.AutoSizeChanged -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackColorChanged { add => base.BackColorChanged += value; remove => base.BackColorChanged -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged { add => base.BackgroundImageChanged += value; remove => base.BackgroundImageChanged -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set => base.BackgroundImageLayout = value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageLayoutChanged { add => base.BackgroundImageLayoutChanged += value; remove => base.BackgroundImageLayoutChanged += value;}

        [Browsable(true)]
        public override ContextMenu ContextMenu {
            get {
                return base.ContextMenu;
            }
            set {
                base.ContextMenu = value;
                if (value != null && ContextMenuStrip != null) {
                    ContextMenuStrip = null;
                }
            }
        }

        public override ContextMenuStrip ContextMenuStrip {
            get {
                return base.ContextMenuStrip;
            }
            set {
                base.ContextMenuStrip = value;
                if (value != null && ContextMenu != null) {
                    ContextMenu = null;
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler ContextMenuChanged { add => base.ContextMenuChanged += value; remove => base.ContextMenuChanged -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler ForeColorChanged { add => base.ForeColorChanged += value; remove => base.ForeColorChanged -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event PaintEventHandler Paint { add => base.Paint += value; remove => base.Paint -= value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public override string Text { get => base.Text; set => base.Text = value; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TextChanged { add => base.TextChanged += value; remove => base.TextChanged -= value; }

        #endregion

        #region Public properties

        /// <summary>
        /// The collection of bands
        /// </summary>
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Description("The collection of bands")]
        [MergableProperty(false)]
        public RebarBandCollection Bands {
            get {
                return _Bands;
            }
        }

        /// <summary>
        /// Gets or sets if bands are separated by a separator
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Gets or sets if bands are separated by a separator")]
        public bool BandSeparator {
            get {
                return _BandSeparator;
            }
            set {
                if (_BandSeparator != value) {
                    _BandSeparator = value;
                    RecreateHandle();
                }
            }
        }

        /// <summary>
        /// Gets or sets if Rebar has a divider on the top
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Gets or sets if Rebar has a divider on the top")]
        public bool Divider {
            get {
                return _Divider;
            }
            set {
                if (_Divider != value) {
                    _Divider = value;
                    RecreateHandle();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("The image list associated to the Rebar")]
        public ImageList ImageList {
            get {
                return _ImageList;
            }
            set {
                if (_ImageList != value) {
                    EventHandler RecreateHandler = new EventHandler(ImageListRecreateHandle);
                    EventHandler DisposedHandler = new EventHandler(ImageListDetach);
                    if (_ImageList != null) {
                        _ImageList.Disposed -= DisposedHandler;
                        _ImageList.RecreateHandle -= RecreateHandler;
                    }
                    _ImageList = value;
                    if (value != null) {
                        _ImageList.Disposed += DisposedHandler;
                        _ImageList.RecreateHandle += RecreateHandler;
                    }
                    if (IsHandleCreated) RecreateHandle();
                }
            }
        }

        /// <summary>
        /// The orientation of the Rebar
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(Orientation.Horizontal)]
        [Description("The orientation of the Rebar")]
        public Orientation Orientation {
            get {
                return _Orientation;
            }
            set {
                if (_Orientation != value) {
                    _Orientation = value;
                    RecreateHandle();
                }
            }
        }

        /// <summary>
        /// Gets or sets if Rebar height is defined by the bands and not by the Size property
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if Rebar height is defined by the bands and not by the Size property")]
        public bool VariantHeight {
            get {
                return _VariantHeight;
            }
            set {
                if (_VariantHeight != value) {
                    _VariantHeight = value;
                    RecreateHandle();
                    OnVariantHeightChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Public events

        /// <summary>
        /// Fires when a break should occur
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when a break should occur")]
        public event AutoBreakEventHandler AutoBreak;

        /// <summary>
        /// Fires when <see cref="Rebar"/> resizes itself
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when Rebar resizes itself")]
        public event BandAutoSizeEventHandler BandAutoSize;

        /// <summary>
        /// Fires when an user begins dragging a <see cref="RebarBand"/>
        /// </summary>
        [Category("DragDrop")]
        [Description("Fires when an user begins dragging a band")]
        public event BandCancelEventHandler BandBeginDrag;

        /// <summary>
        /// Fires after a <see cref="RebarBand"/> has been deleted
        /// </summary>
        /// <param name="e"></param>
        [Category("Behavior")]
        [Description("Fires when a band was deleted")]
        public event BandEventHandler BandDeleted;

        /// <summary>
        /// Fires when a <see cref="RebarBand"/> is about to be deleted
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when a band is about to be deleted")]
        public event BandEventHandler BandDeleting;

        /// <summary>
        /// Fires when an user stops dragging a <see cref="RebarBand"/>
        /// </summary>
        [Category("DragDrop")]
        [Description("Fires when an user stops dragging a band")]
        public event BandEventHandler BandEndDrag;

        /// <summary>
        /// Fires when an user pushes the chevron button
        /// </summary>
        [Category("Action")]
        [Description("Fires when an user pushes the chevron button")]
        public event ChevronPushedEventHandler ChevronPushed;

        /// <summary>
        /// Fires when child size of a <see cref="RebarBand"/> changes
        /// </summary>
        [PropertyChangedCategory]
        [Description("Fires when child size of a band changes")]
        public event ChildSizeChangedEventHandler ChildSizeChanged;

        /// <summary>
        /// Fires when an user changes the layout of the <see cref="Rebar"/>
        /// </summary>
        [Category("Behavior")]
        [Description("Fires when an user changes the layout of the Rebar")]
        public event EventHandler LayoutChanged;

        /// <summary>
        /// Fires when an user drags a splitter
        /// </summary>
        [Category("DragDrop")]
        [Description("Fires when an user drags a splitter")]
        public event SplitterDragEventHandler SplitterDrag;

        /// <summary>
        /// Raises the <see cref="AutoBreak"/> event
        /// </summary>
        /// <param name="e">An <see cref="AutoBreakEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnAutoBreak(AutoBreakEventArgs e) {
            AutoBreak?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BandAutoSize"/> event
        /// </summary>
        /// <param name="e">A <see cref="BandAutoSizeEventArgs"/>that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBandAutoSize(BandAutoSizeEventArgs e) {
            BandAutoSize?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BandBeginDrag"/> event
        /// </summary>
        /// <param name="e">A <see cref="BandCancelEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBandBeginDrag(BandCancelEventArgs e) {
            BandBeginDrag?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ChevronPushed"/> event
        /// </summary>
        /// <param name="e">A <see cref="ChevronPushedEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnChevronPushed(ChevronPushedEventArgs e) {
            ChevronPushed?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ChildSizeChanged"/> event
        /// </summary>
        /// <param name="e">A <see cref="ChildSizeChangedEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnChildSizeChanged(ChildSizeChangedEventArgs e) {
            ChildSizeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BandDeleted"/> event
        /// </summary>
        /// <param name="e">A <see cref="BandEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBandDeleted(BandEventArgs e) {
            BandDeleted?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BandDeleting"/> event
        /// </summary>
        /// <param name="e">A <see cref="BandEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBandDeleting(BandEventArgs e) {
            BandDeleting?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BandEndDrag"/> event
        /// </summary>
        /// <param name="e">A <see cref="BandEventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBandEndDrag(BandEventArgs e) {
            BandEndDrag?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="LayoutChanged"/> event
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLayoutChanged(EventArgs e) {
            LayoutChanged?.Invoke(this, e);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnSplitterDrag(SplitterDragEventArgs e) {
            SplitterDrag?.Invoke(this, e);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Provides hit information given a point
        /// </summary>
        /// <param name="Point">The <code>Point</code> at which to retrieve the information. The coordinates are relative to the upper-left corner of the control</param>
        /// <param name="Element">The element at which to retrieve the information</param>
        public void HitTest(Point Point, RebarHitTest Element) {
            HitTest(Point, Element, -1);
        }

        /// <summary>
        /// Provides hit information given a point
        /// </summary>
        /// <param name="Point">The <code>Point</code> at which to retrieve the information. The coordinates are relative to the upper-left corner of the control</param>
        /// <param name="Element">The element at which to retrieve the information</param>
        /// <param name="Band">The <code>RebarBand</code> at which to retrieve the information</param>
        public void HitTest(Point Point, RebarHitTest Element, RebarBand Band) {
            if (Bands.IndexOf(Band) == -1) throw new ArgumentException("Specified Band is not found on the collection", "Band");
            HitTest(Point, Element, Bands.IndexOf(Band));
        }

        /// <summary>
        /// Provides hit information given a point
        /// </summary>
        /// <param name="Point">The <code>Point</code> at which to retrieve the information. The coordinates are relative to the upper-left corner of the control</param>
        /// <param name="Element">The element at which to retrieve the information</param>
        /// <param name="BandIndex">The <code>RebarBand</code> index at which to retrieve the information</param>
        public void HitTest(Point Point, RebarHitTest Element, int BandIndex) {
            if (BandIndex < -1 || BandIndex > BandCount) throw new ArgumentOutOfRangeException("Specified Index is out of bounds", "BandIndex");
            NativeMethods.RBHITTESTINFO info = new NativeMethods.RBHITTESTINFO {
                pt = Point,
                flags = Element,
                iBand = BandIndex
            };
            NativeMethods.SendMessage(Handle, NativeMethods.RB_HITTEST, 0, ref info);
        }

        #endregion

        #region Property changed events

        [PropertyChangedCategory]
        [Description("Fires when VariantHeight property changed")]
        public event EventHandler VariantHeightChanged;

        protected virtual void OnVariantHeightChanged(EventArgs e) {
            VariantHeightChanged?.Invoke(this, e);
        }

        #endregion

        #region Internal, private and overriden members

        [DefaultValue(DockStyle.Top)]
        public override DockStyle Dock {
            get {
                return base.Dock;
            }
            set {
                base.Dock = value;
                if ((Dock == DockStyle.Top || Dock == DockStyle.Bottom) && Orientation != Orientation.Horizontal)
                    Orientation = Orientation.Horizontal;
                else if ((Dock == DockStyle.Left || Dock == DockStyle.Right) && Orientation != Orientation.Vertical)
                    Orientation = Orientation.Vertical;
            }
        }

        [DefaultValue(false)]
        public new bool TabStop { get => base.TabStop; set => base.TabStop = value; }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (ImageList != null) {
                NativeMethods.REBARINFO info = new NativeMethods.REBARINFO {
                    cbSize = Marshal.SizeOf(typeof(NativeMethods.REBARINFO)),
                    fMask = NativeMethods.RBIM_IMAGELIST,
                    himl = ImageList.Handle
                };
                NativeMethods.SendMessage(Handle, NativeMethods.RB_SETBARINFO, 0, ref info);
            }
            MakeBands();
        }

        private void MakeBands() {
            if (BandArray != null) {
                try {
                    BeginUpdate();
                    for (int i = 0; i < BandCount; i++) {
                        NativeMethods.REBARBANDINFO info = BandArray[i].BuildBandInfo();
                        NativeMethods.SendMessage(Handle, NativeMethods.RB_INSERTBAND, i, ref info);
                    }
                } finally {
                    EndUpdate();
                }
            }
        }

        protected override void CreateHandle() {
            if (!RecreatingHandle) {
                NativeMethods.INITCOMMONCONTROLSEX iccex = new NativeMethods.INITCOMMONCONTROLSEX {
                    dwSize = Marshal.SizeOf(typeof(NativeMethods.INITCOMMONCONTROLSEX)),
                    dwICC = NativeMethods.ICC_COOL_CLASSES
                };
                NativeMethods.InitCommonControlsEx(ref iccex);
            }
            base.CreateHandle();
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassName = NativeMethods.REBARCLASSNAME;
                cp.ExStyle |= NativeMethods.WS_EX_TOOLWINDOW;
                cp.Style |= NativeMethods.CCS_NORESIZE | NativeMethods.CCS_NOPARENTALIGN;
                if (!Divider) cp.Style |= NativeMethods.CCS_NODIVIDER;
                if (BandSeparator) cp.Style |= NativeMethods.RBS_BANDBORDERS;
                if (AutoSize) {
                    cp.Style |= NativeMethods.RBS_AUTOSIZE;
                    cp.Style &= ~(NativeMethods.CCS_NORESIZE);
                }
                if (Orientation == Orientation.Horizontal) cp.Style |= NativeMethods.CCS_TOP;
                else cp.Style |= NativeMethods.CCS_LEFT;
                if (VariantHeight) cp.Style |= NativeMethods.RBS_VARHEIGHT;
                cp.ExStyle &= (~NativeMethods.WS_EX_CLIENTEDGE);
                cp.Style &= (~NativeMethods.WS_BORDER);
                return cp;
            }
        }

        private int CollectionAddBand(RebarBand Band) {
            if (Band == null) throw new ArgumentNullException("Band");
            int index = BandCount;
            InsertToArray(index, Band);
            return index;
        }

        private void CollectionInsertBand(int index, RebarBand Band) {
            if (Band == null) throw new ArgumentNullException("Band");
            if (index < 0 || (BandArray != null && index > BandCount))
                throw new ArgumentOutOfRangeException("index", "Index out of bounds");
            InsertToArray(index, Band);
            if (IsHandleCreated) {
                NativeMethods.REBARBANDINFO info = Band.BuildBandInfo();
                NativeMethods.SendMessage(Handle, NativeMethods.RB_INSERTBAND, index, ref info);
            }
            UpdateBands();
        }

        private void CollectionRemoveAt(int index) {
            BandArray[index].parent = null;
            BandCount--;
            if (index < BandCount)
                Array.Copy(BandArray, index + 1, BandArray, index, BandCount - index);
            BandArray[BandCount] = null;
        }

        private void InsertToArray(int index, RebarBand Band) {
            Band.parent = this;
            if (BandArray == null) BandArray = new RebarBand[4];
            else if (BandArray.Length == BandCount) {
                RebarBand[] NewBands = new RebarBand[BandCount + 4];
                Array.Copy(BandArray, 0, NewBands, 0, BandCount);
                BandArray = NewBands;
            }
            if (index < BandCount)
                Array.Copy(BandArray, index, BandArray, index + 1, BandCount - 1);
            BandArray[index] = Band;
            BandCount++;
        }

        internal void CollectionSetBand(int index, RebarBand value, bool recreate) {
            BandArray[index].parent = null;
            BandArray[index] = value;
            BandArray[index].parent = this;
            if (IsHandleCreated) {
                NativeMethods.REBARBANDINFO info = value.BuildBandInfo();
                NativeMethods.SendMessage(Handle, NativeMethods.RB_SETBANDINFO, index, ref info);
                if (recreate) UpdateBands();
                else Invalidate();
            }
        }

        private void BeginUpdate() {
            MethodInfo method = GetType().GetMethod("BeginUpdateInternal", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(this, new object[] {});
        }

        private void EndUpdate() {
            MethodInfo method = GetType().GetMethod("EndUpdateInternal", BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(bool) }, null);
            method.Invoke(this, new object[] {true});
        }

        private void UpdateBands() {
            if (IsHandleCreated) RecreateHandle();
        }

        private void ImageListRecreateHandle(object sender, EventArgs e) {
            if (IsHandleCreated) RecreateHandle();
        }

        private void ImageListDetach(object sender, EventArgs e) {
            foreach (RebarBand Band in Bands) {
                if (Band.ImageIndex != -1) Band.ImageIndex = -1;
                if (Band.ImageKey != null) Band.ImageKey = null;
            }
            ImageList = null;
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == NativeMethods.WM_NOTIFY || m.Msg == NativeMethods.WM_NOTIFY + NativeMethods.WM_REFLECT) {
                NativeMethods.NMHDR hdr = (NativeMethods.NMHDR) m.GetLParam(typeof(NativeMethods.NMHDR));
                switch (hdr.code) {
                    case NativeMethods.RBN_AUTOBREAK: {
                            NativeMethods.NMREBARAUTOBREAK autoBreak = (NativeMethods.NMREBARAUTOBREAK) m.GetLParam(typeof(NativeMethods.NMREBARAUTOBREAK));
                            if (autoBreak.uBand != -1) {
                                RebarBand Band = Bands[autoBreak.uBand];
                                OnAutoBreak(new AutoBreakEventArgs(Band, autoBreak.fAutoBreak));
                            }
                            break;
                        }
                    case NativeMethods.RBN_AUTOSIZE: {
                            NativeMethods.NMRBAUTOSIZE autoSize = (NativeMethods.NMRBAUTOSIZE) m.GetLParam(typeof(NativeMethods.NMRBAUTOSIZE));
                            OnBandAutoSize(new BandAutoSizeEventArgs(autoSize.fChanged, autoSize.rcActual.Size, autoSize.rcTarget.Size));
                            break;
                        }
                    case NativeMethods.RBN_BEGINDRAG: {
                            NativeMethods.NMREBAR rebar = (NativeMethods.NMREBAR) m.GetLParam(typeof(NativeMethods.NMREBAR));
                            if (rebar.uBand != -1) {
                                RebarBand Band = Bands[rebar.uBand];
                                BandCancelEventArgs e = new BandCancelEventArgs(Band);
                                OnBandBeginDrag(e);
                                m.Result = new IntPtr(NativeMethods.BoolToNative(e.Cancel));
                            }
                            break;
                        }
                    case NativeMethods.RBN_CHEVRONPUSHED: {
                            NativeMethods.NMREBARCHEVRON chevron = (NativeMethods.NMREBARCHEVRON) m.GetLParam(typeof(NativeMethods.NMREBARCHEVRON));
                            if (chevron.uBand != -1) {
                                RebarBand Band = Bands[chevron.uBand];
                                Rectangle Area = chevron.rc.Rect;
                                OnChevronPushed(new ChevronPushedEventArgs(Band, Area));
                            }
                            break;
                        }
                    case NativeMethods.RBN_CHILDSIZE: {
                            NativeMethods.NMREBARCHILDSIZE childSize = (NativeMethods.NMREBARCHILDSIZE) m.GetLParam(typeof(NativeMethods.NMREBARCHILDSIZE));
                            if (childSize.uBand != -1) {
                                RebarBand Band = Bands[childSize.uBand];
                                Size ChildSize = childSize.rcChild.Size;
                                Size BandSize = childSize.rcBand.Size;
                                OnChildSizeChanged(new ChildSizeChangedEventArgs(Band, ChildSize, BandSize));
                            }
                            break;
                        }
                    case NativeMethods.RBN_DELETEDBAND: {
                            NativeMethods.NMREBAR rebar = (NativeMethods.NMREBAR) m.GetLParam(typeof(NativeMethods.NMREBAR));
                            if (rebar.uBand != -1) {
                                RebarBand Band = Bands[rebar.uBand];
                                OnBandDeleted(new BandEventArgs(Band));
                            }
                            break;
                        }
                    case NativeMethods.RBN_DELETINGBAND: {
                            NativeMethods.NMREBAR rebar = (NativeMethods.NMREBAR) m.GetLParam(typeof(NativeMethods.NMREBAR));
                            if (rebar.uBand != -1) {
                                RebarBand Band = Bands[rebar.uBand];
                                OnBandDeleting(new BandEventArgs(Band));
                            }
                            break;
                        }
                    case NativeMethods.RBN_ENDDRAG: {
                            NativeMethods.NMREBAR rebar = (NativeMethods.NMREBAR) m.GetLParam(typeof(NativeMethods.NMREBAR));
                            if (rebar.uBand != -1) {
                                RebarBand Band = Bands[rebar.uBand];
                                OnBandEndDrag(new BandEventArgs(Band));
                            }
                            break;
                        }
                    case NativeMethods.RBN_LAYOUTCHANGED:
                        OnLayoutChanged(EventArgs.Empty);
                        break;
                    case NativeMethods.RBN_SPLITTERDRAG: {
                            NativeMethods.NMREBARSPLITTER splitter = (NativeMethods.NMREBARSPLITTER) m.GetLParam(typeof(NativeMethods.NMREBARSPLITTER));
                            OnSplitterDrag(new SplitterDragEventArgs(splitter.rcSizing.Size));
                            break;
                        }
                }
            } else if (m.Msg == NativeMethods.WM_SETCURSOR && Cursor == Cursors.Default) {
                base.DefWndProc(ref m);
                return;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region RebarBandCollection

        public class RebarBandCollection : IList {

            private Rebar Owner;
            private bool SuspendUpdate;

            public RebarBandCollection(Rebar Owner) {
                this.Owner = Owner;
            }

            public virtual RebarBand this[int index] {
                get {
                    if (index < 0 || (Owner.BandArray != null) && (index >= Owner.BandCount))
                        throw new ArgumentOutOfRangeException("index", "Index out of bounds");
                    return Owner.BandArray[index];
                }
                set {
                    if (index < 0 || (Owner.BandArray != null) && (index >= Owner.BandCount))
                        throw new ArgumentOutOfRangeException("index", "Index out of bounds");
                    if (value == null)
                        throw new ArgumentNullException("value");
                    Owner.CollectionSetBand(index, value, true);
                }
            }

            object IList.this[int index] {
                get {
                    return this[index];
                }
                set {
                    if (value is RebarBand) this[index] = (RebarBand)value;
                    else throw new ArgumentException("Object is not a RebarBand and does not inherit from RebarBand", "value");
                }
            }

            [Browsable(false)]
            public int Count {
                get {
                    return Owner.BandCount;
                }
            }

            object ICollection.SyncRoot {
                get {
                    return this;
                }
            }

            bool ICollection.IsSynchronized {
                get {
                    return false;
                }
            }

            bool IList.IsFixedSize {
                get {
                    return false;
                }
            }

            public bool IsReadOnly {
                get {
                    return false;
                }
            }

            public int Add(RebarBand Band) {
                int index = Owner.CollectionAddBand(Band);
                if (!SuspendUpdate) Owner.UpdateBands();
                return index;
            }

            public int Add(string Text) {
                return Add(new RebarBand(Text));
            }

            public int Add(Control Child) {
                return Add(new RebarBand(Child));
            }

            int IList.Add(object value) {
                if (value is RebarBand) return Add((RebarBand) value);
                else if (value is string) return Add(value.ToString());
                else if (value is Control) return Add((Control) value);
                else throw new ArgumentException("Object is not a RebarBand and does not inherit from RebarBand", "value");
            }

            public void AddRange(RebarBand[] Bands) {
                if (Bands == null) throw new ArgumentNullException("Bands");
                try {
                    SuspendUpdate = true;
                    foreach (RebarBand Band in Bands) Add(Band);
                } finally {
                    SuspendUpdate = false;
                }
            }

            public void Clear() {
                if (Owner.BandArray == null) return;
                for (int i = Owner.BandCount; i > 0; i--) {
                    if (Owner.IsHandleCreated)
                        NativeMethods.SendMessage(Owner.Handle, NativeMethods.RB_DELETEBAND, i - 1, 0);
                }
                Owner.BandArray = null;
                Owner.BandCount = 0;
                if (!Owner.Disposing) Owner.UpdateBands();
            }

            public bool Contains(RebarBand Band) {
                return IndexOf(Band) != -1;
            }

            bool IList.Contains(object value) {
                if (value is RebarBand) return Contains((RebarBand)value);
                else return false;
            }

            void ICollection.CopyTo(Array array, int index) {
                if (Owner.BandCount > 0) Array.Copy(Owner.BandArray, 0, array, index, Owner.BandCount);
            }

            public int IndexOf(RebarBand Band) {
                for (int i = 0; i < Count; i++) if (this[i] == Band) return i;
                return -1;
            }

            int IList.IndexOf(object value) {
                if (value is RebarBand) return IndexOf((RebarBand) value);
                else return -1;
            }

            public void Insert(int index, RebarBand Band) {
                Owner.CollectionInsertBand(index, Band);
            }

            void IList.Insert(int index, object value) {
                if (value is RebarBand) Insert(index, (RebarBand) value);
                else throw new ArgumentException("Object is not a RebarBand and does not inherit from RebarBand", "value");
            }

            private bool IsValidIndex(int index) {
                return (index >= 0) && (index < Count);
            }

            public void RemoveAt(int index) {
                int count = (Owner.BandArray == null) ? 0 : Owner.BandCount;
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException("index", "Index out of bounds");
                if (Owner.IsHandleCreated)
                    NativeMethods.SendMessage(Owner.Handle, NativeMethods.RB_DELETEBAND, index, 0);
                Owner.CollectionRemoveAt(index);
                Owner.UpdateBands();
            }

            public void Remove(RebarBand Band) {
                int index = IndexOf(Band);
                if (index != -1) RemoveAt(index);
            }

            void IList.Remove(object value) {
                if (value is Rebar) Remove((RebarBand) value);
            }

            public IEnumerator GetEnumerator() {
                return new ArraySubsetEnumerator(Owner.BandArray, Owner.BandCount);
            }

        }

        #endregion

        protected override void Dispose(bool disposing) {
            if (disposing) {
                lock(this) {
                    if (ImageList != null) {
                        foreach (RebarBand Band in Bands) {
                            if (Band.ImageIndex != -1) Band.ImageIndex = -1;
                            if (Band.ImageKey != null) Band.ImageKey = null;
                        }
                        ImageList.Disposed -= new EventHandler(ImageListDetach);
                        ImageList = null;
                    }
                    if (Bands != null) {
                        RebarBand[] Backup = new RebarBand[Bands.Count];
                        ((ICollection)Bands).CopyTo(Backup, 0);
                        Bands.Clear();
                        foreach (RebarBand Band in Backup) Band.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

    }

}