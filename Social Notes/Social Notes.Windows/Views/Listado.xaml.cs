using Social_Notes.Common;
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

using Social_Notes.DataModel;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Social_Notes.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Listado : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Listado()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try
            {
                ItemRss[] rss = new ItemRss[]{
                new ItemRss(){ Ruta="http://elshow.pe/rss/", Imagen = ""},
                new ItemRss(){ Ruta="http://trome.pe/feed", Imagen = ""},
                new ItemRss(){ Ruta="http://peru21.pe/feed/actualidad", Imagen = ""},
                };
                container.DataContext = await SourceRSS.GetGroupsAsync(rss);            
            }catch(Exception){}
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var r = ((Grid)sender).DataContext as DataGroup;
            var uri = new Uri(r.Link, UriKind.Absolute);
            this.Frame.Navigate(typeof(Social_Notes.view.Navegador), uri); 
        }

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuFlyoutItem).DataContext as DataGroup;
            switch ((sender as MenuFlyoutItem).Tag.ToString())
            {
                case "1"://Abrir
                    this.Frame.Navigate(typeof(Social_Notes.view.Navegador), new Uri(item.Link,UriKind.Absolute)); 
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
