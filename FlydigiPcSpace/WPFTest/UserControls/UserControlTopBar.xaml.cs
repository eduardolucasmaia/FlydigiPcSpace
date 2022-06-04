// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlTopBar
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.Activitys;
using WPFTest.Utils;
using WPFTest.Windows;

namespace WPFTest.UserControls
{
    public partial class UserControlTopBar : UserControl, IComponentConnector
    {
        private Window mWindow;
        private System.Windows.Controls.Frame mFrame;
        private IDelegateCallback mIDelegateCallback;
        private int mDeviceConnected;
        private int mConfigIndex;
        public const int BACK = 0;
        public const int CONFIG_TEST = 1;
        public const int CONFIG_APPLY_START = 2;
        public const int CONFIG_APPLY_SUCCESSED = 3;
        public const int CONFIG_APPLY_FAILED = 4;
        public const int CONFIG_SAVE_SUCCESSED = 10;
        public const int DEVICE_INFO = 5;
        public const int SAVE_CONFIG_NOTICE = 6;
        public const int TOOLBAR_TYPE_SHOW_LOADING = 7;
        public const int CONFIG_RESET = 8;
        public const int RESET_DEFAULT_CONFIG = 9;
        public const int PAGE_SPLASH = 0;
        public const int PAGE_CONFIG_LIST = 1;
        public const int PAGE_CONFIG_SETTING = 2;
        //internal Image mDeviceState;
        //internal Button btn_min;
        //internal Image btn_min_image;
        //internal Button btn_close;
        //internal Image btn_close_image;
        //internal Button logo;
        //internal Button back;
        //internal Label mCurrentConfigName;
        //internal Label mCurrentModeName;
        //internal Label mLineConfigReset;
        //internal Button mButtonConfigReset;
        //internal Label mLabelConfigReset;
        //internal Image mImageConfigReset;
        //internal Label mLineConfigTest;
        //internal Button mButtonConfigTest;
        //internal Label mLabelConfigTest;
        //internal Image mImageConfigTest;
        //internal Label mLineConfigApply;
        //internal Button mButtonConfigApply;
        //internal Label mLabelConfigApply;
        //internal Image mImageConfigApply;
        //internal Button mButtonInfo;
        private bool _contentLoaded;

        public int mCurrentModeType { get; set; }

        public UserControlTopBar() => this.InitializeComponent();

        public void setWindowAndDelegate(
          Window window,
          System.Windows.Controls.Frame frame,
          IDelegateCallback iDelegateCallback)
        {
            this.mWindow = window;
            this.mFrame = frame;
            this.mIDelegateCallback = iDelegateCallback;
        }

        public void setDeviceState(int deviceState)
        {
            this.mDeviceConnected = deviceState;
            if (deviceState == -1)
                this.mDeviceState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_unconnect.png"));
            else
                this.mDeviceState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_connect.png"));
        }

        public void refreshCurrentPage(int pageId)
        {
            switch (pageId)
            {
                case 0:
                    this.logo.Visibility = Visibility.Visible;
                    this.back.Visibility = Visibility.Hidden;
                    this.mLineConfigReset.Visibility = Visibility.Hidden;
                    this.mButtonConfigReset.Visibility = Visibility.Hidden;
                    this.mLineConfigTest.Visibility = Visibility.Hidden;
                    this.mButtonConfigTest.Visibility = Visibility.Hidden;
                    this.mLineConfigApply.Visibility = Visibility.Hidden;
                    this.mButtonConfigApply.Visibility = Visibility.Hidden;
                    ((WindowMain)this.mWindow).updateTipsState(false);
                    this.mCurrentConfigName.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    this.logo.Visibility = Visibility.Hidden;
                    this.back.Visibility = Visibility.Visible;
                    this.mLineConfigReset.Visibility = Visibility.Visible;
                    this.mButtonConfigReset.Visibility = Visibility.Visible;
                    this.mLineConfigTest.Visibility = Visibility.Visible;
                    this.mButtonConfigTest.Visibility = Visibility.Visible;
                    this.mLineConfigApply.Visibility = Visibility.Visible;
                    this.mButtonConfigApply.Visibility = Visibility.Visible;
                    ((WindowMain)this.mWindow).updateTipsState(true);
                    this.mButtonConfigReset.IsEnabled = true;
                    this.mLabelConfigReset.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mImageConfigReset.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_reset_defaults.png"));
                    this.mButtonConfigTest.IsEnabled = true;
                    this.mLabelConfigTest.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mImageConfigTest.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_test_white.png"));
                    this.mButtonConfigApply.IsEnabled = true;
                    this.mLabelConfigApply.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mCurrentConfigName.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    this.logo.Visibility = Visibility.Hidden;
                    this.back.Visibility = Visibility.Visible;
                    this.mLineConfigReset.Visibility = Visibility.Visible;
                    this.mButtonConfigReset.Visibility = Visibility.Visible;
                    this.mLineConfigTest.Visibility = Visibility.Visible;
                    this.mButtonConfigTest.Visibility = Visibility.Visible;
                    this.mLineConfigApply.Visibility = Visibility.Visible;
                    this.mButtonConfigApply.Visibility = Visibility.Visible;
                    ((WindowMain)this.mWindow).updateTipsState(true);
                    this.mButtonConfigReset.IsEnabled = true;
                    this.mLabelConfigReset.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mButtonConfigTest.IsEnabled = true;
                    this.mLabelConfigTest.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mButtonConfigApply.IsEnabled = true;
                    this.mLabelConfigApply.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                    this.mCurrentConfigName.Visibility = Visibility.Hidden;
                    break;
            }
        }

        public void showModeName(string name)
        {
            this.mCurrentModeName.Visibility = Visibility.Visible;
            this.mCurrentModeName.Content = (object)Application.Current.FindResource((object)name).ToString();
        }

        public void hideModeName() => this.mCurrentModeName.Visibility = Visibility.Hidden;

        private static void ven_OnmCurrentModeTypeChange(object sender, EventArgs e)
        {
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            if (this.mWindow == null)
                return;
            this.mWindow.WindowState = WindowState.Minimized;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            DataManager.getInstance().SendByteArray(DongleCommand.getSTDControl(true));
            CommonUtils.ApplicationExit();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.mDeviceConnected == -1)
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_connect_gamepad_first").ToString());
            else if (this.mFrame.NavigationService.Content is PageConfigSetting)
            {
                PageConfigSetting content = (PageConfigSetting)this.mFrame.NavigationService.Content;
                if (content.getKeySettingState() == 2 && !content.ApplyKeySetting())
                    return;
                if (content.checkConfigChange())
                {
                    if (this.mIDelegateCallback == null)
                        return;
                    this.mIDelegateCallback(6);
                }
                else
                {
                    if (this.mIDelegateCallback == null)
                        return;
                    this.mIDelegateCallback(0);
                }
            }
            else
            {
                this.hideModeName();
                if (this.mIDelegateCallback == null)
                    return;
                this.mIDelegateCallback(0);
            }
        }

        private void logo_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mButtonConfigReset_Click_old(object sender, RoutedEventArgs e)
        {
            WindowDialogCommon windowDialogCommon = new WindowDialogCommon(2, Application.Current.FindResource((object)"config_reset").ToString(), Application.Current.FindResource((object)"config_reset_notice").ToString(), Application.Current.FindResource((object)"ok").ToString(), Application.Current.FindResource((object)"cancel").ToString(), (IDelegateCallback)(result =>
            {
                if (result != 1)
                    return;
                this.applyDefualtConfig();
            }));
            windowDialogCommon.Owner = WindowMain.getInstance();
            windowDialogCommon.ShowDialog();
        }

        private void mButtonConfigReset_Click(object sender, RoutedEventArgs e)
        {
            if (this.mDeviceConnected == -1)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_connect_gamepad_first").ToString());
            }
            else
            {
                if (this.mIDelegateCallback == null)
                    return;
                this.mIDelegateCallback(9);
            }
        }

        public void applyDefualtConfig()
        {
            bool flag = false;
            if (this.mFrame.NavigationService.Content is PageConfigList)
            {
                PageConfigList content = (PageConfigList)this.mFrame.NavigationService.Content;
                if (content.getCurrentReadingState())
                {
                    if (this.mIDelegateCallback == null)
                        return;
                    this.mIDelegateCallback(7);
                    return;
                }
                flag = content.ApplyDefaultConfig(this.mConfigIndex);
            }
            else if (this.mFrame.NavigationService.Content is PageConfigSetting)
                flag = ((PageConfigSetting)this.mFrame.NavigationService.Content).ApplyDefaultConfig(this.mConfigIndex);
            if (flag)
                CommonUtils.showCommonTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"reset_config_success").ToString(), "success");
            else
                CommonUtils.showCommonTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"reset_config_fail").ToString(), "success");
        }

        private void mButtonConfigTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.mDeviceConnected == -1)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_connect_gamepad_first").ToString());
            }
            else
            {
                if (this.mFrame.NavigationService.Content is PageConfigList)
                {
                    if (((PageConfigList)this.mFrame.NavigationService.Content).getCurrentReadingState())
                    {
                        if (this.mIDelegateCallback == null)
                            return;
                        this.mIDelegateCallback(7);
                        return;
                    }
                }
                else if (this.mFrame.NavigationService.Content is PageConfigSetting)
                {
                    PageConfigSetting content = (PageConfigSetting)this.mFrame.NavigationService.Content;
                    if (content.getKeySettingState() == 2 && !content.ApplyKeySetting())
                        return;
                }
                if (this.mIDelegateCallback == null)
                    return;
                this.mIDelegateCallback(1);
            }
        }

        public void ConfigSave(object sender, RoutedEventArgs e)
        {
            if (this.mDeviceConnected == -1)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_connect_gamepad_first").ToString());
            }
            else
            {
                DataManager.getInstance().delayCheckDeviceState();
                if (this.mFrame.NavigationService.Content is PageConfigList)
                {
                    PageConfigList content = (PageConfigList)this.mFrame.NavigationService.Content;
                    if (content.getCurrentReadingState())
                    {
                        if (this.mIDelegateCallback == null)
                            return;
                        this.mIDelegateCallback(7);
                    }
                    else
                        content.SaveCurrentConfig(this.mConfigIndex, (IDelegateCallback)(result =>
                        {
                            switch (result)
                            {
                                case 0:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(2);
                                    break;
                                case 1:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(10);
                                    break;
                                case 2:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(4);
                                    break;
                            }
                        }));
                }
                else if (this.mFrame.NavigationService.Content is PageConfigSetting)
                {
                    PageConfigSetting content = (PageConfigSetting)this.mFrame.NavigationService.Content;
                    if (content.getKeySettingState() == 2 && !content.ApplyKeySetting())
                        return;
                    content.ConfigApply((IDelegateCallback)(result =>
                    {
                        switch (result)
                        {
                            case 0:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(2);
                                break;
                            case 1:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(10);
                                break;
                        }
                    }));
                }
                else
                    ((PageConfigTest)this.mFrame.NavigationService.Content).ApplyCurrentConfig(this.mConfigIndex, (IDelegateCallback)(result =>
                    {
                        switch (result)
                        {
                            case 0:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(2);
                                break;
                            case 1:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(10);
                                break;
                            case 2:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(4);
                                break;
                        }
                    }));
            }
        }

        public void ButtonConfigApply_Click(object sender, RoutedEventArgs e)
        {
            if (this.mDeviceConnected == -1)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_connect_gamepad_first").ToString());
            }
            else
            {
                DataManager.getInstance().delayCheckDeviceState();
                if (this.mFrame.NavigationService.Content is PageConfigList)
                {
                    PageConfigList content = (PageConfigList)this.mFrame.NavigationService.Content;
                    if (content.getCurrentReadingState())
                    {
                        if (this.mIDelegateCallback == null)
                            return;
                        this.mIDelegateCallback(7);
                    }
                    else
                        content.ApplyCurrentConfig(this.mConfigIndex, (IDelegateCallback)(result =>
                        {
                            switch (result)
                            {
                                case 0:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(2);
                                    break;
                                case 1:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(3);
                                    break;
                                case 2:
                                    if (this.mIDelegateCallback == null)
                                        break;
                                    this.mIDelegateCallback(4);
                                    break;
                            }
                        }));
                }
                else if (this.mFrame.NavigationService.Content is PageConfigSetting)
                {
                    PageConfigSetting content = (PageConfigSetting)this.mFrame.NavigationService.Content;
                    if (content.getKeySettingState() == 2 && !content.ApplyKeySetting())
                        return;
                    content.ConfigApply((IDelegateCallback)(result =>
                    {
                        switch (result)
                        {
                            case 0:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(2);
                                break;
                            case 1:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(3);
                                break;
                        }
                    }));
                }
                else
                    ((PageConfigTest)this.mFrame.NavigationService.Content).ApplyCurrentConfig(this.mConfigIndex, (IDelegateCallback)(result =>
                    {
                        switch (result)
                        {
                            case 0:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(2);
                                break;
                            case 1:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(3);
                                break;
                            case 2:
                                if (this.mIDelegateCallback == null)
                                    break;
                                this.mIDelegateCallback(4);
                                break;
                        }
                    }));
            }
        }

        private void mButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(5);
        }

        public void BackLastPage()
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        public void updateConfigId(int configId) => this.mConfigIndex = configId;

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroltopbar.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mDeviceState = (Image)target;
        //            break;
        //        case 2:
        //            this.btn_min = (Button)target;
        //            this.btn_min.Click += new RoutedEventHandler(this.btn_min_Click);
        //            break;
        //        case 3:
        //            this.btn_min_image = (Image)target;
        //            break;
        //        case 4:
        //            this.btn_close = (Button)target;
        //            this.btn_close.Click += new RoutedEventHandler(this.btn_close_Click);
        //            break;
        //        case 5:
        //            this.btn_close_image = (Image)target;
        //            break;
        //        case 6:
        //            this.logo = (Button)target;
        //            this.logo.Click += new RoutedEventHandler(this.logo_Click);
        //            break;
        //        case 7:
        //            this.back = (Button)target;
        //            this.back.Click += new RoutedEventHandler(this.back_Click);
        //            break;
        //        case 8:
        //            this.mCurrentConfigName = (Label)target;
        //            break;
        //        case 9:
        //            this.mCurrentModeName = (Label)target;
        //            break;
        //        case 10:
        //            this.mLineConfigReset = (Label)target;
        //            break;
        //        case 11:
        //            this.mButtonConfigReset = (Button)target;
        //            this.mButtonConfigReset.Click += new RoutedEventHandler(this.mButtonConfigReset_Click);
        //            break;
        //        case 12:
        //            this.mLabelConfigReset = (Label)target;
        //            break;
        //        case 13:
        //            this.mImageConfigReset = (Image)target;
        //            break;
        //        case 14:
        //            this.mLineConfigTest = (Label)target;
        //            break;
        //        case 15:
        //            this.mButtonConfigTest = (Button)target;
        //            this.mButtonConfigTest.Click += new RoutedEventHandler(this.mButtonConfigTest_Click);
        //            break;
        //        case 16:
        //            this.mLabelConfigTest = (Label)target;
        //            break;
        //        case 17:
        //            this.mImageConfigTest = (Image)target;
        //            break;
        //        case 18:
        //            this.mLineConfigApply = (Label)target;
        //            break;
        //        case 19:
        //            this.mButtonConfigApply = (Button)target;
        //            this.mButtonConfigApply.Click += new RoutedEventHandler(this.ButtonConfigApply_Click);
        //            break;
        //        case 20:
        //            this.mLabelConfigApply = (Label)target;
        //            break;
        //        case 21:
        //            this.mImageConfigApply = (Image)target;
        //            break;
        //        case 22:
        //            this.mButtonInfo = (Button)target;
        //            this.mButtonInfo.Click += new RoutedEventHandler(this.mButtonInfo_Click);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
