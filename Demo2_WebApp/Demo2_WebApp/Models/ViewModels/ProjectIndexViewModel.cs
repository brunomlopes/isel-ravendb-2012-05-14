using System.Collections.Generic;
using System.Web.Http.ValueProviders;

namespace Demo2_WebApp.Models.ViewModels
{
    public class ProjectIndexViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}