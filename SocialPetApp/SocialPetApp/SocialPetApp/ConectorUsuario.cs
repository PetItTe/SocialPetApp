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
        public const string seccion = "usuarios";

        public ConectorUsuario()
        {

        }

        public static async Task<Usuario> LogIn(Usuario user, int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            Usuario userList;
            foreach (dynamic item in listMas)
            {
                  userList = new Usuario(item);
                if (user.nombre.Equals(userList.nombre)&&user.password.Equals(userList.password))
                {
                    user.id = userList.id;
                    return user;
                }
            }
            return user;
        }

        public static async void Register(Usuario user)
        {
            if (!(await Exist(user)))
            {
                try
                {
                    var result = await baseURL
                        .AppendPathSegment(seccion)
                        .PostJsonAsync(user.ToJSonPost());
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
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

        public static async Task<List<Mascota>> ObtenerAdoptados(Usuario user, int pagina = 1)
        {
            var listMas = await baseURL.AppendPathSegment(seccion).SetQueryParams(new { page = pagina }).GetJsonListAsync();
            List<Mascota> listFinal = new List<Mascota>();
            Mascota m;
            foreach (dynamic item in listMas)
            {
                m = new Mascota(item);
                if (m.user_adopt == user.id)
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
                if (m.user_alta == user.id)
                {
                    listFinal.Add(m);
                }
            }
            return listFinal;
        }
    }
}
