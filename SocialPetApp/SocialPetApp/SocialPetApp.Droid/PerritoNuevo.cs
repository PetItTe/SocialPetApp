using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PerritoNuevo);
            addButton = FindViewById<Button>(Resource.Id.addButton);
            nombreText = FindViewById<EditText>(Resource.Id.nombreText);
            edadText = FindViewById<EditText>(Resource.Id.edadText);
            descripcionText = FindViewById<EditText>(Resource.Id.descText);
            fotoText = FindViewById<EditText>(Resource.Id.imgText);
            tipoSpin = FindViewById<Spinner>(Resource.Id.tipoSpinner);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tipo_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            tipoSpin.Adapter = adapter;
            addButton.Click += ClickAdd;
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
            if(nombreText.Text.Equals(""))
            {
                //do Nothing
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
    }
}