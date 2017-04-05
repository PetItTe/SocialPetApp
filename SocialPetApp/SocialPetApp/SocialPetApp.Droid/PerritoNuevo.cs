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
        int id;
        Mascota mascota = new Mascota();
        Usuario user = new Usuario();
        ConectorMascota conMas;

        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        protected async override void OnCreate(Bundle savedInstanceState)
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
            fotoView.Visibility = ViewStates.Invisible;

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

            user.id_user = Intent.GetIntExtra("usuario", 0);
            user = await ConectorUsuario.ObtenerByID(user.id_user);

            id = Intent.GetIntExtra("Position", -1);

            conMas = new ConectorMascota(user);
            

            if (id > -1)
            {
                mascota = await conMas.ObtenerID(id);
                nombreText.Text = mascota.nombre;
                edadText.Text = mascota.edad.ToString();
                descripcionText.Text = mascota.descripcion;
                fotoText.Text = mascota.getFotoURL();
                if(mascota.tipo == 1)
                {
                    tipoSpin.SetSelection(0);
                }
                else
                {
                    tipoSpin.SetSelection(1);
                }
            }

            
            // Create your application here
        }

        private void focusTextField(object sender, View.FocusChangeEventArgs e)
        {
            //al dar click al objeto
            EditText textField = (EditText) sender;
            if(e.HasFocus)
            {
                cleanTextField(textField);
            }

        }

        private void cleanTextField(object sender)
        {
            //que hacer si se da click en un textfield
            EditText textField = (EditText) sender;
            textField.Text = "";
        }

        private void ClickAdd(object sender, EventArgs e)
        {
            //que hacer cuando se da click al boton add
            if(nombreText.Text.Equals("")||nombreText.Text.Equals("Name of the pet"))
            {
                edadText.Text = "";
            }
            else
            {
                mascota.nombre = nombreText.Text;
                mascota.foto = fotoText.Text;
                mascota.estado = 1;
                mascota.descripcion = descripcionText.Text;
                mascota.edad = int.Parse(edadText.Text);
                mascota.tipo = tipoSpin.SelectedItemPosition+1;
                mascota.user_alta = user.id_user;
                if (id > -1)
                {
                    conMas.editarMascota(mascota);
                }
                else
                {
                    conMas.publicarMascota(mascota);
                }
            }
            StartActivity(typeof(MainActivity));
        }

        private void CreateDirectoryForPictures()
        {
            //determina el directorio donde se van a guardar las imagenes
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
            //se asegura que al telefono le funcione la camara
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void ClickCamera (object sender, EventArgs eventArgs)
        {
            //que hacer cuando se le da click a la imagen de la camara
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //que hacer cuando se toma la foto
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
            App.bitmap = App._file.Path.LoadAndResizeBitmap(380, 240);
            if (App.bitmap != null)
            {
                fotoView.SetImageBitmap(App.bitmap);
                fotoView.Visibility = ViewStates.Visible;
              //  App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }
    }
}