using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);
    public delegate void RadioButtonClickEventHandler(object sender, RadioButtonClickEventArgs e);
    public delegate void CheckboxCheckedEventHandler(object sender, CheckboxCheckedEventArgs e);
    public delegate void TimerTickEventHandler(object sender, TimerTickEventArgs e);
    public delegate void ExpandCollapseEventHandler(object sender, ExpandCollapseEventArgs e);

}
