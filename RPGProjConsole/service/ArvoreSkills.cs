using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.Migrations;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.service
{
    class ArvoreSkills
    {
        public Jogador Jogador { get; set; }
        public List<JogadorArvore> JogadorArvores { get; set; }
        public List<ArvoreDeHabilidades> Arvores { get; set; }
        public RPGContext _context;
        public ArvoreSkills(Jogador jogador, RPGContext context)
        {
            _context = context;
            Arvores = _context.ArvoreHabilidades.ToList();
            JogadorArvores = _context.JogadorArvores
                .Include(x => x.ArvoreDeHabilidades)
                .Where(x => x.Jogador.Id == jogador.Id)
                .ToList();
        }
        public int DigitarValor()
        {
            int value;
            bool ok = int.TryParse(Console.ReadLine(), out value);
            if (ok)
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
        public void AdicionarArvore()
        {
            Console.WriteLine("Adicione uma árvore");
            Console.WriteLine("Árvores que você têm");
            Console.WriteLine();

            foreach (var arvore in JogadorArvores)
            {
                if (arvore.ArvoreDeHabilidades.IsActive)
                {
                    Console.WriteLine("{0} ativo {1} adicional", arvore.ArvoreDeHabilidades.Nome, arvore.QtdAdicional);
                }
                else
                {
                    Console.WriteLine("{0} passivo {1} adicional", arvore.ArvoreDeHabilidades.Nome, arvore.QtdAdicional);
                }
            }

            Console.WriteLine("Árvores que você têm disponível, escolha uma");
            Console.WriteLine();
            for (int i = 0; i < Arvores.Count; i++)
            {
                var idNecessario = Arvores[i].NecessarioTer;
                bool teste = false;
                foreach (var arvore in JogadorArvores)
                {
                    if (arvore.ArvoreDeHabilidades.Id == idNecessario)
                    {
                        teste = true;
                    }
                }
                if (teste)
                {
                    if (Arvores[i].IsActive)
                    {
                        Console.WriteLine("{0} - {1} ativo", i + 1, Arvores[i].Nome);
                    }
                    else
                    {
                        Console.WriteLine("{0} - {1} passivo", i + 1, Arvores[i].Nome);
                    }
                }
            }
            var arvoreEscolhida = DigitarValor();

            var arvoreEscolhidaObj = Arvores[arvoreEscolhida - 1];

            var jogadorArvoreNova = new JogadorArvore()
            {
                ArvoreDeHabilidades = arvoreEscolhidaObj,
                EscolhaId = 0,
                Jogador = Jogador
            };
        }
        //ARVORE CRIACAO
        public void ImprovisarArma()
        {
            var improvisarArma = JogadorArvores
                .FirstOrDefault(x => x.ArvoreDeHabilidades.Id == 1);//ID DA ARVORE
            if(improvisarArma == null)
            {
                Console.WriteLine("Você não têm esta árvore");
            }
            else
            {
                Jogador.ArmaEquipada = new Arma
                {
                    Nome = "Arma improvisada",
                    Dados = 1,
                    ValorDado = 4,
                    IncrementoDecisivo = 2,
                    IsCorpoACorpo = true,
                    Durabilidade = 3
                };
            }     
        } 
        //ARVORE COMBATE
        public int InimigoPredileto()
        {
            var inimigoPredileto = JogadorArvores
                .FirstOrDefault(x => x.ArvoreDeHabilidades.Id == 1);//ID DA ARVORE
            if (inimigoPredileto == null)
            {
                Console.WriteLine("Você não têm esta árvore");
                return 0;
            }
            else
            {
                if(inimigoPredileto.EscolhaId == 0)
                {
                    Console.WriteLine("Escolha um mob para ser seu inimigo predileto");
                    var mobs = _context.Npcs.ToList();
                    for(int i = 0; i < mobs.Count; i++)
                    {
                        Console.WriteLine("{0} - {1}",i + 1 , mobs[i]);
                    }
                    var escolha = DigitarValor();
                    inimigoPredileto.EscolhaId = mobs[escolha - 1].Id;
                    _context.Entry(inimigoPredileto).State = EntityState.Modified;
                    _context.Update(inimigoPredileto);
                    _context.SaveChanges();
                    return inimigoPredileto.EscolhaId;
                }
                else
                {
                    return inimigoPredileto.EscolhaId;
                }
            }
        }
        //ARVORE FURTIVIDADE
        public int PassosSilenciosos()
        {
            var passosSilenciosos = JogadorArvores
                .FirstOrDefault(x => x.ArvoreDeHabilidades.Id == 1);//ID DA ARVORE
            if (passosSilenciosos == null)
            {
                Console.WriteLine("Você não têm esta árvore");
                return 0;
            }
            else
            {
                return passosSilenciosos.QtdAdicional * 4;
            }
        }
        //CURA
        public int Cura()
        {
            var cura = JogadorArvores
                .FirstOrDefault(x => x.ArvoreDeHabilidades.Id == 1);//ID DA ARVORE
            if (cura == null)
            {
                Console.WriteLine("Você não têm árvore de cura");
                return 0;
            }
            else
            {
                Console.WriteLine("{0} da árvore de cura", cura.QtdAdicional);
                return cura.QtdAdicional;
            }
        }
    }
}
