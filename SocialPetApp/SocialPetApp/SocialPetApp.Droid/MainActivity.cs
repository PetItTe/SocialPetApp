using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace SocialPetApp.Droid
{
	[Activity (Label = "SocialPetApp.Droid")]
	public class MainActivity : Activity, ListView.IOnScrollListener
	{
        ListView mascotasList;
        ImageView nuevoImg;
        Spinner userSpin;
        MascotaAdapter mascotaAdapter;
        int paginaActual = 1;
        Paginador paginador;
        Usuario user;
        Spinner tipoSpin;
        ConectorMascota conMas;
        TextView emailTxt;
        TextView celularTxt;


        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            nuevoImg = FindViewById<ImageView>(Resource.Id.nuevoImg);
            userSpin = FindViewById<Spinner>(Resource.Id.userSpinner);
            mascotasList = FindViewById<ListView>(Resource.Id.MascotasList);
            tipoSpin = FindViewById<Spinner>(Resource.Id.tipoSpinner);
            emailTxt = FindViewById<TextView>(Resource.Id.emailText);
            celularTxt = FindViewById<TextView>(Resource.Id.celText);

            nuevoImg.Clickable = true;
            nuevoImg.Click += clickImage;


            mascotasList.SetOnScrollListener(this);

            //seteamos los spin de filtros y sus adapters
            var userAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.userOpt_array, Android.Resource.Layout.SimpleSpinnerItem);
            userAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            userSpin.Adapter = userAdapter;

            var tipoAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tipoOpt_array, Android.Resource.Layout.SimpleSpinnerItem);
            tipoAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            tipoSpin.Adapter = tipoAdapter;
            //

            tipoSpin.Visibility = ViewStates.Visible;

            userSpin.ItemSelected += userClickItem;
            tipoSpin.ItemSelected += tipoClickItem;

            mascotasList.ItemLongClick += mascotaLongClick;


            try
            {
                user = new Usuario(Intent.GetIntExtra("id_user",0), Intent.GetStringExtra("nombre"), Intent.GetStringExtra("access_token"), Intent.GetStringExtra("username"), Intent.GetIntExtra("roles",0));
                conMas = new ConectorMascota(user);
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerTodos(paginaActual), conMas);
                paginador = await conMas.ObtenerTodosHeader(paginaActual);
                mascotasList.Adapter = mascotaAdapter;
            }
            catch(Exception e1)
            {
                System.Diagnostics.Debug.WriteLine(e1.Message);
                this.Finish();
            }
            


            //user.id_user = Intent.GetIntExtra("usuario", 0);
            // user = await ConectorUsuario.ObtenerByID(user.id);
            

        }

        private async void tipoClickItem(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (tipoSpin.SelectedItemPosition == 1)
            {
                //que hacer si el usuario selecciona la opcion PERRO del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerPerros(), conMas);
                mascotasList.Adapter = mascotaAdapter;
            }
            else if (tipoSpin.SelectedItemPosition == 2)
            {
                //que hacer si el usuario selecciona la opcion GATO del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerGatos(), conMas);
                mascotasList.Adapter = mascotaAdapter;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion TIPO del spinner
                paginaActual = 1;
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerTodos(paginaActual), conMas);
                paginador = await conMas.ObtenerTodosHeader(paginaActual);
                mascotasList.Adapter = mascotaAdapter;
            }
        }

        private async void userClickItem(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (userSpin.SelectedItemPosition == 1)
            {
                //que hacer si el usuario selecciona la opcion ADOPTADOS del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerAdoptados(), conMas);
                mascotasList.Adapter = mascotaAdapter;
                tipoSpin.SetSelection(0);
                tipoSpin.Visibility = ViewStates.Invisible;
                emailTxt.Visibility = ViewStates.Visible;
                celularTxt.Visibility = ViewStates.Visible;
            }
            else if (userSpin.SelectedItemPosition == 2)
            {
                //que hacer si el usuario selecciona la opcion SUBIDOS del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerSubidos(), conMas);
                mascotasList.Adapter = mascotaAdapter;
                tipoSpin.SetSelection(0);
                tipoSpin.Visibility = ViewStates.Invisible;
                emailTxt.Visibility = ViewStates.Invisible;
                celularTxt.Visibility = ViewStates.Invisible;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion HOME del spinner
                paginaActual = 1;
                mascotaAdapter = new MascotaAdapter(
                 this, await conMas.ObtenerTodos(paginaActual), conMas);
                paginador = await conMas.ObtenerTodosHeader(paginaActual);
                mascotasList.Adapter = mascotaAdapter;
                tipoSpin.Visibility = ViewStates.Visible;
                emailTxt.Visibility = ViewStates.Invisible;
                celularTxt.Visibility = ViewStates.Invisible;
            }
        }

        private void mascotaLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Mascota m = new Mascota();
            //m = (Mascota)e.Handled;
            if (userSpin.SelectedItemPosition == 1)
            {
                //que hacer si el usuario mantiene presionado un perro mientras mira la lista de perros que adopto
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Cancelar adopcion");
                alert.SetMessage("¿Poner nuevamente en adopcion la mascota?");
                alert.SetPositiveButton("CONFIRMAR", (senderAlert, args) => {
                    m = mascotaAdapter.mascotas[e.Position];
                    m.estado = Mascota.ESTADO_PUBLICADO;
                    m.user_adopt = 0;
                    conMas.editarMascota(m);
                    mascotaAdapter.mascotas.Remove(m);
                    mascotasList.Adapter = mascotaAdapter;
                });

                alert.SetNegativeButton("CANCELAR", (senderAlert, args) => {
                    //do Nothing;
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else if(userSpin.SelectedItemPosition == 2)
            {
                //que hacer si el usuario mantiene presionado un item mientras mira la lista de perros que subio
                Intent i = new Intent(this, typeof(PerritoNuevo));
                m = mascotaAdapter.mascotas[e.Position];
                i.PutExtra("position", m.id_mas);
                i.PutExtra("usuario", user.id_user);
                
                StartActivity(i);
            }
            else
            {
                //que hacer si el usuario mantiene presionado un item mientras mira la lista de perros en adopcion
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Confirmar adopcion");
                alert.SetMessage("¿Queres adoptar a esta mascota?");
                alert.SetPositiveButton("ADOPTAR", (senderAlert, args) =>
                {
                    m = mascotaAdapter.mascotas[e.Position];
                    conMas.adoptarMascota(m);
                    mascotaAdapter.mascotas.Remove(m);
                    mascotasList.Adapter = mascotaAdapter;
                });

                alert.SetNegativeButton("CANCELAR", (senderAlert, args) => {
                    //do Nothing;
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
        }



        private void clickImage(object sender, EventArgs e)
        {
            //Lo que hace al tocar el simbolito del + verde
            Intent intent = new Intent(this, typeof(PerritoNuevo));
            intent.PutExtra("username", user.username);
            intent.PutExtra("access_token", user.access_token);
            intent.PutExtra("nombre", user.nombre);
            intent.PutExtra("roles", user.roles);
            intent.PutExtra("id_user", user.id_user);
            StartActivity(intent);
        }

        



        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            //throw new NotImplementedException();
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
            //que hacer si el usuario scrollea hasta el final de la lista
            if (MascotaAdapter.endOfList == true)
            {
                MascotaAdapter.endOfList = false;
                if (paginaActual < paginador.ultimaPagina)
                {
                    paginaActual++;
                    try
                    {
                        //si tenes internet decente va a cargar mas perros
                        conMas.CombinarMascotas(mascotaAdapter.mascotas, paginaActual);
                        mascotasList.Adapter = mascotaAdapter;
                    }
                    catch
                    {
                        //si no hay suficiente internet entonces solo podes ver los primeros perros pero la app no rompe
                        paginaActual--;                        
                    }
                }
            }
        }

        protected override void OnResume()
        {
            //que hacer cada vez que se mira esta pantalla
            base.OnResume();
            // create our adapter
           

            //Hook up our adapter to our ListView

        }
    }
}


