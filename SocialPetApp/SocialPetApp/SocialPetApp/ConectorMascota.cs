using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using System.Linq;
using Java.IO;
using System.IO;

namespace SocialPetApp
{
    public class ConectorMascota
    {
        public const string baseURL = "http://petitte.16mb.com";
        public const string seccion = "mascotas";
        public Usuario user;


        public ConectorMascota(Usuario user)
        {
            this.user = user;

        }

        public async Task<List<Mascota>> ObtenerTodos(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina}).WithBasicAuth(user.access_token,"").GetJsonListAsync();
            List<Mascota>  listFinal = new List<Mascota>();
            Mascota m;
            foreach(dynamic item in listMas)
            {
                m = new Mascota(item);
                listFinal.Add(new Mascota(item));              
            }
            return listFinal;
        }

        

        public async Task<List<Mascota>> ObtenerAdoptados(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).AppendPathSegment("adoptados").SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").GetJsonListAsync();
            List<Mascota> listFinal = new List<Mascota>();
            Mascota m;
            foreach (dynamic item in listMas)
            {
                m = new Mascota(item);
                listFinal.Add(new Mascota(item));
                
            }
            return listFinal;
        }


        public async Task<List<Mascota>> ObtenerSubidos(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).AppendPathSegment("subidos").SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").GetJsonListAsync();
            List<Mascota> listFinal = new List<Mascota>();
            foreach (dynamic item in listMas)
            {
                listFinal.Add(new Mascota(item));
                
            }
            return listFinal;
        }

        public async void CombinarMascotas(IList<Mascota> lista, int pagina)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").GetJsonListAsync();
            foreach (dynamic item in listMas)
            {
                lista.Add(new Mascota(item));
            }
        }


        public async Task<Paginador> ObtenerTodosHeader(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").HeadAsync();
            var v = listMas.Headers;       
            return new Paginador(
                Int32.Parse(v.GetValues("X-Pagination-Current-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Per-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Page-Count").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Total-Count").Single<string>())
                );
        }

        public async Task<Paginador> ObtenerAdoptadosHeader(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).AppendPathSegment("adoptados").SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").HeadAsync();
            var v = listMas.Headers;
            return new Paginador(
                Int32.Parse(v.GetValues("X-Pagination-Current-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Per-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Page-Count").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Total-Count").Single<string>())
                );
        }

        public async Task<Paginador> ObtenerSubidosHeader(int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).AppendPathSegment("subidos").SetQueryParams(new { page = pagina }).WithBasicAuth(user.access_token, "").HeadAsync();
            var v = listMas.Headers;
            return new Paginador(
                Int32.Parse(v.GetValues("X-Pagination-Current-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Per-Page").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Page-Count").Single<string>()),
                Int32.Parse(v.GetValues("X-Pagination-Total-Count").Single<string>())
                );
        }

        public async Task<Mascota> ObtenerID(int id)
        {

            dynamic d = await baseURL.AppendPathSegment(seccion).AppendPathSegment(id).WithBasicAuth(user.access_token, "").GetJsonAsync();
            Mascota m = new Mascota(d);       
            return m;
        }

        public async void publicarMascota(Mascota m, Stream file)
        {
            try {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .WithBasicAuth(user.access_token, "")
                     .PostMultipartAsync(mp => mp
                     .AddFile("UploadForm[imageFile]", file, "foto.jpg")                    // local file path       // file stream
                     .AddJson("json", m.ToJSonPost()));         // json; // URL-encoded                      
             //   .PostJsonAsync(m.ToJSonPost());
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public async void editarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .WithBasicAuth(user.access_token, "")
                    .PutJsonAsync(m.ToJSonPut());              
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public async void adoptarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .WithBasicAuth(user.access_token, "")
                    .PutJsonAsync(m.ToJSonPutAdoptar());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public async void eliminarMascota(Mascota m)
        {
            try
            {
                var result = await baseURL
                    .AppendPathSegment(seccion)
                    .AppendPathSegment(m.id_mas)
                    .WithBasicAuth(user.access_token, "")
                    .DeleteAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }


        public async Task<string> ObtenerDummy()
        {
            return await baseURL.AppendPathSegment(seccion).WithBasicAuth(user.access_token, "").GetStringAsync();
        }

        
    }
}
