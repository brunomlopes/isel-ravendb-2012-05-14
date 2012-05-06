using System.Web.Mvc;
using Demo2_WebApp.Infrastructure;
using Demo2_WebApp.Models;
using System.Linq;

namespace Demo2_WebApp.Controllers
{
    public class ProjectController : RavenDbController
    {
        public ActionResult Index()
        {
            var projects = RavenSession.Query<Project>().ToList();
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            var project = RavenSession.Load<Project>(id);
            return View(project);
        }
    }
}
