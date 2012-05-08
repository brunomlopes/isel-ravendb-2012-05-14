using System.Web.Mvc;
using Demo2_WebApp.Indexes;
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
            var projects = RavenSession.Query<ActivitiesCount_PerProject.Result,ActivitiesCount_PerProject>()
                .Statistics(out stats)
                .Skip((page-1) * 100)
                .Take(100)
                .ToList();

            viewModel.TotalResults = stats.TotalResults;

            viewModel.Projects = projects.Select(result => new ProjectIndexViewModel.ProjectViewModel()
                                                                {
                                                                    ProjectName = result.ProjectName,
                                                                    ProjectId = result.ProjectId,
                                                                    NumberOfActivities = result.NumberOfActivities
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
