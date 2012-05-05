using System;
using Raven.Client;
using Raven.Client.Embedded;

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
            IDocumentStore documentStore = new EmbeddableDocumentStore();
            documentStore.Initialize();

            using(IDocumentSession session = documentStore.OpenSession())
            {
                var apresentação = new Apresentação()
                                       {
                                           Id = "Apresentação RavenDB ISEL",
                                           Nome = "Document Databases com RavenDB",
                                           Apresentador = new Pessoa()
                                                              {
                                                                  Nome = "Bruno Lopes"
                                                              },
                                           Data = new DateTime(2012, 05, 06, 09, 00, 00),
                                           Duração = TimeSpan.FromHours(3),
                                       };

                session.Store(apresentação);
                session.SaveChanges();
            }
            using(IDocumentSession session = documentStore.OpenSession())
            {
                Apresentação apresentação = session.Load<Apresentação>("Apresentação RavenDB ISEL");

                Console.Out.WriteLine("apresentação.Id = {0}", apresentação.Id);
                Console.Out.WriteLine("apresentação.Nome = {0}", apresentação.Nome);
                Console.Out.WriteLine("apresentação.Apresentador.Nome = {0}", apresentação.Apresentador.Nome);
                Console.Out.WriteLine("apresentação.Data = {0}", apresentação.Data);
                Console.Out.WriteLine("apresentação.Duração = {0}", apresentação.Duração);
            }
        }
    }
}
