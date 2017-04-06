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
        Button reg2Btn;
        Button cancelBtn;
        EditText mailTxt;
        EditText userTxt;
        EditText celTxt;
        EditText nameTxt;
        EditText locTxt;
        EditText passwordTxt;
        EditText password2Txt;
        LinearLayout singUpLyt;
        LinearLayout buttonsLyt;
        Usuario user = new Usuario();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserLogin);

            //encuentro los botones
            logInBtn = FindViewById<Button>(Resource.Id.logBtn);
            registerBtn = FindViewById<Button>(Resource.Id.regBtn);
            reg2Btn = FindViewById<Button>(Resource.Id.reg2Btn);
            cancelBtn = FindViewById<Button>(Resource.Id.cancelBtn);

            //encuentro los texfilds
            userTxt = FindViewById<EditText>(Resource.Id.userText);
            passwordTxt = FindViewById<EditText>(Resource.Id.passText);
            password2Txt = FindViewById<EditText>(Resource.Id.pass2Text);
            mailTxt = FindViewById<EditText>(Resource.Id.emailText);
            nameTxt = FindViewById<EditText>(Resource.Id.nameText);
            celTxt = FindViewById<EditText>(Resource.Id.celText);
            locTxt = FindViewById<EditText>(Resource.Id.locText);

            //encuentro los Layouts
            singUpLyt = FindViewById<LinearLayout>(Resource.Id.singUpLyt);
            buttonsLyt = FindViewById<LinearLayout>(Resource.Id.buttonsLyt);

            //asgino las funciones a los eventos
            logInBtn.Click += clickLogIn;
            registerBtn.Click += clickShowRegister;
            reg2Btn.Click += clickRegister;
            cancelBtn.Click += clickHideRegister;


            // Resto de la Aplicacion

            singUpLyt.Visibility = ViewStates.Gone;



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
                user.nombre = nameTxt.Text;
                user.password = passwordTxt.Text;
                user.username = userTxt.Text;
                user.email = mailTxt.Text;
                user.celular = celTxt.Text;
                user.localidad = locTxt.Text;
                try
                {
                    dynamic res = await ConectorUsuario.Register(user);
                    Toast toast = Toast.MakeText(this, "Registrado con Exito!", ToastLength.Short);
                    toast.Show();
                    buttonsLyt.Visibility = ViewStates.Visible;
                    singUpLyt.Visibility = ViewStates.Gone;
                    this.limpiarCampos();
                }
                catch(Exception e1)
                {
                    Toast toast = Toast.MakeText(this, e1.Message, ToastLength.Short);
                    toast.Show();
                }
                
                //informar al usuario si se pudo registrar
                
            }
        }

        private void clickShowRegister(object sender, EventArgs e)
        {
            singUpLyt.Visibility = ViewStates.Visible;
            buttonsLyt.Visibility = ViewStates.Gone;
        }

        private void clickHideRegister(object sender, EventArgs e)
        {
            buttonsLyt.Visibility = ViewStates.Visible;
            singUpLyt.Visibility = ViewStates.Gone;
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
                Usuario user = await ConectorUsuario.LogIn(userTxt.Text, passwordTxt.Text);
                Intent intent = new Intent(this, typeof(MainActivity));

                intent.PutExtra("username", user.username);
                intent.PutExtra("access_token", user.access_token);
                intent.PutExtra("nombre", user.nombre);
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

        private void limpiarCampos()
        {
            nameTxt.Text = "";
            passwordTxt.Text = "";
            password2Txt.Text = "";
            userTxt.Text = "";
            mailTxt.Text = "";
            celTxt.Text = "";
            locTxt.Text = "";
        }
    }
}