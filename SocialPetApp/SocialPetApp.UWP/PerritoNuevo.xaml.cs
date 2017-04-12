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
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;


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
        StorageFile photo = null;
        IRandomAccessStream stream;

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
                nombreBox.Text = m.nombre;
                descripcionBox.Text = m.descripcion;
                edadBox.Text = m.edad.ToString();
                tipoBox.SelectedIndex = m.tipo-1;
                btnPhoto.Visibility = Visibility.Collapsed;
                edita = true;
            }
            catch
            {
                user = (Usuario)e.Parameter;
            }
            conMas = new ConectorMascota(user);

        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            m.nombre = nombreBox.Text;
            m.descripcion = descripcionBox.Text;
            m.edad = int.Parse(edadBox.Text);
            m.tipo = tipoBox.SelectedIndex + 1;
            if (edita)
            {
                conMas.editarMascota(m);
            }
            else
            {
                conMas.publicarMascota(m, stream.AsStream());
            }
            Frame.Navigate(typeof(MainPage),user);
        }

        private void cancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), user);
        }

        private async void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                CameraCaptureUI captureUI = new CameraCaptureUI();
                captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

                captureUI.PhotoSettings.CroppedSizeInPixels = new Size(1280, 720);

                photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                stream = await photo.OpenAsync(FileAccessMode.Read);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                imgPhoto.Visibility = Visibility.Visible;
                imgPhoto.Source = bitmapSource;

            }catch { }
        }
    }
}
