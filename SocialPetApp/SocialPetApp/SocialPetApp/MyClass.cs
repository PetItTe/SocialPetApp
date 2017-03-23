using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class MyClass
    {
        public MyClass()
        {
            
        }
        public static async Task<string> obtenerTodo()
        {
            var getResp = await "http://petitte.16mb.com/mascotas/1".GetStringAsync();
            return getResp;
        }
    }
}

