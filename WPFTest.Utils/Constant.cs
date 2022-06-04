namespace WPFTest.Utils
{
    public class Constant
	{
		public static class LED_MODE
		{
			public const int CONTINUE = 0;

			public const int BLINK = 1;

			public const int CLOSE = 2;

			public const int INPUT = 3;

			public const int LOOP = 4;
		}

		public static class GamePadKey
		{
			public const int UP = 0;

			public const int RIGHT = 1;

			public const int DOWN = 2;

			public const int LEFT = 3;

			public const int A = 4;

			public const int B = 5;

			public const int SELECT = 6;

			public const int X = 7;

			public const int Y = 8;

			public const int START = 9;

			public const int LB = 10;

			public const int RB = 11;

			public const int LT = 12;

			public const int RT = 13;

			public const int THUMBL = 14;

			public const int THUMBR = 15;

			public const int C = 16;

			public const int Z = 17;

			public const int M1 = 18;

			public const int M2 = 19;

			public const int M3 = 20;

			public const int M4 = 21;

			public const int M5 = 22;

			public const int M6 = 23;

			public const int MENU = 24;

			public const int HOME = 27;

			public const int BACK = 28;
		}

		public static class DEVICE
		{
			public static readonly int X9 = 16;

			public static readonly int X8 = 17;

			public static readonly int APEX = 18;

			public static readonly int APEX_2 = 19;

			public static readonly int F1 = 20;

			public static readonly int F1_WIRED = 21;

			public static readonly int F1_PRO = 22;

			public static readonly int F1_MERGE = 23;

			public static readonly int WEE1 = 32;

			public static readonly int WEE2 = 33;

			public static readonly int Q1 = 48;

			public static readonly int D1 = 49;

			public static readonly int WASP_BT = 64;

			public static readonly int WASP_N = 65;

			public static readonly int WASP_X = 66;

			public static readonly int WASP_2 = 67;
		}

		public static class DEVICENAME
		{
			public const string X9 = "x9";

			public const string X8 = "x8";

			public const string APEX = "apex";

			public const string APEX_2 = "apex2";

			public const string F1 = "f1";

			public const string F1_WCH = "f1wch";

			public const string F1_PRO = "f1p";

			public const string WEE1 = "wee1";

			public const string WEE2 = "wee2";

			public const string Q1 = "q1";

			public const string D1 = "d1";

			public const string WASP_BT = "waspbt";

			public const string WASP_N = "waspn";

			public const string WASP_X = "waspx";

			public const string WASP_2 = "wasp2";
		}

		public const int CURRENT_PROJECT_SPACE = 0;

		public const int CURRENT_PROJECT_FACTORY = 1;

		public static int CURRENT_PROJECT_TYPE = 0;

		public static int CURRENT_DEVICE_TYPE = 2;

		public const int F1 = 0;

		public const int APEX2 = 1;

		public const int F1P = 2;

		public static int errorRadius = 15;

		public static string minSwitchFmVersion = "6.3.0.9";

		public static string minJoystickResetFmVersion = "6.3.1.3";

		public static string minJoystickResetFmVersionF1 = "6.1.2.5";

		public static bool TIPS_APPLY_CFG = true;

		public static bool TIPS_SWITCH_CFG = true;

		public const string SUPPORT_FIRMWARE_VERSION = "6.0.2.0";

		public const int DEVICE_MODE_DISCONNECT = -1;

		public const int DEVICE_MODE_ANDROID = 0;

		public const int DEVICE_MODE_XINPUT = 1;

		public const int MAX_ONE_MACRO_ACTION_COUNT = 64;

		public const int MAX_ALL_MACRO_ACTION_COUNT = 128;

		public const int MAX_MACRO_COUNT = 5;

		public const int PARTLY_LED = 0;

		public const int PARTLY_JOYSTICK = 1;

		public const int PARTLY_MOTION = 2;

		public const int PARTLY_MOTOR = 3;

		public const int wireless = 1;

		public const int wired = 2;

		public const int unknow = -1;

		public const int pcMode = 1;

		public const int switchMode = 2;

		public const int APP_UPDATE_CANCEL = 0;

		public const int APP_UPDATE_SELECT = 1;

		public const int APP_UPDATE_FORCE = 2;

		public const int APP_UPDATE_NETWORK_ERROR = 3;

		public const string API_HOST = "http://api.flydigi.com/";

		public const string DATA_HOST = "http://data.flydigi.com/";

		public const string URL_POST_EVENT = "http://data.flydigi.com//api/space_station";

		public const string URL_APP_UPDATE = "http://api.flydigi.com//web/update/init";

		public const string URL_FM_UPDATE = "http://api.flydigi.com//android/firmwares";

		public const string URL_FIRMWARE_UPDATE_ZH = "http://bbs.flydigi.com/detail/11816";

		public const string URL_FIRMWARE_UPDATE_EN = "http://bbs.flydigi.com/en/detail/74";

		public const string URL_HELP_CENTER_ZH = "http://bbs.flydigi.com/detail/15346";

		public const string URL_HELP_CENTER_EN = "http://bbs.flydigi.com/en/detail/80";

		public const string URL_WEIXIN_ZH = "https://bs.flydigi.com/topicDetail?id=43199";

		public const int READ_CONFIG_MAX_TIME = 15;
	}
}
