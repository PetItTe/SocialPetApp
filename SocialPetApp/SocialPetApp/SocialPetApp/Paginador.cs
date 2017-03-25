using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPetApp
{
    public class Paginador
    {
        int paginaActual { get; set; }
        int itemPagina { get; set; }
        int ultimaPagina { get; set; }
        int totalRegistros { get; set; }

        public Paginador(int paginaActual, int itemPagina, int ultimaPagina, int totalRegistros)
        {
            this.paginaActual = paginaActual;
            this.itemPagina = itemPagina;
            this.ultimaPagina = ultimaPagina;
            this.totalRegistros = totalRegistros;
        }
    }
}
