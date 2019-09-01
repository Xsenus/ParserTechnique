using ParserTechnique.BL.Core;
using ParserTechnique.BL.Core.Citilink;
using ParserTechnique.BL.Core.DNS;
using System;

namespace ParserTechniqueCMD
{
    class Program
    {
        public static ParserWorker<string[]> parserWorker;
        static void Main(string[] args)
        {
            parserWorker = new ParserWorker<string[]>(new DNSParser());

            parserWorker.OnCompleated += ParserWorker_OnCompleated;

            parserWorker.OnNewData += ParserWorker_OnNewData;

            var start = Console.ReadLine();

            if (start.Contains("start"))
            {
                parserWorker.ParserSettings = new DNSSettings();
                parserWorker.Start();
            }

            Console.ReadLine();
        }

        private static void ParserWorker_OnNewData(object arg1, string[] arg2)
        {
            Console.WriteLine(string.Join("\n", arg2));
        }

        private static void ParserWorker_OnCompleated(object obj)
        {
            Console.WriteLine("Конец");
        }
    }
}
