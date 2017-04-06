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

        public static async Task<dynamic> Register(Usuario user)
        {
            
            var result = await baseURL
                .AppendPathSegment(seccion)
                .AppendPathSegment("signup")
                .PostJsonAsync(user.ToJSonPost());

            return result;
            

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


    }
}
