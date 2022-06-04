using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace WPFTest.Activitys
{
	public class CheckTopMost
	{
		public delegate void AlEventHandler(object sender, EventArgs e);

		private const int WS_EX_TOPMOST = 8;

		protected int tm;

		[method: CompilerGenerated]
		[CompilerGenerated]
		public event CheckTopMost.AlEventHandler alertHandler;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		public static bool CheckIsTopMost(IntPtr hWnd)
		{
			return (CheckTopMost.GetWindowLong(hWnd, 8) & 8) == 8;
		}

		public void OnAlert()
		{
			if (this.alertHandler == null)
			{
				return;
			}
			while (true)
			{
				if (this.tm == 100)
				{
					this.alertHandler(this.tm, new EventArgs());
					this.tm = 0;
				}
				this.tm++;
				Thread.Sleep(10);
			}
		}
	}
}
