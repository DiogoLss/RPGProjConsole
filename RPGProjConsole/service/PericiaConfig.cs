using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.models;
using System;
using System.Linq;


namespace RPGProjConsole.service
{
    class PericiaConfig
    {

        public RPGContext _context;
        public Partida _partida;
        public Dado Dados { get; set; } = new Dado();
        public PericiaConfig(RPGContext context,Partida partida)
        {
            _context = context;
            _partida = partida;
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
        public bool FazerTesteDePericiaContraCriaturas()
        {
            return true;
        }
        public bool FazerTesteDePericiaContraMestre(bool isCombate, int idJogador)
        {
            var periciaTeste = true;
            while (periciaTeste)
            {
                Console.Clear();
                Console.WriteLine("Este é um teste de?");
                Console.WriteLine();
                var testes = _context.Pericias.ToList();
                for(int i = 0; i < testes.Count; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, testes[i].Descricao);
                }

                var teste = DigitarValor();
                if(teste == 0)
                {
                    return true;
                }
                var testeObj = testes[teste - 1];
                var escolha = 0;
                if (isCombate)
                {
                    escolha = 2;
                }
                else
                {
                    Console.WriteLine("Todos farão o teste de {0}? 1 - sim 2 - não", testeObj.Descricao);
                    escolha = DigitarValor();
                }
                
                if (escolha == 0)
                {
                    Console.WriteLine("Não entendi");
                }
                else if(escolha == 1)
                {
                    var result = Dados.RodarDado("Mestre",1,20);
                    var falhos = 0;
                    foreach(var item in _partida.Jogadores)
                    {
                        var periciaN = _context.JogadorPericia
                            .Include(x => x.Pericia)
                            .FirstOrDefault(
                            x => x.Jogador.Id == item.Id
                            &&
                            x.Pericia.Id == testeObj.Id);


                        falhos += TestarPericia(item,result, testeObj, periciaN);
                    }
                    Console.WriteLine("{0} pessoas falharam", falhos);
                }
                else if (escolha == 2)
                {
                    Jogador jogador = null;
                    if (isCombate)
                    {
                        jogador = _partida.Jogadores.Find(x => x.Id == idJogador);
                    }
                    else
                    {
                        Console.WriteLine("Teste de perícia de quem?");
                        for (int i = 0; i < _partida.Jogadores.Count; i++)
                        {
                            Console.WriteLine("{0} - {1}", i + 1, _partida.Jogadores[i].Nome);
                        }
                        var jogadorIndex = DigitarValor();
                        jogador = _partida.Jogadores[jogadorIndex - 1];
                    }
                    

                    var result = Dados.RodarDado("Mestre", 1, 20);
                    var falhos = 0;
                    var periciaN = _context.JogadorPericia
                           .Include(x => x.Pericia)
                           .FirstOrDefault(
                           x => x.Jogador.Id == jogador.Id
                           &&
                           x.Pericia.Id == testeObj.Id);
                    falhos += TestarPericia(jogador, result, testeObj, periciaN);
                    Console.WriteLine("{0} pessoas falharam", falhos);
                    Console.ReadLine();
                }
            }
            
            return true;
        }
        public int TestarPericia(Jogador jogador, int result, Pericia periciaObj, JogadorPericia pericia)
        {
            var testandoPericia = true;

            while (testandoPericia)
            {
                if(periciaObj.SemTreinamento && pericia == null)
                {
                    Console.WriteLine("O jogador {0} falhou no teste porque não tem treinamento.",jogador.Nome);
                    Console.ReadLine();
                    return 1;
                }
                var dadoJogador = Dados.RodarDado(jogador.Nome, 1, 20);

                if(dadoJogador == 20)
                {
                    Console.WriteLine("Jogador {0} passou no teste com 20 no dado");
                    testandoPericia = false;
                    Console.ReadLine();
                    return 0;
                }
                if(pericia != null)
                {
                    dadoJogador += pericia.Quantidade;
                }
                var jogadorHabilidade = jogador.Modifier(periciaObj.IndexHabilidade);
                dadoJogador += jogadorHabilidade;
                if (jogador.ArmaduraEquipada != null)
                {
                    dadoJogador -= jogador.ArmaduraEquipada.PenalidadeDeDestreza;
                }

                if (dadoJogador >= result)
                {
                    if (pericia != null)
                    {
                        Console.WriteLine(
                        "Jogador {0} passou no teste com {1} no dado, {2} de perícia na " +
                        "habilidade {3} contra o mestre no seu total de {4}",
                        jogador.Nome,
                        dadoJogador - pericia.Quantidade,
                        pericia.Quantidade,
                        pericia.Pericia.Descricao,
                        result
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                        "Jogador {0} passou no teste com {1} sem pontos na perícia",
                        jogador.Nome,
                        result
                        );
                    }
                    
                    Console.ReadLine();
                    testandoPericia = false;
                    return 0;
                }
                else if(result > dadoJogador)
                {
                    if (pericia != null)
                    {
                        Console.WriteLine(
                        "Jogador {0} falhou no teste com {1} no dado, {2} de perícia na " +
                        "habilidade {3} contra o mestre no seu total de {4}",
                        jogador.Nome,
                        dadoJogador - pericia.Quantidade,
                        pericia.Quantidade,
                        pericia.Pericia.Descricao,
                        result
                        );
                        
                    }
                    else
                    {
                        Console.WriteLine(
                        "Jogador {0} falhou no teste com {1}, sem pontos na perícia",
                        jogador.Nome,
                        result
                        );               
                    }
                    Console.ReadLine();
                    testandoPericia = false;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;        
        }
    }
}
