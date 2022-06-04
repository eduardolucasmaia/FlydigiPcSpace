// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlLedMode
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace WPFTest.UserControls
{
    public class UserControlLedMode : UserControl, IComponentConnector
    {
        internal Border mBorder;
        internal Label mLabelMode;
        private bool _contentLoaded;

        public UserControlLedMode() => this.InitializeComponent();

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        public void setTitle(string title) => this.mLabelMode.Content = (object)title;

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolledmode.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    ((UIElement)target).MouseEnter += new MouseEventHandler(this.UserControl_MouseEnter);
                    ((UIElement)target).MouseLeave += new MouseEventHandler(this.UserControl_MouseLeave);
                    break;
                case 2:
                    this.mBorder = (Border)target;
                    break;
                case 3:
                    this.mLabelMode = (Label)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
