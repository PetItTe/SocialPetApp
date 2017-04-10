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

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SocialPetApp.UWP
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            lista.Margin = new Thickness(20, 20, 20, 2000);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            SocialPetApp.Usuario user = new SocialPetApp.Usuario();
            user.access_token = "Su4A90QnlbDwjmIhZU9_AYCBrNe5aPDb";
            ConectorMascota conMas = new ConectorMascota(user);
            lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
            lista.Margin = new Thickness(20,20,20,2000);
            
            
        }
    }
}
