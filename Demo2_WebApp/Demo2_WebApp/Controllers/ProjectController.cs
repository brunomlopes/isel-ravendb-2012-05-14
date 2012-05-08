using System.Web.Mvc;
using Demo2_WebApp.Infrastructure;
using Demo2_WebApp.Models;
using System.Linq;
using Demo2_WebApp.Models.ViewModels;
using Raven.Client.Linq;

namespace Demo2_WebApp.Controllers
{
    public class ProjectController : RavenDbController
    {
        public ActionResult Index(int page = 1)
        {
            var viewModel = new ProjectIndexViewModel();

            RavenQueryStatistics stats;
            var projects = RavenSession.Query<Project>()
                .Statistics(out stats)
                .Skip((page-1) * 10)
                .Take(10)
                .ToList();

            viewModel.TotalResults = stats.TotalResults;

            viewModel.Projects = projects.Select(project => new ProjectIndexViewModel.ProjectViewModel()
                                                                {
                                                                    Project = project,
                                                                    NumberOfActivities = RavenSession
                                                                        .Query<Activity>()
                                                                        .Count(a => a.ProjectId == project.Id)
                                                                });
            viewModel.Page = page;
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var project = RavenSession.Load<Project>(id);
            return View(project);
        }
    }
}
