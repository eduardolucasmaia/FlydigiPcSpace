// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigJoystickDisplay
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
    public class UserControlSettingConfigJoystickDisplay : UserControl, IComponentConnector
    {
        internal UserControlJoystick mJoystickLeft;
        internal UserControlJoystick mJoystickRight;
        private bool _contentLoaded;

        public UserControlSettingConfigJoystickDisplay() => this.InitializeComponent();

        public void setData(int byte0, int byte1, int byte2, int byte3)
        {
            this.mJoystickLeft.setData(byte0, byte1);
            this.mJoystickRight.setData(byte2, byte3);
        }

        public void updateJsLeft(int left, int top, int right, int down) => this.mJoystickLeft.Margin = new Thickness((double)left, (double)top, (double)right, (double)down);

        public void updateJsRight(int left, int top, int right, int down) => this.mJoystickRight.Margin = new Thickness((double)left, (double)top, (double)right, (double)down);

        public void setLeftJoystickVisible(bool visible) => this.mJoystickLeft.Visibility = visible ? Visibility.Visible : Visibility.Hidden;

        public void setRightJoystickVisible(bool visible) => this.mJoystickRight.Visibility = visible ? Visibility.Visible : Visibility.Hidden;

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigjoystickdisplay.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId != 1)
            {
                if (connectionId == 2)
                    this.mJoystickRight = (UserControlJoystick)target;
                else
                    this._contentLoaded = true;
            }
            else
                this.mJoystickLeft = (UserControlJoystick)target;
        }
    }
}
