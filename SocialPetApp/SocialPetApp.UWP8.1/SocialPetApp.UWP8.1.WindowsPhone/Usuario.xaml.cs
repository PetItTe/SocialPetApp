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

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace SocialPetApp.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Usuario : Page
    {
        public Usuario()
        {
            this.InitializeComponent();
        }

        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            /*userBox.IsEnabled = false;
            passBox.IsEnabled = false;
            regBtn.IsEnabled = false;
            logBtn.IsEnabled = false;
            reg2Btn.Enabled = false;
            cancelBtn.Enabled = false;
            try
            {
                Toast errorLogIn = Toast.MakeText(this, "Aguarde un momento por favor", ToastLength.Long);
                errorLogIn.Show();
                Usuario user = await ConectorUsuario.LogIn(userTxt.Text, passwordTxt.Text);
                Intent intent = new Intent(this, typeof(MainActivity));

                intent.PutExtra("username", user.username);
                intent.PutExtra("access_token", user.access_token);
                intent.PutExtra("nombre", user.nombre);
                intent.PutExtra("roles", user.roles);
                intent.PutExtra("id_user", user.id_user);

                StartActivity(intent);
            }
            catch (Exception e1)
            {
                Toast errorLogIn = Toast.MakeText(this, "Compruebe que el mail y el password sean correctos", ToastLength.Short);
                System.Diagnostics.Debug.WriteLine(e1.Message);
                errorLogIn.Show();
            }
            userTxt.Enabled = true;
            passwordTxt.Enabled = true;
            registerBtn.Enabled = true;
            logInBtn.Enabled = true;
            reg2Btn.Enabled = true;
            cancelBtn.Enabled = true;*/
        }
    }
}
