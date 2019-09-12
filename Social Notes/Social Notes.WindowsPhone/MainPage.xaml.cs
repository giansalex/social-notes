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
using Social_Notes.Common;
using Social_Notes.DataModel;

namespace Social_Notes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }


        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try
            {
                ItemRss[] rss = new ItemRss[]{
                new ItemRss() { Fuente = "El Show.pe", SubTitle = "Dedicado a farandula" , Ruta = "http://elshow.pe/rss/", Imagen = "ms-appx:///images/showpe.png"},
                new ItemRss() { Fuente = "El Trome", SubTitle = "Dedicado a Noticias" , Ruta = "http://trome.pe/feed", Imagen = "ms-appx:///images/logotrome.png"},
                new ItemRss() { Fuente = "Peru21", SubTitle = "Dedicado a Deportes" , Ruta = "http://peru21.pe/feed/actualidad", Imagen = "ms-appx:///images/peru21.png"},
                };
                container.DataContext = await SourceRSS.GetGroupsAsync(rss);
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync(); 
            }
            catch (Exception) {
                
            }
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var r = ((Grid)sender).DataContext as DataGroup;
            var uri = new Uri(r.Link, UriKind.Absolute);
            this.Frame.Navigate(typeof(Social_Notes.view.Navegador), uri); 
        }
        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuFlyoutItem).DataContext as DataGroup;
            switch ((sender as MenuFlyoutItem).Tag.ToString())
            {
                case "1"://Abrir
                    this.Frame.Navigate(typeof(Social_Notes.view.Navegador), new Uri(item.Link, UriKind.Absolute));
                    break;
                case "2"://Abrir en el Navegador
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(item.Link, UriKind.Absolute));
                    break;
                case "3":// Expandir
                    NotaPanel.SetTitle(item.Title);
                    NotaPanel.SetSubTitle(item.Description);
                    NotaPanel.Show();
                    new Helper.Speaker().SpeakMessage(item.Title + ". " + item.Description);
                    break;
            }
        }

        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



    }
}
