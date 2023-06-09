﻿using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
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
                if(testeObj.Id == 1)//COLOCAR ID CORRESPONDENTE A CURA NO DB
                {
                    Console.WriteLine("Teste para curar");
                    var result = Dados.RodarDado("Mestre", 1, 20);
                    var jogador = new Jogador();
                    var periciaN = new JogadorPericia();
                    if (isCombate)
                    {
                        jogador = _partida.Jogadores.Find(x => x.Id == idJogador);
                        periciaN = _context.JogadorPericia
                           .Include(x => x.Pericia)
                           .FirstOrDefault(
                           x => x.Jogador.Id == jogador.Id
                           &&
                           x.Pericia.Id == testeObj.Id);
                    }
                    else
                    {
                        Console.WriteLine("Quem realizará o teste?");
                        for (int i = 0; i < _partida.Jogadores.Count; i++)
                        {
                            Console.WriteLine("{0} - {1}", i + 1, _partida.Jogadores[i].Nome);
                        }
                        var jogadorIndex = DigitarValor();
                        jogador = _partida.Jogadores[jogadorIndex - 1];
                        periciaN = _context.JogadorPericia
                           .Include(x => x.Pericia)
                           .FirstOrDefault(
                           x => x.Jogador.Id == jogador.Id
                           &&
                           x.Pericia.Id == testeObj.Id);
                    }
                    if (periciaN == null)
                    {
                        Console.WriteLine("Você não pode usar cura");

                    }
                    else
                    {
                        Console.WriteLine("Quem você deseja curar?");
                        for (int i = 0; i < _partida.Jogadores.Count; i++)
                        {
                            Console.WriteLine("{0} - {1} com {2} de HP",
                                i + 1,
                                _partida.Jogadores[i].Nome,
                                _partida.Jogadores[i].Vitalidade
                                );
                        }
                        var jogadorCura = _partida.Jogadores[DigitarValor() - 1];
                        Console.WriteLine("Que Item de cura deseja usar? 0 para não usar");
                        var itens = _context.Items.ToList();
                        for (int i = 0; i < itens.Count; i++)
                        {
                            if (itens[i].ECura)
                            {
                                Console.WriteLine("{0} - {1} cura {2}d4 de HP",
                                i + 1,
                                itens[i].Nome,
                                itens[i].QtdDadosCura
                                );
                            }
                        }
                        var indexItem = DigitarValor() - 1;
                        var item = itens[indexItem];
                        if(indexItem == 0)
                        {
                            Curar(jogadorCura, periciaN, result, jogador, null);
                        }
                        else
                        {
                            Curar(jogadorCura, periciaN, result, jogador, item);
                        }
                        
                    }              
                }
                else
                {
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
                    else if (escolha == 1)
                    {
                        var result = Dados.RodarDado("Mestre", 1, 20);
                        var falhos = 0;
                        foreach (var item in _partida.Jogadores)
                        {
                            var periciaN = _context.JogadorPericia
                                .Include(x => x.Pericia)
                                .FirstOrDefault(
                                x => x.Jogador.Id == item.Id
                                &&
                                x.Pericia.Id == testeObj.Id);


                            falhos += TestarPericia(item, result, testeObj, periciaN);
                        }
                        Console.WriteLine("{0} pessoas falharam", falhos);
                        Console.ReadLine();
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
        public void Curar(Personagem alvo, JogadorPericia pericia, int result, Personagem jogadorTeste, ItemGeneral item)
        {
            var teste = jogadorTeste.Modifier(5) + pericia.Quantidade;
            teste += Dados.RodarDado(jogadorTeste.Nome,1,20);
            teste += jogadorTeste.ArvoreSkills.Cura();
            Console.WriteLine("O teste do jogador {0} resultou em {1} contra {2} do mestre",
                jogadorTeste.Nome, teste, result
                );
            if(teste >= result)
            {
                Console.WriteLine("{0} passou no teste!", jogadorTeste.Nome);
                var cura = 0;
                if (item == null)
                {
                    cura = Dados.RodarDado(jogadorTeste.Nome, 1, 4);
                }
                else
                {
                    cura = Dados.RodarDado(jogadorTeste.Nome, item.QtdDadosCura, 4);
                }
                
                alvo.Vitalidade += cura;
                if(alvo.Vitalidade > alvo.VidaMaxima)
                {
                    alvo.Vitalidade = alvo.VidaMaxima;
                }
                Console.WriteLine("{0} Curou em {1} {2}",
                    jogadorTeste.Nome,
                    cura,
                    alvo.Nome
                    );
                _partida._context.Entry(alvo).State = EntityState.Modified;
                _partida._context.Update(alvo);
                _partida._context.SaveChanges();
            }
            else
            {
                Console.WriteLine("{0} falhou no teste", jogadorTeste.Nome);
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
                for(int i = 0; i < pericias.Count; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, pericias[i].Descricao);
                }
                while(qtd != 0)
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
                foreach(var item in list)
                {
                    Console.WriteLine("{0} pontos em {1}", item.Quantidade, item.Pericia.Descricao);
                }
                var resposta = DigitarValor();
                if(resposta == 1)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].Jogador = jogador;
                        var objJogador = _context.JogadorPericia.FirstOrDefault(
                            o => o.Pericia.Id == list[i].Pericia.Id);
                        if(objJogador == null)
                        {
                            _context.JogadorPericia.Add(list[i]);
                            _context.SaveChanges();
                        }
                        else
                        {
                            objJogador.Quantidade += list[i].Quantidade;
                            _partida._context.Entry(objJogador).State = EntityState.Modified;
                            _partida._context.Update(objJogador);
                            _partida._context.SaveChanges();
                        }
                    }
                    escolhendoPericias = false;
                }
            }
        }
    }
}
