using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Demo2_WebApp.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Abstractions.Extensions;
using Demo2_WebApp.Infrastructure;

namespace Demo2_WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

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
                session.Clear<Person>();
                session.Clear<Project>();
                session.Clear<Activity>();
                session.Clear<Task>();

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
                                  });
                var activity = new Activity
                                   {
                                       Name = "Preparar apresentação",
                                       ProjectId = "Projects/1"
                                   };
                session.Store(activity);
                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Preparar slides",
                                      OwnerId = bruno.Id,
                                      Done = true
                                  });
                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Preparar demos",
                                      OwnerId = bruno.Id,
                                      Done = true
                                  });
                activity = new Activity
                               {
                                   Name = "Apresentar",
                                   ProjectId = "Projects/1"
                               };

                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Chegar a horas",
                                      OwnerId = bruno.Id,
                                      Done = true
                                  });
                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Verificar que portatil funciona",
                                      OwnerId = bruno.Id,
                                      Done = true
                                  });
                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Apresentar",
                                      OwnerId = bruno.Id,
                                  });
                session.Store(new Task
                                  {
                                      ActivityId = activity.Id,
                                      Name = "Responder a perguntas",
                                      OwnerId = bruno.Id,
                                  });

                var users = new[] {bruno, blofeld, scaramanga, largo, goldfinger};

                var project = new Project
                                  {
                                      Id = "Projects/2",
                                      Name = "Take over the world",
                                  };

                session.Store(project);

                {
                    activity = new Activity
                                   {
                                       Name = "Build secret lair",
                                       ProjectId = project.Id
                                   };
                    session.Store(activity);

                    session.Store(new Task
                                      {
                                          Name = "Find remote island",
                                          ActivityId = activity.Id,
                                          OwnerId = scaramanga.Id
                                      });
                    session.Store(new Task
                                      {
                                          Name = "Hire underlings to build lair",
                                          ActivityId = activity.Id,
                                          OwnerId = scaramanga.Id
                                      });

                    activity = new Activity
                                   {
                                       Name = "Hatch evil plan",
                                       ProjectId = project.Id
                                   };
                    session.Store(new Task
                                      {
                                          Name = "Design plan",
                                          ActivityId = activity.Id,
                                          OwnerId = largo.Id
                                      });
                    session.Store(new Task
                                      {
                                          Name = "Add fatal error for hero to use",
                                          ActivityId = activity.Id,
                                          OwnerId = blofeld.Id
                                      });
                    session.Store(new Task
                                      {
                                          Name = "Get approval from SPECTRE",
                                          ActivityId = activity.Id,
                                          OwnerId = blofeld.Id
                                      });
                }

                var r = new Random(1);
                Enumerable.Range(1, 20)
                    .ForEach(i =>
                                 {
                                     project = new Project
                                                   {
                                                       Id = "Projects/42" + i,
                                                       Name = string.Format("Random project {0}", i),
                                                   };
                                     session.Store(project);

                                     Enumerable
                                        .Range(1, r.Next(10))
                                        .ForEach(j =>
                                                     {
                                                         activity = new Activity
                                                                        {
                                                                            Name = string.Format("Activity {0}-{1}", i, j),
                                                                            ProjectId = project.Id
                                                                        };
                                                         session.Store(activity);

                                                        Enumerable.
                                                            Range(1, r.Next(10))
                                                            .ForEach(k =>
                                                                        {
                                                                            var task = new Task
                                                                                            {
                                                                                                Name = string.Format("Task {0}-{1}-{2}",i,j,k),
                                                                                                OwnerId = users[(i + j +k)%users.Length].Id,
                                                                                                Done = r.Next(0, 2) == 1
                                                                                            };
                                                                            session.Store(task);
                                                                        });
                                                     });
                                    
                                });
                session.SaveChanges();
            }

            #endregion
        }
        


        public static IDocumentStore Store;
        private void SetupRavenDB()
        {
            Store = new DocumentStore() { ConnectionStringName = "RavenDB" }.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
        }
    }
}
