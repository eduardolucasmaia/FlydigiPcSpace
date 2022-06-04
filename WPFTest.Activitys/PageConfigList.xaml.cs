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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPFTest.UserControls;
using WPFTest.Utils;
using WPFTest.Windows;

namespace WPFTest.Activitys
{
    public class PageConfigList : Page, IComponentConnector
    {
        private WindowMain mWindowMain;

        private Frame mFrame;

        private Dictionary<int, ConfigBean> mDictionaryConfig = new Dictionary<int, ConfigBean>();

        private int mPageIndex;

        private LedBean mLed;

        private bool mLive = true;

        private int Led_Continue_Time = 500;

        private bool mReadingState;

        internal Grid mGrid;

        internal Image mBgImage;

        internal StackPanel mLayoutSwitchCFGTips;

        internal Image mImgMenu;

        internal Image mImgBack;

        internal Label mLabelTipsSwitch;

        internal Button mButtonConfig1;

        internal Button mButtonConfig2;

        internal Button mButtonConfig3;

        internal Button mButtonConfig4;

        internal UserControlConfig mLayoutConfig;

        private bool _contentLoaded;

        public PageConfigList(WindowMain window, Frame frame)
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
            if (gameHadleName == "f1" || gameHadleName == "f1wch")
            {
                this.mBgImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_config_list.png"));
                this.mImgMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_menu.png"));
                this.mImgBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_back.png"));
                this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 360.0, 0.0, 0.0);
                this.mImgMenu.Margin = new Thickness(0.0, 0.0, 74.0, 0.0);
                this.mLabelTipsSwitch.Content = Application.Current.FindResource("tips_keys_change_config_f1").ToString();
                return;
            }
            if (gameHadleName == "apex2")
            {
                this.mBgImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/background_config_list.png"));
                this.mImgMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_a2_menu.png"));
                this.mImgBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_a2_back.png"));
                this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 392.0, 0.0, 0.0);
                this.mImgMenu.Margin = new Thickness(0.0, 0.0, 61.0, 0.0);
                this.mLabelTipsSwitch.Content = Application.Current.FindResource("tips_keys_change_config_a2").ToString();
                return;
            }
            if (!(gameHadleName == "f1p"))
            {
                return;
            }
            this.mBgImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_config_list.png"));
            this.mImgMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_menu.png"));
            this.mImgBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/icon_f1_back.png"));
            this.mLayoutSwitchCFGTips.Margin = new Thickness(0.0, 360.0, 0.0, 0.0);
            this.mImgMenu.Margin = new Thickness(0.0, 0.0, 74.0, 0.0);
            this.mLabelTipsSwitch.Content = Application.Current.FindResource("tips_keys_change_config_f1").ToString();
            if (this.mWindowMain.mTopBar.mCurrentModeType == 2)
            {
                this.mButtonConfig2.Visibility = Visibility.Hidden;
                this.mButtonConfig3.Visibility = Visibility.Hidden;
                this.mButtonConfig4.Visibility = Visibility.Hidden;
                this.mButtonConfig1.Visibility = Visibility.Hidden;
            }
        }

        private void updateLed()
        {
            new Thread(delegate
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
                                break;
                            case 1:
                                for (int i = 0; i < 2; i++)
                                {
                                    List<int> list = new List<int>();
                                    if (i == 0)
                                    {
                                        list.AddRange(this.mLed.RgbColor0);
                                    }
                                    else
                                    {
                                        list.AddRange(this.mLed.RgbColor1);
                                    }
                                    int num = 0;
                                    while (num < this.mLed.Light * 2 && this.mLed.Mode == 1 && this.mLed.Light == light)
                                    {
                                        if (num <= this.mLed.Light)
                                        {
                                            this.setBackgroundColor(num, list[0], list[1], list[2]);
                                        }
                                        else
                                        {
                                            this.setBackgroundColor(this.mLed.Light * 2 - num, list[0], list[1], list[2]);
                                        }
                                        Thread.Sleep(millisecondsTimeout);
                                        num++;
                                    }
                                }
                                break;
                            case 2:
                                this.setBackgroundColor(0, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                break;
                            case 3:
                                this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                break;
                            case 4:
                                for (int j = 0; j < 7; j++)
                                {
                                    List<int> rGBColorById = CommonUtils.getRGBColorById(j + 1);
                                    int num2 = 0;
                                    while (num2 < this.mLed.Light * 2 && this.mLed.Mode == 4 && this.mLed.Light == light && base.Visibility == Visibility.Visible)
                                    {
                                        if (num2 <= this.mLed.Light)
                                        {
                                            this.setBackgroundColor(num2, rGBColorById[0], rGBColorById[1], rGBColorById[2]);
                                        }
                                        else
                                        {
                                            this.setBackgroundColor(this.mLed.Light * 2 - num2, rGBColorById[0], rGBColorById[1], rGBColorById[2]);
                                        }
                                        Thread.Sleep(millisecondsTimeout);
                                        num2++;
                                    }
                                }
                                break;
                        }
                    }
                }
            }).Start();
        }

        private void setBackgroundColor(int alpha, int r, int g, int b)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.mGrid.Background = new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)r, (byte)g, (byte)b));
            });
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            FLog.d("PageConfigList----Page_Initialized");
            FLog.d("mDictionaryConfig.Count：" + this.mDictionaryConfig.Count.ToString());
            if (this.mDictionaryConfig.Count == 0)
            {
                DataManager.getInstance().getListConfig().Clear();
                this.mWindowMain.showLoading(true);
                new Thread(delegate
                {
                    bool readSuccessed = false;
                    int i = 0;
                    while (i < 2)
                    {
                        i++;
                        if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
                        {
                            int configId = 4;
                            readSuccessed = true;
                            FLog.d("当前手柄固定的配置id：" + configId.ToString());
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                this.ReadingConfig(configId);
                            });
                        }
                        else
                        {
                            DataManager.getInstance().getCurrentConfigId(DongleCommand.getCurrentConfigIdCommand(), delegate (int configId)
                            {
                                readSuccessed = true;
                                FLog.d("获取到当前手柄的配置id：" + configId.ToString());
                                if (configId > 3)
                                {
                                    configId = 0;
                                    FLog.d("转换后手柄的配置id：" + configId.ToString());
                                }
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    this.ReadingConfig(configId);
                                });
                            });
                        }
                        int num = 0;
                        while (num < 5 && !readSuccessed)
                        {
                            Thread.Sleep(100);
                            num++;
                        }
                        if (readSuccessed)
                        {
                            break;
                        }
                    }
                    if (this.mWindowMain.mTopBar.mCurrentModeType != 2 && !readSuccessed)
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            this.ReadingConfig(0);
                        });
                    }
                }).Start();
            }
            this.updateLed();
            this.mLayoutConfig.setDelegate(delegate (int result)
            {
                if (DataManager.getInstance().getDeviceConnectState() == -1)
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), "手柄未连接", 2000);
                    return;
                }
                if (this.mReadingState)
                {
                    FLog.d("mLayoutConfig.setDelegate");
                    this.mWindowMain.showLoading(true);
                    return;
                }
                if (!this.mDictionaryConfig.ContainsKey(this.mPageIndex))
                {
                    this.ReadingConfig(this.mPageIndex);
                    return;
                }
                NetworkUtils.FlydigiPostEvent("点击修改配置" + (this.mPageIndex + 1).ToString());
                FLog.d("点击修改配置" + (this.mPageIndex + 1).ToString() + JsonConvert.SerializeObject(this.mDictionaryConfig[this.mPageIndex]));
                if (this.mFrame != null)
                {
                    try
                    {
                        PageConfigSetting content = new PageConfigSetting(this.mWindowMain, this.mFrame, this.mDictionaryConfig[this.mPageIndex].Clone(), this.mPageIndex);
                        this.mFrame.Navigate(content);
                    }
                    catch (Exception ex)
                    {
                        NetworkUtils.FlydigiPostEvent("进入修改配置页异常" + (this.mPageIndex + 1).ToString() + ex.Message);
                    }
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FLog.d("PageConfigList----Page_Loaded");
            if (this.mDictionaryConfig.ContainsKey(this.mPageIndex))
            {
                this.mLayoutConfig.setData(this.mDictionaryConfig[this.mPageIndex]);
                this.mLed = this.mDictionaryConfig[this.mPageIndex].Led;
                this.mLive = true;
                this.updateLed();
            }
        }

        private void ReadingConfig(int configId)
        {
            PageConfigList.<> c__DisplayClass15_0 <> c__DisplayClass15_ = new PageConfigList.<> c__DisplayClass15_0();

            <> c__DisplayClass15_.configId = configId;

            <> c__DisplayClass15_.<> 4__this = this;
            ((WindowMain)WindowMain.getInstance()).updateConfigId(<> c__DisplayClass15_.configId);
            FLog.d("ReadingConfig");
            this.mWindowMain.showLoading(true);
            if (!this.mReadingState)
            {
                this.mReadingState = true;
                this.mWindowMain.setConfigReading(this.mReadingState);
                new Thread(delegate (object o)
                {
                    bool readSuccessed = false;
                    int i = 0;
                    while (i < 3)
                    {
                        i++;
                        if (!DataManager.getInstance().getListConfig().ContainsKey(<> c__DisplayClass15_.configId))
                        {
                            DataManager.getInstance().readConfig(DongleCommand.getReadConfigCommand(<> c__DisplayClass15_.configId), <> c__DisplayClass15_.configId, true, delegate (int result)
                            {
                                FLog.d("配置" + <> c__DisplayClass15_.configId.ToString() + "读取完成回调");
                                readSuccessed = true;
                                if (result == <> c__DisplayClass15_.configId)
                                {
                                    ThreadStart arg_67_0;
                                    if ((arg_67_0 = <> c__DisplayClass15_.<> 9__3) == null)
                                    {
                                        arg_67_0 = (<> c__DisplayClass15_.<> 9__3 = new ThreadStart(<> c__DisplayClass15_.< ReadingConfig > b__3));
                                    }
                                    new Thread(arg_67_0).Start();
                                }
                            });
                            int num = 0;
                            while (num < 15 && !readSuccessed)
                            {
                                Thread.Sleep(100);
                                num++;
                            }
                            if (readSuccessed)
                            {
                                break;
                            }
                        }
                    }
                    Dispatcher arg_AC_0 = Application.Current.Dispatcher;
                    Action arg_AC_1;
                    if ((arg_AC_1 = <> c__DisplayClass15_.<> 9__2) == null)
                    {
                        arg_AC_1 = (<> c__DisplayClass15_.<> 9__2 = delegate
                        {


                                <> c__DisplayClass15_.<> 4__this.mReadingState = false;


                                <> c__DisplayClass15_.<> 4__this.mWindowMain.setConfigReading(<> c__DisplayClass15_.<> 4__this.mReadingState);


                                <> c__DisplayClass15_.<> 4__this.mWindowMain.showLoading(false);
                        foreach (KeyValuePair<int, ConfigBean> current in <> c__DisplayClass15_.<> 4__this.mDictionaryConfig)


                            {
                            string str = JsonConvert.SerializeObject(current.Value);
                            FLog.d("完整配置" + current.Key.ToString() + "Json：" + str);
                        }
                    });
            }
            arg_AC_0.Invoke(arg_AC_1);
        }).Start();
    }


    private void ButtonConfig1_Click(object sender, RoutedEventArgs e)
    {
        if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
        {
            this.configSelected(4);
        }
        else
        {
            this.configSelected(0);
        }
        if (sender != null)
        {
            this.showTipsSwitchCFG();
        }
    }

    private void ButtonConfig5_Click(object sender, RoutedEventArgs e)
    {
        if (this.mWindowMain.mTopBar.mCurrentModeType == 2 && DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_PRO)
        {
            this.configSelected(4);
        }
        else
        {
            this.configSelected(0);
        }
        if (sender != null)
        {
            this.showTipsSwitchCFG();
        }
    }

    private void ButtonConfig2_Click(object sender, RoutedEventArgs e)
    {
        this.configSelected(1);
        if (sender != null)
        {
            this.showTipsSwitchCFG();
        }
    }

    private void ButtonConfig3_Click(object sender, RoutedEventArgs e)
    {
        this.configSelected(2);
        if (sender != null)
        {
            this.showTipsSwitchCFG();
        }
    }

    private void ButtonConfig4_Click(object sender, RoutedEventArgs e)
    {
        this.configSelected(3);
        if (sender != null)
        {
            this.showTipsSwitchCFG();
        }
    }

    private void ButtonCloseSwitchCFGTips(object sender, RoutedEventArgs e)
    {
        this.mLayoutSwitchCFGTips.Visibility = Visibility.Hidden;
        Constant.TIPS_SWITCH_CFG = false;
    }

    private void showTipsSwitchCFG()
    {
        if (Constant.TIPS_SWITCH_CFG)
        {
            this.mLayoutSwitchCFGTips.Visibility = Visibility.Visible;
        }
    }

    private void configSelected(int pageId)
    {
        if (!this.mDictionaryConfig.ContainsKey(pageId))
        {
            this.ReadingConfig(pageId);
            return;
        }
        FLog.d("切换到配置" + pageId.ToString());
        this.mPageIndex = pageId;
        ((WindowMain)WindowMain.getInstance()).updateConfigId(this.mPageIndex);
        this.mLayoutConfig.setData(this.mDictionaryConfig[pageId]);
        this.colorReset();
        switch (pageId)
        {
            case 0:
                this.mButtonConfig1.Background = (Brush)new BrushConverter().ConvertFrom("#0074FF");
                this.mButtonConfig1.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                this.mButtonConfig1.FontWeight = FontWeights.Bold;
                return;
            case 1:
                this.mButtonConfig2.Background = (Brush)new BrushConverter().ConvertFrom("#0074FF");
                this.mButtonConfig2.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                this.mButtonConfig2.FontWeight = FontWeights.Bold;
                return;
            case 2:
                this.mButtonConfig3.Background = (Brush)new BrushConverter().ConvertFrom("#0074FF");
                this.mButtonConfig3.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                this.mButtonConfig3.FontWeight = FontWeights.Bold;
                return;
            case 3:
                this.mButtonConfig4.Background = (Brush)new BrushConverter().ConvertFrom("#0074FF");
                this.mButtonConfig4.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                this.mButtonConfig4.FontWeight = FontWeights.Bold;
                return;
            default:
                return;
        }
    }

    private void ledUpdate()
    {
        throw new NotImplementedException();
    }

    private void colorReset()
    {
        this.mButtonConfig1.Background = (Brush)new BrushConverter().ConvertFrom("#181818");
        this.mButtonConfig1.Foreground = (Brush)new BrushConverter().ConvertFrom("#868788");
        this.mButtonConfig1.FontWeight = FontWeights.Normal;
        this.mButtonConfig2.Background = (Brush)new BrushConverter().ConvertFrom("#181818");
        this.mButtonConfig2.Foreground = (Brush)new BrushConverter().ConvertFrom("#868788");
        this.mButtonConfig2.FontWeight = FontWeights.Normal;
        this.mButtonConfig3.Background = (Brush)new BrushConverter().ConvertFrom("#181818");
        this.mButtonConfig3.Foreground = (Brush)new BrushConverter().ConvertFrom("#868788");
        this.mButtonConfig3.FontWeight = FontWeights.Normal;
        this.mButtonConfig4.Background = (Brush)new BrushConverter().ConvertFrom("#181818");
        this.mButtonConfig4.Foreground = (Brush)new BrushConverter().ConvertFrom("#868788");
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
                this.ButtonConfig1_Click(null, null);
                break;
            case 1:
                this.ButtonConfig2_Click(null, null);
                break;
            case 2:
                this.ButtonConfig3_Click(null, null);
                break;
            case 3:
                this.ButtonConfig4_Click(null, null);
                break;
            case 4:
                this.ButtonConfig5_Click(null, null);
                break;
        }
        return true;
    }

    public void SaveCurrentConfig(int configId, IDelegateCallback delegateCallback)
    {
        NetworkUtils.FlydigiPostEvent("配置列表页-配置" + (configId + 1).ToString() + "应用到手柄");
        if (delegateCallback != null)
        {
            delegateCallback(0);
        }
        new Thread(delegate (object o)
        {
            bool saveSuccessed = false;
            try
            {
                DataManager.getInstance().writeConfig(ConfigUtils.getDoubleListByConfig(this.mDictionaryConfig[configId]), delegate (int result)
                {
                    if (result == 100)
                    {
                        saveSuccessed = true;
                        if (delegateCallback != null)
                        {
                            delegateCallback(1);
                        }
                    }
                });
            }
            catch (Exception)
            {
                FLog.d("error01");
                DataManager.getInstance().disconnect();
            }
            int num = 0;
            while (num < 90 && !saveSuccessed)
            {
                Thread.Sleep(100);
                num++;
            }
            if (!saveSuccessed && delegateCallback != null)
            {
                delegateCallback(2);
            }
        }).Start();
    }

    public void ApplyCurrentConfig(int configId, IDelegateCallback delegateCallback)
    {
        NetworkUtils.FlydigiPostEvent("配置列表页-配置" + (configId + 1).ToString() + "应用到手柄");
        if (delegateCallback != null)
        {
            delegateCallback(0);
        }
        new Thread(delegate (object o)
        {
            bool applySuccessed = false;
            try
            {
                DataManager.getInstance().writeConfig(ConfigUtils.getDoubleListByConfig(this.mDictionaryConfig[configId]), delegate (int result)
                {
                    if (result == 100)
                    {
                        applySuccessed = true;
                        if (delegateCallback != null)
                        {
                            delegateCallback(1);
                        }
                    }
                });
            }
            catch (Exception)
            {
                FLog.d("error01");
                DataManager.getInstance().disconnect();
            }
            int num = 0;
            while (num < 90 && !applySuccessed)
            {
                Thread.Sleep(100);
                num++;
            }
            if (!applySuccessed && delegateCallback != null)
            {
                delegateCallback(2);
            }
        }).Start();
    }

    public bool getCurrentReadingState()
    {
        return this.mReadingState;
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        FLog.d("PageConfigList--Page_Unloaded");
        this.mLive = false;
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
    public void InitializeComponent()
    {
        if (this._contentLoaded)
        {
            return;
        }
        this._contentLoaded = true;
        Uri resourceLocator = new Uri("/WPFTest;component/pages/pageconfiglist.xaml", UriKind.Relative);
        Application.LoadComponent(this, resourceLocator);
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
    internal Delegate _CreateDelegate(Type delegateType, string handler)
    {
        return Delegate.CreateDelegate(delegateType, this, handler);
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
        switch (connectionId)
        {
            case 1:
                ((PageConfigList)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
                ((PageConfigList)target).Initialized += new EventHandler(this.Page_Initialized);
                ((PageConfigList)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
                return;
            case 2:
                this.mGrid = (Grid)target;
                return;
            case 3:
                this.mBgImage = (Image)target;
                return;
            case 4:
                this.mLayoutSwitchCFGTips = (StackPanel)target;
                return;
            case 5:
                this.mImgMenu = (Image)target;
                return;
            case 6:
                this.mImgBack = (Image)target;
                return;
            case 7:
                this.mLabelTipsSwitch = (Label)target;
                return;
            case 8:
                ((Button)target).Click += new RoutedEventHandler(this.ButtonCloseSwitchCFGTips);
                return;
            case 9:
                this.mButtonConfig1 = (Button)target;
                this.mButtonConfig1.Click += new RoutedEventHandler(this.ButtonConfig1_Click);
                return;
            case 10:
                this.mButtonConfig2 = (Button)target;
                this.mButtonConfig2.Click += new RoutedEventHandler(this.ButtonConfig2_Click);
                return;
            case 11:
                this.mButtonConfig3 = (Button)target;
                this.mButtonConfig3.Click += new RoutedEventHandler(this.ButtonConfig3_Click);
                return;
            case 12:
                this.mButtonConfig4 = (Button)target;
                this.mButtonConfig4.Click += new RoutedEventHandler(this.ButtonConfig4_Click);
                return;
            case 13:
                this.mLayoutConfig = (UserControlConfig)target;
                return;
            default:
                this._contentLoaded = true;
                return;
        }
    }
}
