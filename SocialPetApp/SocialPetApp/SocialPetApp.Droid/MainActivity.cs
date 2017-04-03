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
	[Activity (Label = "SocialPetApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ListView.IOnScrollListener
	{
        Activity context;
        ListView mascotasList;
        ImageView nuevoImg;
        Spinner userSpin;
        MascotaAdapter mascotaAdapter;
        int paginaActual = 1;
        Paginador paginador;
        Usuario user;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            nuevoImg = FindViewById<ImageView>(Resource.Id.nuevoImg);
            userSpin = FindViewById<Spinner>(Resource.Id.userSpinner);
            mascotasList = FindViewById<ListView>(Resource.Id.MascotasList);

            nuevoImg.Clickable = true;
            nuevoImg.Click += clickImage;

   
            mascotasList.SetOnScrollListener(this);

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.userOpt_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            userSpin.Adapter = adapter;

            userSpin.ItemClick += userClickItem;

            mascotasList.ItemLongClick += mascotaLongClick;

            


        }

        private void mascotaLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Mascota m;
            //m = (Mascota)e.Handled;
            if (userSpin.SelectedItemPosition == 1)
            {
                //que hacer si el usuario mantiene presionado un perro mientras mira la lista de perros que adopto
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Adoption process confirmation");
                alert.SetMessage("is this pet in adoption process?");
                alert.SetPositiveButton("YES", (senderAlert, args) => {
                    m = mascotaAdapter.mascotas[e.Position];
                    m.estado = Mascota.ESTADO_PUBLICADO;
                    m.user_adopt = 0;
                    ConectorMascota.editarMascota(m);
                });

                alert.SetNegativeButton("NO", (senderAlert, args) => {
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
                StartActivity(i);
            }
            else
            {
                //que hacer si el usuario mantiene presionado un item mientras mira la lista de perros en adopcion
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Adoption process confirmation");
                alert.SetMessage("do you want to adopt this pet?");
                alert.SetPositiveButton("YES", (senderAlert, args) => {
                    m = mascotaAdapter.mascotas[e.Position];
                    m.estado = Mascota.ESTADO_ADOPTADO;
                    m.user_adopt = user.id;
                    ConectorMascota.editarMascota(m);
                });

                alert.SetNegativeButton("NO", (senderAlert, args) => {
                    //do Nothing;
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
        }

        private async void userClickItem(object sender, AdapterView.ItemClickEventArgs e)
        {
            Spinner userSpin = (Spinner)sender;
            if(userSpin.SelectedItemPosition == 1)
            {
                //que hacer si el usuario selecciona la opcion ADOPTED del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await ConectorUsuario.ObtenerAdoptados(user));
                mascotasList.Adapter = mascotaAdapter;
            }
            else if(userSpin.SelectedItemPosition == 2)
            {
                //que hacer si el usuario selecciona la opcion UPLOADED del spinner
                mascotaAdapter = new MascotaAdapter(
                 this, await ConectorUsuario.ObtenerSubidos(user));
                mascotasList.Adapter = mascotaAdapter;
            }
            else
            {
                //que hacer si el usuario selecciona la opcion HOME del spinner
                paginaActual = 1;
                mascotaAdapter = new MascotaAdapter(
                 this, await ConectorMascota.ObtenerTodos(paginaActual));
                paginador = await ConectorMascota.ObtenerTodosHeader(paginaActual);
                mascotasList.Adapter = mascotaAdapter;
            }
        }

        private void clickImage(object sender, EventArgs e)
        {
            //Lo que hace al tocar el simbolito del + verde
            StartActivity(typeof(PerritoNuevo));
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
                        ConectorMascota.CombinarMascotas(mascotaAdapter.mascotas, paginaActual);
                        mascotasList.Adapter = mascotaAdapter;
                    }
                    catch
                    {
                        paginaActual--;                        
                    }
                }
            }
        }

        protected override async void OnResume()
        {
            //que hacer cada vez que se mira esta pantalla
            base.OnResume();
            // create our adapter
            mascotaAdapter = new MascotaAdapter(
                 this, await ConectorMascota.ObtenerTodos(paginaActual));
            paginador = await ConectorMascota.ObtenerTodosHeader(paginaActual);
            mascotasList.Adapter = mascotaAdapter;

            //Hook up our adapter to our ListView

        }
    }
}


