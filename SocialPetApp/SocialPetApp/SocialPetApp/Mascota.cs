using System;
using Flurl;
using System.Collections.Generic;
using System.Reflection;

namespace SocialPetApp
{
    public class Mascota
    {
        public const int ESTADO_PUBLICADO = 1;
        public const int ESTADO_ADOPTADO = 2;
        public const int ESTADO_BORRADO = 3;

        public const int TIPO_PERRO = 1;
        public const int TIPO_GATO = 2;

        public int id_mas { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int edad { get; set; }
        public int tipo { get; set; }
        public string foto { get; set; }
        public int user_alta { get; set; }
        public int user_adopt { get; set; }
        public int estado { get; set; }
        public string localidad { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string cel { get; set; }
        public string persona { get; set; }
        

        public Mascota()
        {
            id_mas = 0;
            nombre = "";
            descripcion = "";
            edad = 0;
            tipo = 0;
            foto = "";
            user_alta = 0;
            user_adopt = 0;
            estado = 0;
        }

        public Mascota(dynamic d)
        {
            IDictionary<string, object> dictionary_object = d;

            foreach (var prop in this.GetType().GetRuntimeProperties())
            {
                try
                {
                    if (prop.PropertyType == typeof(Int32))
                    {
                        prop.SetValue(this, Convert.ToInt32(dictionary_object[prop.Name]));
                    }
                    else if (prop.PropertyType == typeof(String))
                    {
                        prop.SetValue(this, dictionary_object[prop.Name]);
                    }
                }
                catch (Exception e)
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

        public Mascota(string nombre, string descripcion, int edad, int tipo, string foto, int user_alta)
        {

            this.nombre = nombre;
            this.descripcion = descripcion;
            this.edad = edad;
            this.tipo = tipo;
            this.foto = foto;
            this.user_alta =user_alta;
        }

        public object ToJSonPost()
        {
            return new { nombre = this.nombre,
                descripcion = this.descripcion,
                edad = this.edad,
                tipo = this.tipo,
                foto = this.foto,
                user_alta = this.user_alta,
                user_adopt = this.user_adopt,
                estado = this.estado,
            };
        }

        public object ToJSonPut()
        {
            return new
            {
                nombre = this.nombre,
                descripcion = this.descripcion,
                edad = this.edad,
                tipo = this.tipo,
                foto = this.foto,
                user_adopt = this.user_adopt,
                estado = this.estado,
            };
        }

        public string getFotoURL()
        {
            return ConectorMascota.baseURL.AppendPathSegment(foto);
        }

        public object ToJSonPutAdoptar()
        {
            return new
            {
                user_adopt = this.user_adopt,
                estado = this.estado,
            };
        }
    }
}
