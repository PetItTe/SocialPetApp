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



        protected async override void OnCreate(Bundle bundle)
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

         //   userSpin.ItemClick += userClickItem;

            mascotasList.LongClick += mascotaLongClick;

            mascotaAdapter = new MascotaAdapter(
                 this, await ConectorMascota.ObtenerTodos(paginaActual));
            paginador = await ConectorMascota.ObtenerTodosHeader(paginaActual);
            mascotasList.Adapter = mascotaAdapter;


        }

        private void mascotaLongClick(object sender, View.LongClickEventArgs e)
        {
            
            //  Intent i = new Intent(this, typeof(ParticipanteEdit));
            //  i.PutExtra("position", e.Position);
            //  StartActivity(i);
        }

        private void userClickItem(object sender, AdapterView.ItemClickEventArgs e)
        {
            Spinner userSpin = (Spinner)sender;
            if(userSpin.SelectedItemPosition == 1)
            {
                //StartActivity(typeof(PerritosAdoptados));
            }
            else if(userSpin.SelectedItemPosition == 2)
            {
                //StartActivity(typeof(PerritosSubidos));
            }
        }

        private void clickImage(object sender, EventArgs e)
        {
            StartActivity(typeof(PerritoNuevo));
        }

        protected override async void OnResume()
        {
            base.OnResume();
            // create our adapter
            

            //Hook up our adapter to our ListView
            
        }



        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            //throw new NotImplementedException();
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
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
    }
}


