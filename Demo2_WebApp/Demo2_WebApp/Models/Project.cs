using System.Collections.Generic;

namespace Demo2_WebApp.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<Activity> Activities { get; set; }
    }

    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Person Owner { get; set; }
        public IList<Task> Tasks { get; set; }
    }

    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Person Owner { get; set; }
        public bool Done { get; set; }
    }

    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}