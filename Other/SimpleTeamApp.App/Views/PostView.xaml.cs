using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SimpleTeamApp.App.Views
{
	public sealed partial class PostView : UserControl
	{
		public PostView()
		{
			this.InitializeComponent();
		}

        /*public static readonly DependencyProperty AttachementProperty = DependencyProperty.Register(
            "Attachement",
            typeof(object),
            typeof(PostView),
            new PropertyMetadata(default(object))
        );

        public object Attachement
        {
            get { return (object)GetValue(AttachementProperty); }
            set { SetValue(AttachementProperty, value); }
        }*/
    }
}
