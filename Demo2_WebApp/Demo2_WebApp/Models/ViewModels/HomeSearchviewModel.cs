using System.Collections.Generic;

namespace Demo2_WebApp.Models.ViewModels
{
    public class HomeSearchViewModel
    {
        public string Query { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}