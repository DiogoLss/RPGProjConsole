using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class Arma : Item
    {
        public string TipoDeDano { get; set; }
        public string Dano { get; set; }
        public int Dados { get; set; }
        public int ValorDado { get; set; }
        public int IncrementoDecisivo { get; set; }
        public bool IsCorpoACorpo { get; set; }
        public int Durabilidade { get; set; }
        public int MunicaoMaxima { get; set; }
        public bool DamageOverTime { get; set; }
        public int DadosDOT { get; set; }
        public int ValorDadoDOT { get; set; }

    }
}
