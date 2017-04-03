using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class Usuario
    {
        public int id;
        public string nombre { get; set; }
        public string password { get; set; }


        public Usuario()
        {
            
            nombre = "";
            password = "";
        }

        public Usuario(dynamic d)
        {
            id = d.id;
            nombre = d.nombre;
            password = d.password;
        }

        public Usuario(string nombre, string password)
        {
            this.nombre = nombre;
            this.password = password;
        }

        public object ToJSonPost()
        {
            return new
            {
                id = this.id,
                nombre = this.nombre,
                password = this.password
            };
        }

        public object ToJSonPut()
        {
            return new
            {
                id = this.id,
                nombre = this.nombre,
                password = this.password
            };
        }
    }

    
}
