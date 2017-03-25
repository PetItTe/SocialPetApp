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

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.userOpt_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            userSpin.Adapter = adapter;

         //   userSpin.ItemClick += userClickItem;

            mascotasList.LongClick += mascotaLongClick;


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
            if(userSpin.SelectedItemPosition == 0)
            {
                //StartActivity(typeof(PerritosAdoptados));
            }
            else
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
                 this, await ConectorMascota.ObtenerTodos());

            //Hook up our adapter to our ListView
            mascotasList.Adapter = mascotaAdapter;
        }


    }
}


