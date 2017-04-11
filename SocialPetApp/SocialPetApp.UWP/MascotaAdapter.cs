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
        public BitmapImage foto { get; set; }
        public string nombre { get; set; }
        public string edad { get; set; }
        public string celular { get; set; }
        public string eMail { get; set; }
        public string descripcion { get; set; }
        public int id { get; set; }
        public static List<MascotaAdapter> mascotaAdapter;

        public static List<MascotaAdapter> obtenerTodos(List<Mascota> mascotas)
        {
            mascotaAdapter = new List<MascotaAdapter>();
            foreach(Mascota m in mascotas)
            {

                Uri myUri = new Uri(m.getFotoURL(), UriKind.Absolute);
                if (m.cel.Equals(""))
                {
                    
                    mascotaAdapter.Add(new MascotaAdapter { id=m.id_mas, foto = new BitmapImage(myUri), nombre = "Nombre: " + m.nombre, edad = "Edad: " + m.edad.ToString(), descripcion = "Descripcion: " + m.descripcion });
                }
                else
                {
                    mascotaAdapter.Add(new MascotaAdapter { id=m.id_mas, foto = new BitmapImage(myUri), nombre = "Nombre: " + m.nombre, edad = "Edad: " + m.edad.ToString(), celular = "Celular: " + m.cel, eMail = "Email: " + m.email, descripcion = "Descripcion: " + m.descripcion });
                }

            }
            return mascotaAdapter; 
        }
    }
}
