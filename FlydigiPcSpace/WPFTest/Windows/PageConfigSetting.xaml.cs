// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.PageConfigSetting
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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;

namespace WPFTest.Windows
{
    public class PageConfigSetting : Page, DataManager.IDeviceOperateData, IComponentConnector
    {
        private WindowMain mWindowMain;
        private ConfigBean mConfigBean;
        private ConfigBean mFlashConfigBean;
        private string mLastJsonKeyAndMacro;
        private string mHashConfig = "";
        private int mConfigIndex;
        private int mLastMotionX;
        private int mLastMotionY;
        internal Grid mGrid;
        internal Image mBackgroundImage;
        internal Button mButtonLed;
        internal Image mImageLed;
        internal Label mLabelLed;
        internal Button mButtonKey;
        internal Image mImageKey;
        internal Label mLabelKey;
        internal Button mButtonJoystick;
        internal Image mImageJoystick;
        internal Label mLabelJoystick;
        internal Button mButtonMotion;
        internal Image mImageMotion;
        internal Label mLabelMotion;
        internal Button mButtonTrigger;
        internal Image mImageTrigger;
        internal Label mLabelTrigger;
        internal UserControlSettingConfigBasic mLayoutBasic;
        internal UserControlSettingConfigLed mLayoutLed;
        internal UserControlSettingConfigKey mLayoutKey;
        internal UserControlSettingConfigJoystick mLayoutJoystick;
        internal UserControlSettingConfigMotion mLayoutMotion;
        internal UserControlSettingConfigJoystickDisplay mLayoutJoystickDisplay;
        internal UserControlSettingConfigMotionDisplay mLayoutMotionDisplay;
        internal UserControlSettingConfigJoystick mLayoutTrigger;
        internal UserControlSettingConfigTriggerDisplay mLayoutTriggerDisplay;
        internal Grid mLoading;
        internal Label mLoadingLabel;
        private bool _contentLoaded;

        [DllImport("user32.dll")]
        public static extern int MessageBoxTimeoutA(
          IntPtr hWnd,
          string msg,
          string Caps,
          int type,
          int Id,
          int time);

        public PageConfigSetting(
          WindowMain window,
          System.Windows.Controls.Frame frame,
          ConfigBean configBean,
          int configIndex)
        {
            this.mWindowMain = window;
            this.InitializeComponent();
            NetworkUtils.FlydigiPostEvent("进入配置设置页");
            this.initData(configBean, configIndex);
        }

        public void initData(ConfigBean configBean, int configIndex)
        {
            this.mConfigBean = configBean;
            FLog.d("meng_config_info_obj" + JsonConvert.SerializeObject((object)configBean));
            this.mHashConfig = JsonConvert.SerializeObject((object)configBean);
            this.mLastJsonKeyAndMacro = JsonConvert.SerializeObject((object)this.mConfigBean.ListKeyMapping) + JsonConvert.SerializeObject((object)this.mConfigBean.Macro);
            this.mFlashConfigBean = configBean.Clone();
            this.mConfigIndex = configIndex;
            this.mLayoutLed.setData(this.mFlashConfigBean, this.mConfigBean.Led, this.mGrid, this.mConfigBean.Motor, this.mConfigBean.LunpanMapping);
            this.mLayoutKey.setData(this.mConfigBean.ListKeyMapping, this.mConfigBean.Macro.ListMacro);
            this.mLayoutJoystick.setData(this.mFlashConfigBean, this.mConfigBean.JoyMapping, this.mLayoutJoystickDisplay);
            this.mLayoutMotion.setData(this.mFlashConfigBean, this.mConfigBean.MotionMapping);
            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(false));
            this.updateUI();
        }

        private void updateUI()
        {
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    int num = gameHadleName == "f1p" ? 1 : 0;
                }
                else
                    this.mLayoutJoystickDisplay.Margin = new Thickness(320.0, 0.0, 0.0, 0.0);
            }
            else
                this.mLayoutJoystickDisplay.Margin = new Thickness(334.0, 26.0, -13.0, -24.0);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            FLog.d("PageConfigSetting----Page_Initialized");
            this.mButtonLed_Click((object)null, (RoutedEventArgs)null);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FLog.d("PageConfigSetting----Page_Loaded");
            if (this.getKeySettingState() == 2)
                this.mLayoutKey.setKeyListen(true);
            else
                DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
        }

        public void setKeyListen()
        {
            FLog.d("PageConfigSetting setKeyListen");
            if (this.getKeySettingState() == 2)
                this.mLayoutKey.setKeyListen(true);
            else
                DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
        }

        private void mButtonLed_Click(object sender, RoutedEventArgs e)
        {
            if (this.getKeySettingState() == 2 && !this.ApplyKeySetting())
                return;
            this.layoutReset();
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_led.png"));
                }
                else
                    this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_led.png"));
            }
            else
                this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_led.png"));
            this.mLayoutLed.Visibility = Visibility.Visible;
            this.mButtonLed.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
            this.mImageLed.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_jichu_select.png"));
            this.mLabelLed.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
            this.mLabelLed.FontWeight = FontWeights.Bold;
        }

        private void mButtonKey_Click(object sender, RoutedEventArgs e)
        {
            this.layoutReset();
            this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_key.png"));
            this.mLayoutKey.Visibility = Visibility.Visible;
            this.mButtonKey.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
            this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_key_white.png"));
            this.mLabelKey.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
            this.mLabelKey.FontWeight = FontWeights.Bold;
            this.mLayoutKey.setKeyListen(false);
        }

        private void mButtonJoystick_Click(object sender, RoutedEventArgs e)
        {
            if (this.getKeySettingState() == 2 && !this.ApplyKeySetting())
                return;
            this.layoutReset();
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_joystick.png"));
                }
                else
                    this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_joystick.png"));
            }
            else
                this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_joystick.png"));
            this.mLayoutJoystick.Visibility = Visibility.Visible;
            this.mLayoutJoystickDisplay.Visibility = Visibility.Visible;
            this.mButtonJoystick.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
            this.mImageJoystick.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_joystick_white.png"));
            this.mLabelJoystick.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
            this.mLabelJoystick.FontWeight = FontWeights.Bold;
            this.mLayoutJoystick.InitData();
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
        }

        private void mButtonMotion_Click(object sender, RoutedEventArgs e)
        {
            if (this.getKeySettingState() == 2 && !this.ApplyKeySetting())
                return;
            this.layoutReset();
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_motion.png"));
                }
                else
                    this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_motion.png"));
            }
            else
                this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_motion.png"));
            this.mLayoutMotion.Visibility = Visibility.Visible;
            this.mLayoutMotionDisplay.Visibility = Visibility.Visible;
            this.mButtonMotion.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
            this.mImageMotion.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_motion_white.png"));
            this.mLabelMotion.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#FFFFFF");
            this.mLabelMotion.FontWeight = FontWeights.Bold;
            this.mLayoutJoystick.InitData();
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
        }

        private void mButtonTrigger_Click(object sender, RoutedEventArgs e)
        {
            this.layoutReset();
            if (this.getKeySettingState() == 2 && !this.ApplyKeySetting())
                return;
            this.layoutReset();
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1p_trigger.png"));
                }
                else
                    this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_joystick.png"));
            }
            else
                this.mBackgroundImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/background_f1_trigger.png"));
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
        }

        private void layoutReset()
        {
            this.mLayoutLed.Visibility = Visibility.Hidden;
            this.mButtonLed.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mImageLed.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_jichu_normal.png"));
            this.mLabelLed.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelLed.FontWeight = FontWeights.Normal;
            this.mLayoutKey.Visibility = Visibility.Hidden;
            this.mButtonKey.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_key_gray.png"));
            this.mLabelKey.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelKey.FontWeight = FontWeights.Normal;
            this.mLayoutJoystick.Visibility = Visibility.Hidden;
            this.mLayoutJoystickDisplay.Visibility = Visibility.Hidden;
            this.mButtonJoystick.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mImageJoystick.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_joystick_gray.png"));
            this.mLabelJoystick.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelJoystick.FontWeight = FontWeights.Normal;
            this.mLayoutMotion.Visibility = Visibility.Hidden;
            this.mLayoutMotionDisplay.Visibility = Visibility.Hidden;
            this.mButtonMotion.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mImageMotion.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_setting_motion_gray.png"));
            this.mLabelMotion.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelMotion.FontWeight = FontWeights.Normal;
            this.mLayoutTrigger.Visibility = Visibility.Hidden;
            this.mLayoutTriggerDisplay.Visibility = Visibility.Hidden;
            this.mButtonTrigger.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
            this.mImageTrigger.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_banji_normal.png"));
            this.mLabelTrigger.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelTrigger.FontWeight = FontWeights.Normal;
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.mLayoutLed.BackbroundClick();
            this.mLayoutMotion.BackbroundClick();
        }

        public void ConfigApply(IDelegateCallback delegateCallback)
        {
            if (this.mConfigBean.Macro.ListMacro.Count > 5)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"maximum_number_of_macro").ToString() + 5.ToString());
            }
            else
            {
                int num = 0;
                for (int index = 0; index < this.mConfigBean.Macro.ListMacro.Count; ++index)
                    num += this.mConfigBean.Macro.ListMacro[index].Count_l;
                if (num > 128)
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"maximum_number_of_macro_actions").ToString() + 128.ToString());
                }
                else
                {
                    NetworkUtils.FlydigiPostEvent("配置设置页-配置" + (this.mConfigIndex + 1).ToString() + "应用到手柄，宏个数：" + this.mConfigBean.Macro.ListMacro.Count.ToString());
                    delegateCallback(0);
                    Action action;
                    new Thread((ThreadStart)(() =>
                    {
                        bool applySuccessed = false;
                        FLog.d("准备写入的配置：" + JsonConvert.SerializeObject((object)this.mConfigBean));
                        List<List<byte>> doubleListByConfig = ConfigUtils.getDoubleListByConfig(this.mConfigBean);
                        for (int index = 0; index < doubleListByConfig.Count; ++index)
                            FLog.d("配置写入待发送配置:" + CommonUtils.byteToHexString(doubleListByConfig[index].ToArray()));
                        DataManager.getInstance().writeConfig(ConfigUtils.getDoubleListByConfig(this.mConfigBean), (IDelegateCallback)(result =>
                        {
                            if (result != 100)
                                return;
                            applySuccessed = true;
                            this.mHashConfig = JsonConvert.SerializeObject((object)this.mConfigBean);
                            if (DataManager.getInstance().getListConfig().ContainsKey(this.mConfigIndex))
                            {
                                DataManager.getInstance().getListConfig().Remove(this.mConfigIndex);
                                DataManager.getInstance().getListConfig().Add(this.mConfigIndex, this.mConfigBean);
                            }
                            Application.Current.Dispatcher.Invoke(action ?? (action = (Action)(() => delegateCallback(1))));
                        }));
                        for (int index = 0; index < 90 && !applySuccessed; ++index)
                            Thread.Sleep(100);
                        if (applySuccessed || delegateCallback == null)
                            return;
                        delegateCallback(2);
                    })).Start();
                }
            }
        }

        public bool ApplyDefaultConfig(int configId)
        {
            DataManager.getInstance().setDefaultConfig(configId);
            this.initData(DataManager.getInstance().getListConfig()[configId], configId);
            return true;
        }

        public bool checkConfigChange()
        {
            string str = JsonConvert.SerializeObject((object)this.mConfigBean);
            FLog.d("调整前：" + this.mHashConfig);
            FLog.d("调整后：" + str);
            return !this.mHashConfig.Equals(str);
        }

        public bool checkKeyAndMacroChange()
        {
            (JsonConvert.SerializeObject((object)this.mConfigBean.ListKeyMapping) + JsonConvert.SerializeObject((object)this.mConfigBean.Macro)).Equals(this.mLastJsonKeyAndMacro);
            return true;
        }

        public void writeFlashConfig(IDelegateCallback delegateCallback) => new Thread((ThreadStart)(() =>
        {
            FLog.d("开始配置预览写入");
            DataManager.getInstance().setUpdatePartlyWritingState(true);
            FLog.d("准备写入的预览配置：" + JsonConvert.SerializeObject((object)this.mConfigBean));
            List<List<byte>> doubleListByConfig = ConfigUtils.getDoubleListByConfig(this.mConfigBean);
            ConfigUtils.ApplyConvertReview(doubleListByConfig[0], doubleListByConfig.Count - 1);
            for (int index = 0; index < doubleListByConfig.Count; ++index)
                FLog.d("配置预览待发送配置:" + CommonUtils.byteToHexString(doubleListByConfig[index].ToArray()));
            DataManager.getInstance().writeConfig(doubleListByConfig, (IDelegateCallback)(result =>
            {
                if (result != 100)
                    return;
                FLog.d("配置预览成功,更新配置");
                this.mFlashConfigBean = this.mConfigBean.Clone();
                this.mLastJsonKeyAndMacro = JsonConvert.SerializeObject((object)this.mConfigBean.ListKeyMapping) + JsonConvert.SerializeObject((object)this.mConfigBean.Macro);
                DataManager.getInstance().setUpdatePartlyWritingState(false);
                if (delegateCallback == null)
                    return;
                delegateCallback(0);
                delegateCallback = (IDelegateCallback)null;
            }));
            for (int index = 0; index < 90 && DataManager.getInstance().getUpdatePartlyWritingState(); ++index)
                Thread.Sleep(100);
            if (!DataManager.getInstance().getUpdatePartlyWritingState())
                return;
            FLog.d("6秒超时，配置预览失败");
            DataManager.getInstance().setUpdatePartlyWritingState(false);
            if (delegateCallback == null)
                return;
            delegateCallback(-1);
        })).Start();

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            FLog.d("PageConfigSetting Page_Unloaded");
            this.mLayoutLed.close();
        }

        public void onDeviceOperateData(byte[] data)
        {
            if (DataManager.getInstance().getDeviceConnectState() != 0)
                return;
            int byte0 = (int)data[0] & (int)byte.MaxValue;
            int byte1 = (int)data[1] & (int)byte.MaxValue;
            int byte2 = (int)data[2] & (int)byte.MaxValue;
            int byte3 = (int)data[3] & (int)byte.MaxValue;
            if (this.mConfigBean.MotionMapping.Type == 1)
            {
                this.mLastMotionX = byte0;
                this.mLastMotionY = byte1;
            }
            else if (this.mConfigBean.MotionMapping.Type == 2)
            {
                this.mLastMotionX = byte2;
                this.mLastMotionY = byte3;
            }
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (this.mLayoutJoystickDisplay.Visibility == Visibility.Visible)
                    this.mLayoutJoystickDisplay.setData(byte0, byte1, byte2, byte3);
                if (this.mLayoutMotionDisplay.Visibility != Visibility.Visible)
                    return;
                this.mLayoutMotionDisplay.setData(this.mLastMotionX, this.mLastMotionY);
            }));
        }

        public int getKeySettingState()
        {
            if (this.mLayoutKey.Visibility != Visibility.Visible)
                return 0;
            return this.mLayoutKey.getKeySettingVisible() ? 2 : 1;
        }

        public bool ApplyKeySetting() => this.mLayoutKey.ApplyKeySetting();

        private void mButtonMotion_Click_1(object sender, RoutedEventArgs e)
        {
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/pages/pageconfigsetting.xaml", UriKind.Relative));
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
                    ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
                    ((FrameworkElement)target).Initialized += new EventHandler(this.Page_Initialized);
                    ((UIElement)target).MouseDown += new MouseButtonEventHandler(this.Page_MouseDown);
                    ((FrameworkElement)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
                    break;
                case 2:
                    this.mGrid = (Grid)target;
                    break;
                case 3:
                    this.mBackgroundImage = (Image)target;
                    break;
                case 4:
                    this.mButtonLed = (Button)target;
                    this.mButtonLed.Click += new RoutedEventHandler(this.mButtonLed_Click);
                    break;
                case 5:
                    this.mImageLed = (Image)target;
                    break;
                case 6:
                    this.mLabelLed = (Label)target;
                    break;
                case 7:
                    this.mButtonKey = (Button)target;
                    this.mButtonKey.Click += new RoutedEventHandler(this.mButtonKey_Click);
                    break;
                case 8:
                    this.mImageKey = (Image)target;
                    break;
                case 9:
                    this.mLabelKey = (Label)target;
                    break;
                case 10:
                    this.mButtonJoystick = (Button)target;
                    this.mButtonJoystick.Click += new RoutedEventHandler(this.mButtonJoystick_Click);
                    break;
                case 11:
                    this.mImageJoystick = (Image)target;
                    break;
                case 12:
                    this.mLabelJoystick = (Label)target;
                    break;
                case 13:
                    this.mButtonMotion = (Button)target;
                    this.mButtonMotion.Click += new RoutedEventHandler(this.mButtonMotion_Click);
                    break;
                case 14:
                    this.mImageMotion = (Image)target;
                    break;
                case 15:
                    this.mLabelMotion = (Label)target;
                    break;
                case 16:
                    this.mButtonTrigger = (Button)target;
                    this.mButtonTrigger.Click += new RoutedEventHandler(this.mButtonTrigger_Click);
                    break;
                case 17:
                    this.mImageTrigger = (Image)target;
                    break;
                case 18:
                    this.mLabelTrigger = (Label)target;
                    break;
                case 19:
                    this.mLayoutBasic = (UserControlSettingConfigBasic)target;
                    break;
                case 20:
                    this.mLayoutLed = (UserControlSettingConfigLed)target;
                    break;
                case 21:
                    this.mLayoutKey = (UserControlSettingConfigKey)target;
                    break;
                case 22:
                    this.mLayoutJoystick = (UserControlSettingConfigJoystick)target;
                    break;
                case 23:
                    this.mLayoutMotion = (UserControlSettingConfigMotion)target;
                    break;
                case 24:
                    this.mLayoutJoystickDisplay = (UserControlSettingConfigJoystickDisplay)target;
                    break;
                case 25:
                    this.mLayoutMotionDisplay = (UserControlSettingConfigMotionDisplay)target;
                    break;
                case 26:
                    this.mLayoutTrigger = (UserControlSettingConfigJoystick)target;
                    break;
                case 27:
                    this.mLayoutTriggerDisplay = (UserControlSettingConfigTriggerDisplay)target;
                    break;
                case 28:
                    this.mLoading = (Grid)target;
                    break;
                case 29:
                    this.mLoadingLabel = (Label)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
