// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigTrigger
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlSettingConfigTrigger : UserControl, IComponentConnector
    {
        private ConfigBean mFlashConfigBean;
        private JoyMappingBean mJoyMappingBean;
        private UserControlSettingConfigJoystickDisplay mLayoutJoystickDisplay;
        private string mLastJoystickJson;
        //internal Button mButtonJoystickLeft;
        //internal Button mButtonJoystickRight;
        //internal Label mLineLeft;
        //internal Label mLineRight;
        //internal UserControlLineJoystickSetting mLayoutRight;
        //internal UserControlLineJoystickSetting mLayoutLeft;
        private bool _contentLoaded;

        public UserControlSettingConfigTrigger() => this.InitializeComponent();

        public void setData(
          ConfigBean config,
          JoyMappingBean joyMappingBean,
          UserControlSettingConfigJoystickDisplay layoutJoystickDisplay)
        {
            this.mFlashConfigBean = config;
            this.mJoyMappingBean = joyMappingBean;
            this.mLayoutJoystickDisplay = layoutJoystickDisplay;
            this.mLayoutLeft.setData(this.mJoyMappingBean.LeftJoyStick, (IDelegateCallback)(result => this.updatePartly()));
            this.mLayoutRight.setData(this.mJoyMappingBean.RightJoyStick, (IDelegateCallback)(result => this.updatePartly()));
            this.Button_Joystick_Left((object)null, (RoutedEventArgs)null);
        }

        private void Button_Joystick_Left(object sender, RoutedEventArgs e)
        {
            this.mLayoutLeft.Visibility = Visibility.Visible;
            this.mButtonJoystickLeft.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mButtonJoystickLeft.FontWeight = FontWeights.Bold;
            this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutJoystickDisplay.setLeftJoystickVisible(true);
            this.mLayoutRight.Visibility = Visibility.Hidden;
            this.mButtonJoystickRight.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonJoystickRight.FontWeight = FontWeights.Normal;
            this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            this.mLayoutJoystickDisplay.setRightJoystickVisible(false);
        }

        private void Button_Joystick_Right(object sender, RoutedEventArgs e)
        {
            this.mLayoutLeft.Visibility = Visibility.Hidden;
            this.mButtonJoystickLeft.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonJoystickLeft.FontWeight = FontWeights.Normal;
            this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            this.mLayoutJoystickDisplay.setLeftJoystickVisible(false);
            this.mLayoutRight.Visibility = Visibility.Visible;
            this.mButtonJoystickRight.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mButtonJoystickRight.FontWeight = FontWeights.Bold;
            this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutJoystickDisplay.setRightJoystickVisible(true);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 44, Application.Current.FindResource((object)"joystick_setting_desc").ToString());

        private void Button_MouseLeave(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 44, "");

        private void updatePartly()
        {
            string str = JsonConvert.SerializeObject((object)this.mJoyMappingBean);
            FLog.d("meng_currentJoystickJson" + str);
            if (str.Equals(this.mLastJoystickJson) || DataManager.getInstance().getUpdatePartlyWritingState())
                return;
            DataManager.getInstance().setUpdatePartlyWritingState(true);
            ConfigBean config = this.mFlashConfigBean.Clone();
            config.JoyMapping = this.mJoyMappingBean.Clone();
            List<List<byte>> doubleListByConfig = ConfigUtils.getDoubleListByConfig(config);
            List<List<byte>> partlyDoubleList = new List<List<byte>>();
            List<byte> byteList = new List<byte>();
            byteList.AddRange((IEnumerable<byte>)((IEnumerable<byte>)DongleCommand.getUpdatePartlyStartCommand(1)).ToList<byte>());
            partlyDoubleList.Add(byteList);
            int num1 = (int)byteList[2];
            int num2 = (int)byteList[3];
            for (int index = num1 + 1; index < num1 + num2 + 1; ++index)
                partlyDoubleList.Add(doubleListByConfig[index]);
            for (int index = 0; index < partlyDoubleList.Count; ++index)
                FLog.d("局部更新待发送配置:" + CommonUtils.byteToHexString(partlyDoubleList[index].ToArray()));
            new Thread((ThreadStart)(() =>
            {
                DataManager.getInstance().writeConfig(partlyDoubleList, (IDelegateCallback)(result =>
                {
                    if (result != 100)
                        return;
                    this.mFlashConfigBean.JoyMapping = this.mJoyMappingBean;
                    this.mLastJoystickJson = JsonConvert.SerializeObject((object)this.mJoyMappingBean);
                    FLog.d("配置局部更新完成");
                    DataManager.getInstance().setUpdatePartlyWritingState(false);
                }));
                for (int index = 0; index < 60 && DataManager.getInstance().getUpdatePartlyWritingState(); ++index)
                    Thread.Sleep(100);
                DataManager.getInstance().setUpdatePartlyWritingState(false);
            })).Start();
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigtrigger.xaml", UriKind.Relative));
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
        //            this.mButtonJoystickLeft = (Button)target;
        //            this.mButtonJoystickLeft.Click += new RoutedEventHandler(this.Button_Joystick_Left);
        //            break;
        //        case 2:
        //            this.mButtonJoystickRight = (Button)target;
        //            this.mButtonJoystickRight.Click += new RoutedEventHandler(this.Button_Joystick_Right);
        //            break;
        //        case 3:
        //            ((UIElement)target).MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
        //            ((UIElement)target).MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
        //            break;
        //        case 4:
        //            this.mLineLeft = (Label)target;
        //            break;
        //        case 5:
        //            this.mLineRight = (Label)target;
        //            break;
        //        case 6:
        //            this.mLayoutRight = (UserControlLineJoystickSetting)target;
        //            break;
        //        case 7:
        //            this.mLayoutLeft = (UserControlLineJoystickSetting)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
