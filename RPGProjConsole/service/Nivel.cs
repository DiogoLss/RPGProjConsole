using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.service
{
    class Nivel
    {
        public Jogador Personagem { get; set; }
        public readonly RPGContext _context;
        public Nivel(Jogador personagem, RPGContext context)
        {
            Personagem = personagem;
            _context = context;
        }
        

        //public Personagem AumentarExp(Personagem personagem, Personagem alvoExp, int divisao)
        //{
        //    personagem.Exp += alvoExp.Exp / divisao;
        //    return personagem;
        //}
        public void TentarAumentarNivel()
        {
            var exp = Personagem.Exp;
            var nivel = Personagem.Nivel;
            if(exp < 1000)//1
            {
                Personagem.Nivel = 1;
                Personagem.AtaqueBase = 0;
                Personagem.PericiaMaxima = 4;
            }
            else if (exp < 3000)//2
            {
                Personagem.Nivel = 2;
                Personagem.AtaqueBase = 1;
                Personagem.PericiaMaxima = 5;
            }
            else if (exp < 6000)//3
            {
                Personagem.Nivel = 3;
                Personagem.AtaqueBase = 2;
                Personagem.PericiaMaxima = 6;
            }
            else if (exp < 10000)//4
            {
                Personagem.Nivel = 4;
                Personagem.AtaqueBase = 3;
                Personagem.PericiaMaxima = 7;
            }
            else if (exp < 15000)//5
            {
                Personagem.Nivel = 5;
                Personagem.AtaqueBase = 5;
                Personagem.PericiaMaxima = 8;
            }
            else if (exp < 21000)//6
            {
                Personagem.Nivel = 6;
                Personagem.AtaqueBase = 6;
                Personagem.PericiaMaxima = 9;
            }
            else if (exp < 28000)//7
            {
                Personagem.Nivel = 7;
                Personagem.AtaqueBase = 7;
                Personagem.PericiaMaxima = 10;
            }
            else if (exp < 36000)//8
            {
                Personagem.Nivel = 8;
                Personagem.AtaqueBase = 8;
                Personagem.PericiaMaxima = 11;
            }
            else if (exp < 45000)//9
            {
                Personagem.Nivel = 9;
                Personagem.AtaqueBase = 9;
                Personagem.PericiaMaxima = 12;
            }
            else if (exp < 55000)//10
            {
                Personagem.Nivel = 10;
                Personagem.AtaqueBase = 10;
                Personagem.PericiaMaxima = 13;
            }
            else if (exp < 66000)//11
            {
                Personagem.Nivel = 11;
                Personagem.AtaqueBase = 11;
                Personagem.PericiaMaxima = 14;
            }
            else if (exp < 78000)//12
            {
                Personagem.Nivel = 12;
                Personagem.AtaqueBase = 12;
                Personagem.PericiaMaxima = 15;
            }
            else if (exp < 91000)//13
            {
                Personagem.Nivel = 13;
                Personagem.AtaqueBase = 13;
                Personagem.PericiaMaxima = 16;
            }
            else if (exp < 105000)//14
            {
                Personagem.Nivel = 14;
                Personagem.AtaqueBase = 14;
                Personagem.PericiaMaxima = 17;
            }
            else if (exp < 120000)//15
            {
                Personagem.Nivel = 15;
                Personagem.AtaqueBase = 15;
                Personagem.PericiaMaxima = 18;
            }
            else if (exp < 136000)//16
            {
                Personagem.Nivel = 16;
                Personagem.AtaqueBase = 16;
                Personagem.PericiaMaxima = 19;
            }
            else if (exp < 153000)//17
            {
                Personagem.Nivel = 17;
                Personagem.AtaqueBase = 17;
                Personagem.PericiaMaxima = 20;
            }
            else if (exp < 171000)//18
            {
                Personagem.Nivel = 18;
                Personagem.AtaqueBase = 18;
                Personagem.PericiaMaxima = 21;
            }
            else if (exp < 190000)//19
            {
                Personagem.Nivel = 19;
                Personagem.AtaqueBase = 19;
                Personagem.PericiaMaxima = 22;
            }
            else//20
            {
                Personagem.Nivel = 20;
                Personagem.AtaqueBase = 20;
                Personagem.PericiaMaxima = 23;
            }
            if(nivel != Personagem.Nivel)
            {
                Console.WriteLine("{0} upou para o nível {1}!",
                    Personagem.Nome, Personagem.Nivel);
                Console.ReadLine();
                AdicionarPericias(Personagem);
                Personagem.ArvoreSkills.AdicionarArvore();
            }
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
        public void AdicionarPericias(Jogador jogador)
        {
            var x = jogador.Modifier(4);
            var qtd = ((2 + x) * 2) + x;
            Console.WriteLine("{0} pode distribuir {1} quantidade de perícias",
                jogador.Nome,
                qtd);
            Console.ReadLine();
            bool escolhendoPericias = true;
            var pericias = _context.Pericias.ToList();
            while (escolhendoPericias)
            {
                var list = new List<JogadorPericia>();
                Console.WriteLine("Escolha a perícia e a quantidade");
                for (int i = 0; i < pericias.Count; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, pericias[i].Descricao);
                }
                while (qtd != 0)
                {
                    Console.WriteLine("Perícia");
                    var pericia = DigitarValor();
                    Console.WriteLine("Quantidade");
                    var quantidadeEscolhida = DigitarValor();
                    var periciaQtd = new JogadorPericia()
                    {
                        Pericia = pericias[pericia - 1],
                        Quantidade = quantidadeEscolhida
                    };
                    list.Add(periciaQtd);
                }
                Console.WriteLine("Deseja confirmar 1 - sim 2 não");
                foreach (var item in list)
                {
                    Console.WriteLine("{0} pontos em {1}", item.Quantidade, item.Pericia.Descricao);
                }
                var resposta = DigitarValor();
                if (resposta == 1)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].Jogador = jogador;
                        var objJogador = _context.JogadorPericia.FirstOrDefault(
                            o => o.Pericia.Id == list[i].Pericia.Id);
                        if (objJogador == null)
                        {
                            _context.JogadorPericia.Add(list[i]);
                            _context.SaveChanges();
                        }
                        else
                        {
                            objJogador.Quantidade += list[i].Quantidade;
                            _context.Entry(objJogador).State = EntityState.Modified;
                            _context.Update(objJogador);
                            _context.SaveChanges();
                        }
                    }
                    escolhendoPericias = false;
                }
            }
        }
    }
}
