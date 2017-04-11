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

        SocialPetApp.Usuario usuario = new SocialPetApp.Usuario();
        
        ConectorMascota conMas = new ConectorMascota(user);

        public MainPage()
        {
            this.InitializeComponent();
            userBox.Items.Add("HOME");
            userBox.Items.Add("ADOPTADOS");
            userBox.Items.Add("SUBIDOS");
            tipoBox.Items.Add("TIPO");
            tipoBox.Items.Add("PERROS");
            tipoBox.Items.Add("GATOS");



        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());  
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PerritoNuevo));
        }

        private async void userBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userBox.IsEnabled = false;
            if (tipoBox.SelectedItem.Equals("ADOPTADOS"))
            {
                //que hacer si el usuario selecciona la opcion ADOPTADOS del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerAdoptados());
                tipoBox.SelectedIndex = 0;
                tipoBox.Visibility = Visibility.Collapsed;
                //emailTxt.Visibility = ViewStates.Visible;
                //celularTxt.Visibility = ViewStates.Visible;
                userBox.IsEnabled = true;
            }
            else if (tipoBox.SelectedItem.Equals("SUBIDOS"))
            {
                //que hacer si el usuario selecciona la opcion SUBIDOS del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerSubidos());
                tipoBox.SelectedIndex = 0;
                tipoBox.Visibility = Visibility.Collapsed;
                //emailTxt.Visibility = ViewStates.Invisible;
                //celularTxt.Visibility = ViewStates.Invisible;
                userBox.IsEnabled = true;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion HOME del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
                //paginador = await conMas.ObtenerTodosHeader(paginaActual);
                tipoBox.Visibility = Visibility.Collapsed;
                //emailTxt.Visibility = ViewStates.Invisible;
                //celularTxt.Visibility = ViewStates.Invisible;
                userBox.IsEnabled = true;
            }
        }

        private async void tipoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (tipoBox.SelectedItem.Equals("PERROS"))
            {
                //que hacer si el usuario selecciona la opcion PERRO del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos(1 , Mascota.TIPO_PERRO));
                
            }
            else if (tipoBox.SelectedItem.Equals("GATOS"))
            {
                //que hacer si el usuario selecciona la opcion GATO del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos(1, Mascota.TIPO_GATO));
            }
            else
            {
                //que hacer si el usuario selecciona la opcion TIPO del spinner
                lista.ItemsSource = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
            }
        }
    }
}
