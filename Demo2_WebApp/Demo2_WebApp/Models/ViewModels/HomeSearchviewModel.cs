using System.Collections.Generic;

namespace Demo2_WebApp.Models.ViewModels
{
    public class HomeSearchViewModel
    {
        public class TaskViewModel
        {
            public string Id { get; set; }
            public bool Done { get; set; }
            public string Name { get; set; }
            public string OwnerName { get; set; }
        }

        public string Query { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; set; }
        public int TotalResults { get; set; }
    }
}