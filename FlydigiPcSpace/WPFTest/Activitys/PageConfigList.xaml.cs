// Decompiled with JetBrains decompiler
// Type: WPFTest.Activitys.PageConfigList
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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;
using WPFTest.Windows;

namespace WPFTest.Activitys
{
    public partial class PageConfigList : Page, IComponentConnector
    {
        private WindowMain mWindowMain;
        private System.Windows.Controls.Frame mFrame;
        private Dictionary<int, ConfigBean> mDictionaryConfig = new Dictionary<int, ConfigBean>();
        private int mPageIndex;
        private LedBean mLed;
        private bool mLive = true;
        private int Led_Continue_Time = 500;
        private bool mReadingState;
        //internal Grid mGrid;
        //internal Image mBgImage;
        //internal StackPanel mLayoutSwitchCFGTips;
        //internal Image mImgMenu;
        //internal Image mImgBack;
        //internal Label mLabelTipsSwitch;
        //internal Button mButtonConfig1;
        //internal Button mButtonConfig2;
        //internal Button mButtonConfig3;
        //internal Button mButtonConfig4;
        //internal UserControlConfig mLayoutConfig;
        //private bool _contentLoaded;

        public PageConfigList(WindowMain window, System.Windows.Controls.Frame frame)
        {
            this.mWindowMain = window;
            this.mFrame = frame;
            this.InitializeComponent();
            this.updateUI();
            NetworkUtils.FlydigiPostEvent("进入配置列表页");
        }

        protected void OnDeactivated(EventArgs e)
        {
            FLog.d("hide");
            this.mLive = false;
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
                    this.mBgImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_config_list.png"));
                    this.mImgMenu.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_menu.png"));
                    this.mImgBack.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_back.png"));
                    this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 360.0, 0.0, 0.0);
                    this.mImgMenu.Margin = new Thickness(0.0, 0.0, 74.0, 0.0);
                    this.mLabelTipsSwitch.Content = (object)Application.Current.FindResource((object)"tips_keys_change_config_f1").ToString();
                    if (this.mWindowMain.mTopBar.mCurrentModeType != 2)
                        return;
                    this.mButtonConfig2.Visibility = Visibility.Hidden;
                    this.mButtonConfig3.Visibility = Visibility.Hidden;
                    this.mButtonConfig4.Visibility = Visibility.Hidden;
                    this.mButtonConfig1.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.mBgImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_config_list.png"));
                    this.mImgMenu.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_a2_menu.png"));
                    this.mImgBack.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_a2_back.png"));
                    this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 392.0, 0.0, 0.0);
                    this.mImgMenu.Margin = new Thickness(0.0, 0.0, 61.0, 0.0);
                    this.mLabelTipsSwitch.Content = (object)Application.Current.FindResource((object)"tips_keys_change_config_a2").ToString();
                }
            }
            else
            {
                this.mBgImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_config_list.png"));
                this.mImgMenu.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_menu.png"));
                this.mImgBack.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_back.png"));
                this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 360.0, 0.0, 0.0);
                this.mImgMenu.Margin = new Thickness(0.0, 0.0, 74.0, 0.0);
                this.mLabelTipsSwitch.Content = (object)Application.Current.FindResource((object)"tips_keys_change_config_f1").ToString();
            }
        }

        private void updateLed() => new Thread((ThreadStart)(() =>
        {
            while (this.mLive)
            {
                if (this.mLed == null || this.mLed.Light == 0)
                {
                    this.setBackgroundColor(0, 0, 0, 0);
                    Thread.Sleep(this.Led_Continue_Time);
                }
                else
                {
                    int millisecondsTimeout = 3000 / (this.mLed.Light * 2);
                    int light = this.mLed.Light;
                    switch (this.mLed.Mode)
                    {
                        case 0:
                            this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                            Thread.Sleep(this.Led_Continue_Time);
                            continue;
                        case 1:
                            for (int index = 0; index < 2; ++index)
                            {
                                List<int> intList = new List<int>();
                                if (index == 0)
                                    intList.AddRange((IEnumerable<int>)this.mLed.RgbColor0);
                                else
                                    intList.AddRange((IEnumerable<int>)this.mLed.RgbColor1);
                                for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 1 && this.mLed.Light == light; ++alpha)
                                {
                                    if (alpha <= this.mLed.Light)
                                        this.setBackgroundColor(alpha, intList[0], intList[1], intList[2]);
                                    else
                                        this.setBackgroundColor(this.mLed.Light * 2 - alpha, intList[0], intList[1], intList[2]);
                                    Thread.Sleep(millisecondsTimeout);
                                }
                            }
                            continue;
                        case 2:
                            this.setBackgroundColor(0, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                            Thread.Sleep(this.Led_Continue_Time);
                            continue;
                        case 3:
                            this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                            Thread.Sleep(this.Led_Continue_Time);
                            continue;
                        case 4:
                            for (int index = 0; index < 7; ++index)
                            {
                                List<int> rgbColorById = CommonUtils.getRGBColorById(index + 1);
                                for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 4 && this.mLed.Light == light && this.Visibility == Visibility.Visible; ++alpha)
                                {
                                    if (alpha <= this.mLed.Light)
                                        this.setBackgroundColor(alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                    else
                                        this.setBackgroundColor(this.mLed.Light * 2 - alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                    Thread.Sleep(millisecondsTimeout);
                                }
                            }
                            continue;
                        default:
                            continue;
                    }
                }
            }
        })).Start();

        private void setBackgroundColor(int alpha, int r, int g, int b) => Application.Current.Dispatcher.Invoke((Action)(() => this.mGrid.Background = (Brush)new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)r, (byte)g, (byte)b))));

        private void Page_Initialized(object sender, EventArgs e)
        {
            FLog.d("PageConfigList----Page_Initialized");
            FLog.d("mDictionaryConfig.Count：" + this.mDictionaryConfig.Count.ToString());
            if (this.mDictionaryConfig.Count == 0)
            {
                DataManager.getInstance().getListConfig().Clear();
                this.mWindowMain.showLoading(true);
                new Thread((ThreadStart)(() =>
                {
                    bool readSuccessed = false;
                    int num = 0;
                    while (num < 2)
                    {
                        ++num;
                        if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
                        {
                            int configId = 4;
                            readSuccessed = true;
                            FLog.d("当前手柄固定的配置id：" + configId.ToString());
                            Application.Current.Dispatcher.Invoke((Action)(() => this.ReadingConfig(configId)));
                        }
                        else
                            DataManager.getInstance().getCurrentConfigId(DongleCommand.getCurrentConfigIdCommand(), (IDelegateCallback)(configId =>
                            {
                                readSuccessed = true;
                                FLog.d("获取到当前手柄的配置id：" + configId.ToString());
                                if (configId > 3)
                                {
                                    configId = 0;
                                    FLog.d("转换后手柄的配置id：" + configId.ToString());
                                }
                                Application.Current.Dispatcher.Invoke((Action)(() => this.ReadingConfig(configId)));
                            }));
                        for (int index = 0; index < 5 && !readSuccessed; ++index)
                            Thread.Sleep(100);
                        if (readSuccessed)
                            break;
                    }
                    if (this.mWindowMain.mTopBar.mCurrentModeType == 2 || readSuccessed)
                        return;
                    Application.Current.Dispatcher.Invoke((Action)(() => this.ReadingConfig(0)));
                })).Start();
            }
            this.updateLed();
            this.mLayoutConfig.setDelegate((IDelegateCallback)(result =>
            {
                if (DataManager.getInstance().getDeviceConnectState() == -1)
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), "手柄未连接");
                else if (this.mReadingState)
                {
                    FLog.d("mLayoutConfig.setDelegate");
                    this.mWindowMain.showLoading(true);
                }
                else if (!this.mDictionaryConfig.ContainsKey(this.mPageIndex))
                {
                    this.ReadingConfig(this.mPageIndex);
                }
                else
                {
                    NetworkUtils.FlydigiPostEvent("点击修改配置" + (this.mPageIndex + 1).ToString());
                    FLog.d("点击修改配置" + (this.mPageIndex + 1).ToString() + JsonConvert.SerializeObject((object)this.mDictionaryConfig[this.mPageIndex]));
                    if (this.mFrame == null)
                        return;
                    try
                    {
                        this.mFrame.Navigate((object)new PageConfigSetting(this.mWindowMain, this.mFrame, this.mDictionaryConfig[this.mPageIndex].Clone(), this.mPageIndex));
                    }
                    catch (Exception ex)
                    {
                        NetworkUtils.FlydigiPostEvent("进入修改配置页异常" + (this.mPageIndex + 1).ToString() + ex.Message);
                    }
                }
            }));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FLog.d("PageConfigList----Page_Loaded");
            if (!this.mDictionaryConfig.ContainsKey(this.mPageIndex))
                return;
            this.mLayoutConfig.setData(this.mDictionaryConfig[this.mPageIndex]);
            this.mLed = this.mDictionaryConfig[this.mPageIndex].Led;
            this.mLive = true;
            this.updateLed();
        }

        private void ReadingConfig(int configId)
        {
            ((WindowMain)WindowMain.getInstance()).updateConfigId(configId);
            FLog.d(nameof(ReadingConfig));
            this.mWindowMain.showLoading(true);
            if (this.mReadingState)
                return;
            this.mReadingState = true;
            this.mWindowMain.setConfigReading(this.mReadingState);
            ThreadStart threadStart = null;
            new Thread((ParameterizedThreadStart)(o =>
            {
                bool readSuccessed = false;
                int num = 0;
                while (num < 3)
                {
                    ++num;
                    if (!DataManager.getInstance().getListConfig().ContainsKey(configId))
                    {
                        DataManager.getInstance().readConfig(DongleCommand.getReadConfigCommand(configId), configId, true, (IDelegateCallback)(result =>
                        {
                            FLog.d("配置" + configId.ToString() + "读取完成回调");
                            readSuccessed = true;
                            if (result != configId)
                                return;
                            new Thread(threadStart ?? (threadStart = (ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                this.mWindowMain.showLoading(false);
                                this.mDictionaryConfig = DataManager.getInstance().getListConfig();
                                this.mLed = this.mDictionaryConfig[configId].Led;
                                switch (configId)
                                {
                                    case 0:
                                        this.ButtonConfig1_Click((object)null, (RoutedEventArgs)null);
                                        break;
                                    case 1:
                                        this.ButtonConfig2_Click((object)null, (RoutedEventArgs)null);
                                        break;
                                    case 2:
                                        this.ButtonConfig3_Click((object)null, (RoutedEventArgs)null);
                                        break;
                                    case 3:
                                        this.ButtonConfig4_Click((object)null, (RoutedEventArgs)null);
                                        break;
                                    case 4:
                                        this.ButtonConfig1_Click((object)null, (RoutedEventArgs)null);
                                        break;
                                }
                            }))))).Start();
                        }));
                        for (int index = 0; index < 15 && !readSuccessed; ++index)
                            Thread.Sleep(100);
                        if (readSuccessed)
                            break;
                    }
                }
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    this.mReadingState = false;
                    this.mWindowMain.setConfigReading(this.mReadingState);
                    this.mWindowMain.showLoading(false);
                    foreach (KeyValuePair<int, ConfigBean> keyValuePair in this.mDictionaryConfig)
                    {
                        string str = JsonConvert.SerializeObject((object)keyValuePair.Value);
                        FLog.d("完整配置" + keyValuePair.Key.ToString() + "Json：" + str);
                    }
                }));
            })).Start();
        }

        private void ButtonConfig1_Click(object sender, RoutedEventArgs e)
        {
            if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
                this.configSelected(4);
            else
                this.configSelected(0);
            if (sender == null)
                return;
            this.showTipsSwitchCFG();
        }

        private void ButtonConfig5_Click(object sender, RoutedEventArgs e)
        {
            if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
                this.configSelected(4);
            else
                this.configSelected(0);
            if (sender == null)
                return;
            this.showTipsSwitchCFG();
        }

        private void ButtonConfig2_Click(object sender, RoutedEventArgs e)
        {
            this.configSelected(1);
            if (sender == null)
                return;
            this.showTipsSwitchCFG();
        }

        private void ButtonConfig3_Click(object sender, RoutedEventArgs e)
        {
            this.configSelected(2);
            if (sender == null)
                return;
            this.showTipsSwitchCFG();
        }

        private void ButtonConfig4_Click(object sender, RoutedEventArgs e)
        {
            this.configSelected(3);
            if (sender == null)
                return;
            this.showTipsSwitchCFG();
        }

        private void ButtonCloseSwitchCFGTips(object sender, RoutedEventArgs e)
        {
            this.mLayoutSwitchCFGTips.Visibility = Visibility.Hidden;
            Constant.TIPS_SWITCH_CFG = false;
        }

        private void showTipsSwitchCFG()
        {
            if (!Constant.TIPS_SWITCH_CFG)
                return;
            this.mLayoutSwitchCFGTips.Visibility = Visibility.Visible;
        }

        private void configSelected(int pageId)
        {
            if (!this.mDictionaryConfig.ContainsKey(pageId))
            {
                this.ReadingConfig(pageId);
            }
            else
            {
                FLog.d("切换到配置" + pageId.ToString());
                this.mPageIndex = pageId;
                ((WindowMain)WindowMain.getInstance()).updateConfigId(this.mPageIndex);
                this.mLayoutConfig.setData(this.mDictionaryConfig[pageId]);
                this.colorReset();
                switch (pageId)
                {
                    case 0:
                        this.mButtonConfig1.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
                        this.mButtonConfig1.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                        this.mButtonConfig1.FontWeight = FontWeights.Bold;
                        break;
                    case 1:
                        this.mButtonConfig2.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
                        this.mButtonConfig2.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                        this.mButtonConfig2.FontWeight = FontWeights.Bold;
                        break;
                    case 2:
                        this.mButtonConfig3.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
                        this.mButtonConfig3.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                        this.mButtonConfig3.FontWeight = FontWeights.Bold;
                        break;
                    case 3:
                        this.mButtonConfig4.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
                        this.mButtonConfig4.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
                        this.mButtonConfig4.FontWeight = FontWeights.Bold;
                        break;
                }
            }
        }

        private void ledUpdate() => throw new NotImplementedException();

        private void colorReset()
        {
            this.mButtonConfig1.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mButtonConfig1.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonConfig1.FontWeight = FontWeights.Normal;
            this.mButtonConfig2.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mButtonConfig2.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonConfig2.FontWeight = FontWeights.Normal;
            this.mButtonConfig3.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mButtonConfig3.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonConfig3.FontWeight = FontWeights.Normal;
            this.mButtonConfig4.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mButtonConfig4.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonConfig4.FontWeight = FontWeights.Normal;
        }

        public bool ApplyDefaultConfig(int configId)
        {
            DataManager.getInstance().setDefaultConfig(configId);
            this.mDictionaryConfig = DataManager.getInstance().getListConfig();
            this.mLed = this.mDictionaryConfig[configId].Led;
            switch (configId)
            {
                case 0:
                    this.ButtonConfig1_Click((object)null, (RoutedEventArgs)null);
                    break;
                case 1:
                    this.ButtonConfig2_Click((object)null, (RoutedEventArgs)null);
                    break;
                case 2:
                    this.ButtonConfig3_Click((object)null, (RoutedEventArgs)null);
                    break;
                case 3:
                    this.ButtonConfig4_Click((object)null, (RoutedEventArgs)null);
                    break;
                case 4:
                    this.ButtonConfig5_Click((object)null, (RoutedEventArgs)null);
                    break;
            }
            return true;
        }

        public void SaveCurrentConfig(int configId, IDelegateCallback delegateCallback)
        {
            NetworkUtils.FlydigiPostEvent("配置列表页-配置" + (configId + 1).ToString() + "应用到手柄");
            if (delegateCallback != null)
                delegateCallback(0);
            new Thread((ParameterizedThreadStart)(o =>
            {
                bool saveSuccessed = false;
                try
                {
                    DataManager.getInstance().writeConfig(ConfigUtils.getDoubleListByConfig(this.mDictionaryConfig[configId]), (IDelegateCallback)(result =>
                    {
                        if (result != 100)
                            return;
                        saveSuccessed = true;
                        if (delegateCallback == null)
                            return;
                        delegateCallback(1);
                    }));
                }
                catch (Exception ex)
                {
                    FLog.d("error01");
                    DataManager.getInstance().disconnect();
                }
                for (int index = 0; index < 90 && !saveSuccessed; ++index)
                    Thread.Sleep(100);
                if (saveSuccessed || delegateCallback == null)
                    return;
                delegateCallback(2);
            })).Start();
        }

        public void ApplyCurrentConfig(int configId, IDelegateCallback delegateCallback)
        {
            NetworkUtils.FlydigiPostEvent("配置列表页-配置" + (configId + 1).ToString() + "应用到手柄");
            if (delegateCallback != null)
                delegateCallback(0);
            new Thread((ParameterizedThreadStart)(o =>
            {
                bool applySuccessed = false;
                try
                {
                    DataManager.getInstance().writeConfig(ConfigUtils.getDoubleListByConfig(this.mDictionaryConfig[configId]), (IDelegateCallback)(result =>
                    {
                        if (result != 100)
                            return;
                        applySuccessed = true;
                        if (delegateCallback == null)
                            return;
                        delegateCallback(1);
                    }));
                }
                catch (Exception ex)
                {
                    FLog.d("error01");
                    DataManager.getInstance().disconnect();
                }
                for (int index = 0; index < 90 && !applySuccessed; ++index)
                    Thread.Sleep(100);
                if (applySuccessed || delegateCallback == null)
                    return;
                delegateCallback(2);
            })).Start();
        }

        public bool getCurrentReadingState() => this.mReadingState;

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            FLog.d("PageConfigList--Page_Unloaded");
            this.mLive = false;
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/pages/pageconfiglist.xaml", UriKind.Relative));
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
        //            ((FrameworkElement)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
        //            break;
        //        case 2:
        //            this.mGrid = (Grid)target;
        //            break;
        //        case 3:
        //            this.mBgImage = (Image)target;
        //            break;
        //        case 4:
        //            this.mLayoutSwitchCFGTips = (StackPanel)target;
        //            break;
        //        case 5:
        //            this.mImgMenu = (Image)target;
        //            break;
        //        case 6:
        //            this.mImgBack = (Image)target;
        //            break;
        //        case 7:
        //            this.mLabelTipsSwitch = (Label)target;
        //            break;
        //        case 8:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.ButtonCloseSwitchCFGTips);
        //            break;
        //        case 9:
        //            this.mButtonConfig1 = (Button)target;
        //            this.mButtonConfig1.Click += new RoutedEventHandler(this.ButtonConfig1_Click);
        //            break;
        //        case 10:
        //            this.mButtonConfig2 = (Button)target;
        //            this.mButtonConfig2.Click += new RoutedEventHandler(this.ButtonConfig2_Click);
        //            break;
        //        case 11:
        //            this.mButtonConfig3 = (Button)target;
        //            this.mButtonConfig3.Click += new RoutedEventHandler(this.ButtonConfig3_Click);
        //            break;
        //        case 12:
        //            this.mButtonConfig4 = (Button)target;
        //            this.mButtonConfig4.Click += new RoutedEventHandler(this.ButtonConfig4_Click);
        //            break;
        //        case 13:
        //            this.mLayoutConfig = (UserControlConfig)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
