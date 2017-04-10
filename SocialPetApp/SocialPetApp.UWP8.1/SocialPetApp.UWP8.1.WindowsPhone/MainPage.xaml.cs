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
using System.IO;

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
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            SocialPetApp.Usuario user = new SocialPetApp.Usuario();
            user.access_token = "Su4A90QnlbDwjmIhZU9_AYCBrNe5aPDb";
            ConectorMascota conMas = new ConectorMascota(user);
            List<MascotaAdapter> mascAdapter = new List<MascotaAdapter>();
            mascAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
            lista.ItemsSource = mascAdapter;
        }
    }
}
