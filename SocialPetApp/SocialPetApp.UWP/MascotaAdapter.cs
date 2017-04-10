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

        public static List<MascotaAdapter> obtenerTodos(List<Mascota> mascotas)
        {
            List<MascotaAdapter> mascotaAdapter = new List<MascotaAdapter>();
            foreach(Mascota m in mascotas)
            {
                Uri myUri = new Uri(m.getFotoURL(), UriKind.Absolute);
                BitmapImage bmp = new BitmapImage(myUri);
                
                mascotaAdapter.Add(new MascotaAdapter { foto = new BitmapImage(myUri), nombre = "Nombre: "+m.nombre, edad = "Edad: "+m.edad.ToString(), celular = "Celular: "+"", eMail = "Email: "+"", descripcion = "Descripcion: "+m.descripcion });
                
            }
            return mascotaAdapter; 
        }
    }
}
