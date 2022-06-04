// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.WindowDialogFm
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WPFTest.Utils;

namespace WPFTest.Windows
{
    public partial class WindowDialogFm : Window, IComponentConnector
    {
        private string mAppUrl;
        private string mAppVersion;
        private string mNewApplicationName;
        //internal ProgressBar mProgressBar;
        //internal Label mLabelProgress;
        //internal Button mButtonStartNewApp;
        //internal Button mButtonDownload;
        //internal Label mLabelVersion;
        private bool _contentLoaded;

        public WindowDialogFm(string url, string version)
        {
            this.InitializeComponent();
            this.mAppUrl = url;
            this.mAppVersion = version;
            this.mLabelVersion.Content = (object)(Application.Current.FindResource((object)"new_version").ToString() + "V" + this.mAppVersion);
            this.downloadApp();
        }

        private void downloadApp()
        {
            this.mButtonStartNewApp.Visibility = Visibility.Collapsed;
            this.mButtonDownload.Visibility = Visibility.Collapsed;
            this.mNewApplicationName = "飞智空间站V" + this.mAppVersion + ".exe";
            new Thread((ThreadStart)(() => NetworkUtils.DownloadFile(this.mAppUrl, "飞智空间站V" + this.mAppVersion + ".exe", (IDelegateCallback)(progress => Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (progress > 0 && progress < 100)
                {
                    this.mProgressBar.Value = (double)progress;
                    this.mLabelProgress.Content = (object)(Application.Current.FindResource((object)"download_progress").ToString() + progress.ToString() + "%");
                }
                else if (progress == -1)
                {
                    FLog.d("下载异常");
                    this.mButtonDownload.Visibility = Visibility.Visible;
                }
                else
                {
                    if (progress != 200)
                        return;
                    FLog.d("下载完成");
                    this.mProgressBar.Value = 100.0;
                    this.mLabelProgress.Content = (object)(Application.Current.FindResource((object)"download_progress").ToString() + "100 %");
                    this.mButtonStartNewApp.Visibility = Visibility.Visible;
                }
            })))))).Start();
        }

        private void mButtonStartNewApp_Click(object sender, RoutedEventArgs e)
        {
            string str = AppDomain.CurrentDomain.BaseDirectory + this.mNewApplicationName;
            FLog.d(str);
            try
            {
                Process.Start(str);
            }
            catch (Exception ex)
            {
                FLog.d("启动新版本异常：" + ex.Message);
            }
            DataManager.getInstance().SendByteArray(DongleCommand.getSTDControl(true));
            CommonUtils.ApplicationExit();
        }

        private void mButtonDownload_Click(object sender, RoutedEventArgs e) => this.downloadApp();

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowdialogfm.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mProgressBar = (ProgressBar)target;
        //            break;
        //        case 2:
        //            this.mLabelProgress = (Label)target;
        //            break;
        //        case 3:
        //            this.mButtonStartNewApp = (Button)target;
        //            this.mButtonStartNewApp.Click += new RoutedEventHandler(this.mButtonStartNewApp_Click);
        //            break;
        //        case 4:
        //            this.mButtonDownload = (Button)target;
        //            this.mButtonDownload.Click += new RoutedEventHandler(this.mButtonDownload_Click);
        //            break;
        //        case 5:
        //            this.mLabelVersion = (Label)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
