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
        Button nuevoBtn;
        MascotaAdapter mascotaAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            nuevoBtn = FindViewById<Button>(Resource.Id.nuevoButton);
            nuevoBtn.Click += nuevoClick;
            mascotasList = FindViewById<ListView>(Resource.Id.MascotasList);
            mascotasList.ItemClick +=
                participanteClick;


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
        protected void nuevoClick(object sender, EventArgs e)
        {
            StartActivity(typeof(PerritoNuevo));
        }
        protected void participanteClick(object sender, AdapterView.ItemClickEventArgs e)
        {
          //  Intent i = new Intent(this, typeof(ParticipanteEdit));
          //  i.PutExtra("position", e.Position);
          //  StartActivity(i);

        }
    }
}


