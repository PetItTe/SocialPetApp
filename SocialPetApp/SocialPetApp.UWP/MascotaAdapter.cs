using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SocialPetApp.UWP
{
    class MascotaAdapter
    {
        BitmapImage bitmap;
        string nombre;
        int edad;
        string celular;
        string mail;
        string descripcion;

        public static List<MascotaAdapter> obtenerTodos(List<Mascota> mascotas)
        {
            MascotaAdapter mAdap = new MascotaAdapter();
            List<MascotaAdapter> mascotaAdapter = new List<MascotaAdapter>();
            foreach(Mascota m in mascotas)
            {
                mAdap.nombre = m.nombre;
                mAdap.descripcion = m.descripcion;
                mAdap.edad = m.edad;
                mAdap.bitmap.UriSource = new Uri(m.getFotoURL());
                mascotaAdapter.Add(mAdap);
                
            }
            return mascotaAdapter; 
        }
    }
}
