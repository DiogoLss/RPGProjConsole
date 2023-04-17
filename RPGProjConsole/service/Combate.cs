using Microsoft.EntityFrameworkCore;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace RPGProjConsole.service
{
    class Combate
    {
        public List<Personagem> Combatentes { get; set; }
        public Partida Partida { get; set; }
        public Dado Dados { get; set; }
        public bool IsCombateOn { get; set; } = true;

        public Combate(Partida partida)
        {
            Partida = partida;
            Dados = new Dado();
            Combatentes = new List<Personagem>();
            if(partida.Jogadores != null)
            {
                foreach (var item in partida.Jogadores)
                {
                    item.IsNpc = false;
                    //item.IsMobIdentifier = false;
                    Combatentes.Add(item);
                }
            }
            if (partida.Npcs != null)
            {
                foreach (var item in partida.Npcs)
                {
                    item.IsNpc = true;
                    item.IsMob = false;
                    Combatentes.Add(item);
                }
            }
            if (partida.Mobs != null)
            {
                foreach (var item in partida.Mobs)
                {
                    item.IsNpc = true;
                    item.IsMob = true;
                    Combatentes.Add(item);
                }
            }
            else
            {
                var mob1 = new Npcs
                {
                    Nome = "corredor1",
                    Destreza = 12,
                    Vitalidade = 10
                };
                var mob2 = new Npcs
                {
                    Nome = "corredor2",
                    Destreza = 12,
                    Vitalidade = 10
                };
                Combatentes.Add(mob1);
                Combatentes.Add(mob2);
            }
        }
        public void CriarCombate()
        {
            Console.WriteLine("Deseja começar com");
            Console.WriteLine("Iniciar com ataque de oportunidade?");
            Console.WriteLine("Sim: 1, Não: 2");
            var value = Partida.DigitarValor();
            if(value == 1)
            {
                ControleDeRodadas();
                //IniciarComOportunidade();
            }
            else if(value == 2)
            {
                DefinirIniciativa();
            }
            else
            {
                Console.WriteLine("Não entendi deseja finalizar combate?");
                //ADICIONAR ESCOLHA
            }
            Console.Clear();
        }
        public void DefinirIniciativa()
        {
            Console.Clear();
            Random randNum = new Random();
            //var unordered = Combatentes;
            for (int i = 0; i < Combatentes.Count; i++)//TODOS JOGAM SUAS INICIATIVAS
            {
                var dado = Dados.RodarDado(Combatentes[i].Nome, 1,10);
                Combatentes[i].Iniciativa = dado + Combatentes[i].Modifier(2);
            }
            Combatentes = Combatentes.OrderByDescending(x => x.Iniciativa).ThenBy(x => x.Destreza).ToList();//ORDENA POR ORDEM DE JOGADA

            for (int i = 0; i < Combatentes.Count; i++)//TESTE SE ALGUÉM ESTÁ EMPATADO
            {
                if(i != 0 
                    && Combatentes[i - 1].Iniciativa == Combatentes[i].Iniciativa 
                    && Combatentes[i - 1].Destreza == Combatentes[i].Destreza)
                {
                    DesempateIniciativa(i);//DESEMPATA
                }
            }

            for (int i = 1; i <= Combatentes.Count; i++)//MOSTRA ORDEM DE JOGADA
            {
                Console.WriteLine("Aqui está a ordem de jogadas, enter para continuar");
                Console.WriteLine(i + " " + Combatentes[i - 1].Nome + " " + Combatentes[i - 1].Iniciativa);
            }
            Console.ReadLine();
            Console.Clear();
            ControleDeRodadas();
        }
        public void DesempateIniciativa(int posicao)
        {
            Console.WriteLine(Combatentes[posicao].Nome + " precisa desempatar com " + Combatentes[posicao - 1].Nome);
            var dado1 = Dados.RodarDado(Combatentes[posicao].Nome, 1,10);
            var dado2 = Dados.RodarDado(Combatentes[posicao - 1].Nome, 1,10);
            if (dado1 == dado2)
            {
                DesempateIniciativa(posicao);
            }
            else if(dado1 > dado2)
            {
                var combatente = Combatentes[posicao];
                Combatentes.RemoveAt(posicao);
                Combatentes.Insert(posicao - 1, combatente);
            }
            else
            {
                return;
            }
        }
        public void ControleDeRodadas()
        {
            Console.Clear();
            int rodada = 1;
            while (IsCombateOn)
            {   
                for(int i = 0; i < Combatentes.Count; i++) //TURN
                {
                    Console.WriteLine("Rodada: " + rodada);
                    Console.WriteLine();
                    for (int j = 0; j < Combatentes.Count; j++)//MOSTRA STTS COMBATENTES
                    {
                        if(i == j)
                        {
                            var x = Console.BackgroundColor;
                            var y = Console.ForegroundColor;
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Vez de:");
                            ShowStatus(Combatentes[j]);
                            Console.BackgroundColor = x;
                            Console.ForegroundColor = y;
                        }
                        else
                        {
                            ShowStatus(Combatentes[j]);
                        }               
                    }
                    Console.WriteLine();
                    Turn(Combatentes[i]);
                }
                Console.Clear();
                rodada++;
            }        
        }
        public void Turn(Personagem personagemRodada)
        {
            Console.WriteLine("Agora é a vez de " + personagemRodada.Nome);
            
            OpcoesCombateConsole();
            bool estaNaVez = true;
            while (estaNaVez)
            {
                Console.WriteLine("O que deseja fazer?");
                var opc = Partida.DigitarValor();
                if (opc == 1)
                {
                    Console.Clear();
                    var alvo = OpcoesDeAlvo(personagemRodada);
                    if (alvo.Id != 469)
                    {
                        personagemRodada.Acertar(alvo);
                        estaNaVez = false;
                        if (!personagemRodada.IsNpc)
                        {
                            Partida._context.Entry(personagemRodada).State = EntityState.Modified;
                            Partida._context.Update(personagemRodada);
                        }
                        else if (!alvo.IsNpc)
                        {
                            Partida._context.Entry(alvo).State = EntityState.Modified;
                            Partida._context.Update(alvo);
                        }
                        
                        Partida._context.SaveChanges();
                    }
                }
                else if (opc == 2)
                {
                    personagemRodada.InventarioObj.RecarregarArma();
                    estaNaVez = false;
                }
                else if (opc == 3)
                {
                    personagemRodada.EstaMirando = true;
                    estaNaVez = false;
                }
            }
            
            Console.Clear();
        }
        public void IniciarComOportunidade()
        {
            DefinirIniciativa();//No fim do ataque
        }
        public void OpcoesCombateConsole()
        {
            Console.WriteLine("1 - Atacar");
            Console.WriteLine("2 - Recarregar arma");
            Console.WriteLine("3 - Mirar");
            Console.WriteLine("4 - ");
            Console.WriteLine("5 - teste de perícia");
            Console.WriteLine("6 - Tudo que não seja controlado por aqui(correr, etc.)");
        }
        public Personagem OpcoesDeAlvo(Personagem personagemRodada)
        {
            Console.WriteLine("Quem?");
            for (int i = 1; i <= Combatentes.Count; i++)
            {
                Console.WriteLine("opção: " + i);
                ShowStatus(Combatentes[i - 1]);
            }
            Console.WriteLine();
            Console.WriteLine("469 para sair");
            var alvo = Partida.DigitarValor();
            if (alvo == 469)
            {
                return new Personagem
                {
                    Id = 469
                };
            }
            else if (alvo == 0 || alvo > Combatentes.Count)
            {
                Console.WriteLine("Não entendi");
                return new Personagem();
            }
            else
            {
                return Combatentes[alvo - 1];
            }
        }
        public void ShowStatus(Personagem personagem)
        {
            Console.WriteLine("Nome: " + personagem.Nome);
            Console.WriteLine("HP atual: " + personagem.Vitalidade);
            Console.WriteLine("Medo: " + personagem.Medo);
            Console.WriteLine("CA: " + personagem.CA);
            if(personagem.ArmaEquipada != null)
            {
                Console.WriteLine("Arma equipada: " + personagem.ArmaEquipada.Nome);
            }
            if (personagem.ArmaduraEquipada != null)
            {
                Console.WriteLine("Armadura equipada: " + personagem.ArmaduraEquipada.Nome);
            }
            Console.WriteLine();
        }
    }
}
