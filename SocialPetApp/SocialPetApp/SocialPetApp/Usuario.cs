using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class Usuario
    {
        public int id_user;
        public string nombre { get; set; }
        public string access_token { get; set; }
        public string username { get; set; }

        public string apellido { get; set; }
        public int roles;
        private int v1;
        private string v2;

        public Usuario()
        {

            id_user = 0;
            nombre = "";
            access_token = "";
            username = "";
            apellido = "";
            roles = 0;
        }

        public Usuario(int id_user, string nombre, string access_token, string username, string apellido, int roles)
        {

            this.id_user = id_user;
            this.nombre = nombre;
            this.access_token = access_token;
            this.username = username;
            this.apellido = apellido;
            this.roles = roles;
        }

        public Usuario(dynamic d)
        {
            id_user = Convert.ToInt32(d.id_user); 
            nombre = d.nombre;
            access_token = d.access_token;
            username = d.username;
            apellido = d.apellido;
            roles = Convert.ToInt32(d.roles);
        }

        public Usuario(int v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public object ToJSonPost()
        {
            return new
            {
                id_user = this.id_user,
                nombre = this.nombre,
            };
        }

        public object ToJSonPut()
        {
            return new
            {
                id_user = this.id_user,
                nombre = this.nombre,
            };
        }
    }

    
}
