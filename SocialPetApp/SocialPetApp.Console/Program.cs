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
            //Paginador b = await ConectorMascota.ObtenerTodosHeader();
            //List<Mascota> a = await ConectorMascota.ObtenerTodos(2);
            //string respuesta = a.ToString();
            //System.Console.WriteLine(respuesta);
            Mascota m = await ConectorMascota.ObtenerID(5);
            m.foto = "img/img_1.jpg";
            m.nombre = "El Alex Wacho";
            m.descripcion = "Uno no sabe si es una persona o si es un chuchito";
            m.user_alta = 1;
            ConectorMascota.editarMascota(m);
            System.Console.WriteLine("Listo");


        }
    }
}
