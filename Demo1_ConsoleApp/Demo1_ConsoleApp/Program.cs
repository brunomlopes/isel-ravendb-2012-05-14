﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Embedded;

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
            IDocumentStore documentStore = new EmbeddableDocumentStore();
            documentStore.Initialize();

            using (IDocumentSession session = documentStore.OpenSession())
            {
                session.Store(apresentação);
                session.SaveChanges();
            }
            using (IDocumentSession session = documentStore.OpenSession())
            {
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");

                Console.Out.WriteLine("Presentation.Id = {0}", presentation.Id);
                Console.Out.WriteLine("Presentation.Name = {0}", presentation.Name);
                Console.Out.WriteLine("Presentation.Presenter.Name = {0}", presentation.Presenter.Nome);
                Console.Out.WriteLine("Presentation.Date = {0}", presentation.Date);
                Console.Out.WriteLine("Presentation.Duration = {0}", presentation.Duration);
            }
        }
    }
}
