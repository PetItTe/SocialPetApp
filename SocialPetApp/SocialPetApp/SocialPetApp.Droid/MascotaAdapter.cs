using System.Collections.Generic;
using Android.App;
using Android.Widget;
using SocialPetApp;
using System;
using Java.IO;
using Android.Graphics;
using Java.Net;
using System.Net;

namespace SocialPetApp.Droid {
	/// <summary>
	/// Adapter that presents Tasks in a row-view
	/// </summary>
	public class MascotaAdapter : BaseAdapter<Mascota>{
		Activity context = null;
		IList<Mascota> mascotas = new List<Mascota>();

        public MascotaAdapter(Activity context, 
            IList<Mascota> mascotas) : base ()
		{
			this.context = context;
			this.mascotas = mascotas;
		}
		
		public override Mascota this[int position]
		{
			get { return mascotas[position]; }
		}
		
		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override int Count
		{
			get { return mascotas.Count; }
		}

        public override Android.Views.View GetView (int position,
            Android.Views.View convertView,
            Android.Views.ViewGroup parent)
        {
            // Get our object for position
            var item = mascotas[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            // will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
            var view = (convertView ??
                    context.LayoutInflater.Inflate(
                    Resource.Layout.TaskListItem,
                    parent,
                    false)) as LinearLayout;

            var txtNombre = view.FindViewById<TextView>(Resource.Id.nombreText);
            var txtEdad = view.FindViewById<TextView>(Resource.Id.ageText);
            var txtTipo = view.FindViewById<TextView>(Resource.Id.typeText);
            var mascImg = view.FindViewById<ImageView>(Resource.Id.mascImg);
            //var txtDueño = view.FindViewById<TextView>(Resource.Id.ownerText);
            var txtDescripcion = view.FindViewById<TextView>(Resource.Id.descText);


            //Assign item's values to the various subviews
            txtNombre.Text = item.nombre;
            txtDescripcion.Text = item.descripcion;
            try
            {
                var imageBitmap = GetImageBitmapFromUrl(item.getFotoURL());
                mascImg.SetImageBitmap(imageBitmap);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("WTF LOCO" + e.Message);
            }
            if (item.tipo == 1)
            {
                txtTipo.Text = "PERRO";
            }
            else
            {
                txtTipo.Text = "GATO";
            }
            //txtDueño.Text = item.user_adopt.ToString();
            txtEdad.Text = "Edad: " + item.edad.ToString() + (item.edad==1?" año":" años");

            //Finally return the view
            return view;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap bmp = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if(imageBytes != null && imageBytes.Length > 0)
                {
                    bmp = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
                return bmp;
        }

    }

}