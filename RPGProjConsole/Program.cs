using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.DataAccess;
using RPGProjConsole.models;
using RPGProjConsole.service;
using System;
using System.Linq;

namespace RPGProjConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var results = new RPGContext())
            {
                //var update = results.Jogadores.First();
                //Personagem update2 = update;
                //update2.Vitalidade = 10;
                //results.Entry(update2).State = EntityState.Modified;
                //results.Update(update2);
                //results.SaveChanges();

                var partida = new Partida(results);
                partida.ComecarPartida();
            }
            //PlayerAccess.ShowPlayerssStatus();
            Console.ReadLine();
        }
    }
}
