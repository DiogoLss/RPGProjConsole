using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class JogadorArvore
    {
        public int Id { get; set; }
        public Jogador Jogador { get; set; }
        public ArvoreDeHabilidades ArvoreDeHabilidades { get; set; }
        public int QtdAdicional { get; set; }
    }
}
