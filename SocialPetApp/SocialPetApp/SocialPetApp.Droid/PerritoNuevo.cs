using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Java.IO;
using Android.Provider;
using Android.Content.PM;

namespace SocialPetApp.Droid
{
    [Activity(Label = "PerritoNuevo")]
    public class PerritoNuevo : Activity
    {
        EditText nombreText;
        EditText fotoText;
        EditText edadText;
        EditText descripcionText;
        Spinner tipoSpin;
        Button addButton;
        ImageButton cameraButton;
        ImageView fotoView;

        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
            }
            SetContentView(Resource.Layout.PerritoNuevo);
            addButton = FindViewById<Button>(Resource.Id.addButton);
            cameraButton = FindViewById<ImageButton>(Resource.Id.cameraButton);
            nombreText = FindViewById<EditText>(Resource.Id.nombreText);
            edadText = FindViewById<EditText>(Resource.Id.edadText);
            descripcionText = FindViewById<EditText>(Resource.Id.descText);
            fotoText = FindViewById<EditText>(Resource.Id.imgText);
            tipoSpin = FindViewById<Spinner>(Resource.Id.tipoSpinner);
            fotoView = FindViewById<ImageView>(Resource.Id.fotoImg);

            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tipo_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            tipoSpin.Adapter = adapter;

            addButton.Click += ClickAdd;

            cameraButton.Click += ClickCamera;

            nombreText.FocusChange += focusTextField;
            descripcionText.FocusChange += focusTextField;
            edadText.FocusChange += focusTextField;
            fotoText.FocusChange += focusTextField;

            
            // Create your application here
        }

        private void focusTextField(object sender, View.FocusChangeEventArgs e)
        {
            EditText textField = (EditText) sender;
            if(e.HasFocus)
            {
                cleanTextField(textField);
            }

        }

        private void cleanTextField(object sender)
        {
            EditText textField = (EditText) sender;
            textField.Text = "";
        }

        private void ClickAdd(object sender, EventArgs e)
        {
            if(nombreText.Text.Equals("")||nombreText.Text.Equals("Name of the pet"))
            {
                edadText.Text = "";
            }
            else
            {
                Mascota nueva = new Mascota();
                nueva.nombre = nombreText.Text;
                nueva.foto = fotoText.Text;
                nueva.estado = 1;
                nueva.descripcion = descripcionText.Text;
                nueva.edad = int.Parse(edadText.Text);
                nueva.tipo = tipoSpin.SelectedItemPosition+1;
                ConectorMascota.publicarMascota(nueva);
            }
            StartActivity(typeof(MainActivity));
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void ClickCamera (object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

          //  int height = Resources.DisplayMetrics.HeightPixels;
           // int width = cameraButton.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(1024, 768);
            if (App.bitmap != null)
            {
                fotoView.SetImageBitmap(App.bitmap);
              //  App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }
    }
}