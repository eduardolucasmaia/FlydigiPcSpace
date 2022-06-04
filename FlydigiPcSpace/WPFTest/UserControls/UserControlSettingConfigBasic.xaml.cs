// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigBasic
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace WPFTest.UserControls
{
    public partial class UserControlSettingConfigBasic : UserControl, IComponentConnector
    {
        private ConfigBean mFlashConfigBean;
        private JoyMappingBean mJoyMappingBean;
        //internal Grid mLayoutBasic;
        //internal UserControlTitle mTitle1;
        //internal WrapPanel mWrapPanel_1;
        //internal UserControlTitle mTitle2;
        //internal UserControlSelectMenu mMotionTarget;
        //internal UserControlTitle mTitle3;
        //internal UserControlSettingConfigLed mLayoutLed;
        //internal Border mSelectMenuMotionRemoteTarget;
       //private bool _contentLoaded;

        public UserControlSettingConfigBasic()
        {
            this.InitializeComponent();
            this.mTitle1.setTitle(Application.Current.FindResource((object)"basic_vibration_intensity").ToString());
            this.mTitle2.setTitle(Application.Current.FindResource((object)"basic_roulette_mapping").ToString());
            this.mTitle3.setTitle(Application.Current.FindResource((object)"basic_adjusting_colour").ToString());
        }

        public void setData(
          ConfigBean config,
          JoyMappingBean joyMappingBean,
          UserControlSettingConfigJoystickDisplay layoutJoystickDisplay)
        {
            this.mFlashConfigBean = config;
            this.mJoyMappingBean = joyMappingBean;
        }

        private void MotionRemoteTargetSelect_Click(object sender, RoutedEventArgs e)
        {
            if (this.mSelectMenuMotionRemoteTarget.Visibility == Visibility.Visible)
                this.closeSelectMenuMotionTarget();
            else
                this.openSelectMenuMotionTarget();
        }

        private void closeSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionRemoteTarget.Visibility = Visibility.Hidden;
            this.mMotionTarget.setSelected(false);
        }

        private void openSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionRemoteTarget.Visibility = Visibility.Visible;
            this.mMotionTarget.setSelected(true);
        }

        private void MotionTargetItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"left_joystick").ToString());
            this.closeSelectMenuMotionTarget();
        }

        private void MotionTargetItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"right_joystick").ToString());
            this.closeSelectMenuMotionTarget();
        }

        private void MotionTargetItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"motion_close").ToString());
            this.closeSelectMenuMotionTarget();
        }

        private void MotionActiveItem_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void MotionActiveItem_Click_2(object sender, RoutedEventArgs e)
        {
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigbasic.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mLayoutBasic = (Grid)target;
        //            break;
        //        case 2:
        //            this.mTitle1 = (UserControlTitle)target;
        //            break;
        //        case 3:
        //            this.mWrapPanel_1 = (WrapPanel)target;
        //            break;
        //        case 4:
        //            this.mTitle2 = (UserControlTitle)target;
        //            break;
        //        case 5:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionRemoteTargetSelect_Click);
        //            break;
        //        case 6:
        //            this.mMotionTarget = (UserControlSelectMenu)target;
        //            break;
        //        case 7:
        //            this.mTitle3 = (UserControlTitle)target;
        //            break;
        //        case 8:
        //            this.mLayoutLed = (UserControlSettingConfigLed)target;
        //            break;
        //        case 9:
        //            this.mSelectMenuMotionRemoteTarget = (Border)target;
        //            break;
        //        case 10:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_1);
        //            break;
        //        case 11:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_2);
        //            break;
        //        case 12:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_3);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
