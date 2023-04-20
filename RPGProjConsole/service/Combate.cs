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
                    item.ParticipouDoCombate = false;
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
                    Vitalidade = 2,
                    CA = 0,
                    IsNpc = true,
                    IsMob = true,
                    Exp = 2000
                
            };
                var mob2 = new Npcs
                {
                    Nome = "corredor2",
                    Destreza = 12,
                    Vitalidade = 2,
                    CA = 0,
                    IsNpc = true,
                    IsMob = true,
                    Exp = 2000
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
                IniciarComOportunidade(EscolherCombatente());
            }
            else if(value == 2)
            {
                DefinirIniciativa();
            }
            Console.Clear();
            Partida._context.SaveChanges();
        }
        public void DefinirIniciativa()
        {
            Console.Clear();
            Console.WriteLine("Rode suas iniciativas:");
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
            Console.WriteLine("Aqui está a ordem de jogadas, enter para continuar");
            for (int i = 1; i <= Combatentes.Count; i++)//MOSTRA ORDEM DE JOGADA
            {
                
                Console.WriteLine("{0} - nome: {1} com {2} de iniciativa",
                    i, Combatentes[i - 1].Nome,Combatentes[i - 1].Iniciativa);
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
                        if (Combatentes[j].EstaMortoOuInconciente)
                        {
                            var x = Console.BackgroundColor;
                            var y = Console.ForegroundColor;
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("{0} está morto ou inconciente", Combatentes[i].Nome);
                            ShowStatus(Combatentes[j]);
                            Console.BackgroundColor = x;
                            Console.ForegroundColor = y;
                        }
                        else if(i == j)
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
                    if (Combatentes[i].EstaMortoOuInconciente)
                    {
                        Console.WriteLine("{0} esta morto ou inconciente, precisa ter pelo menos 1 de vida para jogar"
                            , Combatentes[i].Nome);
                    }
                    else
                    {
                        Turn(Combatentes[i]);
                    }
                    
                    Console.Clear();
                    if (IsCombateOn == false)
                    {
                        return;
                    }
                }
                
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
                    var alvo = EscolherCombatente();
                    if (alvo.Id != 469)
                    {
                        if(personagemRodada.MunicaoArmaEquipada <= 0 && !personagemRodada.ArmaEquipada.IsCorpoACorpo)
                        {
                            Console.WriteLine("Sem munição");
                            personagemRodada.MunicaoArmaEquipada = 0;
                        }
                        else
                        {
                            personagemRodada.Acertar(alvo);
                        }
                        
                        if (!personagemRodada.ParticipouDoCombate)
                        {
                            personagemRodada.ParticipouDoCombate = true;
                        }
                        TestarSeMorreuEDarExp(alvo);
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
                    if (personagemRodada.ArmaEquipada != null && !personagemRodada.ArmaEquipada.IsCorpoACorpo)
                    {
                        personagemRodada.InventarioObj.RecarregarArma();
                        estaNaVez = false;
                    }
                    else
                    {
                        Console.WriteLine("Está desarmado ou sua arma é corpo a corpo");
                        Console.ReadLine();
                    }
                    
                }
                else if (opc == 3)
                {
                    personagemRodada.EstaMirando = true;
                    estaNaVez = false;
                }
                else if(opc == 4)
                {

                }
                else if(opc == 5)
                { 
                    Console.WriteLine("1 - Perícia contra o mestre");
                    Console.WriteLine("2 - Teste de medo");
                    var escolha = Partida.DigitarValor();
                    if(escolha == 0)
                    {
                        Console.WriteLine("Não entendi");
                    }
                    else if(escolha == 1)
                    {
                        Partida.PericiaConfig.FazerTesteDePericiaContraMestre(true, personagemRodada.Id);
                        if (!personagemRodada.ParticipouDoCombate)
                        {
                            personagemRodada.ParticipouDoCombate = true;
                        }
                        estaNaVez = false;
                    }
                    else if (escolha == 2)
                    {
                        //Partida.PericiaConfig
                    }
                    
                }
                else if(opc == 6)
                {
                    Console.WriteLine("{0} passou ou perdeu a vez");
                    estaNaVez = false;
                }
                else if(opc == 7)
                {
                    IsCombateOn = false;
                    estaNaVez = false;
                }
                Console.ReadLine();
            }
            
            Console.Clear();
        }
        public void IniciarComOportunidade(Personagem personagemRodada)
        {
            Console.WriteLine("{0} está atacando com oportunidade", personagemRodada.Nome);
            var alvo = EscolherCombatente();
            if (alvo.Id != 469)
            {
                personagemRodada.AcertarComOportunidade(alvo);
                if (!personagemRodada.ParticipouDoCombate)
                {
                    personagemRodada.ParticipouDoCombate = true;
                }
                TestarSeMorreuEDarExp(alvo);
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
            DefinirIniciativa();//No fim do ataque
        }
        public void TestarSeMorreuEDarExp(Personagem alvo)
        {
            if (alvo.Vitalidade <= 0)
            {
                Console.WriteLine("Alvo com {0} de vida", alvo.Vitalidade);
                alvo.EstaMortoOuInconciente = true;
                if (alvo.IsNpc)
                {
                    var divisao = 0;
                    for (int i = 0; i < Combatentes.Count; i++)
                    {
                        if (Combatentes[i].ParticipouDoCombate && !Combatentes[i].IsNpc)
                        {
                            divisao++;
                        }
                    }
                    for (int i = 0; i < Combatentes.Count; i++)
                    {
                        if (Combatentes[i].ParticipouDoCombate && !Combatentes[i].IsNpc)
                        {
                            if (divisao == 0) divisao = 1;
                            var jogador = Combatentes[i];
                            jogador.Exp += alvo.Exp / divisao;
                            Partida._context.Entry(jogador).State = EntityState.Modified;
                            Partida._context.Update(jogador);
                        }
                    }
                    Partida._context.SaveChanges();
                }
            }
        }
        public void OpcoesCombateConsole()
        {
            Console.WriteLine("1 - Atacar");
            Console.WriteLine("2 - Recarregar arma");
            Console.WriteLine("3 - Mirar");
            Console.WriteLine("4 - Trocar de arma");
            Console.WriteLine("5 - teste de perícia");
            Console.WriteLine("6 - Pular vez");
            Console.WriteLine("7 - Finalizar combate");
        }
        public Personagem EscolherCombatente()
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
                return new Personagem
                {
                    Id = 469
                };
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
