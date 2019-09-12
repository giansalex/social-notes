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

using Social_Notes.Common;
using Social_Notes.DataModel;

namespace Social_Notes
{
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        ItemRss[] itemsRSS;
        public MainPage()
        {
            this.InitializeComponent();
            
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }
        #region ItemsRSS
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            container.DataContext = await SourceRSS.GetGroupsAsync(new String[][]{
            new string[]{"El Show Peru","","http://127.0.0.1/Project/rss/show.xml"},
            new string[]{"El Trome","","http://127.0.0.1/Project/rss/Trome.xml"}, 
            });
            itemsRSS = new ItemRss[] {
                new ItemRss() { Fuente = "El show Peru", SubTitle = "Dedicado a farandula" },
                new ItemRss() { Fuente = "El Trome Peru", SubTitle = "Dedicado a Noticias" }
            };
            ListaRss.ItemsSource = itemsRSS;
            /*
            var comercio = new FuenteRss("El Comercio", "http://s.3.elcomercio.pe.s3-website-us-east-1.amazonaws.com/f/2214-ec/i/btn_suscriptores.png");
            var sampleDataGroups = await DataSource.GetGroupsAsync("http://elcomercio.feedsportal.com/rss/politica.xml");
            Header1.DataContext = comercio;
            foreach (var d in sampleDataGroups)
            {
                lista.Items.Add(d);
            }
            sampleDataGroups = await DataSource.GetGroupsAsync("http://elshow.pe/rss/");
            foreach (var d in sampleDataGroups)
            {
                lista2.Items.Add(d);
            }
            sampleDataGroups = await DataSource.GetGroupsAsync("http://peru21.pe/feed/actualidad");
            foreach (var d in sampleDataGroups)
            {
                lista3.Items.Add(d);
            }
             */
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var r = ((Grid)sender).DataContext as DataGroup;
            System.Diagnostics.Debug.WriteLine(r.Title+r.Link);
        }

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }
        #endregion

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

        #region Lista y titulo 
        private void Grid_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            if (!e.NewSize.IsEmpty)
            {
                if (e.NewSize.Width < 140)
                    titulo.FontSize = 28;
                else
                    titulo.FontSize = 50;
            }
        }

        #endregion

        private void ListaRss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaRss.SelectedIndex != -1)
            {
                scrolls.ChangeView(null,scrolls.ScrollableHeight/2,null);
            }
        }

    }
}
