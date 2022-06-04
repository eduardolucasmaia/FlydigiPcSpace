// Decompiled with JetBrains decompiler
// Type: WPFTest.Activitys.PageSplash
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;

namespace WPFTest.Activitys
{
    public partial class PageSplash : Page, IComponentConnector
    {
        private WindowMain mWindowMain;
        private System.Windows.Controls.Frame mFrame;
        private short times;
        //internal Image mImgDeviceFront;
        //internal Button mButtonStart;
        //internal Button mButtonStartSwitch;
        //internal Button mFactoryTest;
        //internal UserControlDialogConnect connect;
        //internal Grid mPageSplashLoading;
        //internal Label mPageSplashLoadingLabel;
        //internal Label currentModeName;
        private bool _contentLoaded;

        public PageSplash(WindowMain window, System.Windows.Controls.Frame frame)
        {
            this.mWindowMain = window;
            this.mFrame = frame;
            this.InitializeComponent();
            switch (Constant.CURRENT_PROJECT_TYPE)
            {
                case 0:
                    this.mButtonStart.Visibility = Visibility.Visible;
                    this.mFactoryTest.Visibility = Visibility.Hidden;
                    this.currentModeName.Visibility = Visibility.Hidden;
                    DataManager.getInstance().setMainWindow(this.mWindowMain);
                    break;
                case 1:
                    this.mButtonStart.Visibility = Visibility.Hidden;
                    this.mButtonStartSwitch.Visibility = Visibility.Hidden;
                    this.mFactoryTest.Visibility = Visibility.Visible;
                    if (DataManager.getInstance().getDeviceConnectState() == 1)
                    {
                        this.currentModeName.Content = (object)"Xinput模式";
                        break;
                    }
                    this.currentModeName.Content = (object)"STD模式";
                    break;
            }
            this.updateUI();
        }

        private void updateUI()
        {
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (!(gameHadleName == "f1p"))
                        return;
                    this.mImgDeviceFront.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front.png"));
                }
                else
                    this.mImgDeviceFront.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front.png"));
            }
            else
                this.mImgDeviceFront.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front.png"));
        }

        private void Page_Initialized(object sender, EventArgs e) => FLog.d("PageSplash----Page_Initialized");

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.mWindowMain.mLanguageBox.Visibility = Visibility.Visible;
            this.updateUI();
            FLog.d("PageSplash----Page_Loaded");
            FLog.d("PageSplash----Page_Loaded---DeviceId：" + DataManager.getInstance().getDeviceInfo().DeviceId.ToString());
        }

        private void installDriver()
        {
            if (!Directory.Exists("C:\\Windows\\System32\\drivers"))
                FLog.d("C:\\Windows\\System32\\drivers\\CH375WDM.SYS not exitst");
            if (!File.Exists("C:\\Windows\\System32\\drivers\\CH375W64.SYS"))
                FLog.d("C:\\Windows\\System32\\drivers\\CH375W64.SYS not exitst");
            FLog.d("systerm:" + NetworkUtils.Is64Bit().ToString());
            FLog.d("files1:" + Directory.Exists("C:\\Windows\\System32\\drivers").ToString());
            FLog.d("files2:" + File.Exists("C:\\Windows\\System32\\drivers\\gm.dls").ToString());
            string str1 = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Sysnative\\drivers\\CH375W64.SYS";
            FLog.d(str1);
            FLog.d("files3:" + File.Exists(str1).ToString());
            try
            {
                string str2 = "cd CH372DRV & setup /p";
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.WriteLine(str2 + "&exit");
                process.StandardInput.AutoFlush = true;
                string end = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                Console.WriteLine(end);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n跟踪;" + ex.StackTrace);
            }
        }

        public void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            ++this.times;
            this.mWindowMain.closeLayoutInfo();
            if (DataManager.getInstance().getDeviceConnectState() == -1)
                this.mWindowMain.showDialogConnectPage();
            else if (DataManager.getInstance().getDeviceConnectState() == 1)
            {
                NetworkUtils.FlydigiPostEvent("切换XInput模式");
                this.mWindowMain.showLoading(true, true);
                this.mWindowMain.updateLoaidngTips(Application.Current.FindResource((object)"tips_switch_xinput_step1").ToString());
                new Thread((ThreadStart)(() =>
                {
                    FLog.d("准备切模式");
                    DongleCommand.SwitchXInput2Android();
                    DataManager.getInstance().delayCheckDeviceState();
                    Thread.Sleep(6000);
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        this.mWindowMain.showLoading(false);
                        if (DataManager.getInstance().getDeviceConnectState() != 0)
                            return;
                        this.Button_Start_Click((object)null, (RoutedEventArgs)null);
                    }));
                })).Start();
            }
            else
            {
                if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
                    return;
                int deviceId = DataManager.getInstance().getDeviceInfo().DeviceId;
                int apex2 = Constant.DEVICE.APEX_2;
                if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode < CommonUtils.getFirmwareCodeByVersion("6.0.2.0"))
                {
                    CommonUtils.showDialogUpdateFirmware();
                }
                else
                {
                    NetworkUtils.FlydigiPostEvent("点击进入飞智游戏厅");
                    this.mWindowMain.mTopBar.showModeName("pcMode");
                    this.mWindowMain.mTopBar.mCurrentModeType = 1;
                    if (this.mFrame != null)
                        this.mFrame.Navigate((object)new PageConfigList(this.mWindowMain, this.mFrame));
                    this.mWindowMain.mLanguageBox.Visibility = Visibility.Collapsed;
                    this.updateUI();
                }
            }
        }

        public void Button_Start_Click_Switch(object sender, RoutedEventArgs e)
        {
            if (DataManager.getInstance().getDeviceConnectState() == 1)
            {
                NetworkUtils.FlydigiPostEvent("切换XInput模式");
                this.mWindowMain.showLoading(true, true);
                this.mWindowMain.updateLoaidngTips(Application.Current.FindResource((object)"tips_switch_xinput_step1").ToString());
                new Thread((ThreadStart)(() =>
                {
                    FLog.d("准备切模式");
                    DongleCommand.SwitchXInput2Android();
                    DataManager.getInstance().delayCheckDeviceState();
                    Thread.Sleep(6000);
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        this.mWindowMain.showLoading(false);
                        if (DataManager.getInstance().getDeviceConnectState() != 0)
                            return;
                        this.Button_Start_Click_Switch((object)null, (RoutedEventArgs)null);
                    }));
                })).Start();
            }
            else if (DataManager.getInstance().getDeviceInfo().DeviceId < 0)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"tipinfo1").ToString());
            }
            else
            {
                FLog.d("infoinfo:" + DataManager.getInstance().getDeviceInfo().DeviceId.ToString());
                if (DataManager.getInstance().getDeviceInfo().DeviceId != Constant.DEVICE.F1_PRO)
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"tipinfo2").ToString());
                }
                else
                {
                    int num = CommonUtils.compareVersion(DataManager.getInstance().getDeviceInfo().FirmwareVersion, Constant.minSwitchFmVersion);
                    FLog.d("infoinfo supportState+" + num.ToString());
                    if (num < 0)
                    {
                        CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"tipinfo3").ToString());
                    }
                    else
                    {
                        ++this.times;
                        this.mWindowMain.closeLayoutInfo();
                        if (DataManager.getInstance().getDeviceConnectState() == -1)
                        {
                            this.mWindowMain.showDialogConnectPage();
                        }
                        else
                        {
                            this.mWindowMain.mTopBar.showModeName("switchMode");
                            this.mWindowMain.mTopBar.mCurrentModeType = 2;
                            if (DataManager.getInstance().getDeviceConnectState() == 1)
                            {
                                NetworkUtils.FlydigiPostEvent("切换XInput模式");
                                this.mWindowMain.showLoading(true, true);
                                this.mWindowMain.updateLoaidngTips(Application.Current.FindResource((object)"tips_switch_xinput_step1").ToString());
                                new Thread((ThreadStart)(() =>
                                {
                                    FLog.d("准备切模式");
                                    DongleCommand.SwitchXInput2Android();
                                    DataManager.getInstance().delayCheckDeviceState();
                                    Thread.Sleep(6000);
                                    Application.Current.Dispatcher.Invoke((Action)(() =>
                                    {
                                        this.mWindowMain.showLoading(false);
                                        if (DataManager.getInstance().getDeviceConnectState() != 0)
                                            return;
                                        this.Button_Start_Click((object)null, (RoutedEventArgs)null);
                                    }));
                                })).Start();
                            }
                            else
                            {
                                if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
                                    return;
                                int deviceId = DataManager.getInstance().getDeviceInfo().DeviceId;
                                int apex2 = Constant.DEVICE.APEX_2;
                                if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode < CommonUtils.getFirmwareCodeByVersion("6.0.2.0"))
                                {
                                    CommonUtils.showDialogUpdateFirmware();
                                }
                                else
                                {
                                    NetworkUtils.FlydigiPostEvent("点击进入飞智游戏厅");
                                    if (this.mFrame != null)
                                        this.mFrame.Navigate((object)new PageConfigList(this.mWindowMain, this.mFrame));
                                    this.mWindowMain.mLanguageBox.Visibility = Visibility.Collapsed;
                                    this.updateUI();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FactoryTest_Click(object sender, RoutedEventArgs e)
        {
            this.mWindowMain.closeLayoutInfo();
            if (DataManager.getInstance().getDeviceConnectState() == -1)
                this.mWindowMain.showDialogConnect();
            else if (DataManager.getInstance().getDeviceConnectState() == 1)
            {
                this.mWindowMain.showLoading(true, true);
                this.mWindowMain.updateLoaidngTips("模式切换中...");
                new Thread((ThreadStart)(() =>
                {
                    DongleCommand.SwitchXInput2Android();
                    DataManager.getInstance().delayCheckDeviceState();
                })).Start();
            }
            else if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
                CommonUtils.showDialog("提示", "正在读取手柄固件信息等,请稍等");
            else if (!(DataManager.getInstance().getDeviceInfo().gameHadleName == "apex2") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1wch") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1p"))
            {
                CommonUtils.showDialog("提示", "工厂测试工具当前只支持八爪鱼2及黑武士2，其他手柄测试需求请联系技术支持");
            }
            else
            {
                FLog.d("log:::cpuName:" + DataManager.getInstance().getDeviceInfo().cpuName);
                FLog.d("log:::gameHadleName:" + DataManager.getInstance().getDeviceInfo().gameHadleName);
                FLog.d("log:::cpuType:" + DataManager.getInstance().getDeviceInfo().cpuType);
                FLog.d("log:::DeviceMac:" + DataManager.getInstance().getDeviceInfo().DeviceMac);
                FLog.d("log:::DeviceId:" + DataManager.getInstance().getDeviceInfo().DeviceId.ToString());
                FLog.d("log:::getDeviceInfo:" + DataManager.getInstance().getDeviceInfo()?.ToString());
                this.mWindowMain.showFactoryTest(DataManager.getInstance().getDeviceInfo());
            }
        }

        public void showLoading(bool show)
        {
            this.mPageSplashLoading.Visibility = show ? Visibility.Visible : Visibility.Hidden;
            this.mPageSplashLoadingLabel.Visibility = Visibility.Hidden;
        }

        public void showLoading(bool show, string showTips)
        {
            this.mPageSplashLoading.Visibility = show ? Visibility.Visible : Visibility.Hidden;
            if (show)
            {
                this.mPageSplashLoadingLabel.Visibility = Visibility.Visible;
                this.mPageSplashLoadingLabel.Content = (object)showTips;
            }
            else
                this.mPageSplashLoadingLabel.Visibility = Visibility.Hidden;
        }

        public void updateLoaidngTips(string tips) => this.mPageSplashLoadingLabel.Content = (object)tips;

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/pages/pagesplash.xaml", UriKind.Relative));
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
        //            ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
        //            ((FrameworkElement)target).Initialized += new EventHandler(this.Page_Initialized);
        //            break;
        //        case 2:
        //            this.mImgDeviceFront = (Image)target;
        //            break;
        //        case 3:
        //            this.mButtonStart = (Button)target;
        //            this.mButtonStart.Click += new RoutedEventHandler(this.Button_Start_Click);
        //            break;
        //        case 4:
        //            this.mButtonStartSwitch = (Button)target;
        //            this.mButtonStartSwitch.Click += new RoutedEventHandler(this.Button_Start_Click_Switch);
        //            break;
        //        case 5:
        //            this.mFactoryTest = (Button)target;
        //            this.mFactoryTest.Click += new RoutedEventHandler(this.FactoryTest_Click);
        //            break;
        //        case 6:
        //            this.connect = (UserControlDialogConnect)target;
        //            break;
        //        case 7:
        //            this.mPageSplashLoading = (Grid)target;
        //            break;
        //        case 8:
        //            this.mPageSplashLoadingLabel = (Label)target;
        //            break;
        //        case 9:
        //            this.currentModeName = (Label)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
