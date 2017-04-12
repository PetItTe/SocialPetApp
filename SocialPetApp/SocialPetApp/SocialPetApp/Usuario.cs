using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;


namespace SocialPetApp
{
    public class Usuario
    {
        public int id_user { get; set; }
        public string nombre { get; set; }
        public string access_token { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string localidad { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public int roles { get; set; }


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

            IDictionary<string, object> dictionary_object = d;

            foreach (var prop in this.GetType().GetRuntimeProperties())
            {
                try
                {
                    if(prop.PropertyType == typeof(Int32))
                    {
                        prop.SetValue(this, Convert.ToInt32(dictionary_object[prop.Name]));
                    }
                    else if(prop.PropertyType == typeof(String))
                    {
                        prop.SetValue(this, dictionary_object[prop.Name]);
                    }                  
                }
                catch(Exception e)
                {
                    if (prop.PropertyType == typeof(Int32))
                    {
                        prop.SetValue(this, 0);
                    }
                    else if (prop.PropertyType == typeof(String))
                    {
                        prop.SetValue(this, "");
                    }
                }
                

            }

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
