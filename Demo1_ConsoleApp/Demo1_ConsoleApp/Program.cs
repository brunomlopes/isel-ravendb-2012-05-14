using System;
using System.Transactions;
using Raven.Client;
using Raven.Client.Document;

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
            IDocumentStore documentStore = new DocumentStore()
                                               {
                                                   Url = "http://localhost:8080"
                                               };
            documentStore.Initialize();

            using(IDocumentSession session = documentStore.OpenSession())
            {
                // CREATE
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

                session.Store(apresentação);
                session.SaveChanges();
            }
            using(IDocumentSession session = documentStore.OpenSession())
            {
                // READ
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");

                Console.Out.WriteLine("Presentation.Id = {0}", presentation.Id);
                Console.Out.WriteLine("Presentation.Name = {0}", presentation.Name);
                Console.Out.WriteLine("Presentation.Presenter.Name = {0}", presentation.Presenter.Nome);
                Console.Out.WriteLine("Presentation.Date = {0}", presentation.Date);
                Console.Out.WriteLine("Presentation.Duration = {0}", presentation.Duration);
            }
            
            using(IDocumentSession session = documentStore.OpenSession())
            {
                // UPDATE
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");

                presentation.Date = new DateTime(2012, 05, 07, 20, 0, 0);

                session.SaveChanges();
            }            
            
            using(IDocumentSession session = documentStore.OpenSession())
            {
                // UPDATE - Check
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");

                Console.Out.WriteLine("presentation.Date = {0}", presentation.Date);
            }
            
            using(IDocumentSession session = documentStore.OpenSession())
            {
                // DELETE
                // por entidade
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");
                session.Delete(presentation);
                session.SaveChanges();

                // por chave (atenção, não entra dentro do "unit of work", mas participa em transacções)
                //session.Advanced.DatabaseCommands.Delete("RavenDB/ISEL", null);

                // dentro de uma transacção, até ter o complete, não é apagado
                //using(var transaction = new TransactionScope()){
                //    session.Advanced.DatabaseCommands.Delete("RavenDB/ISEL", null);
                //    transaction.Complete();
                //}
            }
            
            using(IDocumentSession session = documentStore.OpenSession())
            {
                Presentation presentation = session.Load<Presentation>("RavenDB/ISEL");
                Console.Out.WriteLine("presentation = {0}", presentation);
            }
        }
    }
}
