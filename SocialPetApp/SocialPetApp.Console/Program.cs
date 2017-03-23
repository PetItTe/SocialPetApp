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
            

            Mascota m = new Mascota(new {nombre = "juancito",  });

            string respeusta = await ConectorMascota.ObtenerDummy();
            List<Mascota> a = await ConectorMascota.ObtenerTodos();
            System.Console.WriteLine(respeusta);
        }
    }
}
