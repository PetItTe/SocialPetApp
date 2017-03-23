using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialPetApp
{
    public class ConectorMascota
    {
        private const string baseURL = "http://petitte.16mb.com";
        

        public ConectorMascota()
        {
            

        }
        /*
        public static async Task<List<Mascota>> ObtenerTodo()
        {

            var getResp = await baseURL.AppendPathSegment("mascotas").GetStringAsync();

        
            return null;
        }
        */

        public static async Task<string> ObtenerDummy()
        {

            return await baseURL.AppendPathSegment("mascotas").GetStringAsync();

        }
    }
}
