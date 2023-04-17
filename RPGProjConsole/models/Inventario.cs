using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class Inventario
    {
        public int Id { get; set; }
        public double PesoTotal { get; set; }
        public double Dinheiro { get; set; }
        public List<InventarioArma> InventarioArmas { get; set; }
        public List<InventarioArmadura> InventarioArmaduras { get; set; }
        public List<InventarioItem> InventarioItems { get; set; }
    }
}
