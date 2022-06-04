// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlTrigger
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTest.UserControls
{
    public partial class UserControlTrigger : UserControl, IComponentConnector
    {
        private int MaxDataValue = 254;
        private int MaxUIValue = 260;
        private double PointHalf = 10.0;
        //internal Image mImageBg;
        //internal Line mLine1;
        //internal Ellipse mBlueCircle;
        //internal Image mPoint;
        //internal Arc arc;
       //private bool _contentLoaded;

        public UserControlTrigger() => this.InitializeComponent();

        public void setData(int x, int y)
        {
        }

        private double getUIPositionFormCenter(int pos) => (double)(int)((double)this.MaxUIValue * ((double)pos / (double)this.MaxDataValue));

        private double getUIPosition(int pos) => (double)(int)((double)this.MaxUIValue * ((double)pos / (double)this.MaxDataValue));

        public void setBgImgToBlue() => this.mImageBg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/bg_joystick_blue.png"));

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroltrigger.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mImageBg = (Image)target;
        //            break;
        //        case 2:
        //            this.mLine1 = (Line)target;
        //            break;
        //        case 3:
        //            this.mBlueCircle = (Ellipse)target;
        //            break;
        //        case 4:
        //            this.mPoint = (Image)target;
        //            break;
        //        case 5:
        //            this.arc = (Arc)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
