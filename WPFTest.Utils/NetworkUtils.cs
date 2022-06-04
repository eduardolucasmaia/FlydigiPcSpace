using ApexSpace;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WPFTest.Bean;

namespace WPFTest.Utils
{
	public class NetworkUtils
	{
		public static FDGEventInfo mFDGEventInfo = null;

		public static string mCurrentLanguage = "zh";

		public static bool enableDataReport = true;

		public static FDGEventInfo GetFDGEventInfo()
		{
			if (NetworkUtils.mFDGEventInfo == null)
			{
				string uid = "";
				string cpuType = "";
				string deviceType = "";
				string oSModel = NetworkUtils.getOSModel();
				string osBit = NetworkUtils.Is64Bit() ? "64 Bit" : "32 Bit";
				NetworkUtils.mFDGEventInfo = new FDGEventInfo();
				NetworkUtils.mFDGEventInfo.Uid = uid;
				NetworkUtils.mFDGEventInfo.AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				NetworkUtils.mFDGEventInfo.OsVersion = oSModel;
				NetworkUtils.mFDGEventInfo.OsBit = osBit;
				NetworkUtils.mFDGEventInfo.Lang = NetworkUtils.getCurrentLanguage();
				NetworkUtils.mFDGEventInfo.cpuType = cpuType;
				NetworkUtils.mFDGEventInfo.deviceType = deviceType;
			}
			return NetworkUtils.mFDGEventInfo;
		}

		public static void setUid(string uid)
		{
			NetworkUtils.GetFDGEventInfo().Uid = uid;
		}

		public static void setCpuType(string cpu_type)
		{
			NetworkUtils.GetFDGEventInfo().cpuType = cpu_type;
		}

		public static void setDeviceType(string device_type)
		{
			NetworkUtils.GetFDGEventInfo().deviceType = device_type;
			FLog.d("current device type:::" + NetworkUtils.GetFDGEventInfo().deviceType);
		}

		public static string getCurrentLanguage()
		{
			return NetworkUtils.mCurrentLanguage;
		}

		public static void setCurrentLanguage(string lang)
		{
			NetworkUtils.mCurrentLanguage = lang;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

		public static bool Is64Bit()
		{
			bool result;
			NetworkUtils.IsWow64Process(Process.GetCurrentProcess().Handle, out result);
			return result;
		}

		public static string getOSModel()
		{
			return new ComputerInfo().OSFullName;
		}

		public static void FlydigiPostEvent(string action)
		{
			if (Constant.CURRENT_PROJECT_TYPE == 1)
			{
				return;
			}
			new Thread(delegate
			{
				NetworkUtils.GetFDGEventInfo().Action = action;
				string postData = "";
				try
				{
					postData = JsonConvert.SerializeObject(NetworkUtils.GetFDGEventInfo());
				}
				catch (Exception ex)
				{
					FLog.d("FlydigiPost：Json异常：" + ex.Message);
					return;
				}
				NetworkUtils.PostEventUrl("http://data.flydigi.com//api/space_station", postData);
			}).Start();
		}

		public static void FlydigiCheckAppVersion(string currentAppVerion, bool userClick, IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
		{
			new Thread(delegate
			{
				string text = "";
				try
				{
					ServicePointManager.Expect100Continue = false;
					int num = 13;
					if (NetworkUtils.getCurrentLanguage().Equals("zh"))
					{
						num = 13;
					}
					else if (NetworkUtils.getCurrentLanguage().Equals("en"))
					{
						num = 14;
					}
					HttpWebRequest expr_54 = (HttpWebRequest)WebRequest.Create("http://api.flydigi.com//web/update/init?app_class_type=" + num.ToString());
					expr_54.ServicePoint.Expect100Continue = false;
					expr_54.Method = "POST";
					expr_54.ContentType = "application/json";
					using (StreamReader streamReader = new StreamReader(((HttpWebResponse)expr_54.GetResponse()).GetResponseStream(), Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
					FLog.d("app update post result:" + text);
					try
					{
						AppUpdateInfo appUpdateInfo = JsonConvert.DeserializeObject<AppUpdateInfo>(text);
						if (appUpdateInfo.status.Equals("0"))
						{
							FLog.d("apk_url:" + appUpdateInfo.data.apk_url);
							if (CommonUtils.compareVersion(appUpdateInfo.data.version_code, currentAppVerion) == 1)
							{
								if (appUpdateInfo.data.is_force.Equals("0"))
								{
									FLog.d("软件需升级 选择弹窗");
									if (delegateCallbackValueTwoObject != null)
									{
										delegateCallbackValueTwoObject(1, appUpdateInfo);
									}
									return;
								}
								if (appUpdateInfo.data.is_force.Equals("1"))
								{
									FLog.d("软件需升级 强制弹窗");
									if (delegateCallbackValueTwoObject != null)
									{
										delegateCallbackValueTwoObject(2, appUpdateInfo);
									}
									return;
								}
								if (appUpdateInfo.data.is_force.Equals("2") & userClick)
								{
									FLog.d("软件需升级 手动更新");
									if (delegateCallbackValueTwoObject != null)
									{
										delegateCallbackValueTwoObject(1, appUpdateInfo);
									}
									return;
								}
							}
							else
							{
								FLog.d("软件无需升级");
							}
						}
						else
						{
							FLog.d("软件无需升级");
						}
					}
					catch (Exception ex)
					{
						FLog.d("app update json error:" + ex.Message);
					}
				}
				catch (Exception ex2)
				{
					FLog.d("app update check error:" + ex2.Message);
					if (delegateCallbackValueTwoObject != null & userClick)
					{
						delegateCallbackValueTwoObject(3, null);
					}
					return;
				}
				if (delegateCallbackValueTwoObject != null & userClick)
				{
					delegateCallbackValueTwoObject(0, null);
				}
			}).Start();
		}

		public static void FlydigiCheckFmVersion(string currentAppVerion, bool userClick, IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
		{
			new Thread(delegate
			{
				string text = "";
				try
				{
					ServicePointManager.Expect100Continue = false;
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=ch573&lang=" + NetworkUtils.getCurrentLanguage());
					FLog.d(string.Concat(new string[]
					{
						"http://api.flydigi.com//android/firmwares?type=",
						DataManager.getInstance().getDeviceInfo().getFirmwareName(),
						"&cpuName=",
						DataManager.getInstance().getDeviceInfo().cpuName,
						"&lang=",
						NetworkUtils.getCurrentLanguage()
					}));
					httpWebRequest.ServicePoint.Expect100Continue = false;
					httpWebRequest.Method = "POST";
					httpWebRequest.ContentType = "application/json";
					using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream(), Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
					FLog.d("app firmware update post result:" + text);
					FLog.d("current version:" + currentAppVerion);
					try
					{
						AppUpdateInfo appUpdateInfo = JsonConvert.DeserializeObject<AppUpdateInfo>(text);
						if (appUpdateInfo.status.Equals("0"))
						{
							FLog.d("apk_url:" + appUpdateInfo.data.apk_url);
							if (CommonUtils.compareVersion(appUpdateInfo.data.version, currentAppVerion) == 1)
							{
								FLog.d("软件需升级 手动更新");
								if (delegateCallbackValueTwoObject != null)
								{
									delegateCallbackValueTwoObject(1, appUpdateInfo);
								}
								return;
							}
							FLog.d("软件无需升级");
						}
						else
						{
							FLog.d("软件无需升级");
						}
					}
					catch (Exception ex)
					{
						FLog.d("app firmware update json error:" + ex.Message);
					}
				}
				catch (Exception ex2)
				{
					FLog.d("app update check error:" + ex2.Message);
					if (delegateCallbackValueTwoObject != null & userClick)
					{
						delegateCallbackValueTwoObject(3, null);
					}
					return;
				}
				if (delegateCallbackValueTwoObject != null & userClick)
				{
					delegateCallbackValueTwoObject(0, null);
				}
			}).Start();
		}

		public static void FlydigiCheckDongleVersion(string currentAppVerion, bool userClick, IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
		{
			new Thread(delegate
			{
				string text = "";
				try
				{
					ServicePointManager.Expect100Continue = false;
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=ch571&lang=" + NetworkUtils.getCurrentLanguage());
					FLog.d(string.Concat(new string[]
					{
						"http://api.flydigi.com//android/firmwares?type=",
						DataManager.getInstance().getDeviceInfo().getFirmwareName(),
						"&cpuName=",
						DataManager.getInstance().getDeviceInfo().cpuName,
						"&lang=",
						NetworkUtils.getCurrentLanguage()
					}));
					httpWebRequest.ServicePoint.Expect100Continue = false;
					httpWebRequest.Method = "POST";
					httpWebRequest.ContentType = "application/json";
					using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream(), Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
					FLog.d("app dongle update post result:" + text);
					FLog.d("current version:" + currentAppVerion);
					try
					{
						AppUpdateInfo appUpdateInfo = JsonConvert.DeserializeObject<AppUpdateInfo>(text);
						if (appUpdateInfo.status.Equals("0"))
						{
							FLog.d("apk_url:" + appUpdateInfo.data.apk_url);
							if (CommonUtils.compareVersion(appUpdateInfo.data.version, currentAppVerion) == 1)
							{
								FLog.d("软件需升级 手动更新");
								if (delegateCallbackValueTwoObject != null)
								{
									delegateCallbackValueTwoObject(1, appUpdateInfo);
								}
								return;
							}
							FLog.d("软件无需升级");
						}
						else
						{
							FLog.d("软件无需升级");
						}
					}
					catch (Exception ex)
					{
						FLog.d("app dongle update json error:" + ex.Message);
					}
				}
				catch (Exception ex2)
				{
					FLog.d("app update check error:" + ex2.Message);
					if (delegateCallbackValueTwoObject != null & userClick)
					{
						delegateCallbackValueTwoObject(3, null);
					}
					return;
				}
				if (delegateCallbackValueTwoObject != null & userClick)
				{
					delegateCallbackValueTwoObject(0, null);
				}
			}).Start();
		}

		private static void PostEventUrl(string url, string postData)
		{
			if (NetworkUtils.enableDataReport)
			{
				FLog.d("事件发送：" + postData);
				string str = "";
				try
				{
					ServicePointManager.Expect100Continue = false;
					HttpWebRequest expr_31 = (HttpWebRequest)WebRequest.Create(url);
					expr_31.ServicePoint.Expect100Continue = false;
					expr_31.Method = "POST";
					expr_31.ContentType = "application/json";
					Stream arg_65_0 = expr_31.GetRequestStream();
					byte[] bytes = Encoding.UTF8.GetBytes(postData);
					arg_65_0.Write(bytes, 0, bytes.Length);
					arg_65_0.Close();
					using (StreamReader streamReader = new StreamReader(((HttpWebResponse)expr_31.GetResponse()).GetResponseStream(), Encoding.UTF8))
					{
						str = streamReader.ReadToEnd();
					}
					FLog.d("post event result:" + str);
				}
				catch (Exception ex)
				{
					FLog.d("post event error:" + ex.Message);
				}
			}
		}

		public static void DownloadFile(string URL, string filename, IDelegateCallback delegateCallback)
		{
			try
			{
				HttpWebRequest expr_11 = (HttpWebRequest)WebRequest.Create(URL);
				expr_11.Timeout = 10000;
				HttpWebResponse expr_26 = (HttpWebResponse)expr_11.GetResponse();
				long contentLength = expr_26.ContentLength;
				Stream responseStream = expr_26.GetResponseStream();
				Stream stream = new FileStream(filename, FileMode.Create);
				long num = 0L;
				byte[] array = new byte[1024];
				int i = responseStream.Read(array, 0, array.Length);
				while (i > 0)
				{
					num = (long)i + num;
					stream.Write(array, 0, i);
					i = responseStream.Read(array, 0, array.Length);
					float num2 = (float)num / (float)contentLength * 100f;
					if (delegateCallback != null)
					{
						delegateCallback((int)num2);
					}
				}
				stream.Close();
				responseStream.Close();
				if (delegateCallback != null)
				{
					FLog.d("软件下载完成");
					delegateCallback(200);
				}
			}
			catch (Exception ex)
			{
				FLog.d("软件下载异常：" + ex.Message);
				if (delegateCallback != null)
				{
					delegateCallback(-1);
				}
			}
		}

		private static string GetHash(string s)
		{
			HashAlgorithm arg_12_0 = new MD5CryptoServiceProvider();
			byte[] bytes = new ASCIIEncoding().GetBytes(s);
			return NetworkUtils.GetHexString(arg_12_0.ComputeHash(bytes));
		}

		private static string GetHexString(byte[] bt)
		{
			string text = string.Empty;
			for (int i = 0; i < bt.Length; i++)
			{
				byte expr_10 = bt[i];
				int num = (int)(expr_10 & 15);
				int num2 = expr_10 >> 4 & 15;
				if (num2 > 9)
				{
					text += ((char)(num2 - 10 + 65)).ToString();
				}
				else
				{
					text += num2.ToString();
				}
				if (num > 9)
				{
					text += ((char)(num - 10 + 65)).ToString();
				}
				else
				{
					text += num.ToString();
				}
				if (i + 1 != bt.Length && (i + 1) % 2 == 0)
				{
					text += "-";
				}
			}
			return text;
		}
	}
}
