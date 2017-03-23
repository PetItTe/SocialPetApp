using System.Collections.Generic;
using Android.App;
using Android.Widget;
using SocialPetApp;
using System;


namespace SocialPetApp.Droid {
	/// <summary>
	/// Adapter that presents Tasks in a row-view
	/// </summary>
	public class ParticipanteAdapter : BaseAdapter<Mascota> {
		Activity context = null;
		IList<Mascota> mascotas = new List<Mascota>();
		
		public ParticipanteAdapter(Activity context, 
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
            


			//Finally return the view
			return view;
		}
	}
}