using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace SocialPetApp.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UserLogin : Page
    {
        public UserLogin()
        {
            this.InitializeComponent();
        }

        private async void logBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Usuario user = await ConectorUsuario.LogIn(userBox.Text, passBox.Password);
                this.Frame.Navigate(typeof(MainPage), user);
                //si devuelve los datos abrimos el main
            }
            catch (Exception e1)
            {
                var dialog = new MessageDialog("Nombre de usuario o contraseña invalida.");
                await dialog.ShowAsync();
                System.Diagnostics.Debug.WriteLine(e1.Message);
            }
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {
            regSP.Visibility = Visibility.Visible;
            loginSP.Visibility = Visibility.Collapsed;
        }

        private void canBtn_Click(object sender, RoutedEventArgs e)
        {
            loginSP.Visibility = Visibility.Visible;
            regSP.Visibility = Visibility.Collapsed;
        }

        private async void reg2Btn_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = new Usuario();

            user.nombre = nombreBox.Text;
            user.password = passBox.Password;
            user.username = userBox.Text;
            user.email = mailBox.Text;
            user.celular = celBox.Text;
            user.localidad = localidadBox.Text;
            try
            {

                dynamic res = await ConectorUsuario.Register(user);
                var dialog = new MessageDialog("Registrado con exito!");
                await dialog.ShowAsync();
                loginSP.Visibility = Visibility.Visible;
                regSP.Visibility = Visibility.Collapsed;
                this.limpiarCampos();
            }
            catch (Exception e1)
            {
                var dialog = new MessageDialog(e1.Message);
                await dialog.ShowAsync();

            }
        }

        private void limpiarCampos()
        {
            nombreBox.Text = "";
            passBox.Password = "";
            pass2Box.Password = "";
            userBox.Text = "";
            mailBox.Text = "";
            celBox.Text = "";
            localidadBox.Text = "";
        }

    }
}
