// Decompiled with JetBrains decompiler
// Type: WPFTest.Windows.WindowDialogConnect
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.UserControls;

namespace WPFTest.Windows
{
    public partial class WindowDialogConnect : Window, IComponentConnector
    {
        //internal UserControlDialogConnect mGuide;
        //internal Image mImageClose;
        private bool _contentLoaded;

        public WindowDialogConnect()
        {
            this.InitializeComponent();
            this.mGuide.mHelpCenter.Margin = new Thickness(0.0, 20.0, 30.0, 0.0);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void mImageClose_MouseEnter(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_white.png"));

        private void mImageClose_MouseLeave(object sender, MouseEventArgs e) => this.mImageClose.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_close_gray.png"));

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/windows/windowdialogconnect.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mGuide = (UserControlDialogConnect)target;
        //            break;
        //        case 2:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click);
        //            break;
        //        case 3:
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
