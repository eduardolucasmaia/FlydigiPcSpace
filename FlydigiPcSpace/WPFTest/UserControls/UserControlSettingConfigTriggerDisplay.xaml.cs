// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigTriggerDisplay
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WPFTest.UserControls
{
    public partial class UserControlSettingConfigTriggerDisplay : UserControl, IComponentConnector
    {
        //internal UserControlTrigger mTriggerLeft;
        //internal UserControlTrigger mTriggerRight;
        private bool _contentLoaded;

        public UserControlSettingConfigTriggerDisplay() => this.InitializeComponent();

        public void setData(int byte0, int byte1, int byte2, int byte3)
        {
        }

        public void updateJsLeft(int left, int top, int right, int down)
        {
        }

        public void updateJsRight(int left, int top, int right, int down)
        {
        }

        public void setLeftJoystickVisible(bool visible)
        {
        }

        public void setRightJoystickVisible(bool visible)
        {
        }

        private void mTriggerLeft_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigtriggerdisplay.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    if (connectionId != 1)
        //    {
        //        if (connectionId == 2)
        //            this.mTriggerRight = (UserControlTrigger)target;
        //        else
        //            this._contentLoaded = true;
        //    }
        //    else
        //        this.mTriggerLeft = (UserControlTrigger)target;
        //}
    }
}
