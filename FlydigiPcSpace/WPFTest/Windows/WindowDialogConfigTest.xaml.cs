// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.WindowDialogConfigTest
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
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
    public class WindowDialogConfigTest : Window, DataManager.IDeviceOperateData, IComponentConnector
    {
        private Dictionary<int, Image> mDictionaryKey = new Dictionary<int, Image>();
        private IDelegateCallback mDelegateCallback;
        private int mConfigIndex;
        private PageConfigSetting mPageConfigSetting;
        public bool isApplicationActive;
        private bool testJoystickReset;
        internal Label mLabelTitle;
        internal Image mImageClose;
        internal ImageBrush mDeviceImg1;
        internal Image mKeyUP;
        internal Image mKeyRight;
        internal Image mKeyLeft;
        internal Image mKeyDown;
        internal Image mKeyA;
        internal Image mKeyB;
        internal Image mKeyX;
        internal Image mKeyY;
        internal Image mKeyC;
        internal Image mKeyZ;
        internal Image mKeySelect;
        internal Image mKeyStart;
        internal Image mKeyL3;
        internal Image mKeyR3;
        internal Image mKeyMenu;
        internal Image mKeyHome;
        internal Image mKeyBack;
        internal ImageBrush mDeviceImg2;
        internal Image mKeyM1;
        internal Image mKeyM2;
        internal Image mKeyM3;
        internal Image mKeyM4;
        internal ImageBrush mDeviceImg3;
        internal Image mKeyRB;
        internal Image mKeyRT;
        internal Image mKeyLB;
        internal Image mKeyLT;
        internal Image mKeyM5;
        internal Image mKeyM6;
        internal UserControlJoystick mJoystickLeft;
        internal UserControlJoystick mJoystickRight;
        internal Label mLabelJoystickResetNotice;
        internal Button mButtonJoystickReset;
        internal Grid mLoading;
        private bool _contentLoaded;

        public WindowDialogConfigTest(
          PageConfigSetting pageConfigSetting,
          int configId,
          IDelegateCallback delegateCallback)
        {
            this.InitializeComponent();
            NetworkUtils.FlydigiPostEvent("配置测试" + (configId + 1).ToString());
            this.mPageConfigSetting = pageConfigSetting;
            this.mConfigIndex = configId;
            this.mLabelTitle.Content = (object)(Application.Current.FindResource((object)"config").ToString() + (this.mConfigIndex + 1).ToString());
            this.mDelegateCallback = delegateCallback;
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
            if (this.mPageConfigSetting != null && this.mPageConfigSetting.checkKeyAndMacroChange())
            {
                this.mLoading.Visibility = Visibility.Visible;
                this.mPageConfigSetting.writeFlashConfig((IDelegateCallback)(result =>
                {
                    Application.Current.Dispatcher.Invoke((Action)(() => this.mLoading.Visibility = Visibility.Hidden));
                    this.sendTestCommand();
                }));
            }
            else
                this.sendTestCommand();
            this.updateUI();
        }

        private void updateUI()
        {
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch") && !(gameHadleName == "f1p"))
            {
                if (!(gameHadleName == "apex2"))
                    return;
                this.mDeviceImg1.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front.png"));
                this.mDeviceImg2.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_after.png"));
                this.mDeviceImg3.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_top.png"));
                this.mKeyMenu.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_menu.png"));
                this.mKeyHome.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_home.png"));
                this.mKeyBack.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_back.png"));
                this.mKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_select.png"));
                this.mKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_start.png"));
                this.mKeyUP.Margin = new Thickness(168.0, 146.0, 310.0, 218.0);
                this.mKeyRight.Margin = new Thickness(207.0, 173.0, 288.0, 188.0);
                this.mKeyLeft.Margin = new Thickness(149.0, 165.0, 351.0, 178.0);
                this.mKeyDown.Margin = new Thickness(179.0, 203.0, 323.0, 157.0);
                this.mKeyA.Margin = new Thickness(383.0, 126.0, 107.0, 231.0);
                this.mKeyB.Margin = new Thickness(419.0, 82.0, 80.0, 275.0);
                this.mKeyX.Margin = new Thickness(350.0, 80.0, 140.0, 279.0);
                this.mKeyY.Margin = new Thickness(381.0, 48.0, 106.0, 309.0);
                this.mKeyC.Margin = new Thickness(409.0, 179.0, 84.0, 175.0);
                this.mKeyZ.Margin = new Thickness(439.0, 148.0, 47.0, 208.0);
                this.mKeySelect.Margin = new Thickness(200.0, 65.0, 288.0, 290.0);
                this.mKeyStart.Margin = new Thickness(286.0, 65.0, 199.0, 289.0);
                this.mKeyL3.Margin = new Thickness(89.0, 61.0, 374.0, 265.0);
                this.mKeyR3.Margin = new Thickness(302.0, 148.0, 162.0, 180.0);
                this.mKeyMenu.Margin = new Thickness(195.0, 247.0, 282.0, 124.0);
                this.mKeyHome.Margin = new Thickness(241.0, 247.0, 242.0, 124.0);
                this.mKeyBack.Margin = new Thickness(282.0, 247.0, 196.0, 124.0);
                this.mKeyM1.Margin = new Thickness(80.0, 95.0, 207.0, 81.0);
                this.mKeyM2.Margin = new Thickness(208.0, 94.0, 78.0, 80.0);
                this.mKeyM3.Margin = new Thickness(108.0, 85.0, 174.0, 89.0);
                this.mKeyM4.Margin = new Thickness(175.0, 85.0, 107.0, 89.0);
                this.mKeyRB.Margin = new Thickness(44.0, 32.0, 189.0, 67.0);
                this.mKeyRT.Margin = new Thickness(45.0, 55.0, 216.0, 23.0);
                this.mKeyLB.Margin = new Thickness(190.0, 31.0, 44.0, 66.0);
                this.mKeyLT.Margin = new Thickness(210.0, 57.0, 41.0, 25.0);
                this.mKeyM5.Margin = new Thickness(91.0, 61.0, 190.0, 49.0);
                this.mKeyM6.Margin = new Thickness(191.0, 61.0, 90.0, 49.0);
                this.mJoystickLeft.Margin = new Thickness(42.0, 94.0, 0.0, 0.0);
                this.mJoystickRight.Margin = new Thickness(0.0, 182.0, 484.0, 0.0);
            }
            else
            {
                this.mDeviceImg1.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front.png"));
                this.mDeviceImg2.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_after.png"));
                this.mDeviceImg3.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_top.png"));
                this.mKeyMenu.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_menu.png"));
                this.mKeyHome.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_home.png"));
                this.mKeyBack.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_back.png"));
                this.mKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
                this.mKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
                this.mKeyUP.Margin = new Thickness(171.0, 147.0, 307.0, 217.0);
                this.mKeyRight.Margin = new Thickness(210.0, 174.0, 285.0, 187.0);
                this.mKeyLeft.Margin = new Thickness(151.0, 166.0, 349.0, 177.0);
                this.mKeyDown.Margin = new Thickness(182.0, 204.0, 320.0, 156.0);
                this.mKeyA.Margin = new Thickness(377.0, 126.0, 113.0, 231.0);
                this.mKeyB.Margin = new Thickness(416.0, 89.0, 83.0, 268.0);
                this.mKeyX.Margin = new Thickness(341.0, 90.0, 149.0, 269.0);
                this.mKeyY.Margin = new Thickness(376.0, 54.0, 111.0, 303.0);
                this.mKeyC.Margin = new Thickness(401.0, 179.0, 92.0, 175.0);
                this.mKeyZ.Margin = new Thickness(432.0, 148.0, 54.0, 208.0);
                this.mKeySelect.Margin = new Thickness(185.0, 50.0, 303.0, 305.0);
                this.mKeyStart.Margin = new Thickness(301.0, 53.0, 184.0, 301.0);
                this.mKeyL3.Margin = new Thickness(100.0, 75.0, 363.0, 251.0);
                this.mKeyR3.Margin = new Thickness(297.0, 158.0, 167.0, 170.0);
                this.mKeyMenu.Margin = new Thickness(193.0, 241.0, 284.0, 130.0);
                this.mKeyHome.Margin = new Thickness(239.0, 242.0, 244.0, 129.0);
                this.mKeyBack.Margin = new Thickness(282.0, 242.0, 196.0, 129.0);
                this.mKeyM1.Margin = new Thickness(76.0, 102.0, 211.0, 74.0);
                this.mKeyM2.Margin = new Thickness(209.0, 100.0, 77.0, 74.0);
                this.mKeyM3.Margin = new Thickness(112.0, 97.0, 179.0, 83.0);
                this.mKeyM4.Margin = new Thickness(180.0, 97.0, 111.0, 83.0);
                this.mKeyRB.Margin = new Thickness(38.0, 23.0, 195.0, 76.0);
                this.mKeyRT.Margin = new Thickness(36.0, 51.0, 225.0, 27.0);
                this.mKeyLB.Margin = new Thickness(196.0, 22.0, 38.0, 75.0);
                this.mKeyLT.Margin = new Thickness(218.0, 52.0, 33.0, 30.0);
                this.mKeyM5.Margin = new Thickness(91.0, 61.0, 190.0, 49.0);
                this.mKeyM6.Margin = new Thickness(191.0, 61.0, 90.0, 49.0);
                this.mJoystickLeft.Margin = new Thickness(53.0, 107.0, 0.0, 0.0);
                this.mJoystickRight.Margin = new Thickness(0.0, 190.0, 491.0, 0.0);
            }
        }

        private void sendTestCommand() => new Thread((ParameterizedThreadStart)(o =>
        {
            Thread.Sleep(100);
            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(false));
            Thread.Sleep(100);
            DataManager.getInstance().SendByteArray(DongleCommand.getStopMacroActionCommand());
        })).Start();

        public void onDeviceOperateData(byte[] data)
        {
            if (DataManager.getInstance().getUpdatePartlyWritingState() || DataManager.getInstance().getDeviceConnectState() != 0)
                return;
            int byte0 = (int)data[0] & (int)byte.MaxValue;
            int byte1 = (int)data[1] & (int)byte.MaxValue;
            int byte2 = (int)data[2] & (int)byte.MaxValue;
            int byte3 = (int)data[3] & (int)byte.MaxValue;
            int byte4 = (int)data[4] & (int)byte.MaxValue;
            int byte5 = (int)data[5] & (int)byte.MaxValue;
            int num1 = (int)data[6];
            int num2 = (int)data[7];
            int byte8 = (int)data[8] & (int)byte.MaxValue;
            int byte9 = (int)data[9] & (int)byte.MaxValue;
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
                this.setKeyState(this.mKeyM1, (byte9 & 4) != 0);
                this.setKeyState(this.mKeyM2, (byte9 & 8) != 0);
                this.setKeyState(this.mKeyM3, (byte9 & 16) != 0);
                this.setKeyState(this.mKeyM4, (byte9 & 32) != 0);
                this.setKeyState(this.mKeyM5, (byte9 & 64) != 0);
                this.setKeyState(this.mKeyM6, (byte9 & 128) != 0);
                this.setKeyState(this.mKeyMenu, (byte8 & 1) != 0);
                this.setKeyState(this.mKeyHome, (byte8 & 8) != 0);
                this.setKeyState(this.mKeyBack, (byte8 & 16) != 0);
                if ((int)Math.Sqrt((double)(Math.Abs(byte0 - 128) * Math.Abs(byte0 - 128) + Math.Abs(byte1 - 128) * Math.Abs(byte1 - 128))) < 10)
                {
                    this.mJoystickLeft.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.mJoystickLeft.Visibility = Visibility.Visible;
                    this.mJoystickLeft.setData(byte0, byte1);
                }
                if ((int)Math.Sqrt((double)(Math.Abs(byte2 - 128) * Math.Abs(byte2 - 128) + Math.Abs(byte3 - 128) * Math.Abs(byte3 - 128))) < 10)
                {
                    this.mJoystickRight.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.mJoystickRight.Visibility = Visibility.Visible;
                    this.mJoystickRight.setData(byte2, byte3);
                }
            }));
        }

        private void setKeyState(Image image, bool state) => image.Visibility = state ? Visibility.Visible : Visibility.Hidden;

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Window_Closed(object sender, EventArgs e)
        {
            DataManager.getInstance().SendByteArray(DongleCommand.getStopMacroActionCommand());
            if (this.mDelegateCallback == null)
                return;
            this.mDelegateCallback(0);
        }

        private void Joystick_Reset(object sender, RoutedEventArgs e)
        {
            if (this.testJoystickReset)
                return;
            this.testJoystickReset = true;
            this.mLabelJoystickResetNotice.Visibility = Visibility.Visible;
            this.mButtonJoystickReset.Visibility = Visibility.Hidden;
            new Thread((ParameterizedThreadStart)(o =>
            {
                DataManager.getInstance().SendByteArray(DongleCommand.getTestJoystickResetCommand());
                Thread.Sleep(2000);
                this.testJoystickReset = false;
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    this.mLabelJoystickResetNotice.Visibility = Visibility.Hidden;
                    this.mButtonJoystickReset.Visibility = Visibility.Visible;
                }));
            })).Start();
        }

        private void mImageClose_MouseEnter(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_white.png"));

        private void mImageClose_MouseLeave(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_gray.png"));

        private void mButtonJoystickReset_MouseEnter(object sender, MouseEventArgs e) => this.mButtonJoystickReset.FontSize = 17.0;

        private void mButtonJoystickReset_MouseLeave(object sender, MouseEventArgs e) => this.mButtonJoystickReset.FontSize = 16.0;

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowdialogconfigtest.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    ((Window)target).Closed += new EventHandler(this.Window_Closed);
                    break;
                case 2:
                    this.mLabelTitle = (Label)target;
                    break;
                case 3:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click);
                    break;
                case 4:
                    this.mImageClose = (Image)target;
                    this.mImageClose.MouseEnter += new MouseEventHandler(this.mImageClose_MouseEnter);
                    this.mImageClose.MouseLeave += new MouseEventHandler(this.mImageClose_MouseLeave);
                    break;
                case 5:
                    this.mDeviceImg1 = (ImageBrush)target;
                    break;
                case 6:
                    this.mKeyUP = (Image)target;
                    break;
                case 7:
                    this.mKeyRight = (Image)target;
                    break;
                case 8:
                    this.mKeyLeft = (Image)target;
                    break;
                case 9:
                    this.mKeyDown = (Image)target;
                    break;
                case 10:
                    this.mKeyA = (Image)target;
                    break;
                case 11:
                    this.mKeyB = (Image)target;
                    break;
                case 12:
                    this.mKeyX = (Image)target;
                    break;
                case 13:
                    this.mKeyY = (Image)target;
                    break;
                case 14:
                    this.mKeyC = (Image)target;
                    break;
                case 15:
                    this.mKeyZ = (Image)target;
                    break;
                case 16:
                    this.mKeySelect = (Image)target;
                    break;
                case 17:
                    this.mKeyStart = (Image)target;
                    break;
                case 18:
                    this.mKeyL3 = (Image)target;
                    break;
                case 19:
                    this.mKeyR3 = (Image)target;
                    break;
                case 20:
                    this.mKeyMenu = (Image)target;
                    break;
                case 21:
                    this.mKeyHome = (Image)target;
                    break;
                case 22:
                    this.mKeyBack = (Image)target;
                    break;
                case 23:
                    this.mDeviceImg2 = (ImageBrush)target;
                    break;
                case 24:
                    this.mKeyM1 = (Image)target;
                    break;
                case 25:
                    this.mKeyM2 = (Image)target;
                    break;
                case 26:
                    this.mKeyM3 = (Image)target;
                    break;
                case 27:
                    this.mKeyM4 = (Image)target;
                    break;
                case 28:
                    this.mDeviceImg3 = (ImageBrush)target;
                    break;
                case 29:
                    this.mKeyRB = (Image)target;
                    break;
                case 30:
                    this.mKeyRT = (Image)target;
                    break;
                case 31:
                    this.mKeyLB = (Image)target;
                    break;
                case 32:
                    this.mKeyLT = (Image)target;
                    break;
                case 33:
                    this.mKeyM5 = (Image)target;
                    break;
                case 34:
                    this.mKeyM6 = (Image)target;
                    break;
                case 35:
                    this.mJoystickLeft = (UserControlJoystick)target;
                    break;
                case 36:
                    this.mJoystickRight = (UserControlJoystick)target;
                    break;
                case 37:
                    this.mLabelJoystickResetNotice = (Label)target;
                    break;
                case 38:
                    this.mButtonJoystickReset = (Button)target;
                    this.mButtonJoystickReset.Click += new RoutedEventHandler(this.Joystick_Reset);
                    this.mButtonJoystickReset.MouseEnter += new MouseEventHandler(this.mButtonJoystickReset_MouseEnter);
                    this.mButtonJoystickReset.MouseLeave += new MouseEventHandler(this.mButtonJoystickReset_MouseLeave);
                    break;
                case 39:
                    this.mLoading = (Grid)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
