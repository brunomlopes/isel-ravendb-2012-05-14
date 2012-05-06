using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Demo2_WebApp.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Demo2_WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDocumentStore Store;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();

            SetupRavenDB();

            #region Load Sample Data
            using(var session = Store.OpenSession())
            {
                var goldfinger = new Person()
                                 {
                                     Id = "Persons/1", Name = "Auric Goldfinger"
                                 };
                session.Store(goldfinger);
                var largo = new Person()
                                 {
                                     Id = "Persons/2", Name = "Emilio Largo"
                                 };
                session.Store(largo);
                var blofeld = new Person()
                                 {
                                     Id = "Persons/3", Name = "Stavro Blofeld"
                                 };
                session.Store(blofeld);
                var scaramanga = new Person()
                                 {
                                     Id = "Persons/4", Name = "Francisco Scaramanga"
                                 };
                session.Store(scaramanga);

                var bruno = new Person
                                {
                                    Id = "Persons/5",
                                    Name = "Bruno Lopes"
                                };
                session.Store(bruno);

                session.Store(new Project
                                  {
                                      Id = "Projects/1",
                                      Name = "Apresentar ravendb",
                                      Activities = new List<Activity>
                                                       {
                                                           new Activity
                                                               {
                                                                   Name = "Preparar apresentação",
                                                                   Tasks = new List<Task>
                                                                               {
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Preparar slides",
                                                                                           Owner = bruno,
                                                                                           Done = true
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Preparar demos",
                                                                                           Owner = bruno,
                                                                                           Done = true
                                                                                       }
                                                                               }
                                                               },
                                                           new Activity
                                                               {
                                                                   Name = "Apresentar",
                                                                   Tasks = new List<Task>
                                                                               {
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Chegar a horas",
                                                                                           Owner = bruno,
                                                                                           Done = true
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Verificar que portatil funciona",
                                                                                           Owner = bruno,
                                                                                           Done = true
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Apresentar",
                                                                                           Owner = bruno
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Responder a perguntas",
                                                                                           Owner = bruno
                                                                                       },
                                                                               }
                                                               }
                                                       }
                                  });
                session.Store(new Project
                                  {
                                      Id = "Projects/2",
                                      Name = "Take over the world",
                                      Activities = new List<Activity>
                                                       {
                                                           new Activity
                                                               {
                                                                   Name = "Build secret lair",
                                                                   Tasks = new List<Task>
                                                                               {
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Find remote island",
                                                                                           Owner = scaramanga
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Hire underlings to build lair",
                                                                                           Owner = scaramanga
                                                                                       }
                                                                               }
                                                               },
                                                           new Activity
                                                               {
                                                                   Name = "Hatch evil plan",
                                                                   Tasks = new List<Task>
                                                                               {
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Design plan",
                                                                                           Owner = largo
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Add fatal error for hero to use",
                                                                                           Owner = blofeld
                                                                                       },
                                                                                   new Task
                                                                                       {
                                                                                           Name = "Get approval from SPECTRE",
                                                                                           Owner = blofeld
                                                                                       },
                                                                               }
                                                               }
                                                       }
                                  });

                session.SaveChanges();
            }

            #endregion
        }

        private void SetupRavenDB()
        {
            Store = new DocumentStore() { ConnectionStringName = "RavenDB" }.Initialize();
        }


    }
}