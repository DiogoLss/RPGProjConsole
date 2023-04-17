using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class InventarioArmadura
    {
        public int Id { get; set; }
        public Armadura Armadura { get; set; }
        public Inventario Inventario { get; set; }
    }
}
