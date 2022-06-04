namespace WPFTest.Bean
{
    public class FDGEventInfo
	{
		private string uid;

		private string appVersion;

		private string osVersion;

		private string osBit;

		private string lang;

		private string action;

		private string cpu_type;

		private string device_type;

		public string Uid
		{
			get
			{
				return this.uid;
			}
			set
			{
				this.uid = value;
			}
		}

		public string AppVersion
		{
			get
			{
				return this.appVersion;
			}
			set
			{
				this.appVersion = value;
			}
		}

		public string OsVersion
		{
			get
			{
				return this.osVersion;
			}
			set
			{
				this.osVersion = value;
			}
		}

		public string OsBit
		{
			get
			{
				return this.osBit;
			}
			set
			{
				this.osBit = value;
			}
		}

		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		public string Lang
		{
			get
			{
				return this.lang;
			}
			set
			{
				this.lang = value;
			}
		}

		public string cpuType
		{
			get
			{
				return this.cpu_type;
			}
			set
			{
				this.cpu_type = value;
			}
		}

		public string deviceType
		{
			get
			{
				return this.device_type;
			}
			set
			{
				this.device_type = value;
			}
		}
	}
}
