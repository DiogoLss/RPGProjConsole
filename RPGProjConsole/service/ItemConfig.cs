using RPGProjConsole.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.service
{
    class ItemConfig
    {
        public RPGContext _context;
        public ItemConfig( RPGContext context)
        {
            _context = context;
        }
        public void ListarItems()
        {

        }
        public void ListarArmas()
        {
            var armas = _context.Armas.ToList();
            foreach (var arma in armas)
            {
                Console.WriteLine(arma.Nome);
            }
        }
        public void ListarArmaduras()
        {

        }
    }
}
