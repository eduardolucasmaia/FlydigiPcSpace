// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.WindowFactoryTest
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.Bean;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;

namespace WPFTest.Windows
{
    public partial class WindowFactoryTest : Window, DataManager.IDeviceOperateData, IComponentConnector
    {
        private FDGDeviceInfo mFDGDeviceInfo;
        private IDelegateCallback mIDelegateCallback;
        private List<Control> mListControl = new List<Control>();
        private int testStepCount = 34;
        private bool testLeding;
        private bool testColoring;
        private bool testMotoring;
        private bool testJoystickResetting;
        private bool mLBLastPressed;
        private bool mRBLastPressed;
        //internal Label mLabelTitle;
        //internal Image mImageClose;
        //internal Label mKeyUP;
        //internal Label mKeyLeft;
        //internal Label mKeyRight;
        //internal Label mKeyDown;
        //internal UserControlJoystick mJoystickWheel;
        //internal Label mKeyA;
        //internal Label mKeyB;
        //internal Label mKeyX;
        //internal Label mKeyY;
        //internal Label mKeySelect;
        //internal Label mKeyStart;
        //internal Label mKeyLB;
        //internal Label mKeyLT;
        //internal Label mKeyRB;
        //internal Label mKeyRT;
        //internal Label mKeyL3;
        //internal Label mKeyR3;
        //internal Label mKeyC;
        //internal Label mKeyZ;
        //internal Label mKeyMenu;
        //internal Label mKeyHome;
        //internal Label mKeyBack;
        //internal Label mKeyM1;
        //internal Label mKeyM2;
        //internal Label mKeyM3;
        //internal Label mKeyM4;
        //internal Label mKeyM5;
        //internal Label mKeyM6;
        //internal UserControlJoystick mJoystickLeft;
        //internal UserControlJoystick mJoystickRight;
        //internal Label mLabelJoystickResetNotice;
        //internal Button mButtonLedTest;
        //internal Button mButtonColorTest;
        //internal Button mButtonMotorLeftTest;
        //internal Button mButtonMotorRightTest;
        //internal Button mButtonJoystickReset;
        //internal Slider mSliderLT;
        //internal Slider mSliderRT;
        //internal Label mLabelLT;
        //internal Label mLabelRT;
        //internal Label mLabelBattery;
        //internal Label mLabelFirmwareVersion;
        //internal Label mNoticeTestSuccess;
        //internal Grid mLoading;
        //internal Label mLabelL3X;
        //internal Label mLabelL3Y;
        //internal Label mLabelR3X;
        //internal Label mLabelR3Y;
       //private bool _contentLoaded;

        public WindowFactoryTest(FDGDeviceInfo info, IDelegateCallback delegateCallback)
        {
            this.InitializeComponent();
            this.updateDeviceInfo(info);
            this.mIDelegateCallback = delegateCallback;
            string gameHadleName = this.mFDGDeviceInfo.gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                    {
                        this.mLabelTitle.Content = (object)"F1_PRO手柄测试";
                        this.testStepCount = 31;
                        this.mJoystickWheel.Visibility = Visibility.Collapsed;
                        this.mKeyM5.Visibility = Visibility.Collapsed;
                        this.mKeyM6.Visibility = Visibility.Collapsed;
                        this.mSliderLT.Visibility = Visibility.Visible;
                        this.mSliderRT.Visibility = Visibility.Visible;
                        this.mLabelLT.Visibility = Visibility.Visible;
                        this.mLabelRT.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    this.mLabelTitle.Content = (object)"Apex 2手柄测试";
                    this.mSliderLT.Visibility = Visibility.Collapsed;
                    this.mSliderRT.Visibility = Visibility.Collapsed;
                    this.mLabelLT.Visibility = Visibility.Collapsed;
                    this.mLabelRT.Visibility = Visibility.Collapsed;
                    this.testStepCount = 34;
                }
            }
            else
            {
                this.mLabelTitle.Content = (object)"F1手柄测试";
                this.testStepCount = 31;
                this.mJoystickWheel.Visibility = Visibility.Collapsed;
                this.mKeyM5.Visibility = Visibility.Collapsed;
                this.mKeyM6.Visibility = Visibility.Collapsed;
                this.mSliderLT.Visibility = Visibility.Visible;
                this.mSliderRT.Visibility = Visibility.Visible;
                this.mLabelLT.Visibility = Visibility.Visible;
                this.mLabelRT.Visibility = Visibility.Visible;
            }
            this.mLoading.Visibility = Visibility.Visible;
            new Thread((ParameterizedThreadStart)(o =>
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getStopMacroActionCommand());
                Thread.Sleep(100);
                DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(true));
                Thread.Sleep(100);
                DataManager.getInstance().SendByteArray(DongleCommand.getTestJoystickResetCommand());
                Thread.Sleep(2000);
                DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
                Application.Current.Dispatcher.Invoke((Action)(() => this.mLoading.Visibility = Visibility.Hidden));
            })).Start();
        }

        public void updateDeviceInfo(FDGDeviceInfo info)
        {
            this.mFDGDeviceInfo = info;
            if (this.mFDGDeviceInfo.BatteryPercent > 0)
            {
                this.mLabelBattery.Visibility = Visibility.Visible;
                Label mLabelBattery1 = this.mLabelBattery;
                int batteryPercent = this.mFDGDeviceInfo.BatteryPercent;
                string str1 = "设备电量：" + batteryPercent.ToString() + "%";
                mLabelBattery1.Content = (object)str1;
                if (this.mFDGDeviceInfo.BatteryPercent < 50)
                {
                    Label mLabelBattery2 = this.mLabelBattery;
                    batteryPercent = this.mFDGDeviceInfo.BatteryPercent;
                    string str2 = "设备电量：" + batteryPercent.ToString() + "%【电量低于50%，不能出厂！】";
                    mLabelBattery2.Content = (object)str2;
                    this.mLabelBattery.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FF0000");
                }
                else
                    this.mLabelBattery.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
            }
            else
                this.mLabelBattery.Visibility = Visibility.Hidden;
            this.mLabelFirmwareVersion.Content = (object)("固件版本号：V" + this.mFDGDeviceInfo.FirmwareVersion + "  ( Dongle：V" + info.DongleVersion + " )");
        }

        public void onDeviceOperateData(byte[] data)
        {
            if (DataManager.getInstance().getDeviceConnectState() != 0)
                return;
            int byte0 = (int)data[0] & (int)byte.MaxValue;
            int byte1 = (int)data[1] & (int)byte.MaxValue;
            int byte2 = (int)data[2] & (int)byte.MaxValue;
            int byte3 = (int)data[3] & (int)byte.MaxValue;
            int byte4 = (int)data[4] & (int)byte.MaxValue;
            int byte5 = (int)data[5] & (int)byte.MaxValue;
            int byte6 = (int)data[6] & (int)byte.MaxValue;
            int byte7 = (int)data[7] & (int)byte.MaxValue;
            int byte8 = (int)data[8] & (int)byte.MaxValue;
            int byte9 = (int)data[9] & (int)byte.MaxValue;
            int byte10 = (int)data[14] & (int)byte.MaxValue;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                this.setKeyState(this.mKeyUP, (byte4 & 1) != 0);
                this.setKeyState(this.mKeyRight, (byte4 & 2) != 0);
                this.setKeyState(this.mKeyDown, (byte4 & 4) != 0);
                this.setKeyState(this.mKeyLeft, (byte4 & 8) != 0);
                this.setKeyState(this.mKeyA, (byte4 & 16) != 0);
                this.setKeyState(this.mKeyB, (byte4 & 32) != 0);
                this.setKeyState(this.mKeyX, (byte4 & 128) != 0);
                this.setKeyState(this.mKeyY, (byte5 & 1) != 0);
                this.setKeyState(this.mKeySelect, (byte4 & 64) != 0);
                this.setKeyState(this.mKeyStart, (byte5 & 2) != 0);
                this.setKeyState(this.mKeyLB, (byte5 & 4) != 0);
                this.setKeyState(this.mKeyRB, (byte5 & 8) != 0);
                this.setKeyState(this.mKeyLT, (byte5 & 16) != 0);
                this.setKeyState(this.mKeyRT, (byte5 & 32) != 0);
                this.setKeyState(this.mKeyL3, (byte5 & 64) != 0);
                this.setKeyState(this.mKeyR3, (byte5 & 128) != 0);
                this.setKeyState(this.mKeyC, (byte9 & 1) != 0);
                this.setKeyState(this.mKeyZ, (byte9 & 2) != 0);
                this.setKeyState(this.mKeyM1, (byte9 & 4) != 0 || byte10 == 129);
                this.setKeyState(this.mKeyM2, (byte9 & 8) != 0);
                this.setKeyState(this.mKeyM3, (byte9 & 16) != 0);
                this.setKeyState(this.mKeyM4, (byte9 & 32) != 0);
                this.setKeyState(this.mKeyM5, (byte9 & 64) != 0);
                this.setKeyState(this.mKeyM6, (byte9 & 128) != 0);
                this.setKeyState(this.mKeyMenu, (byte8 & 1) != 0);
                this.setKeyState(this.mKeyHome, (byte8 & 8) != 0);
                this.setKeyState(this.mKeyBack, (byte8 & 16) != 0);
                if ((int)Math.Sqrt((double)(Math.Abs(byte0 - 128) * Math.Abs(byte0 - 128) + Math.Abs(byte1 - 128) * Math.Abs(byte1 - 128))) < Constant.errorRadius)
                {
                    this.mJoystickLeft.setData(128, 128);
                }
                else
                {
                    this.mJoystickLeft.setData(byte0, byte1);
                    this.checkJoystickPassed(this.mJoystickLeft, byte0, byte1);
                }
                int num1 = byte0 - 128;
                int num2 = byte1 - 128;
                this.mLabelL3X.Content = (object)("X：" + num1.ToString());
                this.mLabelL3Y.Content = (object)("Y：" + num2.ToString());
                if ((int)Math.Sqrt((double)(Math.Abs(byte2 - 128) * Math.Abs(byte2 - 128) + Math.Abs(byte3 - 128) * Math.Abs(byte3 - 128))) < Constant.errorRadius)
                {
                    this.mJoystickRight.setData(128, 128);
                }
                else
                {
                    this.mJoystickRight.setData(byte2, byte3);
                    this.checkJoystickPassed(this.mJoystickRight, byte2, byte3);
                }
                int num3 = byte2 - 128;
                int num4 = byte3 - 128;
                this.mLabelR3X.Content = (object)("X：" + num3.ToString());
                this.mLabelR3Y.Content = (object)("Y：" + num4.ToString());
                string gameHadleName = this.mFDGDeviceInfo.gameHadleName;
                if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch") && !(gameHadleName == "f1p"))
                {
                    if (!(gameHadleName == "apex2"))
                        return;
                    if ((int)Math.Sqrt((double)(Math.Abs(byte6 - 128) * Math.Abs(byte6 - 128) + Math.Abs(byte7 - 128) * Math.Abs(byte7 - 128))) < 14)
                    {
                        this.mJoystickWheel.setData(128, 128);
                    }
                    else
                    {
                        this.mJoystickWheel.setData(byte6, byte7);
                        this.checkJoystickPassed(this.mJoystickWheel, byte6, byte7);
                    }
                }
                else
                {
                    this.mSliderLT.Value = (double)byte6;
                    this.mSliderRT.Value = (double)byte7;
                    this.mLabelLT.Content = (object)("线性按键LT：" + byte6.ToString());
                    this.mLabelRT.Content = (object)("线性按键RT：" + byte7.ToString());
                }
            }));
        }

        private void checkJoystickPassed(UserControlJoystick mJoystick, int x, int y)
        {
            if (mJoystick.Tag == null)
                mJoystick.Tag = (object)0;
            int tag = (int)mJoystick.Tag;
            if (x <= 50 && y <= 50)
                tag |= 1;
            else if (x >= 205 && y <= 50)
                tag |= 2;
            else if (x <= 50 && y >= 205)
                tag |= 4;
            else if (x >= 205 && y >= 205)
                tag |= 8;
            mJoystick.Tag = (object)tag;
            if (tag != 15)
                return;
            mJoystick.setBgImgToBlue();
            if (!this.mListControl.Contains((Control)mJoystick))
                this.mListControl.Add((Control)mJoystick);
            this.checkTestResult();
        }

        private void setKeyState(Label label, bool state)
        {
            if (label.Tag == null)
            {
                if (state)
                {
                    label.Tag = (object)1;
                    label.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    if (!this.mListControl.Contains((Control)label))
                        this.mListControl.Add((Control)label);
                    this.checkTestResult();
                }
                else
                    label.Background = (Brush)new BrushConverter().ConvertFrom((object)"#AAAAAA");
            }
            else if (state)
                label.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00DFDF");
            else
                label.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            if (state)
            {
                if (label.Equals((object)this.mKeyC))
                    this.Led_Test_Click((object)null, (RoutedEventArgs)null);
                else if (label.Equals((object)this.mKeyZ))
                    this.Color_Test_Click((object)null, (RoutedEventArgs)null);
            }
            if (label.Equals((object)this.mKeyLB))
                this.MotorLeftControlByKeyPress(state);
            if (!label.Equals((object)this.mKeyRB))
                return;
            this.MotoRightControlByKeyPress(state);
        }

        private void checkTestResult()
        {
            FLog.d("mListControl.Count" + this.mListControl.Count.ToString());
            if (this.mListControl.Count != this.testStepCount)
                return;
            this.mNoticeTestSuccess.Visibility = Visibility.Visible;
            this.testStepCount = 0;
            new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(1000);
                DataManager.getInstance().SendByteArray(DongleCommand.getSwitchAndroid2XInput());
            })).Start();
            Thread.Sleep(1000);
            this.Close();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void mImageClose_MouseEnter(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_white.png"));

        private void mImageClose_MouseLeave(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_gray.png"));

        private void Led_Test_Click(object sender, RoutedEventArgs e)
        {
            if (this.testLeding)
                return;
            this.testLeding = true;
            new Thread((ParameterizedThreadStart)(o =>
            {
                for (int index = 0; index < 4; ++index)
                {
                    if (index == 0)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestLedCommand(0));
                    else if (index == 1)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestLedCommand(1));
                    else if (index == 2)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestLedCommand(2));
                    else if (index == 3)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestLedCommand(3));
                    Thread.Sleep(250);
                }
                if (!this.mListControl.Contains((Control)this.mButtonLedTest))
                    this.mListControl.Add((Control)this.mButtonLedTest);
                Application.Current.Dispatcher.Invoke((Action)(() => this.checkTestResult()));
                this.testLeding = false;
            })).Start();
        }

        private void Color_Test_Click(object sender, RoutedEventArgs e)
        {
            if (this.testColoring)
                return;
            this.testColoring = true;
            new Thread((ParameterizedThreadStart)(o =>
            {
                for (int index = 0; index < 3; ++index)
                {
                    if (index == 0)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestRGBCommand((int)byte.MaxValue, 0, 0));
                    else if (index == 1)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestRGBCommand(0, (int)byte.MaxValue, 0));
                    else if (index == 2)
                        DataManager.getInstance().SendByteArray(DongleCommand.getTestRGBCommand(0, 0, (int)byte.MaxValue));
                    Thread.Sleep(250);
                }
                if (!this.mListControl.Contains((Control)this.mButtonColorTest))
                    this.mListControl.Add((Control)this.mButtonColorTest);
                Application.Current.Dispatcher.Invoke((Action)(() => this.checkTestResult()));
                this.testColoring = false;
            })).Start();
        }

        private void Motor_Left_Test_Click(object sender, RoutedEventArgs e)
        {
            if (this.testMotoring)
                return;
            this.testMotoring = true;
            new Thread((ParameterizedThreadStart)(o =>
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(48, 0));
                Thread.Sleep(6000);
                if (!this.mListControl.Contains((Control)this.mButtonMotorLeftTest))
                    this.mListControl.Add((Control)this.mButtonMotorLeftTest);
                Application.Current.Dispatcher.Invoke((Action)(() => this.checkTestResult()));
                this.testMotoring = false;
            })).Start();
        }

        private void Motor_Right_Test_Click(object sender, RoutedEventArgs e)
        {
            if (this.testMotoring)
                return;
            this.testMotoring = true;
            new Thread((ParameterizedThreadStart)(o =>
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(0, 48));
                Thread.Sleep(6000);
                if (!this.mListControl.Contains((Control)this.mButtonMotorRightTest))
                    this.mListControl.Add((Control)this.mButtonMotorRightTest);
                Application.Current.Dispatcher.Invoke((Action)(() => this.checkTestResult()));
                this.testMotoring = false;
            })).Start();
        }

        private void Joystick_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (this.testJoystickResetting)
                return;
            this.testJoystickResetting = true;
            int num = 0;
            if (DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
                num = CommonUtils.compareVersion(DataManager.getInstance().getDeviceInfo().FirmwareVersion, Constant.minJoystickResetFmVersion);
            else if (DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1)
                num = CommonUtils.compareVersion(DataManager.getInstance().getDeviceInfo().FirmwareVersion, Constant.minJoystickResetFmVersionF1);
            FLog.d("支持摇杆矫正 supportState：" + num.ToString());
            if (num >= 0 && DataManager.getInstance().getDeviceInfo().cpuType == "wch")
            {
                new Thread((ParameterizedThreadStart)(o =>
                {
                    byte d = 1;
                    DataManager.getInstance().SendByteArray(DongleCommand.getTestNewJoystickResetCommand(d));
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        new WindowDialogCommon(0, Application.Current.FindResource((object)"joytick_reset_title").ToString(), Application.Current.FindResource((object)"joytick_reset_tip").ToString(), Application.Current.FindResource((object)"joytick_reset_ok").ToString(), "", (IDelegateCallback)(result =>
                        {
                            if (result == 1)
                            {
                                byte d2 = 2;
                                DataManager.getInstance().SendByteArray(DongleCommand.getTestNewJoystickResetCommand(d2));
                                this.testJoystickResetting = false;
                            }
                            if (result != 2)
                                return;
                            this.testJoystickResetting = false;
                        }))
                        {
                            Owner = WindowMain.getInstance()
                        }.ShowDialog();
                    }));
                })).Start();
            }
            else
            {
                this.mLabelJoystickResetNotice.Visibility = Visibility.Visible;
                new Thread((ParameterizedThreadStart)(o =>
                {
                    DataManager.getInstance().SendByteArray(DongleCommand.getTestJoystickResetCommand());
                    Thread.Sleep(2000);
                    this.testJoystickResetting = false;
                    Application.Current.Dispatcher.Invoke((Action)(() => this.mLabelJoystickResetNotice.Visibility = Visibility.Hidden));
                })).Start();
            }
        }

        private void MotorLeftControlByKeyPress(bool currentPressed)
        {
            if (!this.mLBLastPressed & currentPressed)
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(48, 0));
                if (!this.mListControl.Contains((Control)this.mButtonMotorLeftTest))
                    this.mListControl.Add((Control)this.mButtonMotorLeftTest);
                this.checkTestResult();
            }
            else if (this.mLBLastPressed && !currentPressed)
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(0, 0));
            this.mLBLastPressed = currentPressed;
        }

        private void MotoRightControlByKeyPress(bool currentPressed)
        {
            if (!this.mRBLastPressed & currentPressed)
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(0, 48));
                if (!this.mListControl.Contains((Control)this.mButtonMotorRightTest))
                    this.mListControl.Add((Control)this.mButtonMotorRightTest);
                this.checkTestResult();
            }
            else if (this.mRBLastPressed && !currentPressed)
                DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(0, 0));
            this.mRBLastPressed = currentPressed;
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowfactorytest.xaml", UriKind.Relative));
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
        //            this.mLabelTitle = (Label)target;
        //            break;
        //        case 2:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Close_Click);
        //            break;
        //        case 3:
        //            this.mImageClose = (Image)target;
        //            this.mImageClose.MouseEnter += new MouseEventHandler(this.mImageClose_MouseEnter);
        //            this.mImageClose.MouseLeave += new MouseEventHandler(this.mImageClose_MouseLeave);
        //            break;
        //        case 4:
        //            this.mKeyUP = (Label)target;
        //            break;
        //        case 5:
        //            this.mKeyLeft = (Label)target;
        //            break;
        //        case 6:
        //            this.mKeyRight = (Label)target;
        //            break;
        //        case 7:
        //            this.mKeyDown = (Label)target;
        //            break;
        //        case 8:
        //            this.mJoystickWheel = (UserControlJoystick)target;
        //            break;
        //        case 9:
        //            this.mKeyA = (Label)target;
        //            break;
        //        case 10:
        //            this.mKeyB = (Label)target;
        //            break;
        //        case 11:
        //            this.mKeyX = (Label)target;
        //            break;
        //        case 12:
        //            this.mKeyY = (Label)target;
        //            break;
        //        case 13:
        //            this.mKeySelect = (Label)target;
        //            break;
        //        case 14:
        //            this.mKeyStart = (Label)target;
        //            break;
        //        case 15:
        //            this.mKeyLB = (Label)target;
        //            break;
        //        case 16:
        //            this.mKeyLT = (Label)target;
        //            break;
        //        case 17:
        //            this.mKeyRB = (Label)target;
        //            break;
        //        case 18:
        //            this.mKeyRT = (Label)target;
        //            break;
        //        case 19:
        //            this.mKeyL3 = (Label)target;
        //            break;
        //        case 20:
        //            this.mKeyR3 = (Label)target;
        //            break;
        //        case 21:
        //            this.mKeyC = (Label)target;
        //            break;
        //        case 22:
        //            this.mKeyZ = (Label)target;
        //            break;
        //        case 23:
        //            this.mKeyMenu = (Label)target;
        //            break;
        //        case 24:
        //            this.mKeyHome = (Label)target;
        //            break;
        //        case 25:
        //            this.mKeyBack = (Label)target;
        //            break;
        //        case 26:
        //            this.mKeyM1 = (Label)target;
        //            break;
        //        case 27:
        //            this.mKeyM2 = (Label)target;
        //            break;
        //        case 28:
        //            this.mKeyM3 = (Label)target;
        //            break;
        //        case 29:
        //            this.mKeyM4 = (Label)target;
        //            break;
        //        case 30:
        //            this.mKeyM5 = (Label)target;
        //            break;
        //        case 31:
        //            this.mKeyM6 = (Label)target;
        //            break;
        //        case 32:
        //            this.mJoystickLeft = (UserControlJoystick)target;
        //            break;
        //        case 33:
        //            this.mJoystickRight = (UserControlJoystick)target;
        //            break;
        //        case 34:
        //            this.mLabelJoystickResetNotice = (Label)target;
        //            break;
        //        case 35:
        //            this.mButtonLedTest = (Button)target;
        //            this.mButtonLedTest.Click += new RoutedEventHandler(this.Led_Test_Click);
        //            break;
        //        case 36:
        //            this.mButtonColorTest = (Button)target;
        //            this.mButtonColorTest.Click += new RoutedEventHandler(this.Color_Test_Click);
        //            break;
        //        case 37:
        //            this.mButtonMotorLeftTest = (Button)target;
        //            this.mButtonMotorLeftTest.Click += new RoutedEventHandler(this.Motor_Left_Test_Click);
        //            break;
        //        case 38:
        //            this.mButtonMotorRightTest = (Button)target;
        //            this.mButtonMotorRightTest.Click += new RoutedEventHandler(this.Motor_Right_Test_Click);
        //            break;
        //        case 39:
        //            this.mButtonJoystickReset = (Button)target;
        //            this.mButtonJoystickReset.Click += new RoutedEventHandler(this.Joystick_Reset_Click);
        //            break;
        //        case 40:
        //            this.mSliderLT = (Slider)target;
        //            break;
        //        case 41:
        //            this.mSliderRT = (Slider)target;
        //            break;
        //        case 42:
        //            this.mLabelLT = (Label)target;
        //            break;
        //        case 43:
        //            this.mLabelRT = (Label)target;
        //            break;
        //        case 44:
        //            this.mLabelBattery = (Label)target;
        //            break;
        //        case 45:
        //            this.mLabelFirmwareVersion = (Label)target;
        //            break;
        //        case 46:
        //            this.mNoticeTestSuccess = (Label)target;
        //            break;
        //        case 47:
        //            this.mLoading = (Grid)target;
        //            break;
        //        case 48:
        //            this.mLabelL3X = (Label)target;
        //            break;
        //        case 49:
        //            this.mLabelL3Y = (Label)target;
        //            break;
        //        case 50:
        //            this.mLabelR3X = (Label)target;
        //            break;
        //        case 51:
        //            this.mLabelR3Y = (Label)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
