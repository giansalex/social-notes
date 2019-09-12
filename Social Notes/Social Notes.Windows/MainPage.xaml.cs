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
using Social_Notes.Views;

namespace Social_Notes
{
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
            Window.Current.SizeChanged += Current_SizeChanged;
            this.InvalidateVisualState();
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

        #region VisualState Manager
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }
        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }
        private string DetermineVisualState()
        {
            // Update the back button's enabled state when the view state changes
            var logicalPageBack = this.UsingLogicalPageNavigation();

            return logicalPageBack ? "MinWindows" : "FullWindows";
        }
        private const int MinimumWidthForSupportingTwoPanes = 1024;
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }
        #endregion

        #region Lista y titulo
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            ItemRss[] itemsRSS = new ItemRss[] {
                new ItemRss() { Fuente = "El Show.pe", SubTitle = "Dedicado a farandula" , Ruta = "http://elshow.pe/rss/", Imagen = "ms-appx:///images/showpe.png"},
                new ItemRss() { Fuente = "El Trome", SubTitle = "Dedicado a Noticias" , Ruta = "http://trome.pe/feed", Imagen = "ms-appx:///images/logotrome.png"},
                new ItemRss() { Fuente = "Peru21", SubTitle = "Dedicado a Noticias" , Ruta = "http://peru21.pe/feed/actualidad", Imagen = "ms-appx:///images/peru21.png"},
            };
            ListaRss.ItemsSource = itemsRSS;
            FrameMain.Navigate(typeof(Listado));
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
        private void ListaRss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaRss.SelectedIndex != -1)
            {
                var item = ListaRss.SelectedItem as ItemRss;
                FrameMain.Navigate(typeof(ItemsPage1), new[] { item.Ruta, item.Imagen });
            }
        }
        private void titulo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameMain.Navigate(typeof(Listado));
        }
        private void FrameMain_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.SourcePageType.Name.Equals("Listado"))
                ListaRss.SelectedIndex = -1; ;
        }
        #endregion

    }
}
