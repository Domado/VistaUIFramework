//--------------------------------------------------------------------
// <copyright file="SR.CS" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    internal sealed class SR {

        private static string propertyChanged;

        public static string PropertyChanged {
            get {
                if (string.IsNullOrEmpty(propertyChanged)) {
                    EventInfo info = typeof(Control).GetEvent("AutoSizeChanged");
                    object[] attrs = info.GetCustomAttributes(true);
                    foreach (object attr in attrs) {
                        if (attr is CategoryAttribute) {
                            propertyChanged = ((CategoryAttribute) attr).Category;
                            return propertyChanged;
                        }
                    }
                    propertyChanged = "Property changed";
                }
                return propertyChanged;
            }
        }

    }

    internal sealed class SRCategoryAttribute : CategoryAttribute {

        public SRCategoryAttribute(string Category) : base(Category) {
        }

        protected override string GetLocalizedString(string value) {
            return base.GetLocalizedString(value);
        }

    }

    internal sealed class PropertyChangedCategoryAttribute : CategoryAttribute {

        public PropertyChangedCategoryAttribute() : base(SR.PropertyChanged) {}

    }

}
