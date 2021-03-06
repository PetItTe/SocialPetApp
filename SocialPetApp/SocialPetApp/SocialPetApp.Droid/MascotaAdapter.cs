using System.Collections.Generic;
using Android.App;
using Android.Widget;
using SocialPetApp;
using System;
using Java.IO;
using Android.Graphics;
using Java.Net;
using System.Net;
using Com.Bumptech.Glide;

namespace SocialPetApp.Droid {
	/// <summary>
	/// Adapter that presents Tasks in a row-view
	/// </summary>
	public class MascotaAdapter : BaseAdapter<Mascota>{
		Activity context = null;
		public IList<Mascota> mascotas = new List<Mascota>();
        public static bool endOfList { get; set; }
        private ConectorMascota conMas;
        private int estadoSpin;


        

        public MascotaAdapter(Activity context, 
            IList<Mascota> mascotas, ConectorMascota conMas, int estadoSpin) : base ()
		{
            this.conMas = conMas;
            endOfList = false;
			this.context = context;
			this.mascotas = mascotas;
            this.estadoSpin = estadoSpin;

        }

        public async void combinarMascotas(int pagina)
        {
            List<Mascota> mascota = new List<Mascota>();
            mascota = await conMas.ObtenerTodos(pagina);
            foreach (Mascota m in mascotas)
            {
                this.mascotas.Add(m);
            }
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

        public override Android.Views.View GetView(int position,
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
            var txtLocalidad = view.FindViewById<TextView>(Resource.Id.localidadText);
            // var txtTipo = view.FindViewById<TextView>(Resource.Id.typeText);
            var mascImg = view.FindViewById<ImageView>(Resource.Id.mascImg);
            var txtDue�o = view.FindViewById<TextView>(Resource.Id.due�oText);
            var txtDescripcion = view.FindViewById<TextView>(Resource.Id.descText);
            var txtMail = view.FindViewById<TextView>(Resource.Id.emailText);
            var txtCel = view.FindViewById<TextView>(Resource.Id.telText);
            var txtNombrePersona = view.FindViewById<TextView>(Resource.Id.nombrePersonaText);


            //Assign item's values to the various subviews
            txtNombre.Text = item.nombre;
            txtDescripcion.Text = item.descripcion;
            txtLocalidad.Text = item.localidad;
            txtDue�o.Text = item.username;

            if (estadoSpin == 1)
            {
                //que hacer si el usuario selecciona la opcion ADOPTADOS del spinner
                txtNombrePersona.Text = item.persona;
                txtNombrePersona.Visibility = Android.Views.ViewStates.Visible;
                txtCel.Text = item.cel;
                txtCel.Visibility = Android.Views.ViewStates.Visible;
                txtMail.Text = item.email;
                txtMail.Visibility = Android.Views.ViewStates.Visible;

            }
            else if (estadoSpin == 2)
            {
                //que hacer si el usuario selecciona la opcion SUBIDOS del spinner

            }
            else
            {
                //que hacer si el usuario selecciona la opcion HOME del spinner
            }



            //TE INSTALAS EL GLIDE DESDE NUGET
            //TODAVIA NO ANDA LOCO, AHORA ANDA A VER->OTRAS VENTANAS->CONSOLA DE ADMINISTRAR PAQUETES O ALGO ASI Y
            //PONES ESTE CODIGO Install-Package Xamarin.Android.Support.v4 -Version 23.0.1.3 sos imputable
            Glide.With(context).Load(item.getFotoURL()).Into(mascImg);
            /*if (item.tipo == 1)
            {
                txtTipo.Text = "PERRO";
            }
            else
            {
                txtTipo.Text = "GATO";
            }*/
            //txtDue�o.Text = item.user_adopt.ToString();
            txtEdad.Text = item.edad.ToString() + (item.edad == 1 ? " a�o" : " a�os");

            //Codigo loco
            //ESTO NO HACE UNA GOD DAMN SHIT
            // SI QUE ANDA FORRO, YO LO PROBE Y ME ANDA
            if (reachedEndOfList(position))
            {
                endOfList = true;
                loadMoreData();
                System.Console.WriteLine("es el final de la lista loco \n \naca andamo todo");
            }
            //Finally return the view
            return view;
        }

        private bool reachedEndOfList(int position)
        {
            // can check if close or exactly at the end
            return position == Count - 1;
        }

        private void loadMoreData()
        {
            // Perhaps set flag to indicate you're loading and check flag before proceeding with AsyncTask or whatever
            //Llegaste al final
        }

      
    }

}