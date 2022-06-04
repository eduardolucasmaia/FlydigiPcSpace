// Decompiled with JetBrains decompiler
// Type: WPFTest.WindowMain
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using WPFTest.Activitys;
using WPFTest.Bean;
using WPFTest.Pages;
using WPFTest.UserControls;
using WPFTest.Utils;
using WPFTest.Windows;

namespace WPFTest
{
    public partial class WindowMain : Window, DataManager.IDeviceState, IComponentConnector
    {
        private int mDeviceConnected = -1;
        private bool mFirstDialogConnect;
        private WindowDialogConnect mWindowDialogConnect;
        private WindowDialogConfigTest mWindowDialogConfigTest;
        private WindowFactoryTest mWindowFactoryTest;
        public static Window mWindow;
        private int mConfigIndex;
        private string mAPPVersion;
        public bool isApplicationActive = true;
        private PageConnectGuide connectGuidePage;
        public string currentLanguage = "zh";
        public int showNum;
        public int hideNum;
        private bool mReadingState;
        //internal UserControlTopBar mTopBar;
        //internal UserControlDialogConnect mGuide;
        //internal System.Windows.Controls.Frame mFrame;
        //internal StackPanel mLayoutApplyTips;
        //internal Image mImageClose;
        //internal Grid mLayoutInfo;
        //internal Label mInfoAppVersion;
        //internal Button CheckAppVersion;
        //internal Label mInfoFirmwareVersion;
        //internal Button CheckAppVersion1;
        //internal Label mInfoDongleVersion;
        //internal Label mInfoDongleVersionType0;
        //internal Label mInfoDongleVersionType1;
        //internal Label mInfoDongleVersionType2;
        //internal Button CheckAppVersion2;
        //internal Label mInfoQQ;
        //internal Button GoWeixin;
        //internal Button GoHelpCenter;
        //internal Grid mLanguageBox;
        //internal ComboBox mLangComboBox;
        //internal CheckBox LogButtonObj;
        //internal StackPanel mLayoutFunctionDesc;
        //internal TextBlock mFunctionDesc;
        //internal Grid mLoading;
        //internal Label mLoadingLabel;
        //internal Border mTips;
        //internal TextBlock mSuccessIcon;
        //internal TextBlock mFailIcon;
        //internal Label mTipsLabel;
        //internal Border mLongTips;
        //internal TextBlock mLongIcon;
        //internal Label mLongTipsLabel;
        //internal Grid mApplyConfigNotice;
        //internal Button mApplyButtonCancel;
        //internal Button mApplyButtonOK;
        //internal Grid mApplyDefaultConfigNotice;
        //internal Button mApplyDefaultConfigButtonCancel;
        //internal Button mApplyDefaultConfigButtonOK;
       //private bool _contentLoaded;

        public WindowMain()
        {
            this.initLanguage();
            this.InitializeComponent();
            WindowMain.mWindow = (Window)this;
            switch (Constant.CURRENT_PROJECT_TYPE)
            {
                case 0:
                    this.CheckAppVersion.Visibility = Visibility.Visible;
                    if (this.WindowState == WindowState.Normal)
                        this.showDialogConnectPage();
                    this.mLangComboBox.SelectedValue = (object)this.currentLanguage;
                    break;
                case 1:
                    this.CheckAppVersion.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private bool chekDriversisExits()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            return File.Exists(!NetworkUtils.Is64Bit() ? folderPath + "\\Sysnative\\drivers\\CH375WDM.SYS" : folderPath + "\\Sysnative\\drivers\\CH375W64.SYS");
        }

        private void installDriver()
        {
            try
            {
                string str = "cd CH372DRV & setup /s";
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.WriteLine(str + "&exit");
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

        private void appStateChanged(object sender, EventArgs e)
        {
        }

        private void changeXBoxMode()
        {
            if (DataManager.getInstance().getDeviceConnectState() != 0)
                return;
            FLog.d("界面隐藏 配置应用成功点击去玩游戏");
            Thread.Sleep(100);
            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchAndroid2XInput());
        }

        private void appActivated(object sender, EventArgs e)
        {
        }

        private void appDeactivated(object sender, EventArgs e)
        {
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.OnMouseLeftButtonDown(e);
            Point position = e.GetPosition((IInputElement)this.mTopBar);
            if (e.LeftButton != MouseButtonState.Pressed || position.X < 0.0 || position.X >= this.mTopBar.ActualWidth || position.Y < 0.0 || position.Y >= this.mTopBar.ActualHeight)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.mTopBar.setWindowAndDelegate((Window)this, this.mFrame, (IDelegateCallback)(type =>
            {
                switch (type)
                {
                    case 0:
                        if (!this.mFrame.CanGoBack)
                            break;
                        this.mFrame.GoBack();
                        break;
                    case 1:
                        if (this.mFrame.NavigationService.Content is PageConfigSetting)
                        {
                            this.showConfigTestPage((PageConfigSetting)this.mFrame.NavigationService.Content);
                            break;
                        }
                        this.showConfigTestPage((PageConfigSetting)null);
                        break;
                    case 2:
                        Application.Current.Dispatcher.Invoke((Action)(() => this.mLoading.Visibility = Visibility.Visible));
                        break;
                    case 3:
                        if (this.mTopBar.mCurrentModeType == 2)
                        {
                            new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                this.mLoading.Visibility = Visibility.Hidden;
                                string str = Application.Current.FindResource((object)"config_apply_success_content_tip").ToString();
                                string content = DataManager.getInstance().getDeviceInfo().ConnectType != 1 ? str + Application.Current.FindResource((object)"config_apply_success_content_wired").ToString() : str + Application.Current.FindResource((object)"config_apply_success_contnet_Wireless").ToString();
                                new WindowDialogCommon(0, Application.Current.FindResource((object)"config_apply_success_title").ToString(), content, Application.Current.FindResource((object)"config_apply_success_confirm").ToString(), (string)null, (IDelegateCallback)(result => { }))
                                {
                                    Owner = WindowMain.getInstance()
                                }.ShowDialog();
                            })))).Start();
                            break;
                        }
                        new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            this.mLoading.Visibility = Visibility.Hidden;
                            CommonUtils.showCommonTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"apply_successed_notice").ToString(), "success");
                            this.changeXBoxMode();
                        })))).Start();
                        break;
                    case 4:
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            this.mLoading.Visibility = Visibility.Hidden;
                            CommonUtils.showCommonTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"config_apply_failed").ToString(), "fail");
                        }));
                        break;
                    case 5:
                        if (this.mLayoutInfo.Visibility == Visibility.Visible)
                        {
                            this.closeLayoutInfo();
                            break;
                        }
                        this.mLayoutInfo.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        this.mApplyConfigNotice.Visibility = Visibility.Visible;
                        break;
                    case 7:
                        this.mLoading.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        this.mApplyDefaultConfigNotice.Visibility = Visibility.Visible;
                        break;
                    case 10:
                        this.mLoading.Visibility = Visibility.Hidden;
                        CommonUtils.showCommonTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"save_successed_notice").ToString(), "success");
                        break;
                }
            }));
            this.closeLayoutInfo();
            this.mInfoAppVersion.Content = (object)("V" + this.mAPPVersion);
            this.mInfoFirmwareVersion.Visibility = Visibility.Collapsed;
            this.mInfoDongleVersion.Visibility = Visibility.Collapsed;
            DataManager.getInstance().setIDeviceStateImpl((DataManager.IDeviceState)this);
            this.showSuccessConnectPage();
        }

        private void showConfigTest(PageConfigSetting pageConfigSetting)
        {
            this.mWindowDialogConfigTest = new WindowDialogConfigTest(pageConfigSetting, this.mConfigIndex, (IDelegateCallback)(re =>
            {
                if (!(this.mFrame.NavigationService.Content is PageConfigSetting))
                    return;
                ((PageConfigSetting)this.mFrame.NavigationService.Content).setKeyListen();
            }));
            this.mWindowDialogConfigTest.Owner = (Window)this;
            this.mWindowDialogConfigTest.ShowDialog();
        }

        private void showConfigTestPage(PageConfigSetting pageConfigSetting)
        {
            if (this.mFrame == null || this.mFrame.Content is PageConfigTest)
                return;
            this.mFrame.Navigate((object)new PageConfigTest(pageConfigSetting, this.mConfigIndex, (IDelegateCallback)(re =>
            {
                if (!(this.mFrame.NavigationService.Content is PageConfigSetting))
                    return;
                ((PageConfigSetting)this.mFrame.NavigationService.Content).setKeyListen();
            })));
        }

        public void showFactoryTest(FDGDeviceInfo info)
        {
            Console.WriteLine((object)info);
            if (!(DataManager.getInstance().getDeviceInfo().gameHadleName == "apex2") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1wch") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1p"))
                return;
            if (this.mWindowFactoryTest != null)
            {
                this.mWindowFactoryTest.updateDeviceInfo(info);
            }
            else
            {
                this.mWindowFactoryTest = new WindowFactoryTest(info, (IDelegateCallback)(result => this.mWindowFactoryTest = (WindowFactoryTest)null));
                this.mWindowFactoryTest.Owner = (Window)this;
                this.showLoading(false, false);
                this.mWindowFactoryTest.ShowDialog();
            }
        }

        public void closeLayoutInfo() => this.mLayoutInfo.Visibility = Visibility.Hidden;

        public void showDialogConnect()
        {
            this.mWindowDialogConnect = new WindowDialogConnect();
            this.mWindowDialogConnect.Owner = (Window)this;
            this.mWindowDialogConnect.ShowDialog();
        }

        public void showDialogConnectPage()
        {
            if (this.connectGuidePage == null)
                this.connectGuidePage = new PageConnectGuide();
            this.mFrame.Navigate((object)this.connectGuidePage);
        }

        public void showSuccessConnectPage()
        {
            PageSplash content = new PageSplash(this, this.mFrame);
            this.mFrame.Navigate((object)content);
            if (!this.isApplicationActive || this.showNum <= 1)
                return;
            content.Button_Start_Click((object)null, (RoutedEventArgs)null);
        }

        private void mFrame_Navigated(object sender, NavigationEventArgs e)
        {
            FLog.d("mFrame_Navigated:" + e.Content.ToString());
            if (e.Content is PageSplash)
                this.mTopBar.refreshCurrentPage(0);
            else if (e.Content is PageConfigList)
            {
                this.mTopBar.refreshCurrentPage(1);
            }
            else
            {
                if (!(e.Content is PageConfigSetting))
                    return;
                this.mTopBar.refreshCurrentPage(2);
            }
        }

        public void onDeviceState(int deviceState)
        {
            this.mDeviceConnected = deviceState;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                this.mTopBar.setDeviceState(this.mDeviceConnected);
                if (this.mDeviceConnected == -1)
                {
                    while (this.mFrame.CanGoBack)
                        this.mFrame.GoBack();
                    if (this.mWindowDialogConfigTest != null)
                    {
                        this.mWindowDialogConfigTest.Close();
                        this.mWindowDialogConfigTest = (WindowDialogConfigTest)null;
                    }
                    if (this.mWindowFactoryTest != null)
                    {
                        this.mWindowFactoryTest.Close();
                        this.mWindowFactoryTest = (WindowFactoryTest)null;
                    }
                    this.mInfoFirmwareVersion.Visibility = Visibility.Collapsed;
                    if (!this.mFirstDialogConnect)
                    {
                        this.mFirstDialogConnect = true;
                        if (this.WindowState == WindowState.Normal)
                            this.showDialogConnectPage();
                    }
                    this.showLoading(false);
                }
                else
                {
                    this.mFirstDialogConnect = false;
                    this.mFrame.Visibility = Visibility.Visible;
                    this.mGuide.Visibility = Visibility.Hidden;
                    if (this.mWindowDialogConnect != null)
                    {
                        this.mWindowDialogConnect.Close();
                        this.mWindowDialogConnect = (WindowDialogConnect)null;
                    }
                    this.mLoadingLabel.Content = (object)Application.Current.FindResource((object)"tips_switch_xinput_step2").ToString();
                    this.showSuccessConnectPage();
                    if (Constant.CURRENT_PROJECT_TYPE != 1)
                        return;
                    DataManager.getInstance().getDeviceConnectState();
                }
            }));
        }

        public void onDeviceInfo(FDGDeviceInfo info)
        {
            if (info == null)
                return;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (info.BatteryPercent != -1)
                {
                    int deviceId = info.DeviceId;
                    int f1Wired = Constant.DEVICE.F1_WIRED;
                }
                FLog.d("当前固件版本号:" + info.FirmwareVersionCode.ToString());
                if (info.FirmwareVersionCode == 0)
                    this.mInfoFirmwareVersion.Visibility = Visibility.Collapsed;
                else if (info.DongleVersion == "")
                {
                    this.mInfoDongleVersion.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.mInfoFirmwareVersion.Visibility = Visibility.Visible;
                    this.mInfoFirmwareVersion.Content = (object)("V" + info.FirmwareVersion);
                    this.mInfoDongleVersion.Visibility = Visibility.Visible;
                    this.mInfoDongleVersionType0.Visibility = Visibility.Collapsed;
                    this.mInfoDongleVersionType1.Visibility = info.connectType == 1 ? Visibility.Visible : Visibility.Collapsed;
                    this.mInfoDongleVersionType2.Visibility = info.connectType == 2 ? Visibility.Visible : Visibility.Collapsed;
                    this.mInfoDongleVersion.Content = (object)("V" + info.DongleVersion);
                    if (Constant.CURRENT_PROJECT_TYPE == 1)
                    {
                        new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() => this.showFactoryTest(info))))).Start();
                    }
                    else
                    {
                        if (info.FirmwareVersionCode < CommonUtils.getFirmwareCodeByVersion("6.0.2.0"))
                            new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() => CommonUtils.showDialogUpdateFirmware())))).Start();
                        this.mLoadingLabel.Content = (object)Application.Current.FindResource((object)"tips_switch_xinput_step3").ToString();
                    }
                }
            }));
        }

        public static Window getInstance() => WindowMain.mWindow;

        public void showLayoutFunctionDesc(bool visibel, int top, string desc)
        {
            this.mLayoutFunctionDesc.Visibility = visibel ? Visibility.Visible : Visibility.Hidden;
            this.mLayoutFunctionDesc.Margin = new Thickness(this.mLayoutFunctionDesc.Margin.Left, (double)(68 + top), 0.0, 0.0);
            this.mFunctionDesc.Text = desc;
        }

        public void updateConfigId(int configId)
        {
            this.mConfigIndex = configId;
            this.mTopBar.updateConfigId(this.mConfigIndex);
        }

        public void showLoading(bool show)
        {
            this.mLoading.Visibility = show ? Visibility.Visible : Visibility.Hidden;
            this.mLoadingLabel.Visibility = Visibility.Hidden;
        }

        public void showLoading(bool show, bool showTips)
        {
            this.mLoading.Visibility = show ? Visibility.Visible : Visibility.Hidden;
            if (show & showTips)
            {
                this.mLoadingLabel.Visibility = Visibility.Visible;
                this.mLoadingLabel.Content = (object)"";
            }
            else
                this.mLoadingLabel.Visibility = Visibility.Hidden;
        }

        public void updateLoaidngTips(string tips) => this.mLoadingLabel.Content = (object)tips;

        public void showTip(string tips)
        {
            this.mTips.Visibility = Visibility.Visible;
            this.mTipsLabel.Content = (object)tips;
        }

        public void hideTip()
        {
            this.mTips.Visibility = Visibility.Collapsed;
            this.mSuccessIcon.Visibility = Visibility.Collapsed;
            this.mFailIcon.Visibility = Visibility.Collapsed;
            this.mTipsLabel.Content = (object)"";
        }

        public void showLongTip(string tips)
        {
            this.mLongTips.Visibility = Visibility.Visible;
            this.mLongTipsLabel.Content = (object)tips;
        }

        public void hideLongTip()
        {
            this.mLongTips.Visibility = Visibility.Collapsed;
            this.mLongTipsLabel.Content = (object)"";
        }

        public void setConfigReading(bool reading) => this.mReadingState = reading;

        private void mApplyButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.mApplyConfigNotice.Visibility = Visibility.Hidden;
            this.mTopBar.BackLastPage();
        }

        private void mApplyButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.mApplyConfigNotice.Visibility = Visibility.Hidden;
            this.mTopBar.ConfigSave((object)null, (RoutedEventArgs)null);
        }

        private void mApplyDefaultConfigButtonCancel_Click(object sender, RoutedEventArgs e) => this.mApplyDefaultConfigNotice.Visibility = Visibility.Hidden;

        private void mApplyDefaultConfigButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.mApplyDefaultConfigNotice.Visibility = Visibility.Hidden;
            this.mTopBar.applyDefualtConfig();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            NetworkUtils.FlydigiPostEvent("启动");
            this.mAPPVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (Constant.CURRENT_PROJECT_TYPE != 0)
                return;
            NetworkUtils.FlydigiCheckAppVersion(this.mAPPVersion, false, (IDelegateCallbackValueTwoObject)((updateType, appInfo) => this.showAppUpdateDialog(updateType, appInfo)));
        }

        private void ButtonCheckAppVersion(object sender, RoutedEventArgs e)
        {
            this.closeLayoutInfo();
            NetworkUtils.FlydigiCheckAppVersion(this.mAPPVersion, true, (IDelegateCallbackValueTwoObject)((updateType, appInfo) => this.showAppUpdateDialog(updateType, appInfo)));
        }

        private void ButtonCheckFmVersion(object sender, RoutedEventArgs e)
        {
            if (!this.chekDriversisExits())
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    new WindowDialogCommon(0, Application.Current.FindResource((object)"driver_install_title").ToString(), Application.Current.FindResource((object)"driver_install_tip").ToString(), Application.Current.FindResource((object)"driver_install_btn").ToString(), "", (IDelegateCallback)(result =>
                    {
                        if (result != 1)
                            return;
                        this.installDriver();
                    }))
                    {
                        Owner = WindowMain.getInstance()
                    }.ShowDialog();
                }));
            }
            else
            {
                this.closeLayoutInfo();
                if (DataManager.getInstance().getDeviceInfo().cpuType != "wch")
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"The_handle_does_not_support_PC_firmware_upgrade").ToString());
                }
                else
                {
                    string firmwareVersion = DataManager.getInstance().getDeviceInfo().FirmwareVersion;
                    if (firmwareVersion == "" || firmwareVersion.Length == 0 || firmwareVersion == string.Empty)
                    {
                        CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"Firmware_version_not_obtained").ToString());
                    }
                    else
                    {
                        FLog.d("version:" + firmwareVersion);
                        NetworkUtils.FlydigiCheckFmVersion(firmwareVersion, true, (IDelegateCallbackValueTwoObject)((updateType, appInfo) => this.showFmUpdateDialog(updateType, appInfo)));
                    }
                }
            }
        }

        private void ButtonCheckDongleVersion(object sender, RoutedEventArgs e)
        {
            if (!this.chekDriversisExits())
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    new WindowDialogCommon(0, Application.Current.FindResource((object)"driver_install_title").ToString(), Application.Current.FindResource((object)"driver_install_tip").ToString(), Application.Current.FindResource((object)"driver_install_btn").ToString(), "", (IDelegateCallback)(result =>
                    {
                        if (result != 1)
                            return;
                        this.installDriver();
                    }))
                    {
                        Owner = WindowMain.getInstance()
                    }.ShowDialog();
                }));
            }
            else
            {
                this.closeLayoutInfo();
                if (DataManager.getInstance().getDeviceInfo().cpuType != "wch")
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"The_handle_does_not_support_PC_firmware_upgrade").ToString());
                }
                else
                {
                    string dongleVersion = DataManager.getInstance().getDeviceInfo().DongleVersion;
                    if (dongleVersion == "" || dongleVersion.Length == 0 || dongleVersion == string.Empty)
                    {
                        CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"Firmware_version_not_obtained").ToString());
                    }
                    else
                    {
                        FLog.d("version:" + dongleVersion);
                        NetworkUtils.FlydigiCheckDongleVersion(dongleVersion, true, (IDelegateCallbackValueTwoObject)((updateType, appInfo) => this.showDongleUpdateDialog(updateType, appInfo)));
                    }
                }
            }
        }

        private void ButtonGoHelpCenter(object sender, RoutedEventArgs e)
        {
            if (NetworkUtils.getCurrentLanguage().Equals("zh"))
                Process.Start("http://bbs.flydigi.com/detail/15346");
            else
                Process.Start("http://bbs.flydigi.com/en/detail/80");
        }

        private bool checkFmTipVersion() => false;

        private void openUrl(string url)
        {
            if (NetworkUtils.getCurrentLanguage().Equals("zh"))
                Process.Start(url);
            else
                Process.Start(url);
        }

        private void ButtonGoWeixin(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.getCurrentLanguage().Equals("zh"))
                return;
            Process.Start("https://bs.flydigi.com/topicDetail?id=43199");
        }

        private void ButtonCloseApplyTips(object sender, RoutedEventArgs e)
        {
            this.mLayoutApplyTips.Visibility = Visibility.Hidden;
            Constant.TIPS_APPLY_CFG = false;
        }

        private void ButtonGoSwitchLog(object sender, RoutedEventArgs e)
        {
            FLog.log = !FLog.log;
            if (FLog.log)
                this.LogButtonObj.Content = (object)"开启";
            else
                this.LogButtonObj.Content = (object)"关闭";
        }

        public void updateTipsState(bool visible)
        {
            if (visible)
            {
                if (!Constant.TIPS_APPLY_CFG)
                    return;
                this.mLayoutApplyTips.Visibility = Visibility.Visible;
            }
            else
                this.mLayoutApplyTips.Visibility = Visibility.Hidden;
        }

        private void showAppUpdateDialog(int updateType, object appInfo) => Application.Current.Dispatcher.Invoke((Action)(() =>
        {
            if (updateType == 0)
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"current_version_is_the_latest").ToString());
            else if (updateType == 3)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_check_network").ToString());
            }
            else
            {
                if (updateType != 1 && updateType != 2)
                    return;
                AppUpdateInfo appUpdateInfo = (AppUpdateInfo)appInfo;
                if (updateType == 1)
                {
                    new WindowDialogCommon(0, Application.Current.FindResource((object)"new_version_of_flydigi_space").ToString() + "V" + appUpdateInfo.data.version_code, appUpdateInfo.data.upgrade_point, Application.Current.FindResource((object)"update").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        switch (result)
                        {
                            case 0:
                                NetworkUtils.FlydigiPostEvent("版本更新点击下次再说");
                                break;
                            case 1:
                                NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                                this.showDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version_code);
                                break;
                            case 2:
                                NetworkUtils.FlydigiPostEvent("版本更新点击关闭");
                                break;
                        }
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
                else
                {
                    if (updateType != 2)
                        return;
                    new WindowDialogCommon(1, Application.Current.FindResource((object)"new_version_of_flydigi_space").ToString() + "V" + appUpdateInfo.data.version_code, appUpdateInfo.data.upgrade_point, Application.Current.FindResource((object)"update").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        if (result != 1)
                            return;
                        NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                        this.showDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version_code);
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
            }
        }));

        private void showDownloadWindow(string url, string version)
        {
            WindowDialogDownload windowDialogDownload = new WindowDialogDownload(url, version);
            windowDialogDownload.Owner = (Window)this;
            windowDialogDownload.ShowDialog();
        }

        private void showFmUpdateDialog(int updateType, object appInfo)
        {
            Action action1 = null;
            Action action2 = null;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (updateType == 0)
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"current_version_is_the_latest").ToString());
                else if (updateType == 3)
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_check_network").ToString());
                }
                else
                {
                    if (updateType != 1 && updateType != 2)
                        return;
                    AppUpdateInfo appUpdateInfo = (AppUpdateInfo)appInfo;
                    if (updateType == 1)
                    {
                        new WindowDialogCommon(0, Application.Current.FindResource((object)"new_version_of_flydigi_space_fm").ToString() + "V" + appUpdateInfo.data.version, appUpdateInfo.data.desc, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                        {
                            FLog.d("Dialog返回结果：" + result.ToString());
                            switch (result)
                            {
                                case 0:
                                    NetworkUtils.FlydigiPostEvent("版本更新点击下次再说");
                                    break;
                                case 1:
                                    NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                                    this.showFmDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version);
                                    if (!this.checkFmTipVersion())
                                        break;
                                    Application.Current.Dispatcher.Invoke(action1 ?? (action1 = (Action)(() =>
                                    {
                                        new WindowDialogCommon(2, Application.Current.FindResource((object)"Calibrate_The_Joystick_title").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_content").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_btn_go").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_btn_ok").ToString(), (IDelegateCallback)(result2 =>
                                        {
                                            if (result2 != 1)
                                                return;
                                            this.openUrl("https://bs.flydigi.com/topicDetail?id=45893");
                                        }))
                                        {
                                            Owner = WindowMain.getInstance()
                                        }.ShowDialog();
                                    })));
                                    break;
                                case 2:
                                    NetworkUtils.FlydigiPostEvent("版本更新点击关闭");
                                    break;
                            }
                        }))
                        {
                            Owner = ((Window)this)
                        }.ShowDialog();
                    }
                    else
                    {
                        if (updateType != 2)
                            return;
                        new WindowDialogCommon(1, Application.Current.FindResource((object)"new_version_of_flydigi_space").ToString() + "V" + appUpdateInfo.data.version_code, appUpdateInfo.data.upgrade_point, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                        {
                            FLog.d("Dialog返回结果：" + result.ToString());
                            if (result != 1)
                                return;
                            NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                            this.showFmDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version_code);
                            if (!this.checkFmTipVersion())
                                return;
                            Application.Current.Dispatcher.Invoke(action2 ?? (action2 = (Action)(() =>
                            {
                                new WindowDialogCommon(2, Application.Current.FindResource((object)"Calibrate_The_Joystick_title").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_content").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_btn_go").ToString(), Application.Current.FindResource((object)"Calibrate_The_Joystick_btn_ok").ToString(), (IDelegateCallback)(result2 =>
                                {
                                    if (result2 != 1)
                                        return;
                                    this.openUrl("https://bs.flydigi.com/topicDetail?id=45893");
                                }))
                                {
                                    Owner = WindowMain.getInstance()
                                }.ShowDialog();
                            })));
                        }))
                        {
                            Owner = ((Window)this)
                        }.ShowDialog();
                    }
                }
            }));
        }

        private void showInstallFmDriverDialog(int updateType, object appInfo) => Application.Current.Dispatcher.Invoke((Action)(() =>
        {
            if (updateType == 0)
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"current_version_is_the_latest").ToString());
            else if (updateType == 3)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_check_network").ToString());
            }
            else
            {
                if (updateType != 1 && updateType != 2)
                    return;
                AppUpdateInfo appUpdateInfo = (AppUpdateInfo)appInfo;
                if (updateType == 1)
                {
                    new WindowDialogCommon(0, Application.Current.FindResource((object)"new_version_of_flydigi_space_fm").ToString() + "V" + appUpdateInfo.data.version, appUpdateInfo.data.desc, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        switch (result)
                        {
                            case 0:
                                NetworkUtils.FlydigiPostEvent("版本更新点击下次再说");
                                break;
                            case 1:
                                NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                                this.showDongleDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version);
                                break;
                            case 2:
                                NetworkUtils.FlydigiPostEvent("版本更新点击关闭");
                                break;
                        }
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
                else
                {
                    if (updateType != 2)
                        return;
                    new WindowDialogCommon(1, Application.Current.FindResource((object)"new_version_of_flydigi_space").ToString() + "V" + appUpdateInfo.data.version_code, appUpdateInfo.data.upgrade_point, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        if (result != 1)
                            return;
                        NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                        this.showDongleDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version_code);
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
            }
        }));

        private void showDongleUpdateDialog(int updateType, object appInfo) => Application.Current.Dispatcher.Invoke((Action)(() =>
        {
            if (updateType == 0)
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"current_version_is_the_latest").ToString());
            else if (updateType == 3)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_check_network").ToString());
            }
            else
            {
                if (updateType != 1 && updateType != 2)
                    return;
                AppUpdateInfo appUpdateInfo = (AppUpdateInfo)appInfo;
                if (updateType == 1)
                {
                    new WindowDialogCommon(0, Application.Current.FindResource((object)"new_version_of_flydigi_space_fm").ToString() + "V" + appUpdateInfo.data.version, appUpdateInfo.data.desc, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        switch (result)
                        {
                            case 0:
                                NetworkUtils.FlydigiPostEvent("版本更新点击下次再说");
                                break;
                            case 1:
                                NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                                this.showDongleDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version);
                                break;
                            case 2:
                                NetworkUtils.FlydigiPostEvent("版本更新点击关闭");
                                break;
                        }
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
                else
                {
                    if (updateType != 2)
                        return;
                    new WindowDialogCommon(1, Application.Current.FindResource((object)"new_version_of_flydigi_space").ToString() + "V" + appUpdateInfo.data.version_code, appUpdateInfo.data.upgrade_point, Application.Current.FindResource((object)"start_app").ToString(), "", (IDelegateCallback)(result =>
                    {
                        FLog.d("Dialog返回结果：" + result.ToString());
                        if (result != 1)
                            return;
                        NetworkUtils.FlydigiPostEvent("版本更新点击立即更新");
                        this.showDongleDownloadWindow(appUpdateInfo.data.apk_url, appUpdateInfo.data.version_code);
                    }))
                    {
                        Owner = ((Window)this)
                    }.ShowDialog();
                }
            }
        }));

        private void showFmDownloadWindow(string url, string version)
        {
            if (DataManager.getInstance().getDeviceInfo().cpuType == "nordic")
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"The_handle_does_not_support_PC_firmware_upgrade").ToString());
            }
            else
            {
                FLog.d("open flydigi firmware tool params:" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "_ch573_" + NetworkUtils.getCurrentLanguage() + "_" + DataManager.getInstance().getDeviceInfo().FirmwareVersion);
                string arguments = DataManager.getInstance().getDeviceInfo().getFirmwareName() + "_ch573_" + NetworkUtils.getCurrentLanguage() + "_" + DataManager.getInstance().getDeviceInfo().FirmwareVersion;
                try
                {
                    Process.Start(".\\tools\\MFCFlydigiPCFmTool.exe", arguments);
                }
                catch (Exception ex)
                {
                    FLog.d("PC固件升级工具打开异常:" + ex.Message);
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"open_pc_error").ToString());
                }
            }
        }

        private void showDongleDownloadWindow(string url, string version)
        {
            if (DataManager.getInstance().getDeviceInfo().cpuType == "nordic")
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"The_handle_does_not_support_PC_firmware_upgrade").ToString());
            }
            else
            {
                FLog.d("open flydigi firmware tool params:" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "_ch571_" + NetworkUtils.getCurrentLanguage() + "_" + DataManager.getInstance().getDeviceInfo().FirmwareVersion);
                string arguments = DataManager.getInstance().getDeviceInfo().getFirmwareName() + "_ch571_" + NetworkUtils.getCurrentLanguage() + "_" + DataManager.getInstance().getDeviceInfo().FirmwareVersion;
                try
                {
                    Process.Start(".\\tools\\MFCFlydigiPCFmTool.exe", arguments);
                }
                catch (Exception ex)
                {
                    FLog.d("PC固件升级工具打开异常:" + ex.Message);
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"open_pc_error").ToString());
                }
            }
        }

        private void initLanguage()
        {
            List<ResourceDictionary> source = new List<ResourceDictionary>();
            foreach (ResourceDictionary mergedDictionary in Application.Current.Resources.MergedDictionaries)
                source.Add(mergedDictionary);
            string requestedCulture = string.Format("Language\\StringResource.{0}.xaml", (object)CultureInfo.CurrentCulture);
            FLog.d("当前区域：" + requestedCulture);
            FLog.d("current area：" + requestedCulture);
            ResourceDictionary resourceDictionary = source.FirstOrDefault<ResourceDictionary>((Func<ResourceDictionary, bool>)(d => d.Source.OriginalString.Equals(requestedCulture)));
            if (resourceDictionary == null)
            {
                FLog.d("Language select：use default language");
                requestedCulture = "Language\\StringResource.xaml";
                resourceDictionary = source.FirstOrDefault<ResourceDictionary>((Func<ResourceDictionary, bool>)(d => d.Source.OriginalString.Equals(requestedCulture)));
            }
            if (resourceDictionary == null)
                return;
            this.currentLanguage = !requestedCulture.ToLower().Contains("zh-cn") ? (!requestedCulture.ToLower().Contains("ko") ? "en" : "ko-KR") : "zh";
            FLog.d("currentLanguage：" + this.currentLanguage);
            NetworkUtils.setCurrentLanguage(this.currentLanguage);
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            FLog.d("The end language selected：" + resourceDictionary.Source.ToString());
        }

        private void ButtonGoChangeLanguageEn(object sender, RoutedEventArgs e)
        {
            this.currentLanguage = "en";
            this.SetLanguage(this.currentLanguage);
            FLog.d("切换到语言：" + this.currentLanguage);
        }

        private void ButtonGoChangeLanguageZh(object sender, RoutedEventArgs e)
        {
            this.currentLanguage = "zh";
            this.SetLanguage(this.currentLanguage);
            FLog.d("切换到语言：" + this.currentLanguage);
        }

        private void mLangComboBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (this.mLangComboBox.SelectedIndex == 0)
            {
                this.currentLanguage = "zh";
                this.SetLanguage(this.currentLanguage);
            }
            else if (this.mLangComboBox.SelectedIndex == 1)
            {
                this.currentLanguage = "en";
                this.SetLanguage(this.currentLanguage);
            }
            else
            {
                this.currentLanguage = "ko-KR";
                this.SetLanguage(this.currentLanguage);
            }
            FLog.d("切换到语言：" + this.currentLanguage);
        }

        public void SetLanguage(string lg)
        {
            List<ResourceDictionary> source = new List<ResourceDictionary>();
            foreach (ResourceDictionary mergedDictionary in Application.Current.Resources.MergedDictionaries)
                source.Add(mergedDictionary);
            string requestedCulture = string.Format("Language\\StringResource.{0}.xaml", (object)lg);
            ResourceDictionary resourceDictionary = source.FirstOrDefault<ResourceDictionary>((Func<ResourceDictionary, bool>)(d => d.Source.OriginalString.Equals(requestedCulture)));
            if (resourceDictionary != null)
            {
                NetworkUtils.setCurrentLanguage(this.currentLanguage);
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                FLog.d("最终语言选择：" + resourceDictionary.Source.ToString());
                FLog.d("mWindowDialogConnect:" + this.connectGuidePage?.ToString());
            }
            if (this.connectGuidePage != null)
            {
                FLog.d("update UI");
                this.connectGuidePage.mGuide.updateUI();
            }
            else
            {
                if (this.mGuide == null)
                    return;
                this.mGuide.updateUI();
            }
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowmain.xaml", UriKind.Relative));
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
        //            ((UIElement)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
        //            ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
        //            ((FrameworkElement)target).Initialized += new EventHandler(this.Window_Initialized);
        //            ((Window)target).Deactivated += new EventHandler(this.appDeactivated);
        //            ((Window)target).StateChanged += new EventHandler(this.appStateChanged);
        //            break;
        //        case 2:
        //            this.mTopBar = (UserControlTopBar)target;
        //            break;
        //        case 3:
        //            this.mGuide = (UserControlDialogConnect)target;
        //            break;
        //        case 4:
        //            this.mFrame = (System.Windows.Controls.Frame)target;
        //            this.mFrame.Navigated += new NavigatedEventHandler(this.mFrame_Navigated);
        //            break;
        //        case 5:
        //            this.mLayoutApplyTips = (StackPanel)target;
        //            break;
        //        case 6:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.ButtonCloseApplyTips);
        //            break;
        //        case 7:
        //            this.mImageClose = (Image)target;
        //            break;
        //        case 8:
        //            this.mLayoutInfo = (Grid)target;
        //            break;
        //        case 9:
        //            this.mInfoAppVersion = (Label)target;
        //            break;
        //        case 10:
        //            this.CheckAppVersion = (Button)target;
        //            this.CheckAppVersion.Click += new RoutedEventHandler(this.ButtonCheckAppVersion);
        //            break;
        //        case 11:
        //            this.mInfoFirmwareVersion = (Label)target;
        //            break;
        //        case 12:
        //            this.CheckAppVersion1 = (Button)target;
        //            this.CheckAppVersion1.Click += new RoutedEventHandler(this.ButtonCheckFmVersion);
        //            break;
        //        case 13:
        //            this.mInfoDongleVersion = (Label)target;
        //            break;
        //        case 14:
        //            this.mInfoDongleVersionType0 = (Label)target;
        //            break;
        //        case 15:
        //            this.mInfoDongleVersionType1 = (Label)target;
        //            break;
        //        case 16:
        //            this.mInfoDongleVersionType2 = (Label)target;
        //            break;
        //        case 17:
        //            this.CheckAppVersion2 = (Button)target;
        //            this.CheckAppVersion2.Click += new RoutedEventHandler(this.ButtonCheckDongleVersion);
        //            break;
        //        case 18:
        //            this.mInfoQQ = (Label)target;
        //            break;
        //        case 19:
        //            this.GoWeixin = (Button)target;
        //            this.GoWeixin.Click += new RoutedEventHandler(this.ButtonGoWeixin);
        //            break;
        //        case 20:
        //            this.GoHelpCenter = (Button)target;
        //            this.GoHelpCenter.Click += new RoutedEventHandler(this.ButtonGoHelpCenter);
        //            break;
        //        case 21:
        //            this.mLanguageBox = (Grid)target;
        //            break;
        //        case 22:
        //            this.mLangComboBox = (ComboBox)target;
        //            this.mLangComboBox.SelectionChanged += new SelectionChangedEventHandler(this.mLangComboBox_TextChanged);
        //            break;
        //        case 23:
        //            this.LogButtonObj = (CheckBox)target;
        //            this.LogButtonObj.Click += new RoutedEventHandler(this.ButtonGoSwitchLog);
        //            break;
        //        case 24:
        //            this.mLayoutFunctionDesc = (StackPanel)target;
        //            break;
        //        case 25:
        //            this.mFunctionDesc = (TextBlock)target;
        //            break;
        //        case 26:
        //            this.mLoading = (Grid)target;
        //            break;
        //        case 27:
        //            this.mLoadingLabel = (Label)target;
        //            break;
        //        case 28:
        //            this.mTips = (Border)target;
        //            break;
        //        case 29:
        //            this.mSuccessIcon = (TextBlock)target;
        //            break;
        //        case 30:
        //            this.mFailIcon = (TextBlock)target;
        //            break;
        //        case 31:
        //            this.mTipsLabel = (Label)target;
        //            break;
        //        case 32:
        //            this.mLongTips = (Border)target;
        //            break;
        //        case 33:
        //            this.mLongIcon = (TextBlock)target;
        //            break;
        //        case 34:
        //            this.mLongTipsLabel = (Label)target;
        //            break;
        //        case 35:
        //            this.mApplyConfigNotice = (Grid)target;
        //            break;
        //        case 36:
        //            this.mApplyButtonCancel = (Button)target;
        //            this.mApplyButtonCancel.Click += new RoutedEventHandler(this.mApplyButtonCancel_Click);
        //            break;
        //        case 37:
        //            this.mApplyButtonOK = (Button)target;
        //            this.mApplyButtonOK.Click += new RoutedEventHandler(this.mApplyButtonOK_Click);
        //            break;
        //        case 38:
        //            this.mApplyDefaultConfigNotice = (Grid)target;
        //            break;
        //        case 39:
        //            this.mApplyDefaultConfigButtonCancel = (Button)target;
        //            this.mApplyDefaultConfigButtonCancel.Click += new RoutedEventHandler(this.mApplyDefaultConfigButtonCancel_Click);
        //            break;
        //        case 40:
        //            this.mApplyDefaultConfigButtonOK = (Button)target;
        //            this.mApplyDefaultConfigButtonOK.Click += new RoutedEventHandler(this.mApplyDefaultConfigButtonOK_Click);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
