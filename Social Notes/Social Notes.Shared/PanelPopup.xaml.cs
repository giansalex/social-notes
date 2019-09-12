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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Social_Notes
{
    public sealed partial class PanelPopup : UserControl
    {
        public PanelPopup()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Inserta Titulo al panel
        /// </summary>
        /// <param name="titulo">Texto a asignar</param>
        public void SetTitle(String titulo)
        {
            this.Titulo.Text = titulo;
        }

        /// <summary>
        /// Inserta SubTitulo al panel
        /// </summary>
        /// <param name="subtitulo">Texto a asignar</param>
        public void SetSubTitle(String subtitulo)
        {
            this.Subtitulo.Text = subtitulo;
        }

        public void Show()
        {
            if(Panel1.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                PanelView.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HidePopup.Begin();
        }

    }
}
