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

        Usuario usuario = new Usuario();
        ConectorMascota conMas;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usuario = (Usuario)e.Parameter;
            conMas = new ConectorMascota(usuario);
            userBox.SelectedIndex = 0;
            tipoBox.SelectedIndex = 0;
            

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PerritoNuevo), usuario);
        }

        private async void userBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userBox.IsEnabled = false;
            if (userBox.SelectedItem.Equals("ADOPTADOS"))
            {
                
                //que hacer si el usuario selecciona la opcion ADOPTADOS del spinner
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerAdoptados());
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                tipoBox.SelectedIndex = 0;
                tipoBox.Visibility = Visibility.Collapsed;
                //emailTxt.Visibility = ViewStates.Visible;
                //celularTxt.Visibility = ViewStates.Visible;
                userBox.IsEnabled = true;
            }
            else if (userBox.SelectedItem.Equals("SUBIDOS"))
            {
                //que hacer si el usuario selecciona la opcion SUBIDOS del spinner
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerSubidos());
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                tipoBox.SelectedIndex = 0;
                tipoBox.Visibility = Visibility.Collapsed;
                //emailTxt.Visibility = ViewStates.Invisible;
                //celularTxt.Visibility = ViewStates.Invisible;
                userBox.IsEnabled = true;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion HOME del spinner
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                //paginador = await conMas.ObtenerTodosHeader(paginaActual);
                tipoBox.Visibility = Visibility.Visible;
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
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos(1, Mascota.TIPO_PERRO));
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                
            }
            else if (tipoBox.SelectedItem.Equals("GATOS"))
            {
                //que hacer si el usuario selecciona la opcion GATO del spinner
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos(1, Mascota.TIPO_GATO));
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion TIPO del spinner
                MascotaAdapter.mascotaAdapter = MascotaAdapter.obtenerTodos(await conMas.ObtenerTodos());
                lista.ItemsSource = MascotaAdapter.mascotaAdapter;
            }
        }

        private async void listaTapped(object sender, TappedRoutedEventArgs e)
        {

            MascotaAdapter ma = new MascotaAdapter();
            Mascota m = new Mascota();
            var s = (FrameworkElement)sender;
            var d = s.DataContext;
            ma = (MascotaAdapter)d;
            m = await conMas.ObtenerID(ma.id);
            //m = (Mascota)e.Handled;
            if (userBox.SelectedItem.Equals("ADOPTADOS"))
            {
                //que hacer si el usuario mantiene presionado un perro mientras mira la lista de perros que adopto
                ContentDialog ventanita = new ContentDialog()
                {
                    Title = "Cancelar adopcion",
                    Content = "¿Poner nuevamente en adopcion la mascota?",
                    PrimaryButtonText = "CONFIRMAR",
                    SecondaryButtonText = "CANCELAR"
                    
                };
                ContentDialogResult result = await ventanita.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {     
                    MascotaAdapter.mascotaAdapter.Remove(ma);
                    m = await conMas.ObtenerID(ma.id);
                    m.estado = Mascota.ESTADO_PUBLICADO;
                    m.user_adopt = 0;
                    conMas.editarMascota(m);
                    lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                }
            }
            else if (userBox.SelectedItem.Equals("SUBIDOS"))
            {
                //que hacer si el usuario mantiene presionado un item mientras mira la lista de perros que subio
                ContentDialog ventanita = new ContentDialog()
                {
                    Title = "Confirmar accion",
                    Content = "Elija que desea hacer con esta mascota",
                    PrimaryButtonText = "EDITAR",
                    SecondaryButtonText = "BORRAR"
                };
                ContentDialogResult result = await ventanita.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    EditorDePerritos editor = new EditorDePerritos();
                    editor.mascota = m;
                    editor.user = usuario;
                    Frame.Navigate(typeof(PerritoNuevo), editor);
                }
                else
                {
                    MascotaAdapter.mascotaAdapter.Remove(ma);
                    conMas.eliminarMascota(m);
                    lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                }
            }
            else
            {
                //que hacer si el usuario mantiene presionado un item mientras mira la lista de perros en adopcion
                ContentDialog ventanita = new ContentDialog()
                {
                    Title = "Confirmar adopcion",
                    Content = "¿Queres adoptar a esta mascota?",
                    PrimaryButtonText = "ADOPTAR",
                    SecondaryButtonText = "CANCELAR"
                };
                ContentDialogResult result = await ventanita.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    conMas.adoptarMascota(m);
                    MascotaAdapter.mascotaAdapter.Remove(ma);
                    lista.ItemsSource = MascotaAdapter.mascotaAdapter;
                }
            }
        }
    }
}
