using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using System.Linq;

namespace SocialPetApp
{
    public class ConectorMascota
    {
        public const string baseURL = "http://petitte.16mb.com";
        public const string seccion = "mascotas";


        public ConectorMascota()
        {
            

        }

        public static async Task<List<Mascota>> ObtenerTodos(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina}).GetJsonListAsync();
            List<Mascota>  listFinal = new List<Mascota>();
            foreach(dynamic item in listMas)
            {
                listFinal.Add(new Mascota(item));
            }
            return listFinal;
        }

        public static async void CombinarMascotas(IList<Mascota> lista, int pagina)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            foreach (dynamic item in listMas)
            {
                lista.Add(new Mascota(item));
            }
        }


        public static async Task<Paginador> ObtenerTodosHeader(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).HeadAsync();
            var v = listMas.Headers;

            
            return new Paginador(
                Int32.Parse(v.GetValues("X-Pagination-Current-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Per-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Page-Count").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Total-Count").Single<string>())
                );



        }


        public static async Task<Mascota> ObtenerID(int id)
        {

            dynamic d = await baseURL.AppendPathSegment(seccion).AppendPathSegment(id).GetJsonAsync();
            Mascota m = new Mascota(d);       
            return m;
        }

        public static async void publicarMascota(Mascota m)
        {
            try { 
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .PostJsonAsync(m.ToJSonPost());
                 }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public static async void editarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .PutJsonAsync(m.ToJSonPut());              
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public static async void adoptarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .PutJsonAsync(m.ToJSonPutAdoptar());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public static async void eliminarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .DeleteAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }


        public static async Task<string> ObtenerDummy()
        {
            return await baseURL.AppendPathSegment(seccion).GetStringAsync();
        }
    }
}
