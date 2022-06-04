// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlLineJoystickDisplay
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlLineJoystickDisplay : UserControl, IComponentConnector
    {
        private int MaxDataValue = (int)sbyte.MaxValue;
        private int MaxUILength;
        //internal Grid mGrid;
        //internal Canvas mCanvas;
        //internal Line mLine1;
        //internal Line mLine2;
        //internal Line mLine3;
       //private bool _contentLoaded;

        public UserControlLineJoystickDisplay()
        {
            this.InitializeComponent();
            this.MaxUILength = (int)this.mCanvas.Width;
        }

        public void setData(OneJoystickBean oneJoystickBean)
        {
            FLog.d("UserControlLineJoystickDisplay setData：" + JsonConvert.SerializeObject((object)oneJoystickBean));
            int positionX1 = this.getPositionX(oneJoystickBean.Zero);
            int maxUiLength1 = this.MaxUILength;
            int positionX2 = this.getPositionX(oneJoystickBean.Point0_X);
            int positionY1 = this.getPositionY(oneJoystickBean.Point0_Y);
            int positionX3 = this.getPositionX(oneJoystickBean.Point1_X);
            int positionY2 = this.getPositionY(oneJoystickBean.Point1_Y);
            int maxUiLength2 = this.MaxUILength;
            int num = 0;
            this.mLine1.X1 = (double)positionX1;
            this.mLine1.Y1 = (double)maxUiLength1;
            this.mLine1.X2 = (double)positionX2;
            this.mLine1.Y2 = (double)positionY1;
            this.mLine2.X1 = (double)positionX2;
            this.mLine2.Y1 = (double)positionY1;
            this.mLine2.X2 = (double)positionX3;
            this.mLine2.Y2 = (double)positionY2;
            this.mLine3.X1 = (double)positionX3;
            this.mLine3.Y1 = (double)positionY2;
            this.mLine3.X2 = (double)maxUiLength2;
            this.mLine3.Y2 = (double)num;
        }

        private int getPositionX(int pos) => (int)((double)this.MaxUILength * ((double)pos / (double)this.MaxDataValue));

        private int getPositionY(int pos) => this.MaxUILength - (int)((double)this.MaxUILength * ((double)pos / (double)this.MaxDataValue));

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrollinejoystickdisplay.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mGrid = (Grid)target;
        //            break;
        //        case 2:
        //            this.mCanvas = (Canvas)target;
        //            break;
        //        case 3:
        //            this.mLine1 = (Line)target;
        //            break;
        //        case 4:
        //            this.mLine2 = (Line)target;
        //            break;
        //        case 5:
        //            this.mLine3 = (Line)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
