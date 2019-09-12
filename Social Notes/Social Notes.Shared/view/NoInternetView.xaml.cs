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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Social_Notes.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoInternetView : Page
    {
        public NoInternetView()
        {
            this.InitializeComponent();
            this.Loaded += NoInternetView_Loaded;
            NoInter.Completed += NoInter_Completed;
        }

        void NoInter_Completed(object sender, object e)
        {
            NoInter.Begin();
        }

        void NoInternetView_Loaded(object sender, RoutedEventArgs e)
        {
            NoInter.Begin();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
    }
}
