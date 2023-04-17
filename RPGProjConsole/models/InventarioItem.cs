using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class InventarioItem
    {
        public int Id { get; set; }
        public ItemGeneral Item { get; set; }
        public Inventario Inventario { get; set; }
        public int Qtd { get; set; }
    }
}
