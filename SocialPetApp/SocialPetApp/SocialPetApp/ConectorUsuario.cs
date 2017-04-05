using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class ConectorUsuario
    {
        public const string baseURL = "http://petitte.16mb.com";
        public const string seccion = "users";



        public ConectorUsuario()
        {

        }

        public static async Task<Usuario> LogIn(string usuario, string clave)
        {
            dynamic d = await baseURL.AppendPathSegment(seccion).AppendPathSegment("login").WithBasicAuth(usuario,clave).GetJsonAsync();
            return new Usuario(d);
        }

        public static async Task<string> Register(Usuario user)
        {
            if (!(await Exist(user)))
            {
                try
                {
                    var result = await baseURL
                        .AppendPathSegment(seccion)
                        .PostJsonAsync(user.ToJSonPost());
                    return "Usuario registrado, por favor loggearse";
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }

            }
            return "El usuario ya existe";
        }

        public static async Task<bool> Exist(Usuario user, int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            Usuario userList;
            foreach (dynamic item in listMas)
            {
                userList = new Usuario(item);
                if (user.nombre.Equals(userList.nombre))
                {
                    return true;
                }
            }
            return false;
        }

        public static async Task<Usuario> ObtenerByID(int id)
        {

            dynamic d = await baseURL.AppendPathSegment(seccion).AppendPathSegment(id).GetJsonAsync();
            Usuario m = new Usuario(d);
            return m;
        }

        public static async Task<List<Mascota>> ObtenerAdoptados(Usuario user, int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            List<Mascota> listFinal = new List<Mascota>();
            Mascota m;
            foreach (dynamic item in listMas)
            {
                m = new Mascota(item);
                if (m.user_adopt == user.id_user)
                {
                    listFinal.Add(m);
                }
            }
            return listFinal;
        }

        public static async Task<List<Mascota>> ObtenerSubidos(Usuario user, int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            List<Mascota> listFinal = new List<Mascota>();
            Mascota m;
            foreach (dynamic item in listMas)
            {
                m = new Mascota(item);
                if (m.user_alta == user.id_user)
                {
                    listFinal.Add(m);
                }
            }
            return listFinal;
        }
    }
}
