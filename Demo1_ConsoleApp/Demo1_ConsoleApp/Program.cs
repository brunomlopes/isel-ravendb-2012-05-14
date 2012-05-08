using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo1_ConsoleApp
{
    class Program
    {
        class Presentation
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Duration { get; set; }
            public Person Presenter { get; set; }
        }

        class Person
        {
            public string Nome { get; set; }
        }

        static void Main(string[] args)
        {
        }
    }
}
