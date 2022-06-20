using RotinaTimer.Service;
using System;

namespace TimerRotina
{
    public class Program
    {
        private static ConverterService _converterService = new ConverterService();

        public static void Main(string[] args)
        {
            var sair = false;
            Console.WriteLine("Digite START para começar,  STOP para parar e EXIT para sair");

            while (!sair)
            {
                var console = Console.ReadLine().ToUpper();

                if (console == "START")
                {
                    Start();
                }else

                if (console == "EXIT")
                {
                    sair = Exit();
                }
            }
        }

        public static void Start()
        {
            _converterService.Start();
        }

        private static bool Exit()
        {
            return true;
        }
    }
}
