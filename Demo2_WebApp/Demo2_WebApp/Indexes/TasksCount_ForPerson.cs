using Demo2_WebApp.Models;
using Raven.Client.Indexes;
using System.Linq;

namespace Demo2_WebApp.Indexes
{
    public class TasksCount_ForPerson : AbstractIndexCreationTask<Project, TasksCount_ForPerson.Result>
    {
        public class Result
        {
            public Person Owner { get; set; }
            public int Count { get; set; }
        }

        public TasksCount_ForPerson()
        {
            Map = projects =>
                projects
                .SelectMany(p => p.Activities)
                .SelectMany(a => a.Tasks)
                    .Select(task =>
                            new
                                {
                                    task.Owner,
                                    Count = 1
                                }
                    );

            Reduce = 
                results => results
                    .GroupBy(g => g.Owner)
                    .Select(p => new
                                     {
                                         Owner = p.Key, 
                                         Count = p.Sum(result => result.Count)
                                     });



        }
    }
}