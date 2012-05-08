using System.Collections.Generic;

namespace Demo2_WebApp.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Activity
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public Person Owner { get; set; }
    }

    public class Task
    {
        public string Id { get; set; }
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public bool Done { get; set; }
    }

    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}