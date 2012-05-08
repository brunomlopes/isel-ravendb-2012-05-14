using System.Collections.Generic;
using System.Web.Http.ValueProviders;

namespace Demo2_WebApp.Models.ViewModels
{
    public class ProjectIndexViewModel
    {
        public class ProjectViewModel
        {
            public string ProjectName { get; set; }
            public string ProjectId { get; set; }
            public int NumberOfActivities { get; set; }
        }

        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}