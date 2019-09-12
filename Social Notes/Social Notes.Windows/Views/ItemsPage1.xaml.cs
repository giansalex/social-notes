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

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233
using Social_Notes.DataModel;
using Social_Notes.view;
namespace Social_Notes.Views
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ItemsPage1 : Page
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

        public ItemsPage1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.GoBackCommand = new Social_Notes.Common.RelayCommand(() => navigationHelper.GoBack(), () => navigationHelper.CanGoBack());
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter == null) return;
            var param = e.NavigationParameter as string[];
            try
            {
                var sourceRss = await DataSource.GetGroupsAsync(param[0]);
                sourceRss.Imagen = param[1];
                container.DataContext = sourceRss;
            }catch(Exception){}
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

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri(((sender as Grid).DataContext as DataGroup).Link,UriKind.Absolute);
            this.Frame.Navigate(typeof(Navegador),uri); 
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
        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }

    }
}
