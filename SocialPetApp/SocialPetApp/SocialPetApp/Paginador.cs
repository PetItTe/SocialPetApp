using System;


namespace SocialPetApp
{
    public class Paginador
    {
        public int paginaActual { get; set; }
        public int itemPagina { get; set; }
        public int ultimaPagina { get; set; }
        public int totalRegistros { get; set; }

        public Paginador(int paginaActual, int itemPagina, int ultimaPagina, int totalRegistros)
        {
            this.paginaActual = paginaActual;
            this.itemPagina = itemPagina;
            this.ultimaPagina = ultimaPagina;
            this.totalRegistros = totalRegistros;
        }

    }
}
