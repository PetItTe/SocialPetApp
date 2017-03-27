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
	public class MainActivity : Activity
	{
        ListView mascotasList;
        ImageView nuevoImg;
        Spinner userSpin;
        MascotaAdapter mascotaAdapter;
        int paginaActual = 1;
        Paginador paginador;
        
        

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

            mascotasList.ScrollChange += scrollChanged;

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.userOpt_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            userSpin.Adapter = adapter;

         //   userSpin.ItemClick += userClickItem;

            mascotasList.LongClick += mascotaLongClick;


        }

        private async void scrollChanged(object sender, View.ScrollChangeEventArgs e)
        {

            ListView mascotaList = (ListView)sender;
            if (mascotasList.ScrollY == 100)
            {
                paginaActual++;
                if (paginaActual <= paginador.ultimaPagina)
                {
                    mascotaAdapter = new MascotaAdapter(
                 this, await ConectorMascota.ObtenerTodos(paginaActual));

                    mascotasList.Adapter = mascotaAdapter;
                }
            }
            //System.Console.WriteLine(mascotaList.ScrollIndicators);
           // System.Console.WriteLine(mascotaList.ScrollX);
            System.Console.WriteLine("valor ="+mascotasList.ScrollY);
            System.Console.WriteLine("valor =" + mascotaList.ScrollY);
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
            mascotaAdapter = new MascotaAdapter(
                 this, await ConectorMascota.ObtenerTodos(paginaActual));
            paginador = await ConectorMascota.ObtenerTodosHeader(paginaActual);

            //Hook up our adapter to our ListView
            mascotasList.Adapter = mascotaAdapter;
        }


    }
}


