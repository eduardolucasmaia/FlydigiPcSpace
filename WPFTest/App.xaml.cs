// Decompiled with JetBrains decompiler
// Type: WPFTest.App
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace WPFTest
{
    public partial class App : Application
    {
        private bool isApplicationActive;
        //private bool _contentLoaded;

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            this.StartupUri = new Uri("/Windows/WindowMain.xaml", UriKind.Relative);
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/app.xaml", UriKind.Relative));
        }

        //[STAThread]
        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public static void Main()
        //{
        //    App app = new App();
        //    app.InitializeComponent();
        //    app.Run();
        //}
    }
}
