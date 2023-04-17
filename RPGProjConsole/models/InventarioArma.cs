using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class InventarioArma
    {
        public int Id { get; set; }
        public Arma Arma { get; set; }
        public Inventario Inventario { get; set; }
        public int MunicaoCarregada { get; set; }
        public int MunicaoReserva { get; set; }

    }
}
