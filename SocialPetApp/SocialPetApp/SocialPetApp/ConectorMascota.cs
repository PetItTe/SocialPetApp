using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialPetApp
{
    class ConectorMascota
    {
        private const string baseURL = "http://petitte.16mb.com/";
        

        public ConectorMascota()
        {
            

        }

        public async Task<List<Mascota>> ObtenerTodo()
        {
            HttpClient client;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            var uri = new Uri(baseURL + "mascotas");

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
            }
                return null;
        }
    }
}
