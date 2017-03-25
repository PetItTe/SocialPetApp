using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

            System.Console.ReadKey();
        }

        static async Task MainAsync()
        {


            //Mascota m = new Mascota("juancito","Perro gigante, diez metros mide vite", 4, 1,"dummy.jpg", 1);
            //ConectorMascota.publicarMascota(m);
            //string respeusta = await ConectorMascota.ObtenerDummy();
            
            List<Mascota> a = await ConectorMascota.ObtenerTodos();
            string respuesta = a.ToString();
            System.Console.WriteLine(respuesta);
        }
    }
}
