using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class ArvoreDeHabilidades
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Arvore { get; set; }
        public bool IsActive { get; set; }
        public int AdicionalMaximo { get; set; }
    }
}
