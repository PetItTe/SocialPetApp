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
        public string password { get; set; }
        public string localidad { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public int roles;
        private int v1;
        private string v2;

        public Usuario()
        {

            id_user = 0;
            nombre = "";
            password = "";
            access_token = "";
            username = "";
            roles = 0;
        }

        public Usuario(int id_user, string nombre, string access_token, string username, int roles)
        {

            this.id_user = id_user;
            this.nombre = nombre;
            this.access_token = access_token;
            this.username = username;

            this.roles = roles;
            password = "";
        }

        public Usuario(dynamic d)
        {
            id_user = Convert.ToInt32(d.id_user); 
            nombre = d.nombre;
            access_token = d.access_token;
            username = d.username;
            roles = Convert.ToInt32(d.roles);
            password = "";
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
                nombre = this.nombre,
                username = this.username,
                password = this.password,
                email = this.email,
                cel = this.celular,
                localidad = this.localidad
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
