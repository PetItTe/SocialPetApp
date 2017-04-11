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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace SocialPetApp.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PerritoNuevo : Page
    {

        bool edita = false;
        Mascota m = new Mascota();
        ConectorMascota conMas;
        EditorDePerritos editor;
        Usuario user;
        CameraPicture camera = new CameraPicture();

        public PerritoNuevo()
        {
            this.InitializeComponent();
            tipoBox.Items.Add("PERRO");
            tipoBox.Items.Add("GATO");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                editor = (EditorDePerritos)e.Parameter;
                m = editor.mascota;
                user = editor.user;
                conMas = new ConectorMascota(user);
                nombreBox.Text = m.nombre;
                descripcionBox.Text = m.descripcion;
                edadBox.Text = m.edad.ToString();
                tipoBox.SelectedIndex = m.tipo-1;
                edita = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            m.nombre = nombreBox.Text;
            m.descripcion = descripcionBox.Text;
            m.edad = int.Parse(edadBox.Text);
            m.tipo = tipoBox.SelectedIndex+1;
            if(edita)
            {
                conMas.editarMascota(m);
            }
            else
            {
                byte[] bitmapData;
                using (var stream = new MemoryStream())
                {
                    WriteableBitmap bitmap = new WriteableBitmap(camera.bmp);
                }
                MemoryStream ms = new MemoryStream(bitmapData);
                conMas.publicarMascota(m, ms);
            }
        }

        private void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            camera.takePicture();
        }
    }
}
