// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.WindowDialogCommon
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.Utils;

namespace WPFTest.Windows
{
    public partial class WindowDialogCommon : Window, IComponentConnector
    {
        private IDelegateCallback mIDelegateCallback;
        public const int DIALOG_OK = 0;
        public const int DIALOG_OK_FORCE = 1;
        public const int DIALOG_OK_CANCEL = 2;
        public const int RESULT_OK = 1;
        public const int RESULT_CANCEL = 0;
        public const int RESULT_CLOSE = 2;
        public bool isApplicationActive;
        //internal Label mLabelTitle;
        //internal TextBlock mLabelContent;
        //internal Button mButtonCancel;
        //internal Button mButtonConfirm;
        //internal Button mButtonClose;
        //internal Image mImageClose;
       //private bool _contentLoaded;

        public WindowDialogCommon(
          int dialog,
          string title,
          string content,
          string strOk,
          string strCancel,
          IDelegateCallback iDelegateCallback)
        {
            this.InitializeComponent();
            this.mLabelTitle.Content = (object)title;
            this.mLabelContent.Text = content;
            this.mIDelegateCallback = iDelegateCallback;
            if (strOk != null)
                this.mButtonConfirm.Content = (object)strOk;
            if (strCancel != null)
                this.mButtonCancel.Content = (object)strCancel;
            if (dialog == 0)
                this.mButtonCancel.Visibility = Visibility.Collapsed;
            else if (dialog == 1)
            {
                this.mButtonCancel.Visibility = Visibility.Collapsed;
                this.mButtonClose.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(2);
        }

        private void mButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void mButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(1);
        }

        private void mImageClose_MouseEnter(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_white.png"));

        private void mImageClose_MouseLeave(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_gray.png"));

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowdialogcommon.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mLabelTitle = (Label)target;
        //            break;
        //        case 2:
        //            this.mLabelContent = (TextBlock)target;
        //            break;
        //        case 3:
        //            this.mButtonCancel = (Button)target;
        //            this.mButtonCancel.Click += new RoutedEventHandler(this.mButtonCancel_Click);
        //            break;
        //        case 4:
        //            this.mButtonConfirm = (Button)target;
        //            this.mButtonConfirm.Click += new RoutedEventHandler(this.mButtonConfirm_Click);
        //            break;
        //        case 5:
        //            this.mButtonClose = (Button)target;
        //            this.mButtonClose.Click += new RoutedEventHandler(this.ButtonClose_Click);
        //            break;
        //        case 6:
        //            this.mImageClose = (Image)target;
        //            this.mImageClose.MouseEnter += new MouseEventHandler(this.mImageClose_MouseEnter);
        //            this.mImageClose.MouseLeave += new MouseEventHandler(this.mImageClose_MouseLeave);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
