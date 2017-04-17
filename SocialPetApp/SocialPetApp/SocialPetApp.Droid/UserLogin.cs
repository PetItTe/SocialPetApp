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
    [Activity(Label = "Iniciar Sesion", MainLauncher = true, Icon = "@drawable/icon")]
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
                Toast toast = Toast.MakeText(this, "Aguarde mientras procesamos el registro", ToastLength.Long);
                toast.Show();
                //que hacer si el usuario ingreso todo correctamente
                user.nombre = nameTxt.Text;
                user.password = passwordTxt.Text;
                user.username = userTxt.Text;
                user.email = mailTxt.Text;
                user.cel = celTxt.Text;
                user.localidad = locTxt.Text;
                nameTxt.Enabled = false;
                passwordTxt.Enabled = false;
                password2Txt.Enabled = false;
                userTxt.Enabled = false;
                mailTxt.Enabled = false;
                celTxt.Enabled = false;
                locTxt.Enabled = false;
                registerBtn.Enabled = false;
                logInBtn.Enabled = false;
                reg2Btn.Enabled = false;
                cancelBtn.Enabled = false;
                try
                {
                    
                    dynamic res = await ConectorUsuario.Register(user);
                    Toast register = Toast.MakeText(this, "Registrado con Exito!", ToastLength.Short);
                    register.Show();
                    buttonsLyt.Visibility = ViewStates.Visible;
                    singUpLyt.Visibility = ViewStates.Gone;
                    this.limpiarCampos();
                }
                catch(Exception e1)
                {
                    Toast errorReg = Toast.MakeText(this, e1.Message, ToastLength.Short);
                    errorReg.Show();
                    
                }
                nameTxt.Enabled = true;
                passwordTxt.Enabled = true;
                userTxt.Enabled = true;
                mailTxt.Enabled = true;
                password2Txt.Enabled = true;
                celTxt.Enabled = true;
                locTxt.Enabled = true;
                registerBtn.Enabled = true;
                logInBtn.Enabled = true;
                reg2Btn.Enabled = true;
                cancelBtn.Enabled = true;

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
            userTxt.Enabled = false;
            passwordTxt.Enabled = false;
            registerBtn.Enabled = false;
            logInBtn.Enabled = false;
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
            catch(Exception e1)
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
            cancelBtn.Enabled = true;
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