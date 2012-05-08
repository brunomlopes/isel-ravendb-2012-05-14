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
            var apresentação = new Presentation
            {
                Id = "RavenDB/ISEL",
                Name = "Document Databases com RavenDB",
                Presenter = new Person()
                {
                    Nome = "Bruno Lopes"
                },
                Date = new DateTime(2012, 05, 06, 09, 00, 00),
                Duration = TimeSpan.FromHours(3),
            };
        }
    }
}
