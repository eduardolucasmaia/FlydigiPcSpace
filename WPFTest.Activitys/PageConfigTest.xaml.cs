using ApexSpace;
using ApexSpace.data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;
using WPFTest.Windows;

namespace WPFTest.Activitys
{
    public class PageConfigTest : Page, DataManager.IDeviceOperateData, IComponentConnector
    {
        [CompilerGenerated]
        [Serializable]
        private sealed class <>c
		{
			public static readonly PageConfigTest.<>c<>9 = new PageConfigTest.<>c();

        public static ParameterizedThreadStart<>9__12_0;

			internal void <sendTestCommand>b__12_0(object o)
        {
            Thread.Sleep(100);
            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(false));
            Thread.Sleep(100);
            DataManager.getInstance().SendByteArray(DongleCommand.getStopMacroActionCommand());
        }
    }

    private WindowMain mWindowMain;

    private Frame mFrame;

    private Dictionary<int, Image> mDictionaryKey = new Dictionary<int, Image>();

    private Dictionary<int, ConfigBean> mDictionaryConfig = new Dictionary<int, ConfigBean>();

    private IDelegateCallback mDelegateCallback;

    private int mConfigIndex;

    private PageConfigSetting mPageConfigSetting;

    public bool isApplicationActive;

    private bool testJoystickReset;

    internal Label mLabelTitle;

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

    public PageConfigTest(PageConfigSetting pageConfigSetting, int configId, IDelegateCallback delegateCallback)
    {
        FLog.d("配置测试" + (configId + 1).ToString());
        this.InitializeComponent();
        NetworkUtils.FlydigiPostEvent("配置测试" + (configId + 1).ToString());
        this.mPageConfigSetting = pageConfigSetting;
        this.mConfigIndex = configId;
        this.mLabelTitle.Content = Application.Current.FindResource("config").ToString() + (this.mConfigIndex + 1).ToString();
        this.mDelegateCallback = delegateCallback;
        DataManager.getInstance().setIDeviceOperateData(this);
        if (this.mPageConfigSetting != null && this.mPageConfigSetting.checkKeyAndMacroChange())
        {
            this.mLoading.Visibility = Visibility.Visible;
            this.mPageConfigSetting.writeFlashConfig(delegate (int result)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.mLoading.Visibility = Visibility.Hidden;
                });
                this.sendTestCommand();
            });
        }
        else
        {
            this.sendTestCommand();
        }
        this.updateUI();
    }

    private void Page_Initialized(object sender, EventArgs e)
    {
        FLog.d("PageConfigTest----Page_Initialized");
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        FLog.d("PageConfigTest----Page_Loaded");
    }

    private void updateUI()
    {
        string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
        if (gameHadleName == "f1" || gameHadleName == "f1wch")
        {
            this.mDeviceImg1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front.png"));
            this.mDeviceImg2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_after.png"));
            this.mDeviceImg3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_top.png"));
            this.mKeyMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_menu.png"));
            this.mKeyHome.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_home.png"));
            this.mKeyBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_back.png"));
            this.mKeySelect.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
            this.mKeyStart.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
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
            this.mJoystickRight.Margin = new Thickness(0.0, 190.0, 258.0, 0.0);
            return;
        }
        if (gameHadleName == "apex2")
        {
            this.mDeviceImg1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front.png"));
            this.mDeviceImg2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_after.png"));
            this.mDeviceImg3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_top.png"));
            this.mKeyMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_menu.png"));
            this.mKeyHome.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_home.png"));
            this.mKeyBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_back.png"));
            this.mKeySelect.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_select.png"));
            this.mKeyStart.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_start.png"));
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
            return;
        }
        if (!(gameHadleName == "f1p"))
        {
            return;
        }
        this.mDeviceImg1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front.png"));
        this.mDeviceImg2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_after.png"));
        this.mDeviceImg3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_top.png"));
        this.mKeyMenu.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_menu.png"));
        this.mKeyHome.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_home.png"));
        this.mKeyBack.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_back.png"));
        this.mKeySelect.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
        this.mKeyStart.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
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
        this.mJoystickRight.Margin = new Thickness(0.0, 190.0, 258.0, 0.0);
    }

    private void sendTestCommand()
    {
        ParameterizedThreadStart arg_1F_0;
        if ((arg_1F_0 = PageConfigTest.<> c.<> 9__12_0) == null)
        {
            arg_1F_0 = (PageConfigTest.<> c.<> 9__12_0 = new ParameterizedThreadStart(PageConfigTest.<> c.<> 9.< sendTestCommand > b__12_0));
        }
        new Thread(arg_1F_0).Start();
    }

    public void onDeviceOperateData(byte[] data)
    {
        if (DataManager.getInstance().getUpdatePartlyWritingState())
        {
            return;
        }
        if (DataManager.getInstance().getDeviceConnectState() != 0)
        {
            return;
        }
        int byte0 = (int)(data[0] & 255);
        int byte1 = (int)(data[1] & 255);
        int byte2 = (int)(data[2] & 255);
        int byte3 = (int)(data[3] & 255);
        int byte4 = (int)(data[4] & 255);
        int byte5 = (int)(data[5] & 255);
        byte arg_84_0 = data[6];
        byte arg_88_0 = data[7];
        int byte8 = (int)(data[8] & 255);
        int byte9 = (int)(data[9] & 255);
        Application.Current.Dispatcher.Invoke(delegate
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
                return;
            }
            this.mJoystickRight.Visibility = Visibility.Visible;
            this.mJoystickRight.setData(byte2, byte3);
        });
    }

    private void setKeyState(Image image, bool state)
    {
        image.Visibility = (state ? Visibility.Visible : Visibility.Hidden);
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        DataManager.getInstance().SendByteArray(DongleCommand.getStopMacroActionCommand());
    }

    private void Joystick_Reset(object sender, RoutedEventArgs e)
    {
        if (!this.testJoystickReset)
        {
            this.testJoystickReset = true;
            this.mLabelJoystickResetNotice.Visibility = Visibility.Visible;
            this.mButtonJoystickReset.Visibility = Visibility.Hidden;
            int num = CommonUtils.compareVersion(DataManager.getInstance().getDeviceInfo().FirmwareVersion, Constant.minJoystickResetFmVersion);
            FLog.d("infoinfo supportState22：" + num.ToString());
            if (num < 0)
            {
                new Thread(delegate (object o)
                {
                    DataManager.getInstance().SendByteArray(DongleCommand.getTestJoystickResetCommand());
                    Thread.Sleep(2000);
                    this.testJoystickReset = false;
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        this.mLabelJoystickResetNotice.Visibility = Visibility.Hidden;
                        this.mButtonJoystickReset.Visibility = Visibility.Visible;
                    });
                }).Start();
                return;
            }
            new Thread(delegate (object o)
            {
                byte d = 1;
                DataManager.getInstance().SendByteArray(DongleCommand.getTestNewJoystickResetCommand(d));
                Thread.Sleep(2000);
                this.testJoystickReset = false;
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.mLabelJoystickResetNotice.Visibility = Visibility.Hidden;
                    this.mButtonJoystickReset.Visibility = Visibility.Visible;
                });
            }).Start();
        }
    }

    private void mImageClose_MouseEnter(object sender, MouseEventArgs e)
    {
    }

    private void mImageClose_MouseLeave(object sender, MouseEventArgs e)
    {
    }

    private void mButtonJoystickReset_MouseEnter(object sender, MouseEventArgs e)
    {
        this.mButtonJoystickReset.FontSize = 17.0;
    }

    private void mButtonJoystickReset_MouseLeave(object sender, MouseEventArgs e)
    {
        this.mButtonJoystickReset.FontSize = 16.0;
    }

    public void ApplyCurrentConfig(int configId, IDelegateCallback delegateCallback)
    {
        NetworkUtils.FlydigiPostEvent("配置列表页-配置" + (configId + 1).ToString() + "应用到手柄");
        this.mDictionaryConfig = DataManager.getInstance().getListConfig();
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

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
    public void InitializeComponent()
    {
        if (this._contentLoaded)
        {
            return;
        }
        this._contentLoaded = true;
        Uri resourceLocator = new Uri("/WPFTest;component/pages/pageconfigtest.xaml", UriKind.Relative);
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
                ((PageConfigTest)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
                ((PageConfigTest)target).Initialized += new EventHandler(this.Page_Initialized);
                return;
            case 2:
                this.mLabelTitle = (Label)target;
                return;
            case 3:
                this.mDeviceImg1 = (ImageBrush)target;
                return;
            case 4:
                this.mKeyUP = (Image)target;
                return;
            case 5:
                this.mKeyRight = (Image)target;
                return;
            case 6:
                this.mKeyLeft = (Image)target;
                return;
            case 7:
                this.mKeyDown = (Image)target;
                return;
            case 8:
                this.mKeyA = (Image)target;
                return;
            case 9:
                this.mKeyB = (Image)target;
                return;
            case 10:
                this.mKeyX = (Image)target;
                return;
            case 11:
                this.mKeyY = (Image)target;
                return;
            case 12:
                this.mKeyC = (Image)target;
                return;
            case 13:
                this.mKeyZ = (Image)target;
                return;
            case 14:
                this.mKeySelect = (Image)target;
                return;
            case 15:
                this.mKeyStart = (Image)target;
                return;
            case 16:
                this.mKeyL3 = (Image)target;
                return;
            case 17:
                this.mKeyR3 = (Image)target;
                return;
            case 18:
                this.mKeyMenu = (Image)target;
                return;
            case 19:
                this.mKeyHome = (Image)target;
                return;
            case 20:
                this.mKeyBack = (Image)target;
                return;
            case 21:
                this.mDeviceImg2 = (ImageBrush)target;
                return;
            case 22:
                this.mKeyM1 = (Image)target;
                return;
            case 23:
                this.mKeyM2 = (Image)target;
                return;
            case 24:
                this.mKeyM3 = (Image)target;
                return;
            case 25:
                this.mKeyM4 = (Image)target;
                return;
            case 26:
                this.mDeviceImg3 = (ImageBrush)target;
                return;
            case 27:
                this.mKeyRB = (Image)target;
                return;
            case 28:
                this.mKeyRT = (Image)target;
                return;
            case 29:
                this.mKeyLB = (Image)target;
                return;
            case 30:
                this.mKeyLT = (Image)target;
                return;
            case 31:
                this.mKeyM5 = (Image)target;
                return;
            case 32:
                this.mKeyM6 = (Image)target;
                return;
            case 33:
                this.mJoystickLeft = (UserControlJoystick)target;
                return;
            case 34:
                this.mJoystickRight = (UserControlJoystick)target;
                return;
            case 35:
                this.mLabelJoystickResetNotice = (Label)target;
                return;
            case 36:
                this.mButtonJoystickReset = (Button)target;
                this.mButtonJoystickReset.Click += new RoutedEventHandler(this.Joystick_Reset);
                this.mButtonJoystickReset.MouseEnter += new MouseEventHandler(this.mButtonJoystickReset_MouseEnter);
                this.mButtonJoystickReset.MouseLeave += new MouseEventHandler(this.mButtonJoystickReset_MouseLeave);
                return;
            case 37:
                this.mLoading = (Grid)target;
                return;
            default:
                this._contentLoaded = true;
                return;
        }
    }
}
