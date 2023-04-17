using RPGProjConsole.context;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGProjConsole.DataAccess
{
    class PlayerAccess
    {
        //Console.WriteLine(Jogadores[0].Nome);
        //Jogadores[0].Vitalidade --;
        //Partida._context.Entry(Jogadores[0]).State = EntityState.Modified;
        //Partida._context.Update(Jogadores[0]);
        //Partida._context.SaveChanges();
        //Partida._context.Jogadores.
        public static List<Jogador> GetJogadores()
        {
            using (var results = new RPGContext())
            {
                var jogadores = results.Jogadores;
                return jogadores.ToList();
            }
        }
        public static Jogador GetJogador(int id)
        {
            using (var results = new RPGContext())
            {
                var jogador = results.Jogadores.FirstOrDefault(i => i.Id == id);
                if(jogador == null) { Console.WriteLine("Jogador não encontrado"); }
                return jogador;
            }
        }
        public static void ShowPlayerssStatus()
        {
            using (var results = new RPGContext())
            {
                var jogadores = results.Jogadores;
                foreach(var jogador in jogadores)
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
        }
        public static void ShowPlayersAbilities(int id)
        {
            using (var results = new RPGContext())
            {
                var jogador = results.Jogadores.FirstOrDefault(i => i.Id == id);

                if(jogador != null)
                {
                    Console.WriteLine("Jogador " + jogador.Nome + " tem");
                    Console.WriteLine(jogador.Forca + " de força,");
                    Console.WriteLine(jogador.Destreza + " de destreza,");
                    Console.WriteLine(jogador.Constituicao + " de constituição,");
                    Console.WriteLine(jogador.Inteligencia + " de inteligência,");
                    Console.WriteLine(jogador.Sabedoria + " de sabedoria,");
                    Console.WriteLine(jogador.Carisma + " de carisma");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Jogador não encontrao");
                }
                
            }
        }
    }
}
