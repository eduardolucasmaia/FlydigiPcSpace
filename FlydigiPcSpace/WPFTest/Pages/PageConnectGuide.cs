// Decompiled with JetBrains decompiler
// Type: WPFTest.Pages.PageConnectGuide
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
using WPFTest.UserControls;

namespace WPFTest.Pages
{
  public class PageConnectGuide : Page, IComponentConnector
  {
    internal UserControlDialogConnect mGuide;
    private bool _contentLoaded;

    public PageConnectGuide() => this.InitializeComponent();

    private void Page_Initialized(object sender, EventArgs e)
    {
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/WPFTest;component/pages/pageconnectguide.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId != 1)
      {
        if (connectionId == 2)
          this.mGuide = (UserControlDialogConnect) target;
        else
          this._contentLoaded = true;
      }
      else
      {
        ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.Page_Loaded);
        ((FrameworkElement) target).Initialized += new EventHandler(this.Page_Initialized);
      }
    }
  }
}
