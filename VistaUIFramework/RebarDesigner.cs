//--------------------------------------------------------------------
// <copyright file="RebarDesigner.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyAPKapp.VistaUIFramework {
    internal class RebarDesigner : ControlDesigner {

        private Rebar rebar;
        private DesignerActionListCollection _ActionLists;

        public RebarDesigner() : base() {
            _ActionLists = new DesignerActionListCollection();
            _ActionLists.AddRange(base.ActionLists);
            _ActionLists.Add(new RebarActionList(this));
        }

        public override void Initialize(IComponent component) {
            base.Initialize(component);
            if (Control is Rebar) {
                rebar = (Rebar) Control;
            }
        }

        public override DesignerActionListCollection ActionLists {
            get {
                return _ActionLists;
            }
        }

        public override SelectionRules SelectionRules {
            get {
                if (rebar != null) {
                    SelectionRules Rules = SelectionRules.Visible;
                    if (rebar.Orientation == Orientation.Horizontal) {
                        if (rebar.Dock == DockStyle.Top) {
                            if (!rebar.VariantHeight) Rules |= SelectionRules.BottomSizeable;
                        } else if (rebar.Dock == DockStyle.Bottom) {
                            if (!rebar.VariantHeight) Rules |= SelectionRules.TopSizeable;
                        } else {
                            Rules |= SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                            if (!rebar.VariantHeight) Rules |= SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
                        }
                    } else {
                        if (rebar.Dock == DockStyle.Left) {
                            Rules |= SelectionRules.RightSizeable;
                        } else if (rebar.Dock == DockStyle.Right) {
                            Rules |= SelectionRules.LeftSizeable;
                        } else {
                            Rules |= SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
                            if (!rebar.VariantHeight) Rules |= SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                        }
                    }
                    if (rebar.Dock == DockStyle.None) {
                        Rules |= SelectionRules.Moveable;
                    }
                    return Rules;
                } else {
                    return base.SelectionRules;
                }
            }
        }

        private class RebarActionList : DesignerActionList {

            private RebarDesigner Designer;

            public RebarActionList(RebarDesigner Designer) : base(Designer.Component) {
                this.Designer = Designer;
            }

            public bool AutoSize {
                get {
                    return Designer.rebar.AutoSize;
                }
                set {
                    Designer.rebar.AutoSize = value;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public Rebar.RebarBandCollection Bands {
                get {
                    return Designer.rebar.Bands;
                }
            }

            public ImageList ImageList {
                get {
                    return Designer.rebar.ImageList;
                }
                set {
                    Designer.rebar.ImageList = value;
                }
            }

            public Orientation Orientation {
                get {
                    return Designer.rebar.Orientation;
                }
                set {
                    Designer.rebar.Orientation = value;
                }
            }

            public override DesignerActionItemCollection GetSortedActionItems() {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                items.Add(new DesignerActionPropertyItem("Bands", "Bands", "Behavior", "The collection of bands"));
                items.Add(new DesignerActionPropertyItem("ImageList", "Image list", "Appearance", "The imagelist associated to the control"));
                items.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "The orientation of the rebar"));
                items.Add(new DesignerActionPropertyItem("AutoSize", "Auto. size", "Design", "Set if rebar size is set automatically"));
                return items;
            }

        }

    }
}