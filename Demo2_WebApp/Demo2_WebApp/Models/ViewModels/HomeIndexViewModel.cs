using System;
using System.Collections.Generic;
using Demo2_WebApp.Indexes;

namespace Demo2_WebApp.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<TasksCount_ForPerson.Result> TasksPerPerson { get; set; }

        public Dictionary<string, Person> Persons { get; set; }
    }
}