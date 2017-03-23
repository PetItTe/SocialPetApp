using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class Mascota
    {
        public const int ESTADO_PUBLICADO = 1;
        public const int ESTADO_ADOPTADO = 2;
        public const int ESTADO_BORRADO = 3;

        public const int TIPO_PERRO = 1;
        public const int TIPO_GATO = 2;

        public int id_mas;
        public string nombre;
        public string descripcion;
        public int edad;
        public int tipo;
        public string foto;
        public int user_alta;
        public int user_adopt;
        public int estado;

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
            id_mas = Convert.ToInt32(d.id_mas);
            nombre = d.nombre;
            descripcion = d.descripcion;
            edad = Convert.ToInt32(d.edad);
            tipo = Convert.ToInt32(d.tipo);
            foto = d.foto;
            user_alta = Convert.ToInt32(d.user_alta);
            user_adopt = Convert.ToInt32(d.user_adopt);
            estado = Convert.ToInt32(d.estado);
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
    }
}
