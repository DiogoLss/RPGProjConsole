using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.service
{
    class Dado
    {
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
        public int RodarDado(string nome, int x, int y)//X = 1 Y = 10 === 1d10
        {
            Random randNum = new Random();
            Console.WriteLine("Rode seu(s) dado(s) " + nome);
            int result = 0;
            var dado = DigitarValor();
            if (dado == 0 || dado > x * y)
            {
                for(int i = 0; i < x; i++)
                {
                    dado = randNum.Next(1, y);
                    Console.WriteLine("1d{0} resultou: " + dado,y);
                    result += dado;
                }
            }
            else
            {
                Console.WriteLine("1d{0} resultou: " + dado,y);
                result = dado;
            }
            Console.WriteLine();
            return result;
        }
    }
}
