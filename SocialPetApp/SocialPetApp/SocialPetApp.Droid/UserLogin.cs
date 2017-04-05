using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SocialPetApp.Droid
{
    [Activity(Label = "UserLogin", MainLauncher = true, Icon = "@drawable/icon")]
    public class UserLogin : Activity
    {
        Button logInBtn;
        Button registerBtn;
        EditText mailTxt;
        EditText passwordTxt;
        Usuario user = new Usuario();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserLogin);

            logInBtn = FindViewById<Button>(Resource.Id.logBtn);
            registerBtn = FindViewById<Button>(Resource.Id.regBtn);
            mailTxt = FindViewById<EditText>(Resource.Id.userText);
            passwordTxt = FindViewById<EditText>(Resource.Id.passText);

            logInBtn.Click += clickLogIn;
            registerBtn.Click += clickRegister;

            // Create your application here
        }

        private async void clickRegister(object sender, EventArgs e)
        {
            //que hacer si el usuario da click al boton de registrar
            if (mailTxt.Text.Equals("") || passwordTxt.Text.Equals(""))
            {
                //que mensaje mostrar si el usuario no ingreso cuenta o password
                Toast missdata = Toast.MakeText(this, "es necesario completar todos los campos", ToastLength.Short);
                missdata.Show();
            }
            else
            {
                //que hacer si el usuario ingreso todo correctamente
                user.nombre = mailTxt.Text;
                //user.password = passwordTxt.Text;
                string res = await ConectorUsuario.Register(user);
                //informar al usuario si se pudo registrar
                Toast toast = Toast.MakeText(this, res, ToastLength.Short);
                toast.Show();
            }
        }

        private async void clickLogIn(object sender, EventArgs e)
        {
            /*
            Intent intent = new Intent(this, typeof(MainActivity));
            //intent.PutExtra("usuario", user.id);
            StartActivity(intent);
            */
            try
            {
                Usuario user = await ConectorUsuario.LogIn(mailTxt.Text, passwordTxt.Text);
                Intent intent = new Intent(this, typeof(MainActivity));

                intent.PutExtra("username", user.username);
                intent.PutExtra("access_token", user.access_token);
                intent.PutExtra("nombre", user.nombre);
                intent.PutExtra("apellido", user.apellido);
                intent.PutExtra("roles", user.roles);
                intent.PutExtra("id_user", user.id_user);

                StartActivity(intent);
            }
            catch(Exception e1)
            {
                Toast errorLogIn = Toast.MakeText(this, "Compruebe que el mail y el password sean correctos", ToastLength.Short);
                System.Diagnostics.Debug.WriteLine(e1.Message);
                errorLogIn.Show();
            }                                   
        }
    }
}