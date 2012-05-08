using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo2_WebApp.Indexes;
using Demo2_WebApp.Infrastructure;
using Demo2_WebApp.Models;
using Demo2_WebApp.Models.ViewModels;

namespace Demo2_WebApp.Controllers
{
    public class HomeController : RavenDbController
    {
        public ActionResult Index()
        {
            var tasksPerPerson = RavenSession
                .Query<TasksCount_ForPerson.Result, TasksCount_ForPerson>()
                //.OrderBy(r => r.OwnerName)
                .ToList();

            var personIds = tasksPerPerson.Select(p => p.OwnerId);
            var persons = RavenSession.Load<Person>(personIds).ToDictionary(t => t.Id);

            return View(new HomeIndexViewModel()
                            {
                                TasksPerPerson = tasksPerPerson,
                                Persons = persons
                            });
        }

        public ActionResult Search(string query)
        {
            var homeSearchViewModel = new HomeSearchViewModel();
            var result = RavenSession.Advanced.LuceneQuery<Task, Tasks>().Search("Name", "*" + query + "*")
                .Include("OwnerId")
                .Take(100);
            homeSearchViewModel.Tasks = result.Select(tasks => new HomeSearchViewModel.TaskViewModel()
                                                               {
                                                                   Id = tasks.Id,
                                                                   Name = tasks.Name,
                                                                   Done = tasks.Done,
                                                                   OwnerName = RavenSession.Load<Person>(tasks.OwnerId).Name
                                                               }).ToList();
            homeSearchViewModel.Query = query;
            homeSearchViewModel.TotalResults = result.QueryResult.TotalResults;
            return View(homeSearchViewModel);
        }
    }
}
