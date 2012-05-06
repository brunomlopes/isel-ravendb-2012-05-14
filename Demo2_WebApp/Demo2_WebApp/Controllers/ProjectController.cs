using System.Web.Mvc;
using Demo2_WebApp.Infrastructure;
using Demo2_WebApp.Models;

namespace Demo2_WebApp.Controllers
{
    public class ProjectController : RavenDbController
    {
        public ActionResult Details(int id)
        {
            var project = RavenSession.Load<Project>(id);
            return View(project);
        }
    }
}
