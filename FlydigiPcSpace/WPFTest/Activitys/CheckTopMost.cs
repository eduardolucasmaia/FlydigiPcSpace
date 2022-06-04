// Decompiled with JetBrains decompiler
// Type: WPFTest.Activitys.CheckTopMost
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WPFTest.Activitys
{
  public class CheckTopMost
  {
    private const int WS_EX_TOPMOST = 8;
    protected int tm;

    public event CheckTopMost.AlEventHandler alertHandler;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    public static bool CheckIsTopMost(IntPtr hWnd) => (CheckTopMost.GetWindowLong(hWnd, 8) & 8) == 8;

    public void OnAlert()
    {
      if (this.alertHandler == null)
        return;
      while (true)
      {
        if (this.tm == 100)
        {
          this.alertHandler((object) this.tm, new EventArgs());
          this.tm = 0;
        }
        ++this.tm;
        Thread.Sleep(10);
      }
    }

    public delegate void AlEventHandler(object sender, EventArgs e);
  }
}
