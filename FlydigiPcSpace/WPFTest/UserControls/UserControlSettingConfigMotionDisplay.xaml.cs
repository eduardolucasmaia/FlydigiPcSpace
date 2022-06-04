// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigMotionDisplay
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

namespace WPFTest.UserControls
{
    public class UserControlSettingConfigMotionDisplay : UserControl, IComponentConnector
    {
        private int mUIWidth;
        private int mUIHeight;
        private int mPointWidthHalf;
        private int mPointHeightHalf;
        private int MaxDataValue = 254;
        private double mLastX;
        private double mLastY;
        internal Canvas mCanvas;
        internal Image mPoint;
        private bool _contentLoaded;

        public UserControlSettingConfigMotionDisplay()
        {
            this.InitializeComponent();
            this.mUIWidth = (int)this.mCanvas.Width;
            this.mUIHeight = (int)this.mCanvas.Height;
            this.mPointWidthHalf = (int)this.mPoint.Width / 2;
            this.mPointHeightHalf = (int)this.mPoint.Height / 2;
        }

        public void setData(int x, int y)
        {
            Canvas.SetLeft((UIElement)this.mPoint, this.getUIPositionX(x) - (double)this.mPointWidthHalf);
            Canvas.SetTop((UIElement)this.mPoint, this.getUIPositionY(y) - (double)this.mPointHeightHalf);
        }

        private double getUIPositionX(int pos) => (double)(int)((double)this.mUIWidth * ((double)pos / (double)this.MaxDataValue));

        private double getUIPositionY(int pos) => (double)(int)((double)this.mUIHeight * ((double)pos / (double)this.MaxDataValue));

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigmotiondisplay.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId != 1)
            {
                if (connectionId == 2)
                    this.mPoint = (Image)target;
                else
                    this._contentLoaded = true;
            }
            else
                this.mCanvas = (Canvas)target;
        }
    }
}
