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

		public PageConnectGuide()
		{
			this.InitializeComponent();
		}

		private void Page_Initialized(object sender, EventArgs e)
		{
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/WPFTest;component/pages/pageconnectguide.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((PageConnectGuide)target).Loaded += new RoutedEventHandler(this.Page_Loaded);
				((PageConnectGuide)target).Initialized += new EventHandler(this.Page_Initialized);
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.mGuide = (UserControlDialogConnect)target;
		}
	}
}
