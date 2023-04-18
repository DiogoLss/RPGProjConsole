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
    class Partida
    {
        public readonly RPGContext _context;
        public PericiaConfig PericiaConfig { get; set; }
        public bool IsPartidaOn { get; set; }
        public List<Jogador> Jogadores { get; set; }
        public List<Npcs> Npcs { get; set; }
        public List<Npcs> Mobs { get; set; }

        public Partida(RPGContext context)
        {
            _context = context;
            Jogadores = _context.Jogadores
                .Include(j => j.ArmaEquipada)
                .Include(j => j.ArmaduraEquipada)
                .ToList();
            IsPartidaOn = true;
            for(int i = 0; i < Jogadores.Count; i++)
            {
                Jogadores[i].IsNpc = false;
                Jogadores[i].InventarioObj = new InventarioConfig(Jogadores[i],_context);
            }
            PericiaConfig = new PericiaConfig(_context, this);
        }
        public void ComecarPartida()
        {
            while (IsPartidaOn)
            {
                Console.Clear();
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Mostrar personagens na partida");
                Console.WriteLine("2 - Entrar em combate");
                Console.WriteLine("3 - Adicionar mob à partida");
                Console.WriteLine("4 - Abrir inventário");
                Console.WriteLine("5 - Perícia");
                Console.WriteLine("");
                Console.WriteLine("Finalizar partida: 469");
                Console.WriteLine();

                var value = DigitarValor();

                if (value == 0)
                {
                    Console.WriteLine("Digite um valor válido");
                    Console.Clear();
                }
                else
                {
                    if (value == 1)
                    {
                        Console.WriteLine("1 para jogadores 2 para mobs 3 para npcs");
                        var escolha = DigitarValor();
                        if(escolha == 1)
                        {
                            ShowPlayerssStatus(Jogadores);
                        }
                        else if(escolha == 2)
                        {
                            if(Mobs is null)
                            {
                                Console.Clear();
                                Console.WriteLine("Não há mobs na partida, enter para continua");  
                                Console.ReadLine();
                            }
                            else
                            {
                                ShowMobssStatus(Mobs);
                            }
                            
                        }
                        else if(escolha == 3)
                        {
                            if (Mobs is null)
                            {
                                Console.Clear();
                                Console.WriteLine("Não há npcs na partida, enter para continua");
                                Console.ReadLine();
                            }
                            else
                            {
                                ShowMobssStatus(Npcs);
                            }
                        }
                        
                    }
                    else if(value == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Você está entrando em combate!");
                        EntrarEmCombate();
                    }
                    else if(value == 3)
                    {
                        AdicionarPersonagemACena();
                    }
                    else if (value == 4)
                    {
                        Console.WriteLine("Quer abrir inventário de qual jogador?");
                        for(int i = 0; i< Jogadores.Count; i++)
                        {
                            Console.WriteLine("{0} - {1}", i + 1, Jogadores[i].Nome);
                        }
                        var escolha = DigitarValor();
                        Jogadores[escolha - 1].InventarioObj.AbrirOpcoesInventario();
                    }
                    else if (value == 5)
                    {
                        PericiaConfig.FazerTesteDePericiaContraMestre(false, 0);
                    }
                    else if(value == 469){
                        IsPartidaOn = false;
                        _context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            }
            
        }
        public void ShowPlayerssStatus(List<Jogador> Jogadores)
        {
            Console.Clear();
            foreach (var jogador in Jogadores)
            {
                Console.WriteLine("Nome: " + jogador.Nome);
                Console.WriteLine("HP: " + jogador.Vitalidade);
                Console.WriteLine("Medo: " + jogador.Medo);
                Console.WriteLine("CA: " + jogador.CA);
                Console.WriteLine("EX: " + jogador.Exp);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public void ShowMobssStatus(List<Npcs> Mobs)
        {
            Console.Clear();
            foreach (var mob in Mobs)
            {
                Console.WriteLine("Nome: " + mob.Nome);
                Console.WriteLine("HP: " + mob.Vitalidade);
                Console.WriteLine("CA: " + mob.CA);
                Console.WriteLine("EX: " + mob.Exp);
                Console.WriteLine();
                Console.WriteLine();
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
        public int Escolher()
        {
            int value;
            bool ok = int.TryParse(Console.ReadLine(), out value);
            if (ok)
            {
                return value;
            }
            else
            {
                return 123;
            }
        }
        public void EntrarEmCombate()
        {
            var combate = new Combate(this);
            combate.CriarCombate();
        }
        public void AdicionarPersonagemACena()
        {
            Console.WriteLine("O que deseja adicionar? npc: 1 ou mob: 2");
            Console.WriteLine("3 para sair");
            var value = DigitarValor();
            if (value == 1)
            {
                var npcs = _context.Npcs.Where(n => !n.IsMob).ToList();
                Console.WriteLine("Qual Npc deseja adicionar?");
                for (int i = 0; i < npcs.Count; i++)
                {
                    Console.WriteLine(npcs[i].Nome + " digite " + i);
                }
                var escolha = Escolher();
                if (escolha >= 0 && escolha < npcs.Count)
                {
                var mob = npcs[escolha];

                    if (Mobs is null)
                    {
                        Mobs = new List<Npcs> { mob };
                    }
                    else
                    {
                        Mobs.Add(mob);
                    }
                }
                else if(escolha == 123)
                {
                    Console.Clear();
                    Console.WriteLine("Digite um valor válido, enter para continuar");
                    Console.ReadLine();
                    AdicionarPersonagemACena();
                }
                else
                {
                    AdicionarPersonagemACena();
                }
                Console.Clear();
            }
            else if(value == 2)
            {
                var npcs = _context.Npcs.Where(n => n.IsMob).ToList();
                Console.WriteLine("Qual mob deseja adicionar?");
                //Console.WriteLine("Digite os números separados por virgula");
                for (int i = 0; i < npcs.Count; i++)
                {
                    Console.WriteLine(npcs[i].Nome + " digite " + i);
                }
                var escolha = DigitarValor();
                Console.WriteLine("Quantos?");
                var quantidade = Escolher();
                if(escolha >= 0 && escolha < npcs.Count)
                {
                    var mob = npcs[escolha];
                    for (int i = 0; i < quantidade; i++)
                    {
                        var mobb = new Npcs(mob);
                        if(Mobs is null)
                        {
                            Mobs = new List<Npcs> { mobb };
                        }
                        else
                        {
                            Mobs.Add(mobb);
                        }
                        
                    }
                }
                else if (escolha == 123)
                {
                    Console.Clear();
                    Console.WriteLine("Digite um valor válido, enter para continuar");
                    Console.ReadLine();
                    AdicionarPersonagemACena();
                }
                else
                {
                    AdicionarPersonagemACena();
                }
                Console.Clear();
            }
            else if(value == 3)
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Digite um valor válido, enter para continuar");
                Console.ReadLine();
                Console.Clear();
                AdicionarPersonagemACena();
            }
        }
    }
}
