using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo1_ConsoleApp
{
    class Program
    {
        class Apresentação
        {
            public string Id { get; set; }
            public string Nome { get; set; }
            public DateTime Data { get; set; }
            public TimeSpan Duração { get; set; }
            public Pessoa Apresentador { get; set; }
        }

        class Pessoa
        {
            public string Nome { get; set; }
        }

        static void Main(string[] args)
        {
        }
    }
}
