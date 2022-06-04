using ApexSpace;
using ApexSpace.Bean;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;
using WPFTest.Utils;

namespace WPFTest.Activitys
{
	public class PageSplash : Page, IComponentConnector
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly PageSplash.<>c<>9 = new PageSplash.<>c();

		public static ThreadStart<>9__10_0;

			internal void <FactoryTest_Click>b__10_0()
		{
			DongleCommand.SwitchXInput2Android();
			DataManager.getInstance().delayCheckDeviceState();
		}
	}

	private WindowMain mWindowMain;

	private Frame mFrame;

	private short times;

	internal Image mImgDeviceFront;

	internal Button mButtonStart;

	internal Button mButtonStartSwitch;

	internal Button mFactoryTest;

	internal UserControlDialogConnect connect;

	internal Grid mPageSplashLoading;

	internal Label mPageSplashLoadingLabel;

	internal Label currentModeName;

	private bool _contentLoaded;

	public PageSplash(WindowMain window, Frame frame)
	{
		this.mWindowMain = window;
		this.mFrame = frame;
		this.InitializeComponent();
		if (Constant.CURRENT_PROJECT_TYPE == 0)
		{
			this.mButtonStart.Visibility = Visibility.Visible;
			this.mFactoryTest.Visibility = Visibility.Hidden;
			this.currentModeName.Visibility = Visibility.Hidden;
			DataManager.getInstance().setMainWindow(this.mWindowMain);
		}
		else if (Constant.CURRENT_PROJECT_TYPE == 1)
		{
			this.mButtonStart.Visibility = Visibility.Hidden;
			this.mButtonStartSwitch.Visibility = Visibility.Hidden;
			this.mFactoryTest.Visibility = Visibility.Visible;
			if (DataManager.getInstance().getDeviceConnectState() == 1)
			{
				this.currentModeName.Content = "Xinput模式";
			}
			else
			{
				this.currentModeName.Content = "STD模式";
			}
		}
		this.updateUI();
	}

	private void updateUI()
	{
		string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
		if (gameHadleName == "f1" || gameHadleName == "f1wch")
		{
			this.mImgDeviceFront.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front.png"));
			return;
		}
		if (gameHadleName == "apex2")
		{
			this.mImgDeviceFront.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front.png"));
			return;
		}
		if (!(gameHadleName == "f1p"))
		{
			return;
		}
		this.mImgDeviceFront.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front.png"));
	}

	private void Page_Initialized(object sender, EventArgs e)
	{
		FLog.d("PageSplash----Page_Initialized");
	}

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
		{
			FLog.d("C:\\Windows\\System32\\drivers\\CH375WDM.SYS not exitst");
		}
		if (!File.Exists("C:\\Windows\\System32\\drivers\\CH375W64.SYS"))
		{
			FLog.d("C:\\Windows\\System32\\drivers\\CH375W64.SYS not exitst");
		}
		FLog.d("systerm:" + NetworkUtils.Is64Bit().ToString());
		FLog.d("files1:" + Directory.Exists("C:\\Windows\\System32\\drivers").ToString());
		FLog.d("files2:" + File.Exists("C:\\Windows\\System32\\drivers\\gm.dls").ToString());
		string text = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Sysnative\\drivers\\CH375W64.SYS";
		FLog.d(text);
		FLog.d("files3:" + File.Exists(text).ToString());
		try
		{
			string str = "cd CH372DRV & setup /p";
			Process expr_CA = new Process();
			expr_CA.StartInfo.FileName = "cmd.exe";
			expr_CA.StartInfo.UseShellExecute = false;
			expr_CA.StartInfo.RedirectStandardInput = true;
			expr_CA.StartInfo.RedirectStandardOutput = true;
			expr_CA.StartInfo.RedirectStandardError = true;
			expr_CA.StartInfo.CreateNoWindow = true;
			expr_CA.Start();
			expr_CA.StandardInput.WriteLine(str + "&exit");
			expr_CA.StandardInput.AutoFlush = true;
			string value = expr_CA.StandardOutput.ReadToEnd();
			expr_CA.WaitForExit();
			expr_CA.Close();
			Console.WriteLine(value);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + "\r\n跟踪;" + ex.StackTrace);
		}
	}

	public void Button_Start_Click(object sender, RoutedEventArgs e)
	{
		this.times += 1;
		this.mWindowMain.closeLayoutInfo();
		if (DataManager.getInstance().getDeviceConnectState() == -1)
		{
			this.mWindowMain.showDialogConnectPage();
			return;
		}
		if (DataManager.getInstance().getDeviceConnectState() == 1)
		{
			NetworkUtils.FlydigiPostEvent("切换XInput模式");
			this.mWindowMain.showLoading(true, true);
			this.mWindowMain.updateLoaidngTips(Application.Current.FindResource("tips_switch_xinput_step1").ToString());
			new Thread(delegate
			{
				FLog.d("准备切模式");
				DongleCommand.SwitchXInput2Android();
				DataManager.getInstance().delayCheckDeviceState();
				Thread.Sleep(6000);
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.mWindowMain.showLoading(false);
					if (DataManager.getInstance().getDeviceConnectState() == 0)
					{
						this.Button_Start_Click(null, null);
					}
				});
			}).Start();
			return;
		}
		if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
		{
			return;
		}
		int arg_B4_0 = DataManager.getInstance().getDeviceInfo().DeviceId;
		int arg_B3_0 = Constant.DEVICE.APEX_2;
		if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode < CommonUtils.getFirmwareCodeByVersion("6.0.2.0"))
		{
			CommonUtils.showDialogUpdateFirmware();
			return;
		}
		NetworkUtils.FlydigiPostEvent("点击进入飞智游戏厅");
		this.mWindowMain.mTopBar.showModeName("pcMode");
		this.mWindowMain.mTopBar.mCurrentModeType = 1;
		if (this.mFrame != null)
		{
			PageConfigList content = new PageConfigList(this.mWindowMain, this.mFrame);
			this.mFrame.Navigate(content);
		}
		this.mWindowMain.mLanguageBox.Visibility = Visibility.Collapsed;
		this.updateUI();
	}

	public void Button_Start_Click_Switch(object sender, RoutedEventArgs e)
	{
		if (DataManager.getInstance().getDeviceConnectState() == 1)
		{
			NetworkUtils.FlydigiPostEvent("切换XInput模式");
			this.mWindowMain.showLoading(true, true);
			this.mWindowMain.updateLoaidngTips(Application.Current.FindResource("tips_switch_xinput_step1").ToString());
			new Thread(delegate
			{
				FLog.d("准备切模式");
				DongleCommand.SwitchXInput2Android();
				DataManager.getInstance().delayCheckDeviceState();
				Thread.Sleep(6000);
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.mWindowMain.showLoading(false);
					if (DataManager.getInstance().getDeviceConnectState() == 0)
					{
						this.Button_Start_Click_Switch(null, null);
					}
				});
			}).Start();
			return;
		}
		if (DataManager.getInstance().getDeviceInfo().DeviceId < 0)
		{
			CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource("tipinfo1").ToString(), 2000);
			return;
		}
		FLog.d("infoinfo:" + DataManager.getInstance().getDeviceInfo().DeviceId.ToString());
		if (DataManager.getInstance().getDeviceInfo().DeviceId != Constant.DEVICE.F1_PRO)
		{
			CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource("tipinfo2").ToString(), 2000);
			return;
		}
		int num = CommonUtils.compareVersion(DataManager.getInstance().getDeviceInfo().FirmwareVersion, Constant.minSwitchFmVersion);
		FLog.d("infoinfo supportState+" + num.ToString());
		if (num < 0)
		{
			CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource("tipinfo3").ToString(), 2000);
			return;
		}
		this.times += 1;
		this.mWindowMain.closeLayoutInfo();
		if (DataManager.getInstance().getDeviceConnectState() == -1)
		{
			this.mWindowMain.showDialogConnectPage();
			return;
		}
		this.mWindowMain.mTopBar.showModeName("switchMode");
		this.mWindowMain.mTopBar.mCurrentModeType = 2;
		if (DataManager.getInstance().getDeviceConnectState() == 1)
		{
			NetworkUtils.FlydigiPostEvent("切换XInput模式");
			this.mWindowMain.showLoading(true, true);
			this.mWindowMain.updateLoaidngTips(Application.Current.FindResource("tips_switch_xinput_step1").ToString());
			new Thread(delegate
			{
				FLog.d("准备切模式");
				DongleCommand.SwitchXInput2Android();
				DataManager.getInstance().delayCheckDeviceState();
				Thread.Sleep(6000);
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.mWindowMain.showLoading(false);
					if (DataManager.getInstance().getDeviceConnectState() == 0)
					{
						this.Button_Start_Click(null, null);
					}
				});
			}).Start();
			return;
		}
		if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
		{
			return;
		}
		int arg_231_0 = DataManager.getInstance().getDeviceInfo().DeviceId;
		int arg_230_0 = Constant.DEVICE.APEX_2;
		if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode < CommonUtils.getFirmwareCodeByVersion("6.0.2.0"))
		{
			CommonUtils.showDialogUpdateFirmware();
			return;
		}
		NetworkUtils.FlydigiPostEvent("点击进入飞智游戏厅");
		if (this.mFrame != null)
		{
			PageConfigList content = new PageConfigList(this.mWindowMain, this.mFrame);
			this.mFrame.Navigate(content);
		}
		this.mWindowMain.mLanguageBox.Visibility = Visibility.Collapsed;
		this.updateUI();
	}

	private void FactoryTest_Click(object sender, RoutedEventArgs e)
	{
		this.mWindowMain.closeLayoutInfo();
		if (DataManager.getInstance().getDeviceConnectState() == -1)
		{
			this.mWindowMain.showDialogConnect();
			return;
		}
		if (DataManager.getInstance().getDeviceConnectState() == 1)
		{
			this.mWindowMain.showLoading(true, true);
			this.mWindowMain.updateLoaidngTips("模式切换中...");
			ThreadStart arg_6D_0;
			if ((arg_6D_0 = PageSplash.<> c.<> 9__10_0) == null)
			{
				arg_6D_0 = (PageSplash.<> c.<> 9__10_0 = new ThreadStart(PageSplash.<> c.<> 9.< FactoryTest_Click > b__10_0));
			}
			new Thread(arg_6D_0).Start();
			return;
		}
		if (DataManager.getInstance().getDeviceInfo().FirmwareVersionCode == 0)
		{
			CommonUtils.showDialog("提示", "正在读取手柄固件信息等,请稍等");
			return;
		}
		if (!(DataManager.getInstance().getDeviceInfo().gameHadleName == "apex2") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1wch") && !(DataManager.getInstance().getDeviceInfo().gameHadleName == "f1p"))
		{
			CommonUtils.showDialog("提示", "工厂测试工具当前只支持八爪鱼2及黑武士2，其他手柄测试需求请联系技术支持");
			return;
		}
		FLog.d("log:::cpuName:" + DataManager.getInstance().getDeviceInfo().cpuName);
		FLog.d("log:::gameHadleName:" + DataManager.getInstance().getDeviceInfo().gameHadleName);
		FLog.d("log:::cpuType:" + DataManager.getInstance().getDeviceInfo().cpuType);
		FLog.d("log:::DeviceMac:" + DataManager.getInstance().getDeviceInfo().DeviceMac);
		FLog.d("log:::DeviceId:" + DataManager.getInstance().getDeviceInfo().DeviceId.ToString());
		string arg_1CE_0 = "log:::getDeviceInfo:";
		FDGDeviceInfo expr_1C2 = DataManager.getInstance().getDeviceInfo();
		FLog.d(arg_1CE_0 + ((expr_1C2 != null) ? expr_1C2.ToString() : null));
		this.mWindowMain.showFactoryTest(DataManager.getInstance().getDeviceInfo());
	}

	public void showLoading(bool show)
	{
		this.mPageSplashLoading.Visibility = (show ? Visibility.Visible : Visibility.Hidden);
		this.mPageSplashLoadingLabel.Visibility = Visibility.Hidden;
	}

	public void showLoading(bool show, string showTips)
	{
		this.mPageSplashLoading.Visibility = (show ? Visibility.Visible : Visibility.Hidden);
		if (show)
		{
			this.mPageSplashLoadingLabel.Visibility = Visibility.Visible;
			this.mPageSplashLoadingLabel.Content = showTips;
			return;
		}
		this.mPageSplashLoadingLabel.Visibility = Visibility.Hidden;
	}

	public void updateLoaidngTips(string tips)
	{
		this.mPageSplashLoadingLabel.Content = tips;
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (this._contentLoaded)
		{
			return;
		}
		this._contentLoaded = true;
		Uri resourceLocator = new Uri("/WPFTest;component/pages/pagesplash.xaml", UriKind.Relative);
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
				((PageSplash)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
				((PageSplash)target).Initialized += new EventHandler(this.Page_Initialized);
				return;
			case 2:
				this.mImgDeviceFront = (Image)target;
				return;
			case 3:
				this.mButtonStart = (Button)target;
				this.mButtonStart.Click += new RoutedEventHandler(this.Button_Start_Click);
				return;
			case 4:
				this.mButtonStartSwitch = (Button)target;
				this.mButtonStartSwitch.Click += new RoutedEventHandler(this.Button_Start_Click_Switch);
				return;
			case 5:
				this.mFactoryTest = (Button)target;
				this.mFactoryTest.Click += new RoutedEventHandler(this.FactoryTest_Click);
				return;
			case 6:
				this.connect = (UserControlDialogConnect)target;
				return;
			case 7:
				this.mPageSplashLoading = (Grid)target;
				return;
			case 8:
				this.mPageSplashLoadingLabel = (Label)target;
				return;
			case 9:
				this.currentModeName = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
		}
	}
}
