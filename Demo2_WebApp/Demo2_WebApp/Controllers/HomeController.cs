using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo2_WebApp.Indexes;
using Demo2_WebApp.Infrastructure;
using Demo2_WebApp.Models.ViewModels;

namespace Demo2_WebApp.Controllers
{
    public class HomeController : RavenDbController
    {
        public ActionResult Index()
        {
            var tasksPerPerson = RavenSession
                .Query<TasksCount_ForPerson.Result, TasksCount_ForPerson>()
                .ToList();
            return View(new HomeIndexViewModel(){TasksPerPerson = tasksPerPerson});
        }
    }
}
