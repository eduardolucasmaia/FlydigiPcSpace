using System;
using System.IO;
using System.Text;

namespace WPFTest.Utils
{
	public class FLog
	{
		public static bool log;

		public static StreamWriter logHandle;

		public static void setLog(bool logs)
		{
			FLog.log = logs;
		}

		public static void d(string text)
		{
			if (FLog.log)
			{
				FLog.Writelog(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff") + " " + text);
				return;
			}
			if (FLog.logHandle != null)
			{
				FLog.logHandle.Close();
				FLog.logHandle = null;
			}
		}

		public static StreamWriter getLogHandleInstance()
		{
			if (FLog.logHandle == null)
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				if (!Directory.Exists(baseDirectory))
				{
					Directory.CreateDirectory(baseDirectory);
				}
				try
				{
					FLog.logHandle = new StreamWriter(baseDirectory + "\\flydigi_log.txt", true, Encoding.Default);
				}
				catch (Exception)
				{
					FLog.logHandle = new StreamWriter(baseDirectory + "\\flydigi_logs.txt", true, Encoding.Default);
				}
			}
			return FLog.logHandle;
		}

		public static void Writelog(string msg)
		{
			FLog.getLogHandleInstance();
			FLog.logHandle.Write(DateTime.Now.ToString() + ":" + msg);
			FLog.logHandle.Write("\r\n");
			FLog.logHandle.Flush();
		}
	}
}
